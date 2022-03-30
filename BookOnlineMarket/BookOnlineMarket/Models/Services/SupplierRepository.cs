using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.Services
{
    public class SupplierRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BookDB"].ToString());

        public void AddSupplier(Supplier supplier)
        {
            try
            {
                SqlCommand com = new SqlCommand("AddSupplier", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SupName", supplier.SupName);
                com.Parameters.AddWithValue("@Phone", supplier.Phone);
                com.Parameters.AddWithValue("@Email", supplier.Email);
                com.Parameters.AddWithValue("@Adress", supplier.Adress);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }
        }
        public List<Supplier> GetAllSupplier()
        {
            List<Supplier> SupplierList = new List<Supplier>();
            SqlCommand com = new SqlCommand("GetAllSupplier", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                SupplierList.Add(
                    new Supplier
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        SupName = Convert.ToString(dr["SupName"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Email = Convert.ToString(dr["Email"]),
                        Adress = Convert.ToString(dr["Adress"])   
                    }
                    );
            }
            return SupplierList;
        }
        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                SqlCommand com = new SqlCommand("UpdateSupplier", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", supplier.Id);
                com.Parameters.AddWithValue("@SupName", supplier.SupName);
                com.Parameters.AddWithValue("@Phone", supplier.Phone);
                com.Parameters.AddWithValue("@Email", supplier.Email);
                com.Parameters.AddWithValue("@Adress", supplier.Adress);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }
            
        }
        public void DeleteSupplier(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("DeleteSupplier", con);
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