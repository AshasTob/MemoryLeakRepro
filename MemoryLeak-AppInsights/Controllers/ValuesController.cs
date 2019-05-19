using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MemoryLeak_AppInsights.Controllers
{
    [Route("triggers")]
    [Route("members/{memberId}/tickets")]
    [Route("members/{memberId}/rewards")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        private readonly SqlConnection _connection;

        public ValuesController(SqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            string result = "";
            await _connection.OpenAsync();
            using (SqlCommand command = new SqlCommand("SELECT * FROM GameConfiguration", _connection))
            {
                for (var i = 0; i < 10; i++)
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result += reader["GameId"];
                        }
                    }
                }
            }

            _connection.Close();

            return new string[] { result };
        }
    }
}
