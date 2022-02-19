namespace Common.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public int OwnerId { get; set; }
        public SystemUser Owner { get; set; }
    }
}
