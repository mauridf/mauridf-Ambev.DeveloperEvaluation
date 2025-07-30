using Ambev.DeveloperEvaluation.Application.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Queries
{
    public class GetAllSalesQuery : IRequest<List<SaleDto>> { }
}
