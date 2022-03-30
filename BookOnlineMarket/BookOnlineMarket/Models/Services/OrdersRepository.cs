using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.Services
{
    public class OrdersRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BookDB"].ToString());

        public void AddOrders(Orders orders)
        {
            try
            {
                SqlCommand com = new SqlCommand("AddOrders", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookID", orders.BookID);
                com.Parameters.AddWithValue("@ClientID", orders.ClientID);
                com.Parameters.AddWithValue("@OrderDate", DateTime.Now.ToString("dd/MM/yyyy"));
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }
        public List<Orders> GetAllOrders()
        {
            List<Orders> OrdersList = new List<Orders>();
            SqlCommand com = new SqlCommand("GetAllOrders", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                OrdersList.Add(
                    new Orders
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        BookID = Convert.ToInt32(dr["BookID"]),
                        ClientID = Convert.ToInt32(dr["ClientID"]),
                        OrderDate = Convert.ToString(dr["OrderDate"])
                    }
                    );
            }
            return OrdersList;
        }
        public void UpdateOrders(Orders orders)
        {
            try
            {
                SqlCommand com = new SqlCommand("UpdateOrders", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", orders.Id);
                com.Parameters.AddWithValue("@BookID", orders.BookID);
                com.Parameters.AddWithValue("@Phone", orders.ClientID);
                com.Parameters.AddWithValue("@Email", orders.OrderDate);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }

        }
        public void DeleteOrders(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("DeleteOrders", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }

        }
    }
}