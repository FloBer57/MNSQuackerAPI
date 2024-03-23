namespace QuackerMNS.Model
{
    public class PersonXLoggedIn
    {
        public int Person_Id { get; set; }
        public int Logged_Id { get; set; }

        // Clefs étrangères
        public virtual Person Person { get; set; }
        public virtual Logged Logged { get; set; }
    }
}
