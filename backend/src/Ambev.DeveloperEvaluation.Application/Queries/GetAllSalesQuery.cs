using Ambev.DeveloperEvaluation.Application.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Queries
{
    public record GetAllSalesQuery(
        string? ClientName,
        DateTime? StartDate,
        DateTime? EndDate,
        string? OrderBy = "SaleDate",
        bool Desc = false,
        int Page = 1,
        int PageSize = 10
    ) : IRequest<List<SaleDto>>;
}
