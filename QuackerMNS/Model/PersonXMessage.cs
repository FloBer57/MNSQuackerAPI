namespace QuackerMNS.Model
{
    public class PersonXMessage
    {
        public int Person_Id { get; set; }
        public int Message_Id { get; set; }
        public DateTime PersonXMessage_ReadDate { get; set; }

        // Clefs étrangères 
        public virtual Person Person { get; set; }
        public virtual Message Message { get; set; }
    }
}
