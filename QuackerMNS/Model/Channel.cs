namespace QuackerMNS.Model
{
    public class Channel
    {
        public int Channel_Id { get; set; }
        public string Channel_Name { get; set; }
        public string Channel_ImagePath { get; set; }
        public int ChannelType_Id { get; set; }

        // Clefs étrangères 
        public virtual ChannelType ChannelType { get; set; }
    }
}
