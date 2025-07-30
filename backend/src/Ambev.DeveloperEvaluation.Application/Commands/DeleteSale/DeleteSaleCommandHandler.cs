using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.DeleteSale
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Unit>
    {
        private readonly ISaleRepository _repository;

        public DeleteSaleCommandHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id);
            if (sale == null) throw new Exception("Venda não encontrada");

            await _repository.DeleteAsync(sale);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
