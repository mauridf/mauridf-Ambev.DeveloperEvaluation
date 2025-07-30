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
            var sales = await _repository.GetAllAsync();
            return _mapper.Map<List<SaleDto>>(sales);
        }
    }
}
