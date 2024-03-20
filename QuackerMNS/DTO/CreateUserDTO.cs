using QuackerMNS.Model;
using System.ComponentModel.DataAnnotations;

namespace QuackerMNS.DTO
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Le prénom est requis")]
        [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 100 caractères")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom de famille est requis")]
        [StringLength(50, ErrorMessage = "Le nom de famille ne peut pas dépasser 100 caractères")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "L'email doit être une adresse email valide")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est requis")]
        [Phone(ErrorMessage = "Le numéro de téléphone doit être valide")]
        public string PhoneNumber { get; set; }

        public string ProfilPicturePath { get; set; }
        public string Description { get; set; }
        public string TokenResetPassword { get; set; }
        public bool IsTemporaryPassword { get; set; }
        public DateTime CreatedTimeUser { get; set; }

        [Required(ErrorMessage = "Le Job doit être renseigné")]
        public int JobTitleId { get; set; }
        public int UserStatutId { get; set; }
        public int UserRoleId { get; set; }
    }
}
