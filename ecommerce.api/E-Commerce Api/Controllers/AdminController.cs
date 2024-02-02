using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            List < Admin > lst = new List<Admin>();
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("Select * from Admin",con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                Admin admin = new Admin();
                admin.Id = Guid.Parse(dr[0].ToString());
                admin.Email = dr[1].ToString();
                admin.Password = dr[2].ToString();
                lst.Add(admin);
            }
            return Ok(lst);
        }

        [HttpPost]
        [Route("Add-Admin")]

        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            admin.Id = Guid.NewGuid();
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("insert into Admin(Admin_Id,Admin_Email,Admin_Password) values('"+admin.Id+"','"+admin.Email+"','"+admin.Password+"')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            List<Admin> lst = new List<Admin>();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand("select * from Admin where Admin_Id='"+admin.Id+"'",con));
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Admin admin1 = new Admin();
                admin1.Id = Guid.Parse(dr[0].ToString());
                admin1.Email = dr[1].ToString();
                admin1.Password = dr[2].ToString();
                lst.Add(admin1);
            }
            con.Close();
            return Ok(lst);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        
        public async Task<IActionResult> UpdateAdmin([FromRoute]Guid id,Admin admin)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("Update Admin set Admin_Password='"+admin.Password+"' where Admin_Id = '"+id+"'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            List<Admin> lst = new List<Admin>();    
            SqlCommand comm = new SqlCommand("select * from Admin where Admin_Id='" + id + "'",con);
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(dataTable);
            foreach(DataRow i in  dataTable.Rows)
            {
                Admin admin1 = new Admin();
                admin1.Id = Guid.Parse(i[0].ToString());
                admin1.Email = i[1].ToString();
                admin1.Password = i[2].ToString();
                lst.Add(admin1);
            }
            return Ok(lst);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute]Guid id)
        {
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand cmd = new SqlCommand("Delete from Admin where Admin_Id = '" + id + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok();
        }
    }
}
