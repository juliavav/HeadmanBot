namespace HeadmanBot.Data.Entities
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Group Group { get; set; }
    }
}