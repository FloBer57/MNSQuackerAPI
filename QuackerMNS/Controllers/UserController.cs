using QuackerMNS.Database;
using QuackerMNS.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using QuackerMNS.DTO;

namespace QuackerMNS.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseConnection _sqlConnection;

        public UserController(IConfiguration configuration)
        {
            _sqlConnection = new DatabaseConnection(configuration);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            string query = "SELECT * FROM users;";
            DataTable result = _sqlConnection.ExecuteQuery(query);
            List<User> users = new List<User>();

            foreach (DataRow row in result.Rows)
            {
                users.Add(CreateUserFromDataRow(row));
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            string query = $"SELECT * FROM users WHERE User_id=@id";
            var parameters = new Dictionary<string, object>
            {
                {"@Id", id}
            };
            DataTable result = _sqlConnection.ExecuteQuery(query,parameters);

            if (result.Rows.Count == 1)
            {
                DataRow row = result.Rows[0];
                User user = CreateUserFromDataRow(row);
                return Ok(user);
            }
            else
            {
                return NotFound($"Utilisateur avec ID {id} non trouvé.");
            }
        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDTO user)
        {
            try
            {
                string query = "INSERT INTO users (User_LastName, User_FirstName, User_Email, User_Password, User_PhoneNumber, User_ProfilPicturePath, User_Description, User_TokenResetPassword, User_IsTemporaryPassword, User_CreatedTimeUser, User_JobTitleId, User_StatusId, User_RoleId) VALUES (@LastName, @FirstName, @Email, @Password, @PhoneNumber, @ProfilPicturePath, @Description, @TokenResetPassword, @IsTemporaryPassword,@CreatedTimeUser, @JobTitleId, @UserStatutId, @UserRoleId)";

                if (string.IsNullOrEmpty(user.Password))
                {
                    return BadRequest("Le mot de passe est requis.");
                }

                user.Password = Security_Service.HashPassword(user.Password);
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


        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                string query = "SELECT * FROM users WHERE User_Email = @Email";
                var parameters = new Dictionary<string, object>
                {
                    {"@Email", user.Email}
                };

                DataTable result = _sqlConnection.ExecuteQuery(query, parameters); 

                if (result.Rows.Count == 1)
                {
                    DataRow row = result.Rows[0];
                    string hashedPassword = Convert.ToString(row["User_Password"]);

                    if (Security_Service.VerifyPassword(user.Password, hashedPassword))
                    {
                        return Ok("Connexion réussie.");
                    }
                    else
                    {
                        return BadRequest("Mot de passe incorrect.");
                    }
                }
                else
                {
                    return NotFound("Utilisateur non trouvé.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de la connexion : " + ex.Message);
            }
        }


        private User CreateUserFromDataRow(DataRow row)
        {
            return new User
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
                JobTitleId = Convert.ToInt32(row["User_jobtitleid"]),  // Assurez-vous que c'est le bon champ pour JobTitleId
                UserStatutId = Convert.ToInt32(row["User_statusid"]),
                UserRoleId = Convert.ToInt32(row["User_roleid"])
            };
        }
    }
}