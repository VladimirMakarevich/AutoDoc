namespace AutoDoc.DAL.Entities
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string MessageJson { get; set; }
        public int Type { get; set; }

        public virtual Document Document { get; set; }
    }
}
