
using MovieTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace MovieTicketBookingSystem.Models
{
    public class MovieDBHandle
    {
        private SqlConnection con;

        private void Connection()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings[
                    "MovieTicketBookingSystem"
                ].ConnectionString;

            con = new SqlConnection(connectionString);
        }

        // Get all movies
        public List<Movie> GetMovies()
        {
            Connection();

            List<Movie> movies = new List<Movie>();

            string query =
                "SELECT Movie_id, Movie_name, Release_date, Category_id, Rate " +
                "FROM Tbl_Movie";

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Movie movie = new Movie();

                movie.Movie_id =
                    Convert.ToInt32(row["Movie_id"]);

                movie.Movie_name =
                    row["Movie_name"].ToString();

                movie.Release_date =
                    Convert.ToDateTime(row["Release_date"]);

                movie.Category_id =
                    Convert.ToInt32(row["Category_id"]);

                movie.Rate =
                    Convert.ToDecimal(row["Rate"]);

                movies.Add(movie);
            }

            return movies;
        }


        // Get all movie categories
        public List<MovieCategory> GetCategories()
        {
            Connection();

            List<MovieCategory> categories =
                new List<MovieCategory>();

            string query =
                "SELECT Category_id, Type " +
                "FROM Tbl_Movie_Category";

            SqlCommand cmd =
                new SqlCommand(query, con);

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

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


        // Add movie
        public bool AddMovie(Movie movie)
        {
            Connection();

            string query =
                "INSERT INTO Tbl_Movie " +
                "(Movie_name, Release_date, Category_id, Rate) " +
                "VALUES (@Movie_name, @Release_date, @Category_id, @Rate)";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Movie_name",
                movie.Movie_name
            );

            cmd.Parameters.AddWithValue(
                "@Release_date",
                movie.Release_date
            );

            cmd.Parameters.AddWithValue(
                "@Category_id",
                movie.Category_id
            );

            cmd.Parameters.AddWithValue(
                "@Rate",
                movie.Rate
            );

            con.Open();

            int result =
                cmd.ExecuteNonQuery();

            con.Close();

            return result > 0;
        }
        // Delete movie
        public bool DeleteMovie(int id)
        {
            Connection();

            string query =
                "DELETE FROM Tbl_Movie WHERE Movie_id = @Movie_id";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Movie_id",
                id
            );

            con.Open();

            int result =
                cmd.ExecuteNonQuery();

            con.Close();

            return result > 0;
        }
        public List<Movie> SearchMoviesByCategory(int categoryId)
        {
            Connection();

            List<Movie> movies = new List<Movie>();

            SqlCommand cmd =
                new SqlCommand("SearchMovieByCategory", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@Category_id",
                categoryId
            );

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Movie movie = new Movie();

                movie.Movie_id =
                    Convert.ToInt32(row["Movie_id"]);

                movie.Movie_name =
                    row["Movie_name"].ToString();

                movie.Release_date =
                    Convert.ToDateTime(row["Release_date"]);

                movie.Category_id =
                    Convert.ToInt32(row["Category_id"]);

                movie.Rate =
                    Convert.ToDecimal(row["Rate"]);

                movies.Add(movie);
            }

            return movies;
        }
    }

}
