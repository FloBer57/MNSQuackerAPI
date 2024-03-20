namespace QuackerMNS.DTO.response
{
    public class CreateUserResponseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }

        // Constructeur pour initialiser le message lors de la création de l'objet
        public CreateUserResponseDTO(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Message = $"L'utilisateur {FirstName} {LastName} a été créé.";
        }
    }
}
