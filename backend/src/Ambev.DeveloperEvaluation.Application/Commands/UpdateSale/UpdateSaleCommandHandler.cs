using Ambev.DeveloperEvaluation.Application.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Commands.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, SaleDto>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleCommandHandler> _logger;

        public UpdateSaleCommandHandler(ISaleRepository repository, IMapper mapper, ILogger<CreateSaleCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SaleDto> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id);
            if (sale == null) throw new Exception("Venda não encontrada");


            sale.Update(
                request.ClientName,
                request.BranchName,
                request.Items.Select(i => new SaleItem(i.ProductName, i.Quantity, i.UnitPrice)).ToList(),
                _logger
            );

            await _repository.SaveChangesAsync();

            _logger.LogInformation("Evento: SaleModified | Venda atualizada com número: {SaleNumber}, Cliente: {ClientName}, Data: {SaleDate}",
                sale.SaleNumber, sale.ClientName, sale.SaleDate);

            return _mapper.Map<SaleDto>(sale);
        }
    }
}
