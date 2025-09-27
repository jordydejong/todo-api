namespace TodoApi.Models
{
    public record TodoDto : TodoDtoBase
    {
        public required int Id { get; init; }
    }
}