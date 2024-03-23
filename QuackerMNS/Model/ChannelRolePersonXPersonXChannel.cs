namespace QuackerMNS.Model
{
    public class ChannelRolePersonXPersonXChannel
    {
        public int Person_Id { get; set; }
        public int Channel_Id { get; set; }
        public DateTime ChannelPersonRoleXPersonsXChannel_AffectDate { get; set; }
        public int ChannelPersonRole_Id { get; set; }

        // Clefs étrangères
        public virtual Person Person { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual ChannelRolePerson ChannelRoleRole { get; set; }
    }

}
