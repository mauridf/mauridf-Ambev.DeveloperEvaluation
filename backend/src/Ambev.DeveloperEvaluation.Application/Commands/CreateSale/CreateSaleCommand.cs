

using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.CreateSale
{
    public class CreateSaleCommand : IRequest<SaleDto>
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string ClientName { get; set; }
        public string BranchName { get; set; }
        public List<SaleItemInputModel> Items { get; set; }
    }
}
