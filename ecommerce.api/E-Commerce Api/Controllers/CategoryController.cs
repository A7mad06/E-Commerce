using E_Commerce_Api.Models;
using E_Commerce_Api.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
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
        public SqlConnection con = new SqlConnection("Server=DESKTOP-6K91J1U\\SQLEXPRESS;Database=e-commerce;Trusted_Connection=true");


        [HttpGet]
        public async Task<IActionResult> ShowAllCategories()
        {
            SqlCommand com = new SqlCommand("Select * from category", con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(dt);
            List<Category> categories = new List<Category>();
            foreach (DataRow row in dt.Rows)
            {
                Category category = new Category();
                category.Name = row[1].ToString();
                category.Id = int.Parse(row[0].ToString());
                categories.Add(category);
            }
            categories =categories.OrderBy(c=>c.Id).ToList();
            return Ok(categories);
        }




        [HttpGet("{Name}")]
        public async Task<IActionResult> ShowCategoryProduct([FromRoute] string Name)
        {
            List<Product> lst = new List<Product>();
            SqlCommand cmd = new SqlCommand("select * from product where category_name='"+Name+"'", con);
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
    }
}
