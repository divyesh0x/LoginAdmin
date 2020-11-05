using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace LoginAdmin.Data
{
    public class User : DataObject
    {
        private int id;
        private string firstname;
        private string lastname;
        private string address;
        private string city;
        private string province;
        private string country;
        private string phonenumber;
        private string email;
        private string password;
        private int isadmin;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string FirstName
        {
            get => firstname;
            set => firstname = value;
        }

        [Required]
        public string LastName
        {
            get => lastname;
            set => lastname = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }

        public string City
        {
            get => city;
            set => city = value;
        }

        public string Province
        {
            get => province;
            set => province = value;
        }

        public string Country
        {
            get => country;
            set => country = value;
        }

        public string PhoneNumber
        {
            get => phonenumber;
            set => phonenumber = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public int IsAdmin
        {
            get => isadmin;
            set => isadmin = value;
        }
        public object Name => this.firstname + " " + this.lastname;

        public User() : base()
        {

        }

        public User(SqlTransaction trans) : base(trans)
        {

        }

        public User(int id) : base()
        {
            ResetProperties();
            var data = GetUser(id);
            Id = data.Id;
            FirstName= data.FirstName;
            LastName = data.LastName;
            Address = data.Address;
            City = data.City;
            Province = data.Province;
            Country = data.Country;
            PhoneNumber = data.PhoneNumber;
            Email = data.Email;
            Password = data.Password;
            IsAdmin = data.IsAdmin;

        }

        public User(string email) : base()
        {
            ResetProperties();
            var data = GetUser(email);
            Id = data.Id;
            FirstName = data.FirstName;
            LastName = data.LastName;
            Address = data.Address;
            City = data.City;
            Province = data.Province;
            Country = data.Country;
            PhoneNumber = data.PhoneNumber;
            Email = data.Email;
            Password = data.Password;
            IsAdmin = data.IsAdmin;
        }

        private void ResetProperties()
        {  
            Id = -1;
            FirstName = "";
            LastName = "";
            Address = "";
            City = "";
            Province = "";
            PhoneNumber = "";
            Email = "";
            Password = "";
            IsAdmin = -1;
        }

        public User GetUser(int id)
        {
            User user = new User();
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("dbo.RetrieveUserInfo", con)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = SqlTransaction
            };

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();

            SqlDataAdapter sa = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            try
            {
                sa.Fill(ds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

            var dataRowCollection = ds.Tables[0]?.Rows;
            if (dataRowCollection != null)
                foreach (DataRow dataRow in dataRowCollection)
                {
                    user.Id = Convert.ToInt32(dataRow["Id"]);
                    user.FirstName = dataRow["FirstName"].ToString();
                    user.LastName = dataRow["LastName"].ToString();
                    user.Address = dataRow["Address"].ToString();
                    user.City = dataRow["City"].ToString();
                    user.Province = dataRow["Province"].ToString();
                    user.Country = dataRow["Country"].ToString();
                    user.PhoneNumber = dataRow["PhoneNumber"].ToString();
                    user.Email = dataRow["Email"].ToString();
                    user.Password = dataRow["Password"].ToString();
                    user.IsAdmin = Convert.ToInt32(dataRow["IsAdmin"]);
                }

            return user;

        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from UserInfo");

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(sb.ToString(), con)
            {
                Transaction = SqlTransaction
            };

            con.Open();

            SqlDataAdapter sa = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            try
            {
                sa.Fill(ds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

            var dataRowCollection = ds.Tables[0]?.Rows;
            if (dataRowCollection != null)
                foreach (DataRow dataRow in dataRowCollection)
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        FirstName = dataRow["FirstName"].ToString(),
                        LastName = dataRow["LastName"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        City = dataRow["City"].ToString(),
                        Province = dataRow["Province"].ToString(),
                        Country = dataRow["Country"].ToString(),
                        PhoneNumber = dataRow["PhoneNumber"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Password = dataRow["Password"].ToString(),
                        IsAdmin = Convert.ToInt32(dataRow["IsAdmin"]),

                    };
                    users.Add(user);
                }

            return users;

        }

        public User GetUser(string email)
        {
            List<User> users = new List<User>();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from UserInfo where email = @email");

            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(sb.ToString(), con)
            {
                Transaction = SqlTransaction
            };

            cmd.Parameters.AddWithValue("@email", email);

            con.Open();

            SqlDataAdapter sa = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            try
            {
                sa.Fill(ds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }

            var dataRowCollection = ds.Tables[0]?.Rows;
            if (dataRowCollection != null)
            {
                if(dataRowCollection.Count > 1)
                {
                    throw new Exception("More than one user found. Please contact IT support");
                }
                foreach (DataRow dataRow in dataRowCollection)
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        FirstName = dataRow["FirstName"].ToString(),
                        LastName = dataRow["LastName"].ToString(),
                        Address = dataRow["Address"].ToString(),
                        City = dataRow["City"].ToString(),
                        Province = dataRow["Province"].ToString(),
                        Country = dataRow["Country"].ToString(),
                        PhoneNumber = dataRow["PhoneNumber"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Password = dataRow["Password"].ToString(),
                        IsAdmin = Convert.ToInt32(dataRow["IsAdmin"]),

                    };
                    return user;
                }
            }

            throw new Exception("User not found");

        }
        public bool AddUser(User user)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("dbo.CreateUserInfo", con)
            {
                CommandType = CommandType.StoredProcedure,  
                Transaction = SqlTransaction

            };

            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@address", user.Address);
            cmd.Parameters.AddWithValue("@city", user.City);
            cmd.Parameters.AddWithValue("@province", user.Province);
            cmd.Parameters.AddWithValue("@country", user.Country);
            cmd.Parameters.AddWithValue("@phoneNumer", user.PhoneNumber);
            cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool UpdateUser(User user)
        {   
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("dbo.UpdateUserInfo", con)
            {
                CommandType = CommandType.StoredProcedure,  
                Transaction = SqlTransaction
            
            };

            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@address", user.Address);
            cmd.Parameters.AddWithValue("@city", user.City);
            cmd.Parameters.AddWithValue("@province", user.Province);
            cmd.Parameters.AddWithValue("@country", user.Country);
            cmd.Parameters.AddWithValue("@phoneNumer", user.PhoneNumber);
            

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteUser(int id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("dbo.DeleteUserInfo", con)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = SqlTransaction
            };
        
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result >= 1)
                return true;
            else
                return false;
        }

    }
}