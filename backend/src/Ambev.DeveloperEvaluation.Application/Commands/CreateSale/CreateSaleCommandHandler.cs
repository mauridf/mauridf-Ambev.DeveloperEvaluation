

using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, SaleDto>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleCommandHandler> _logger;

        public CreateSaleCommandHandler(ISaleRepository repository, IMapper mapper, ILogger<CreateSaleCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SaleDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var items = request.Items.Select(i => new SaleItem(
                    i.ProductName, 
                    i.Quantity, 
                    i.UnitPrice
                )).ToList();

            var utcSaleDate = DateTime.SpecifyKind(request.SaleDate, DateTimeKind.Utc);

            var sale = new Sale(
                    request.SaleNumber, 
                    request.SaleDate, 
                    request.ClientName, 
                    request.BranchName, items
                );

            await _repository.AddAsync(sale);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Evento: SaleCreated | Venda criada com número: {SaleNumber}, Cliente: {ClientName}, Data: {SaleDate}",
                request.SaleNumber, request.ClientName, request.SaleDate);

            return _mapper.Map<SaleDto>(sale);
        }
    }
}
