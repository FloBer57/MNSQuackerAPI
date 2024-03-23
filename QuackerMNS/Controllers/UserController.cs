using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuackerMNS.Model;
using QuackerMNS.DTO.request;
using QuackerMNS.DTO.response;
using System.Data;
using QuackerMNS.infrastructure.Database;

namespace QuackerMNS.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }


        // Create api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreatePersonRequestDTO createUserRequestDTO)
        {
            Users user = new Users
            {
                FirstName = createUserRequestDTO.Firstname,
                LastName = createUserRequestDTO.LastName,
                Email = createUserRequestDTO.Email,
                PhoneNumber = createUserRequestDTO.PhoneNumber,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var messageDto = new CreatePersonResponseDTO(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new { user, message = messageDto.Message });
        }


        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<Users> users = await _context.Users.ToListAsync();
            string message = $"Nombre total d'utilisateurs récupérés : {users.Count}.";
            return Ok(new { Users = users, Message = message });
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            Users? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"Utilisateur avec l'ID {id} non trouvé.");
            }
            
            string message = $"L'utilisateur avec l'ID {id} a été récupéré avec succès.";
            return Ok(new { User = user, Message = message });
        }

        // UPDATE: api/user:change
        [HttpPost("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequestDTO changePasswordDto)
        {
            Users user = await _context.Users.FindAsync(id);
            user.Password = changePasswordDto.NewPassword;

            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}







/* using QuackerMNS.Database;
using QuackerMNS.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using QuackerMNS.DTO.request;
using QuackerMNS.DTO.response;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;

namespace QuackerMNS.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseConnection _sqlConnection;
        private readonly IConfiguration Configuration;
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
            _sqlConnection = new DatabaseConnection(configuration);
        }


        // CREATE 

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequestDTO createUserDto)
        {
            try
            {
                User user = new User
                {
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    Email = createUserDto.Email,
                    PhoneNumber = createUserDto.PhoneNumber,
                };


                user.Password = Security_Service.HashPassword(user.Password ?? PasswordGenerator.GeneratePassword());

                string query = "INSERT INTO users (User_LastName, User_FirstName, User_Email, User_Password, User_PhoneNumber, User_ProfilPicturePath, User_Description, User_TokenResetPassword, User_IsTemporaryPassword, User_CreatedTimeUser, UserJobTitle_Id , UserStatut_Id, UserRole_Id) VALUES (@LastName, @FirstName, @Email, @Password, @PhoneNumber, @ProfilPicturePath, @Description, @TokenResetPassword, @IsTemporaryPassword, @CreatedTimeUser, @UserJobTitleId, @UserStatutId, @UserRoleId)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
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
            {"@UserJobTitleId", user.JobTitleId},
            {"@UserStatutId", user.UserStatutId},
            {"@UserRoleId", user.UserRoleId}
        };

                _sqlConnection.ExecuteNonQuery(query, parameters);

                var responseDto = new CreateUserResponseDTO(user.FirstName, user.LastName);

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de la création de l'utilisateur : " + ex.Message);
            }
        }

        // READ //

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
            string query = "SELECT * FROM users WHERE User_id = @Id";
            var parameters = new Dictionary<string, object>
            {
                {"@Id", id}
            };
            DataTable result = _sqlConnection.ExecuteQuery(query, parameters);

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

        // UPDATE //

        [HttpPost("{id}/change-password")]
        public IActionResult ChangePasswordById(int id, [FromBody] ChangePasswordRequestDTO changePasswordDto)
        {
            string query = "SELECT * FROM users WHERE User_id = @Id";
            var parameters = new Dictionary<string, object>
            {
                {"@Id", id}
            };
            DataTable result = _sqlConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 1)
            {
                string newPasswordHashed = Security_Service.HashPassword(changePasswordDto.NewPassword);

                string updateQuery = "UPDATE users SET User_Password = @Password WHERE User_id = @Id";
                var updateParameters = new Dictionary<string, object>
                {
                    {"@Password", newPasswordHashed},
                    {"@Id", id}
                };

                _sqlConnection.ExecuteNonQuery(updateQuery, updateParameters);

                return Ok("Mot de passe mis à jour avec succès.");
            }
            else
            {
                return NotFound($"Utilisateur avec ID {id} non trouvé.");
            }
        }

        // DELETE

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

        // LOGS //
        private string GenerateJwtToken(User user)
        {
            var keyString = Configuration["JwtConfig:SecretKey"];
            var key = Encoding.ASCII.GetBytes(keyString);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginRequestDto)
        {
            try
            {
                string query = "SELECT * FROM users WHERE User_Email = @Email";
                var parameters = new Dictionary<string, object>
                {
                    {"@Email", loginRequestDto.Email}
                };

                DataTable result = _sqlConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count == 1)
                {
                    DataRow row = result.Rows[0];
                    User user = CreateUserFromDataRow(row);

                    if (Security_Service.VerifyPassword(loginRequestDto.Password, user.Password))
                    {
                        var token = GenerateJwtToken(user);
                        return Ok($"L'utilisateur est bien connecté! Voici son token :" + new { Token = token }); // A modif vois le TOKEN // 
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


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Indiquer au client de supprimer le token stocké avec justement la fonction du dessous ! 
            return Ok("Déconnexion réussie. Veuillez supprimer le token côté client.");
        }

        /* function logout()
        {
            fetch('/api/user/logout', { method: 'POST' })
        .then(response => response.json())
        .then(data => {
            console.log(data.message);  // Message de succès
            localStorage.removeItem('jwtToken');  // Supprimer le token du stockage
            window.location.href = '/login';  // Rediriger l'utilisateur vers la page de connexion
        })
        .catch(error => console.error('Erreur lors de la déconnexion:', error));
        } 

private User CreateUserFromDataRow(DataRow row)
        {
            return new User
            {
                Id = Convert.ToInt32(row["User_Id"]),
                FirstName = Convert.ToString(row["User_Firstname"]),
                LastName = Convert.ToString(row["User_LastName"]),
                Email = Convert.ToString(row["User_Email"]),
                Password = Convert.ToString(row["User_Password"]),
                PhoneNumber = Convert.ToString(row["User_PhoneNumber"]),
                ProfilPicturePath = Convert.ToString(row["User_ProfilPicturePath"]),
                Description = Convert.ToString(row["User_Description"]),
                TokenResetPassword = Convert.ToString(row["User_TokenResetPassword"]),
                IsTemporaryPassword = Convert.ToBoolean(row["User_IsTemporaryPassword"]),
                JobTitleId = Convert.ToInt32(row["UserJobTitle_Id"]), 
                UserStatutId = Convert.ToInt32(row["UserStatut_Id"]),
                UserRoleId = Convert.ToInt32(row["UserRole_Id"])
            };
        }
    }
} */