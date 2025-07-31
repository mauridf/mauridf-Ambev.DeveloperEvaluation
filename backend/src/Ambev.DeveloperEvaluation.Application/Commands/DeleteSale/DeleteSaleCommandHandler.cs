using Ambev.DeveloperEvaluation.Application.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Commands.DeleteSale
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Unit>
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<CreateSaleCommandHandler> _logger;

        public DeleteSaleCommandHandler(ISaleRepository repository, ILogger<CreateSaleCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id);
            if (sale == null) throw new Exception("Venda não encontrada");

            await _repository.DeleteAsync(sale);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Evento: SaleCancelled | Venda cancelada com ID: {SaleId}, Número: {SaleNumber}",
                sale.Id, sale.SaleNumber);

            return Unit.Value;
        }
    }
}
