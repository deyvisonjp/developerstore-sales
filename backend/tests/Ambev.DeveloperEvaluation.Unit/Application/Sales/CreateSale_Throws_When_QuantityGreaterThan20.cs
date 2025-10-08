using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers.Create;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class SalesControllerTests
{
    private class FakeMediator : IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var dto = ((CreateSaleCommand)(object)request).Dto;

            foreach (var item in dto.Items)
            {
                if (item.Quantity > 20)
                    throw new ArgumentException("Não é possível vender acima de 20 itens.");
            }

            var response = new SaleResponseDto
            {
                SaleNumber = dto.SaleNumber,
                Status = "Criado com sucesso",
                TotalAmount = 100
            };
            return Task.FromResult((TResponse)(object)response);
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification => Task.CompletedTask;

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest => throw new NotImplementedException();
        public Task<object?> Send(object request, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }

    [Fact]
    public async Task Create_DeveRetornarCreated()
    {
        var controller = new SalesController(new FakeMediator());

        var dto = new SaleCreateDto
        {
            SaleNumber = "S123",
            CustomerName = "Cliente Teste",
            Items = new List<SaleItemDto> { new() { ProductId = Guid.NewGuid(), Quantity = 5 } }
        };

        var result = await controller.Create(dto, CancellationToken.None);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var response = Assert.IsType<SaleResponseDto>(created.Value);

        Assert.Equal("S123", response.SaleNumber);
    }

    [Fact]
    public async Task Create_DeveRetornarBadRequest_Quando_QuantidadeMaiorQue20()
    {
        var controller = new SalesController(new FakeMediator());

        var dto = new SaleCreateDto
        {
            SaleNumber = "S124",
            CustomerName = "Cliente Teste",
            Items = new List<SaleItemDto> { new() { ProductId = Guid.NewGuid(), Quantity = 25 } }
        };

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => controller.Create(dto, CancellationToken.None));
        Assert.Equal("Excedeu o limite de 20 itens.", ex.Message);
    }
}
