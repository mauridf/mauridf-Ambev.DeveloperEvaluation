using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.UpdateSale
{
    public class UpdateSaleCommand : IRequest<SaleDto>
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string BranchName { get; set; }
        public List<SaleItemInputModel> Items { get; set; }
    }
}
