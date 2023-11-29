using System.Data.OleDb;
using System.Data.SqlClient;

namespace Avtorizaciya.Model
{
    public interface ILoginService
    {
        bool ValidateUser(string userName, string password);
    }

    public class LoginService : ILoginService
    {
        public bool ValidateUser(string userName, string password)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=UP01;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM logins WHERE login='{userName}' AND password='{password}'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
        }
    }
    public interface IProverkaService
    {
        bool ValidateUser(string userName);
    }

    public class ProverkaService : IProverkaService
    {
        public bool ValidateUser(string userName)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=UP01;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM logins WHERE login='{userName}'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
        }
    }
    public interface IRegistService
    {
        bool ValidateUser(string userName, string password, string name);
    }

    public class RegistService : IRegistService
    {
        public bool ValidateUser(string userName, string password, string name)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=UP01;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO logins ([login], [password], [name], [role]) VALUES('{userName}', '{password}', '{name}', 'user')", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
        }
    }
}