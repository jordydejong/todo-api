namespace TodoApi.Models
{
    public record PatchTodoDto
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Status { get; init; }
        public string? Date { get; init; }
        public string? Assignee { get; init; }
        public string? Creator { get; init; }
    }
}