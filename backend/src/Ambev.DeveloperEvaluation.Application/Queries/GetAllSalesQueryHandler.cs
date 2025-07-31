using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Queries
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<SaleDto>>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSalesQueryHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.Query();

            if (!string.IsNullOrEmpty(request.ClientName))
                query = query.Where(s => s.ClientName.Contains(request.ClientName));

            if (request.StartDate.HasValue)
                query = query.Where(s => s.SaleDate >= request.StartDate.Value);

            if (request.EndDate.HasValue)
                query = query.Where(s => s.SaleDate <= request.EndDate.Value);

            query = request.OrderBy?.ToLower() switch
            {
                "clientname" => request.Desc ? query.OrderByDescending(s => s.ClientName) : query.OrderBy(s => s.ClientName),
                "salenumber" => request.Desc ? query.OrderByDescending(s => s.SaleNumber) : query.OrderBy(s => s.SaleNumber),
                _ => request.Desc ? query.OrderByDescending(s => s.SaleDate) : query.OrderBy(s => s.SaleDate)
            };

            query = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var sales = await query.ToListAsync(cancellationToken);
            return _mapper.Map<List<SaleDto>>(sales);
        }
    }
}
