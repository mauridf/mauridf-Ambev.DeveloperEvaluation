using Ambev.DeveloperEvaluation.Application.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Queries
{
    public class GetSaleByIdQuery : IRequest<SaleDto>
    {
        public Guid Id { get; set; }
    }
}
