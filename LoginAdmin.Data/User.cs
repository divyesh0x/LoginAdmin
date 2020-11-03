using System;
using System.Collections.Generic;
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
        private string email;
        private string password;

        public int Id
        {
            get => id;
            set => id = value;
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
            Email = data.Email;
            Password = data.Password;
            Id = data.Id;
        }

        private void ResetProperties()
        {
            Email = "";
            Password = "";
            Id = -1;
        }

        public User GetUser(int id)
        {
            User user = new User();
            SqlConnection conn = new SqlConnection(user.ConnectionString);

            SqlCommand cmd = new SqlCommand("dbo.RetrieveUserInfo", conn)
            {
                CommandType = CommandType.StoredProcedure,
                Transaction = SqlTransaction
            };

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();

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
                conn.Close();
            }

            var dataRowCollection = ds.Tables[0]?.Rows;
            if (dataRowCollection != null)
                foreach (DataRow dataRow in dataRowCollection)
                {
                    user.Id = Convert.ToInt32(dataRow["Id"]);
                    user.Email = dataRow["Email"].ToString();
                    user.Password = dataRow["Password"].ToString();
                }

            return user;

        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from UserInfo");

            SqlConnection conn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(sb.ToString(), conn)
            {
                Transaction = SqlTransaction
            };

            conn.Open();

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
                conn.Close();
            }

            var dataRowCollection = ds.Tables[0]?.Rows;
            if (dataRowCollection != null)
                foreach (DataRow dataRow in dataRowCollection)
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(dataRow["Id"]),
                        Email = dataRow["Email"].ToString(),
                        Password = dataRow["Password"].ToString()
                    };
                    users.Add(user);
                }

            return users;

        }
    }
}