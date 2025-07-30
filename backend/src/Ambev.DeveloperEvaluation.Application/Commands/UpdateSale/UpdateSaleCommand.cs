using Ambev.DeveloperEvaluation.Application.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.UpdateSale
{
    public class UpdateSaleCommand : IRequest<SaleDto>
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string BranchName { get; set; }
        public List<SaleItemInputModel> Items { get; set; }

        public class SaleItemInputModel
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}
