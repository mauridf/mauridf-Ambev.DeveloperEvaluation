using Ambev.DeveloperEvaluation.Application.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Queries;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration
{
    public class SalesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SalesController _controller;

        public SalesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();

            // Repository não usado diretamente na controller, pode passar null
            _controller = new SalesController(_mediatorMock.Object, null, _mapperMock.Object);
        }

        [Fact]
        public async Task Create_RetornaCreatedAtActionComObjetoCriado()
        {
            var createCommand = new CreateSaleCommand
            {
                // aqui vem os dados de teste conforme CreateSaleCommand
            };

            var createdDto = new SaleDto
            {
                Id = Guid.NewGuid(),
                SaleNumber = "S001"
                // incluir outros campos
            };

            _mediatorMock.Setup(m => m.Send(createCommand, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdDto);

            var result = await _controller.Create(createCommand);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
            Assert.Equal(createdDto, createdResult.Value);
        }

        [Fact]
        public async Task GetById_VendaExistente_RetornaOkComDto()
        {
            var id = Guid.NewGuid();

            var dto = new SaleDto { Id = id, SaleNumber = "S002" };

            _mediatorMock.Setup(m => m.Send(It.Is<GetSaleByIdQuery>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var result = await _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task GetById_VendaNaoEncontrada_RetornaNotFound()
        {
            var id = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetSaleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SaleDto)null);

            var result = await _controller.GetById(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ChamaMediatorERetornaNoContent()
        {
            var id = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.Is<DeleteSaleCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var result = await _controller.Delete(id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
