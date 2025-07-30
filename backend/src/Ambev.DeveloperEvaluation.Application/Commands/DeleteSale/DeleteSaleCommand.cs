using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.DeleteSale
{
    public class DeleteSaleCommand : IRequest<Unit>
    {
        public Guid Id { get; }
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
