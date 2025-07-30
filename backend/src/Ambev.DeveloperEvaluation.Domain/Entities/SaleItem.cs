namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total => (UnitPrice * Quantity) - Discount;

        public SaleItem(string productName, int quantity, decimal unitPrice)
        {
            if (quantity <= 0 || quantity > 20)
                throw new ArgumentException("Quantidade deve estar entre 1 e 20.");

            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            Discount = CalculateDiscount(quantity, unitPrice);
        }

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= 10)
                return 0.2m * unitPrice * quantity;
            if (quantity >= 4)
                return 0.1m * unitPrice * quantity;
            return 0m;
        }
    }
}
