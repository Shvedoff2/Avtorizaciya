using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Avtorizaciya.Model
{
    public class TableService
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=UP01;Integrated Security=True";
    }
    public class UserBusinessLogic
    {
        public void DeleteUser(Login1 selectedUser)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=UP01;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM logins WHERE ID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", selectedUser.ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdateUser(Login1 selectedUser, string userName, string password, string name)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=UP01;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"UPDATE logins SET [login] = '{userName}', [password] = '{password}', [name] = '{name}' WHERE ID = '{selectedUser.ID}'", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public ObservableCollection<Login1> LoadUsers()
        {
            var users = new ObservableCollection<Login1>();

            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=UP01;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM logins ORDER BY ID", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new Login1
                            {
                                ID = reader.GetInt32(0),
                                Login = reader.GetString(1),
                                Password = reader.GetString(2),
                                Name = reader.GetString(3),
                                Role = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return users;
        }
    }
}
