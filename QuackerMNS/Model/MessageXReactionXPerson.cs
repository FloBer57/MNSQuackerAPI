namespace QuackerMNS.Model
{
    public class MessageXReactionXPerson
    {
        public int Person_Id { get; set; }
        public int Message_Id { get; set; }
        public int Reaction_Id { get; set; }
        public DateTime MessageXReactionXPerson_ReactionDate { get; set; }

        // Clefs étrangères
        public virtual Person Person { get; set; }
        public virtual Message Message { get; set; }
        public virtual Reaction Reaction { get; set; }
    }
}
