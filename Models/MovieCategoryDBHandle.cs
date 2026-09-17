using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MovieTicketBookingSystem.Models
{
    public class MovieCategoryDBHandle
    {
        private SqlConnection con;

        // Connection Method
        private void Connection()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings[
                    "MovieTicketBookingSystem"
                ].ConnectionString;

            con = new SqlConnection(connectionString);
        }


        // 1. Get All Categories
        public List<MovieCategory> GetCategories()
        {
            Connection();

            List<MovieCategory> categories =
                new List<MovieCategory>();

            string query =
                "SELECT Category_id, Type " +
                "FROM Tbl_Movie_Category";

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                MovieCategory category =
                    new MovieCategory();

                category.Category_id =
                    Convert.ToInt32(row["Category_id"]);

                category.Type =
                    row["Type"].ToString();

                categories.Add(category);
            }

            return categories;
        }


        // 2. Add Category
        public bool AddCategory(MovieCategory category)
        {
            Connection();

            string query =
                "INSERT INTO Tbl_Movie_Category " +
                "(Cat_Type) VALUES (@Cat_Type)";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Cat_Type",
                category.Type
            );

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            if (result > 0)
                return true;

            return false;
        }


        // 3. Get Category By ID
        public MovieCategory GetCategoryById(int id)
        {
            Connection();

            MovieCategory category = null;

            string query =
                "SELECT Category_id, Type " +
                "FROM Tbl_Movie_Category " +
                "WHERE Category_id = @Category_id";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Category_id",
                id
            );

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            if (reader.Read())
            {
                category =
                    new MovieCategory();

                category.Category_id =
                    Convert.ToInt32(reader["Category_id"]);

                category.Type =
                    reader["Type"].ToString();
            }

            reader.Close();
            con.Close();

            return category;
        }


        // 4. Update Category
        public bool UpdateCategory(
            MovieCategory category)
        {
            Connection();

            string query =
                "UPDATE Tbl_Movie_Category " +
                "SET Type = @Type " +
                "WHERE Category_id = @Category_id";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Category_id",
                category.Category_id
            );

            cmd.Parameters.AddWithValue(
                "@Type",
                category.Type
            );

            con.Open();

            int result =
                cmd.ExecuteNonQuery();

            con.Close();

            if (result > 0)
                return true;

            return false;
        }


        // 5. Delete Category
        public bool DeleteCategory(int id)
        {
            Connection();

            string query =
                "DELETE FROM Tbl_Movie_Category " +
                "WHERE Category_id = @Category_id";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Category_id",
                id
            );

            con.Open();

            int result =
                cmd.ExecuteNonQuery();

            con.Close();

            if (result > 0)
                return true;

            return false;
        }
    }
}