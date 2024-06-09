//using E_Commerce_Api.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics.Contracts;

//namespace E_Commerce_Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartController : ControllerBase
//    {
//        public SqlConnection con = new SqlConnection("Server=DESKTOP-6K91J1U\\SQLEXPRESS;Database=e-commerce;Trusted_Connection=true");


//        [HttpPost]
//        [Route("{CustomerEmail}")]
//        public async Task<IActionResult> AddCart(Cart cart, [FromRoute] string CustomerEmail)
//        {
//            cart.Id = Guid.NewGuid();
//            cart.total = 0;
//            cart.Customer_Email = CustomerEmail;
//            SqlConnection sql = new SqlConnection(con);
//            SqlCommand cmd = new SqlCommand("Insert into Cart values('" + cart.Id + "','" + cart.total + "','" + CustomerEmail + "')", sql);
//            sql.Open();
//            cmd.ExecuteNonQuery();
//            sql.Close();
//            return Ok(cart);
//        }

//        [HttpPut]
//        [Route("Update-Cart/{Cart_id:Guid}")]
//        public async Task<IActionResult> UpdateCart([FromRoute] Guid Cart_id)
//        {
//            SqlConnection sql = new SqlConnection(con);
//            SqlCommand cmd = new SqlCommand("select Item_Subtotal from Order_Item where Cart_Id='" + Cart_id + "'", sql);
//            DataTable dt = new DataTable();
//            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//            adapter.Fill(dt);
//            int total = 0;
//            foreach (DataRow row in dt.Rows)
//            {
//                total += int.Parse(row[0].ToString());
//            }
//            SqlCommand cmdd = new SqlCommand("Update Cart set Cart_Total='" + total + "' where Cart_Id='" + Cart_id + "'", sql);
//            sql.Open();
//            cmdd.ExecuteNonQuery();
//            sql.Close();
//            SqlCommand cm = new SqlCommand("Select * from Cart where Cart_Id='" + Cart_id + "'", sql);
//            DataTable dataTable = new DataTable();
//            SqlDataAdapter ad = new SqlDataAdapter(cm);
//            ad.Fill(dataTable);
//            List<Cart> lst = new List<Cart>();
//            foreach (DataRow row in dataTable.Rows)
//            {
//                Cart cart = new Cart();
//                cart.Id = Guid.Parse(row[0].ToString());
//                cart.total = int.Parse(row[1].ToString());
//                cart.Customer_Email = row[2].ToString();
//                lst.Add(cart);
//            }
//            return Ok(lst);
//        }

//        [HttpDelete]
//        [Route("Delete-Cart/{Id:Guid}")]
//        public async Task<IActionResult> DeletCart([FromRoute] Guid Id)
//        {
//            SqlConnection sql = new SqlConnection(con);
//            SqlCommand com = new SqlCommand("Delete from Cart where Cart_Id='" + Id + "'", sql);
//            sql.Open();
//            com.ExecuteNonQuery();
//            sql.Close();
//            return Ok();
//        }
//    }
//}
