namespace QuackerMNS.Model
{
    public class Attachment
    {
        public int Attachment_Id { get; set; }
        public string Attachment_Name { get; set; }
        public string Attachment_Attachment { get; set; }
        public int Message_Id { get; set; }
        public virtual Message Message { get; set; }
    }
}
