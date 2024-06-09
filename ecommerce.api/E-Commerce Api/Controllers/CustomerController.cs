using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection con = new SqlConnection("Server=DESKTOP-6K91J1U\\SQLEXPRESS;Database=e-commerce;Trusted_Connection=true");


        //SignUp

        [HttpPost("SignUp")]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            SqlCommand check = new SqlCommand("select * from customer where email='"+customer.Email+"'",con);
            SqlDataAdapter da = new SqlDataAdapter(check);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return BadRequest();
            }
            else
            {

                customer.Id = Guid.NewGuid();

                SqlCommand cmd = new SqlCommand("insert into Customer(id,name,email,password,phone,address) values('" + customer.Id + "','" + customer.Name + "','" + customer.Email + "','" + customer.Password + "','" + customer.Phone + "','" + customer.Address + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
        }


        [HttpPost("Login")]

        public async Task<IActionResult> Login(string user,string pass)
        {
            if (user == null)
            {
                return BadRequest();
            }
            int num;
            if (user.StartsWith('0'))
            {
                num = int.Parse(user);
                SqlCommand check = new SqlCommand($"Select * From customer where phone='" + num + "' And password='" + pass + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(check);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                SqlCommand check = new SqlCommand($"Select * From customer where email='" + user + "' And password='" + pass + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(check);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
