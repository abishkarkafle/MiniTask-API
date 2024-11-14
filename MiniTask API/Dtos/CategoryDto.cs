namespace MiniTask_API.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskItemDto> Tasks { get; set; } = new List<TaskItemDto>();
    }


}
