namespace TodoApi.Models
{
    public record TodoDtoBase
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string Status { get; init; }
        public required string Date { get; init; }
        public required string Assignee { get; init; }
        public required string Creator { get; init; }
    }
}