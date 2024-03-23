namespace QuackerMNS.Model
{
    public class Person
    {
        public int Person_Id { get; set; }
        public string Person_Password { get; set; }
        public string Person_Email { get; set; }
        public string Person_PhoneNumber { get; set; }
        public string Person_FirstName { get; set; }
        public string Person_LastName { get; set; }
        public DateTime Person_CreatedTimeUser { get; set; }
        public string Person_ProfilPicturePath { get; set; }
        public string Person_Description { get; set; }
        public bool Person_IsTemporaryPassword { get; set; }
        public string Person_TokenResetPassword { get; set; }
        public int PersonJobTitle_Id { get; set; }
        public int PersonStatut_Id { get; set; }
        public int PersonRole_Id { get; set; }

        // Clefs étrangères 
        public virtual PersonJobTitle PersonJobTitle { get; set; }
        public virtual PersonStatut PersonStatut { get; set; }
        public virtual PersonRole PersonRole { get; set; }

    }
}
