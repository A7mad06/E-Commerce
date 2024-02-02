using E_Commerce_Api.Models;
using E_Commerce_Api.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {



        [HttpGet]
        public async Task<IActionResult> ShowAllCategories()
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand com = new SqlCommand("Select * from Category",con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(dt);
            List<Category> categories = new List<Category>();
            foreach (DataRow row in dt.Rows)
            {
                Category category = new Category();
                category.Name = row[0].ToString();
                category.Id = int.Parse(row[1].ToString());
                categories.Add(category); ;
            }
            return Ok(categories);
        }




        [HttpGet]
        [Route("{Name}")]
        public async Task<IActionResult> ShowCategoryProduct([FromRoute] string Name)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand com = new SqlCommand("Select * from Product where Category_Name='"+Name+"'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(dt);
            List<Product> lst = new List<Product>();
            foreach(DataRow dr in dt.Rows)
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




        [HttpPost]
        [Route("Add-Category")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand com = new SqlCommand("insert into Category(Category_Name) values('"+category.Name+"')", con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            SqlCommand cmd = new SqlCommand("Select * from Category where Category_Name='"+category.Name+"'",con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            List<Category> lst = new List<Category>();
            foreach(DataRow dr in dt.Rows)
            {
                Category category1 = new Category();
                category1.Name =dr[0].ToString();
                category1.Id = int.Parse(dr[1].ToString());
                lst.Add(category1);
            }
            return Ok(lst);
        }
    }
}
