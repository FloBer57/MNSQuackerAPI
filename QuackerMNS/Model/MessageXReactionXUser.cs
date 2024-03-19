namespace QuackerMNS.Model
{
    public class MessageXReactionXUser
    {
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public int ReactionId { get; set; }
        public DateTime DateReaction { get; set; }
    }
}
