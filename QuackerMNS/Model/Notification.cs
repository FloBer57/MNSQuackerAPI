namespace QuackerMNS.Model
{
    public class Notification
    {
        public int Notification_Id { get; set; }
        public string Notification_Name { get; set; }
        public string Notification_Text { get; set; }
        public DateTime Notification_DatePost { get; set; }

        // Clefs étrangères 
        public int NotificationType_Id { get; set; }
    }
}
