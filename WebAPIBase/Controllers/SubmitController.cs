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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public SubmitController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("addOrder")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();

                var query = "INSERT INTO Orders (order_date, order_status, billing_address, shipping_address, customer_id) " +
                            "VALUES (@order_date, @order_status, @billing_address, @shipping_address, @customer_id)";

                var result = connection.Execute(query, order);

                if (result > 0)
                {
                    return Created("", order); // Return 201 Created status
                }
                else
                {
                    return BadRequest("Failed to add order");
                }
            }
        }
    }
}
