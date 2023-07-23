using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BesicLearning.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoAppController : ControllerBase
	{
		private  IConfiguration _configuration;
		public ToDoAppController(IConfiguration configuration)
		{
			_configuration = configuration;
		}


		[HttpGet]
		[Route("GetNotes")]

	   public JsonResult GetNotes()
		{
		   string query = @"select * from dbo.Notes";
			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("ToDoAppDbCon");
			SqlDataReader myReader;
		    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myReader = myCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}

			return new JsonResult(table);
	   }
		[HttpPost]
		[Route("AddNotes")]

		public JsonResult AddNotes([FromForm] string @NewNote)
		{
			string query = @"insert  into  dbo.Notes values(@NewNote)";
			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("ToDoAppDbCon");
			SqlDataReader myReader;
			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myCommand.Parameters.AddWithValue("@NewNote", NewNote);
					myReader = myCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}

			return new JsonResult( "Added Successfully"); 
		}

		[HttpDelete]
		[Route("DeleteNotes")]

		public JsonResult DeleteNotes(int @id)
		{
			string query = @"Delete from  dbo.Notes where id = @id";
			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("ToDoAppDbCon");
			SqlDataReader myReader;
			using (SqlConnection myCon = new SqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (SqlCommand myCommand = new SqlCommand(query, myCon))
				{
					myCommand.Parameters.AddWithValue("@id", id);
					myReader = myCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
					myCon.Close();
				}
			}

			return new JsonResult("Delleted Successfully");
		}
	}
}
