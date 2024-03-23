namespace QuackerMNS.Model
{
    public class PersonXNotification
    {
        public int Person_Id { get; set; }
        public int Notification_Id { get; set; }
        public DateTime PersonXNotification_ReadDate { get; set; }

        // Clefs étrangères 

        public virtual Person Person { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
