namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class CreateSaleDto
    {
        public string Number { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Client { get; set; } = null!;
        public string Branch { get; set; } = null!;
        public List<CreateSaleItemDto> Items { get; set; } = new();
    }
}
