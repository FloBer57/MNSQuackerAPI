using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.Cms;

namespace QuackerMNS.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = PasswordGenerator.GeneratePassword();
        public string PhoneNumber { get; set; }
        public string ProfilPicturePath { get; set; } = "path/to/default/picture"; // A modifier
        public string Description { get; set; } = "Je suis nouveau sur Quacker!"; 
        public string TokenResetPassword { get; set; } = Security_Service.GenerateToken(); // Génération d'un token pour l'envoi de mail
        public bool IsTemporaryPassword { get; set; } = true; // A switch à false quand le token est use
        public DateTime CreatedTimeUser { get; set; } = DateTime.Now;
        public int JobTitleId { get; set; } = 1;
        public int UserStatutId { get; set; } = 1; // Le statut par défault est Hors Ligne 
        public int UserRoleId { get; set; } = 1; // Le role par default est Utilisateur. 
    }
}
