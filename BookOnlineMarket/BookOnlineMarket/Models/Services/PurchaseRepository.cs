using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.Services
{
    public class PurchaseRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BookDB"].ToString());

        public void AddPurchase(Purchase purchase)
        {
            try
            {
                SqlCommand com = new SqlCommand("AddPurchase", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SupplireID", purchase.SupplireID);
                com.Parameters.AddWithValue("@Price", purchase.Price);
                com.Parameters.AddWithValue("@PurchaseDate", DateTime.Now.ToString("dd/MM/yyyy"));
                com.Parameters.AddWithValue("@BookID", purchase.BookID);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }
        }
        public List<Purchase> GetAllPurchase()
        {
            List<Purchase> PurchaseList = new List<Purchase>();
            SqlCommand com = new SqlCommand("GetAllPurchase", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                PurchaseList.Add(
                    new Purchase
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        BookID = Convert.ToInt32(dr["BookID"]),
                        SupplireID = Convert.ToInt32(dr["SupplireID"]),
                        PurchaseDate = Convert.ToString(dr["PurchaseDate"]),
                        Price=Convert.ToDecimal(dr["Price"])
                    }
                    );
            }
            return PurchaseList;
        }
        public void UpdatePurchase(Purchase purchase)
        {
            try
            {
                SqlCommand com = new SqlCommand("UpdatePurchase", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", purchase.Id);
                com.Parameters.AddWithValue("@SupplireID", purchase.SupplireID);
                com.Parameters.AddWithValue("@Price", purchase.Price);
                com.Parameters.AddWithValue("@PurchaseDate",purchase.PurchaseDate);
                com.Parameters.AddWithValue("@BookID", purchase.BookID);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }

        }
        public void DeletePurchase(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("DeletePurchase", con);
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