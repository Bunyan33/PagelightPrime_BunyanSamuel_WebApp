using PagelightPrime_BunyanSamuel_WebApp.Contracts;
using PagelightPrime_BunyanSamuel_WebApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace PagelightPrime_BunyanSamuel_WebApp.Repository
{
    public class UserRepository : IUserContract
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public void CreateUser(User user)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var command =  new SqlCommand("InsertUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Gender", user.Gender);
            command.Parameters.AddWithValue("@Address", user.Address);
            command.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
            command.Parameters.AddWithValue("@DataCreated", DateTime.UtcNow);


            command.ExecuteNonQuery();
        }

        public IEnumerable<User> ReadAllUser()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            using var command = new SqlCommand("ReadAllUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            using var reader = command.ExecuteReader();
            var users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                    Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                    PhoneNum = reader.IsDBNull(reader.GetOrdinal("PhoneNum")) ? null : reader.GetString(reader.GetOrdinal("PhoneNum")),
                    DataCreated = reader.IsDBNull(reader.GetOrdinal("DataCreated")) ? null : reader.GetDateTime(reader.GetOrdinal("DataCreated")),
                    DataUpdated = reader.IsDBNull(reader.GetOrdinal("DataUpdated")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataUpdated")),

                });
            }
            return users;
        }

        public User FindUserById(int Id)
        {
            User user = null;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("FindUserById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNum = reader.IsDBNull(reader.GetOrdinal("PhoneNum")) ? null : reader.GetString(reader.GetOrdinal("PhoneNum"))
                            };
                        }
                    }
                }
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var command = new SqlCommand("UpdateUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id",user.Id);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Gender", user.Gender);
            command.Parameters.AddWithValue("@Address", user.Address);
            command.Parameters.AddWithValue("@PhoneNum", user.PhoneNum);
            command.Parameters.AddWithValue("@DataUpdated", DateTime.UtcNow);
            command.ExecuteNonQuery();
        }

        public void DeleteUser(int Id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var command = new SqlCommand("DeleteUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
        }

    }
}
