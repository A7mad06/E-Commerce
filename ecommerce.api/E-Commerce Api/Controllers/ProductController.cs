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

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            List<Product> lst = new List<Product>();
            SqlCommand cmd = new SqlCommand("select * from product", con);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Product pro = new Product();
                pro.Id = Guid.Parse(dr[0].ToString());
                pro.Name = dr[1].ToString();
                pro.Description = dr[2].ToString();
                pro.Product_Category = dr[3].ToString();
                pro.Stoke = int.Parse(dr[4].ToString());
                pro.Price= int.Parse(dr[5].ToString());
                lst.Add(pro);
            }
            return Ok(lst);
        }


        [HttpPost]
        [Route("Add_Product")]

        public async Task<IActionResult> AddProduct(Product product)
        {
            product.Id=Guid.NewGuid();
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand command = new SqlCommand("insert into product(Product_Id,Product_Name,Product_Description,Category_Name,Stoke,Product_Price) values('" + product.Id + "','" + product.Name + "','" + product.Description + "','" + product.Product_Category + "','" + product.Stoke + "','" + product.Price + "')",con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            return Ok(product);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateProduct([FromRoute]Guid id,Product product1)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand command = new SqlCommand("update Product set Product_Name='" + product1.Name+ "',Product_Description='" + product1.Description+ "',Category_Name='" + product1.Product_Category + "',Stoke='" + product1.Stoke + "',Product_Price='" + product1.Price + "' where Product_Id='"+id+"'", con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            List<Product> lst = new List<Product>();
            SqlCommand cmd = new SqlCommand("select * from product where Product_Id='"+id+"'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Product pro = new Product();
                pro.Id = Guid.Parse(dr[0].ToString());
                pro.Name = dr[1].ToString();
                pro.Description = dr[2].ToString();
                pro.Product_Category = dr[3].ToString();
                pro.Stoke = int.Parse(dr[4].ToString());
                pro.Price = int.Parse(dr[5].ToString());
                lst.Add(pro);
            }
            return Ok(lst);
        }


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("Delete from Product where Product_Id='"+id+"'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok();
        }
    }
}
