using BookOnlineMarket.Models.viewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.Services
{
    public class SalesProcess
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BookDB"].ToString());

        public List<Procces> Procees()
        {
            List<Procces> SalesProcessList = new List<Procces>();
            SqlCommand com = new SqlCommand("sales_proess", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                SalesProcessList.Add(
                    new Procces
                    {
                        BookName = Convert.ToString(dr["BookName"]),
                        Author = Convert.ToString(dr["Author"]),
                        SupName=Convert.ToString(dr["SupName"]),
                        Price = Convert.ToString(dr["Price"]),
                        OrderDate = Convert.ToString(dr["OrderDate"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        
                    }
                    );
            }
            return SalesProcessList;
        }

    }
}