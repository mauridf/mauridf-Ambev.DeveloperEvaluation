

using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleDto>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public CreateSaleCommandHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaleDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var items = request.Items.Select(i => new SaleItem(i.ProductName, i.Quantity, i.UnitPrice)).ToList();
            var sale = new Sale(request.SaleNumber, request.SaleDate, request.ClientName, request.BranchName, items);

            await _repository.AddAsync(sale);

            return _mapper.Map<SaleDto>(sale);
        }
    }
}
