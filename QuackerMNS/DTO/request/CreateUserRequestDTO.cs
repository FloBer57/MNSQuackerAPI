using QuackerMNS.Model;
using System.ComponentModel.DataAnnotations;

namespace QuackerMNS.DTO.request
{
    public class CreateUserRequestDTO
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

        [Required(ErrorMessage = "Le numéro de téléphone est requis")]
        [Phone(ErrorMessage = "Le numéro de téléphone doit être valide")]
        public string PhoneNumber { get; set; }


        // [Required(ErrorMessage = "Le Job doit être renseigné")]
        // public int JobTitleId { get; set; }
    }
}
