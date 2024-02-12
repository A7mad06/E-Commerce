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
            SqlCommand cmd = new SqlCommand("select * from Order_Item where Cart_Id='"+cart_id+"'",sql);
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
                item.Product_Name = dr[8].ToString();
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
            item.subtotal = (item.Quantity * product.Price);
            SqlCommand cmd = new SqlCommand("insert into Order_Item(Order_Item_Id,Product_Id,Product_Quantity,Product_Price,Product_Description,Cart_Id,Item_Subtotal,Product_Name) values('"+item.Id+"','"+product.Id+"','"+item.Quantity+"','"+product.Price+"','"+product.Description+"','"+cart_id+"','"+item.subtotal+"','"+product.Name+"')", sql);
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
                item1.Product_Name = dr[8].ToString();
                items.Add(item1);
            }
            return Ok(items);
        }
        [HttpPut]
        [Route("Add-Item/{Item_id:Guid}")]

        public async Task<IActionResult> EditItem(Item item, [FromRoute] Guid Item_id)
        {
            SqlConnection sql = new SqlConnection(con);
            Product product = new Product();
            SqlCommand cmdd = new SqlCommand("select Product_Id from Order_Item where Order_Item_Id='"+Item_id+"'", sql);
            DataTable dtt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            da.Fill(dtt);
            foreach (DataRow dr in dtt.Rows)
            {
                product.Id = Guid.Parse(dr[0].ToString());
            }
            SqlCommand cmddd = new SqlCommand("select Product_Price from Product where Product_Id='"+product.Id+"'", sql);
            DataTable dttt = new DataTable();
            SqlDataAdapter daa = new SqlDataAdapter(cmddd);
            daa.Fill(dttt);
            foreach (DataRow dr in dttt.Rows)
            {
                product.Price = int.Parse(dr[0].ToString());
            }
            item.subtotal = (item.Quantity * product.Price);
            SqlCommand cmd = new SqlCommand("Update Order_Item set Product_Quantity='"+item.Quantity+"',Item_Subtotal='"+item.subtotal+"' where Order_Item_Id='"+Item_id+"'", sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
            List<Item> items = new List<Item>();
            SqlCommand command = new SqlCommand("select * from Order_Item where Order_Item_Id='"+Item_id+"'", sql);
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
                item1.Product_Name = dr[8].ToString();
                items.Add(item1);
            }
            return Ok(items);
        }

        [HttpDelete]
        [Route("Delete-Item/{Item_id:Guid}")]

        public async Task<IActionResult> DeleteItem([FromRoute] Guid Item_id)
        {
            SqlConnection sql = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand("Delete from Order_Item where Order_Item_Id='"+Item_id+"'", sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
            return Ok();
        }
        [HttpDelete]
        [Route("Delete-All/{Cart_id:Guid}")]

        public async Task<IActionResult> DeleteAllItems([FromRoute] Guid Cart_id)
        {
            SqlConnection sql = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand("Delete from Order_Item where Cart_Id='"+Cart_id+"'", sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
            return Ok();
        }
    }
}
