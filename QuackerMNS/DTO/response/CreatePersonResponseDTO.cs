using QuackerMNS.Model;

namespace QuackerMNS.DTO.response
{
    public class CreatePersonResponseDTO
    {
        public string Message { get; set; }

        public CreatePersonResponseDTO(Person person)
        {
            Message = $"L'utilisateur {person.Person_FirstName} {person.Person_LastName} a été créé.";
        }
    }
}
