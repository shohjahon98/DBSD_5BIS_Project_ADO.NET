using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BookOnlineMarket.Models.Services
{
    public class BookRepository
    {
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BookDB"].ToString());
        public void AddBook(Book book)
        {
            SqlCommand com = new SqlCommand("AddBook", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@BookName", book.BookName);
            com.Parameters.AddWithValue("@Author", book.Author);
            com.Parameters.AddWithValue("@Publisher", book.Publisher);
            com.Parameters.AddWithValue("@Price", book.Price);
            com.Parameters.AddWithValue("@AddDate", DateTime.Now.ToString("dd/MM/yyyy"));
            com.Parameters.AddWithValue("@Genre", book.Genre);
            com.Parameters.AddWithValue("@Quentity", book.Quentity);
            com.Parameters.AddWithValue("@BookImg", book.BookImg);
            com.Parameters.AddWithValue("@Title", book.Title);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            
        }
        public List<Book> GetAllBooks()
        {
            List<Book> BookList = new List<Book>();
            SqlCommand com = new SqlCommand("GetAllBook", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();   
            foreach (DataRow dr in dt.Rows)
            {
                BookList.Add(
                    new Book
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        BookName = Convert.ToString(dr["BookName"]),
                        Author = Convert.ToString(dr["Author"]),
                        Publisher = Convert.ToString(dr["Publisher"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        AddDate = Convert.ToString(dr["AddDate"]),
                        Genre = Convert.ToString(dr["Genre"]),
                        Quentity = Convert.ToInt32(dr["Quentity"]),
                        BookImg = (byte[])(dr["BookImg"]),
                        Title = Convert.ToString(dr["Title"])
                    }
                    ) ;
            }
            return BookList;
        }

        
        public bool UpdateBook(Book book)
        {
            SqlCommand com = new SqlCommand("UpdateBook", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", book.Id);
            com.Parameters.AddWithValue("@BookName", book.BookName);
            com.Parameters.AddWithValue("@Author", book.Author);
            com.Parameters.AddWithValue("@Publisher", book.Publisher);
            com.Parameters.AddWithValue("@Price", book.Price);
            com.Parameters.AddWithValue("@AddDate", book.AddDate);
            com.Parameters.AddWithValue("@Genre", book.Genre);
            com.Parameters.AddWithValue("@Quentity", book.Quentity);
            com.Parameters.AddWithValue("@BookImg", book.BookImg);
            com.Parameters.AddWithValue("@Title", book.Title);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteBook(int id)
        {
            SqlCommand com = new SqlCommand("DeleteBook", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
    
}