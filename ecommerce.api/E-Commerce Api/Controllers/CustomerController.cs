using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            List<Customer> lst = new List<Customer>();
            SqlCommand cm = new SqlCommand("select * from Customer",con);
            DataTable dt = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter(cm);
            dp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Customer obj = new Customer();
                obj.Id = Guid.Parse(dr[0].ToString());
                obj.Name = dr[1].ToString();
                obj.Address = dr[3].ToString();
                obj.Phone = int.Parse(dr[2].ToString());
                obj.Password = dr[4].ToString();
                obj.Email = dr[5].ToString();
                lst.Add(obj);
            }
            return Ok(lst);
        }

        [HttpPost]
        [Route("Creat-Account")]

        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("insert into Customer(Customer_Id,Customer_Name,Customer_Email,Customer_Password,Customer_Phone,Customer_Address) values('"+customer.Id+"','"+customer.Name+"','"+customer.Email+"','"+customer.Password+"','"+customer.Phone+"','"+customer.Address+"')",con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok(customer);
        }

        [HttpPut]
        [Route("{email}")]

        public async Task<IActionResult> UpdateCustomer([FromRoute] string email,Customer customer)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand command = new SqlCommand("Update Customer set Customer_Name='"+customer.Name+"',Customer_Password='"+customer.Password+"',Customer_Phone='"+customer.Phone+"',Customer_Address='"+customer.Address+"' where Customer_Id='" + id + "'",con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            List<Customer> lst = new List<Customer>();
            SqlCommand cm = new SqlCommand("select * from Customer where Customer_Email='"+email+"'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter(cm);
            dp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Customer obj = new Customer();
                obj.Id = Guid.Parse(dr[0].ToString());
                obj.Name = dr[1].ToString();
                obj.Address = dr[3].ToString();
                obj.Phone = int.Parse(dr[2].ToString());
                obj.Password = dr[4].ToString();
                obj.Email = dr[5].ToString();
                lst.Add(obj);
            }
            return Ok(lst);
        }

        [HttpDelete]
        [Route("{email}")]

        public async Task<IActionResult> DeleteCustomer([FromRoute] string email)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand command = new SqlCommand("Delete from Customer where Customer_Email='" + email + "'", con);
            con.Open(); command.ExecuteNonQuery(); con.Close();
            return Ok();
        }
    }
}
