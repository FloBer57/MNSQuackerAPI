using QuackerMNS.Database;
using QuackerMNS.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace QuackerMNS.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqlConnection _sqlConnection;

        public UserController(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            string query = "SELECT * FROM users;";
            List<User> users = new List<User>();
            DataTable result = _sqlConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                User user = new User
                {
                    Id = Convert.ToInt32(row["User_id"]),
                    FirstName = Convert.ToString(row["User_firstname"]),
                    LastName = Convert.ToString(row["User_lastname"]),
                    Email = Convert.ToString(row["User_email"]),
                    Password = Convert.ToString(row["User_password"]),
                    PhoneNumber = Convert.ToString(row["User_phonenumber"]),
                    ProfilPicturePath = Convert.ToString(row["User_profilpicturepath"]),
                    Description = Convert.ToString(row["User_description"]),
                    TokenResetPassword = Convert.ToString(row["User_tokenresetpassword"]),
                    IsTemporaryPassword = Convert.ToBoolean(row["User_istemporarypassword"]),
                    JobTitleId = Convert.ToInt32(row["User_id"]),
                    UserStatutId = Convert.ToInt32(row["User_statusid"]),
                    UserRoleId = Convert.ToInt32(row["User_roleid"]),

                };
                users.Add(user);
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            string query = $"SELECT * FROM users WHERE User_id={id}";
            DataTable result = _sqlConnection.ExecuteQuery(query);

            DataRow row = result.Rows[0];
            User user = new User
            {
                Id = Convert.ToInt32(row["User_id"]),
                FirstName = Convert.ToString(row["User_firstname"]),
                LastName = Convert.ToString(row["User_lastname"]),
                Email = Convert.ToString(row["User_email"]),
                Password = Convert.ToString(row["User_password"]),
                PhoneNumber = Convert.ToString(row["User_phonenumber"]),
                ProfilPicturePath = Convert.ToString(row["User_profilpicturepath"]),
                Description = Convert.ToString(row["User_description"]),
                TokenResetPassword = Convert.ToString(row["User_tokenresetpassword"]),
                IsTemporaryPassword = Convert.ToBoolean(row["User_istemporarypassword"]),
                JobTitleId = Convert.ToInt32(row["User_jobtitleid"]),
                UserStatutId = Convert.ToInt32(row["User_statusid"]),
                UserRoleId = Convert.ToInt32(row["User_roleid"]),
            };
            return Ok(user);
        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                string query = "INSERT INTO users (User_lastName, User_firstName, User_email, User_password, User_phonenumber, User_profilpicturepath, User_description, User_tokenresetpassword, User_istemporarypassword, User_createdtimeuser, User_jobtitleid, User_statusid, User_roleid) VALUES (@LastName, @FirstName, @Email, @Password, @PhoneNumber, @ProfilPicturePath, @Description, @TokenResetPassword, @IsTemporaryPassword,@CreatedTimeUser, @JobTitleId, @UserStatutId, @UserRoleId)";

                if (string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest("Le mot de passe est requis.");
                }

                var parameters = new Dictionary<string, object>
        {
            {"@FirstName", user.FirstName},
            {"@LastName", user.LastName},
            {"@Email", user.Email},
            {"@Password", user.Password},
            {"@PhoneNumber", user.PhoneNumber},
            {"@ProfilPicturePath", user.ProfilPicturePath},
            {"@Description", user.Description},
            {"@TokenResetPassword", user.TokenResetPassword},
            {"@IsTemporaryPassword", user.IsTemporaryPassword},
            {"@CreatedTimeUser", user.CreatedTimeUser},
            {"@JobTitleId", user.JobTitleId},
            {"@UserStatutId", user.UserStatutId},
            {"@UserRoleId", user.UserRoleId}
        };

                // Exécution de la requête avec des paramètres pour éviter les injections SQL
                _sqlConnection.ExecuteNonQuery(query, parameters);

                return Ok("Utilisateur créé avec succès !");
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de la création de l'utilisateur : " + ex.Message);
            }
        }

        [HttpDelete]

        public IActionResult DeleteUserById(int id)
        {
            string query = $"DELETE FROM users WHERE User_id = @Id";  
            var parameters = new Dictionary<string, object>
    {
        {"@Id", id}
    };

            int affectedRows = _sqlConnection.ExecuteNonQuery(query, parameters);
            if (affectedRows > 0)
            {
                return Ok($"Utilisateur avec ID {id} supprimé.");
            }
            else
            {
                return NotFound($"Utilisateur avec ID {id} non trouvé.");
            }
        }



    }
}