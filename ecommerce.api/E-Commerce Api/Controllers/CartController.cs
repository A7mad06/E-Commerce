using E_Commerce_Api.Models;
using E_Commerce_Api.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public SqlConnection con = new SqlConnection("Server=DESKTOP-6K91J1U\\SQLEXPRESS;Database=e-commerce;Trusted_Connection=true");


        [HttpGet("Cart")]
        public async Task<IActionResult> GetCart()
        {
            SqlCommand command = new SqlCommand("select * from cart", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            List<Cart> cart = new List<Cart>();
            da.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
                Cart car = new Cart()
                {
                    Product_id = Guid.Parse(row["product_id"].ToString()),
                    Name = row["name"].ToString(),
                    Img1 = row["img1"].ToString(),
                    Price = int.Parse(row["price"].ToString()),
                    Quantity = int.Parse(row["quantity"].ToString()),
                    Subtotal = int.Parse(row["subtotal"].ToString())
                };
                cart.Add(car);
            }

            return Ok(cart);
        }

        [HttpPost("{id:Guid}")]
        public async Task<IActionResult> AddToCart([FromRoute] Guid id)
        {
            SqlCommand comm = new SqlCommand($"select * from cart where product_id = '"+id+"'", con);
            SqlDataAdapter daa = new SqlDataAdapter(comm);
            DataTable dtable = new DataTable();
            daa.Fill(dtable);
            if (dtable.Rows.Count != 1)
            {
                SqlCommand command = new SqlCommand($"select * from product where id='"+id+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Cart cart = new Cart();
                foreach (DataRow row in dt.Rows)
                {
                    cart = new Cart()
                    {
                        Name = row["name"].ToString(),
                        Price = int.Parse(row["price"].ToString()),
                        Img1 = row["img1"].ToString(),
                    };
                }
                cart.Quantity = 1;

                int sub = (cart.Quantity) * (cart.Price);
                cart.Subtotal = sub;

                SqlCommand command1 = new SqlCommand("INSERT INTO cart values("+id+",'"+cart.Price+"','"+cart.Name+"','"+cart.Quantity+"','"+cart.Subtotal+ "','"+cart.Img1+"')", con);

                con.Open();
                command1.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                int qnt=int.Parse(dtable.Rows[0]["quantity"].ToString());
                qnt++;
                int price = int.Parse(dtable.Rows[0]["price"].ToString());
                int subtotal = qnt * price;
                SqlCommand command = new SqlCommand("Update cart set quantity='"+qnt+"', subtotal='"+subtotal+"' where product_id='"+id+"'", con);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }

            return Ok();
        }

    }
}
