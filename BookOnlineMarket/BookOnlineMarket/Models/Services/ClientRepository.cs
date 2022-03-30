using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.Services
{
    public class ClientRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BookDB"].ToString());

        public void AddClient(Client client)
        {
            try
            {
                SqlCommand com = new SqlCommand("AddClient", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@LastName", client.LastName);
                com.Parameters.AddWithValue("@FirstName", client.FirstName);
                com.Parameters.AddWithValue("@Adress", client.Adress);
                com.Parameters.AddWithValue("@City", client.City);
                com.Parameters.AddWithValue("@Phone", client.Phone);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }
        }
        public List<Client> GetAllClient()
        {
            List<Client> ClientList = new List<Client>();
            SqlCommand com = new SqlCommand("GetAllClient", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                ClientList.Add(
                    new Client
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        Adress = Convert.ToString(dr["Adress"]),
                        City = Convert.ToString(dr["City"]),
                        Phone=Convert.ToString(dr["Phone"])
                    }
                    );
            }
            return ClientList;
        }
        public void UpdateClient(Client client)
        {
            try
            {
                SqlCommand com = new SqlCommand("UpdateClient", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", client.Id);
                com.Parameters.AddWithValue("@LastName", client.LastName);
                com.Parameters.AddWithValue("@FirstName", client.FirstName);
                com.Parameters.AddWithValue("@Adress", client.Adress);
                com.Parameters.AddWithValue("@City", client.City);
                com.Parameters.AddWithValue("@Phone", client.Phone);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {

            }

        }
        public void DeleteClient(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("DeleteClient", con);
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