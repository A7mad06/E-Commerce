using E_Commerce_Api.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductController(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection con = new SqlConnection("Server=DESKTOP-6K91J1U\\SQLEXPRESS;Database=e-commerce;Trusted_Connection=true");


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> lst = new List<Product>();
            SqlCommand cmd = new SqlCommand("select * from product", con);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Product prod = new Product();
                prod.Id = Guid.Parse(dr[0].ToString());
                prod.Name = dr[5].ToString();
                prod.Stoke = int.Parse(dr[2].ToString());
                prod.Img1 = dr[6].ToString();
                prod.Img2 = dr[7].ToString();
                prod.Img3 = dr[8].ToString();
                prod.Price = int.Parse(dr[1].ToString());
                prod.Description = dr[3].ToString();
                prod.Product_category = dr[4].ToString();
                lst.Add(prod);
            }
            return Ok(lst);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            SqlCommand command = new SqlCommand($"insert into product values('"+product.Id+"','"+product.Price+"','"+product.Stoke+"','"+product.Description+"','"+product.Product_category+"','"+product.Name+"','"+product.Img1+"','"+product.Img2+"','"+product.Img3+"')", con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            return Ok();
        }
    }
}
