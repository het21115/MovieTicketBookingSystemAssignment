using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace MovieTicketBookingSystem.Models
{
    public class UserDBHandle
    {
        public UserDBHandle() { }

        public string constr;
        public SqlConnection con;
        public void Connection()
        {
            constr = WebConfigurationManager.ConnectionStrings["MovieTicketBookingSystem"].ToString();
            con = new SqlConnection(constr);
        }
        public UserModel LoginUser(LoginModel login)
        {
            Connection();
            UserModel user = null;

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("login_user", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email_id", login.Email_id);
                cmd.Parameters.AddWithValue("@User_password", login.User_password);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserModel
                    {
                        User_id = Convert.ToInt32(reader["User_id"]),
                        User_name = reader["User_name"].ToString(),
                        Email_id = reader["Email_id"].ToString(),
                        User_password = reader["User_password"].ToString(),
                        City = reader["City"].ToString(),
                        Phone_no = Convert.ToInt64(reader["Phone_no"])
                    };
                }
            }

            return user;
        }
        public UserModel GetUserById(int userId)
        {
            Connection();

            UserModel user = null;

            SqlCommand cmd = new SqlCommand("get_user_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@User_id", userId);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                user = new UserModel
                {
                    User_id = Convert.ToInt32(reader["User_id"]),
                    User_name = reader["User_name"].ToString(),
                    Email_id = reader["Email_id"].ToString(),
                    User_password = reader["User_password"].ToString(),
                    City = reader["City"].ToString(),
                    Phone_no = Convert.ToInt64(reader["Phone_no"])
                };
            }

            reader.Close();
            con.Close();

            return user;
        }
        public bool UpdateUser(UserModel user)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("update_user", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@User_id", user.User_id);
            cmd.Parameters.AddWithValue("@User_name", user.User_name);
            cmd.Parameters.AddWithValue("@Email_id", user.Email_id);
            cmd.Parameters.AddWithValue("@User_password", user.User_password);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@Phone_no", user.Phone_no);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result > 0;
        }
        public bool DeleteMovie(int movieId)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("delete_movie", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Movie_id", movieId);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result > 0;
        }
        public bool AddUser(UserModel user)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("add_user", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@User_name", user.User_name);
            cmd.Parameters.AddWithValue("@Email_id", user.Email_id);
            cmd.Parameters.AddWithValue("@User_password", user.User_password);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@Phone_no", user.Phone_no);

            con.Open();
            cmd.ExecuteNonQuery();

            con.Close();

            return true;
        }
        public List<UserModel> getUsers()
        {
            Connection();

            List<UserModel> users = new List<UserModel>();

            SqlCommand cmd = new SqlCommand("get_user", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                UserModel user = new UserModel();

                user.User_id = Convert.ToInt32(reader["UserId"]);
                user.User_name = reader["UserName"].ToString();
                user.Email_id = reader["Email"].ToString();
                user.User_password = reader["Password"].ToString();
                user.City = reader["City"].ToString();
                user.Phone_no = Convert.ToInt64(reader["PhoneNumber"]);

                users.Add(user);
            }

            reader.Close();
            con.Close();

            return users;
        }
    }
}