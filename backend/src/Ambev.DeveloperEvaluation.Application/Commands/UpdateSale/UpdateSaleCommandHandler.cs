using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, SaleDto>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSaleCommandHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaleDto> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id);
            if (sale == null) throw new Exception("Venda não encontrada");


            sale.Update(request.ClientName, request.BranchName,
                request.Items.Select(i => new Domain.Entities.SaleItem(i.ProductName, i.Quantity, i.UnitPrice)).ToList());

            await _repository.SaveChangesAsync();

            return _mapper.Map<SaleDto>(sale);
        }
    }
}
