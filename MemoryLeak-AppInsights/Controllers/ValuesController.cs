using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace MemoryLeak_AppInsights.Controllers
{
    [Route("triggers")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            string result = "";
            using (var _connection = new SqlConnection("YOUR DB CONNECTION STRING"))
            {
                
                for (var i = 0; i < 10; i++)
                {
                    result = await _connection.QueryFirstOrDefaultAsync<string>("SELECT * FROM SOMETABLE");
                }
            }
            return new string[] { result };
        }
    }
}
