using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebAPIBase.Extensions;
using WebAPIBase.Managers;
using WebAPIBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ItemController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("items")]
        public IActionResult GetItems(int order_id)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();

                var query = "SELECT * FROM Items WHERE order_id = @OrderID";
                var items = connection.Query<Item>(query, new {OrderID = order_id}).ToList();

                return Ok(items);
            }
        }
    }
}
