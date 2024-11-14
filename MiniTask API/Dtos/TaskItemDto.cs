namespace MiniTask_API.Dtos
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
    }


}
