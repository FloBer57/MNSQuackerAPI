namespace QuackerMNS.Model
{
    public class PersonXChannel
    {
        public int Person_Id { get; set; }
        public int Channel_Id { get; set; }
        public DateTime PersonXChannel_SignInDate { get; set; }

        // Clefs étrangères 
        public virtual Person Person { get; set; }
        public virtual Message Message { get; set; }
    }
}
