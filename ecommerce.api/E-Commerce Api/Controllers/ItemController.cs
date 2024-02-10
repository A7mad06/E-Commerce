using E_Commerce_Api.Models;
using E_Commerce_Api.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        string con = "Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true";

        [HttpGet]
        public async Task<IActionResult> GetItems(Guid cart_id)
        {
            SqlConnection sql = new SqlConnection(con);
            List<Item> items = new List<Item>();
            SqlCommand cmd = new SqlCommand("select * from Order_Item where Card_Id='"+cart_id+"'",sql);
            DataTable dt = new DataTable();
            SqlDataAdapter ad   = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Item item = new Item();
                item.Id = Guid.Parse(dr[0].ToString());
                item.Product_Id = Guid.Parse(dr[1].ToString());
                item.Quantity = int.Parse(dr[2].ToString());
                item.Price = int.Parse(dr[3].ToString());
                item.Description = dr[5].ToString();
                item.Cart_Id = Guid.Parse(dr[6].ToString());
                item.subtotal= int.Parse(dr[7].ToString());
                items.Add(item);
            }
            return Ok(items);
        }

        [HttpPost]
        [Route("Add-Item/{cart_id:Guid}/{product_id:Guid}")]

        public async Task<IActionResult> AddItem(Item item,[FromRoute]Guid cart_id,Guid product_id)
        {
            SqlConnection sql = new SqlConnection(con);
            Product product = new Product();
            product.Id = product_id;
            SqlCommand cmdd = new SqlCommand("select * from Product where Product_Id='"+product_id+"'", sql);
            DataTable dtt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            da.Fill(dtt);
            foreach(DataRow dr in dtt.Rows)
            {
                product.Price = int.Parse(dr[5].ToString());
                product.Description = dr[2].ToString();
                product.Name= dr[1].ToString();
            }
            item.Id = Guid.NewGuid();
            item.Quantity= 1;
            item.subtotal = (item.Quantity * item.Price);
            SqlCommand cmd = new SqlCommand("insert into Order_Item(Order_Item_Id,Product_Id,Product_Quatity,Product_Price,Producr_Description,Cart_Id,item_Subtotal,Product_Name) values('"+item.Id+"','"+product.Id+"','"+item.Quantity+"','"+product.Price+"','"+product.Description+"','"+cart_id+"','"+item.subtotal+"','"+product.Name+"')", sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
            List<Item> items = new List<Item>();
            SqlCommand command = new SqlCommand("select * from Order_Item where Order_Item_Id='"+item.Id+"'", sql);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(command);
            ad.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                Item item1 = new Item();
                item1.Id = Guid.Parse(dr[0].ToString());
                item1.Product_Id = Guid.Parse(dr[1].ToString());
                item1.Quantity = int.Parse(dr[2].ToString());
                item1.Price = int.Parse(dr[3].ToString());
                item1.Description = dr[5].ToString();
                item1.Cart_Id = Guid.Parse(dr[6].ToString());
                item1.subtotal = int.Parse(dr[7].ToString());
                items.Add(item1);
            }
            return Ok(items);
        }
        [HttpPut]
        [Route("Add-Item/{Item_id:Guid}/{Product_id:Guid}")]

        public async Task<IActionResult> EditItem(Item item, [FromRoute] Guid item_id,Guid product_id)
        {
            SqlConnection sql = new SqlConnection(con);
            Product product = new Product();
            product.Id = product_id;
            SqlCommand cmdd = new SqlCommand("select * from Product where Product_Id='" + product_id + "'", sql);
            DataTable dtt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            da.Fill(dtt);
            foreach (DataRow dr in dtt.Rows)
            {
                product.Price = int.Parse(dr[5].ToString());
            }
            item.subtotal = (item.Quantity * item.Price);
            SqlCommand cmd = new SqlCommand("Update Order_Item set Product_Quantity='"+item.Quantity+"',Subtotal='"+item.subtotal+"' where Order_Item_Id='"+item_id+"'", sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
            List<Item> items = new List<Item>();
            SqlCommand command = new SqlCommand("select * from Order_Item where Order_Item_Id='" + item_id + "'", sql);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(command);
            ad.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Item item1 = new Item();
                item1.Id = Guid.Parse(dr[0].ToString());
                item1.Product_Id = Guid.Parse(dr[1].ToString());
                item1.Quantity = int.Parse(dr[2].ToString());
                item1.Price = int.Parse(dr[3].ToString());
                item1.Description = dr[5].ToString();
                item1.Cart_Id = Guid.Parse(dr[6].ToString());
                item1.subtotal = int.Parse(dr[7].ToString());
                items.Add(item1);
            }
            return Ok(items);
        }

        [HttpDelete]
        [Route("Delete-Item/{Item_id:Guid}")]

        public async Task<IActionResult> DeleteItem([FromRoute] Guid item_id)
        {
            SqlConnection sql = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand("Delete from Order_Item where Order_Item_Id='" + item_id + "'", sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
            return Ok();
        }
    }
}
