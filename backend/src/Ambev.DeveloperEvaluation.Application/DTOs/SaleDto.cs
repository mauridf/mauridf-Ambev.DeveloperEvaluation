namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string ClientName { get; set; }
        public string BranchName { get; set; }
        public string Status { get; set; }
        public decimal TotalValue { get; set; }
        public List<SaleItemDto> Items { get; set; }

        public class SaleItemDto
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Discount { get; set; }
            public decimal Total { get; set; }
        }
    }
}
