using E_Commerce_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        string con = "Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true";

        [HttpGet]
        public async Task<IActionResult> ShowAllCarriers() { 
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand com = new SqlCommand("select * from Carrier", con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(dt);
            List<Carrier> list = new List<Carrier>();
            foreach(DataRow dr in dt.Rows)
            {
                Carrier carrier = new Carrier();
                carrier.Id = Guid.Parse(dr[0].ToString());
                carrier.Name = dr[1].ToString();
                carrier.Phone = int.Parse(dr[2].ToString());
                carrier.Address = dr[3].ToString();
                list.Add(carrier);
            }
            return Ok(list);
        }

        [HttpPost]
        [Route("Add-Carrier")]
        public async Task<IActionResult> AddCarrier(Carrier carrier)
        {
            carrier.Id=Guid.NewGuid();
            SqlConnection con = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand com = new SqlCommand("insert into Carrier(Carrier_Id,Carrier_Name,Carrier_Phone,Carrier_Address) values('" + carrier.Id + "','" + carrier.Name + "','" + carrier.Phone + "','" + carrier.Address + "')", con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            SqlCommand comm = new SqlCommand("select * from Carrier where Carrier_Id='"+carrier.Id+"'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(dt);
            List<Carrier> list = new List<Carrier>();
            foreach (DataRow dr in dt.Rows)
            {
                Carrier carrier1 = new Carrier();
                carrier1.Id = Guid.Parse(dr[0].ToString());
                carrier1.Name = dr[1].ToString();
                carrier1.Phone = int.Parse(dr[2].ToString());
                carrier1.Address = dr[3].ToString();
                list.Add(carrier1);
            }
            return Ok(list);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> ModifyCarrier([FromRoute] Guid id,Carrier carrier)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-ODD35L0\\SQLEXPRESS;Database=E-Commerce;Trusted_Connection=true");
            SqlCommand com = new SqlCommand("Update Carrier set Carrier_Name='"+carrier.Name+"',Carrier_Phone='"+carrier.Phone+"',Carrier_Address='"+carrier.Address+"' where Carrier_Id='"+id+"'", sqlConnection);
            sqlConnection.Open();
            com.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("select * from Carrier where Carrier_Id='" + id + "'", sqlConnection);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            List<Carrier> list = new List<Carrier>();
            foreach(DataRow dr in dt.Rows)
            {
                Carrier car=new Carrier();
                car.Id = Guid.Parse(dr[0].ToString());
                car.Name = dr[1].ToString();
                car.Phone = int.Parse(dr[2].ToString());
                car.Address = dr[3].ToString();
                list.Add(car);
            }
            sqlConnection.Close();
            return Ok(list);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCarrier([FromRoute] Guid id)
        {
            SqlConnection sqlConnection = new SqlConnection(con);
            SqlCommand com = new SqlCommand("Delete from Carrier where Carrier_Id='"+id+"'",sqlConnection);
            sqlConnection.Open();
            com.ExecuteNonQuery();
            sqlConnection.Close();
            return Ok();
        }
    }
}
