namespace QuackerMNS.Model
{
    public class Message
    {
        public int Message_Id { get; set; }
        public string Message_Text { get; set; }
        public DateTime Message_Date { get; set; }
        public bool Message_IsNotArchived { get; set; }
        public int Channel_Id { get; set; }
        public int Person_Id { get; set; }

        // Clefs étrangères
        public virtual Channel Channel { get; set; }
        public virtual Person Person { get; set; }
    }
}
