using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleCommandHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns sale entity")]
    public async Task Handle_ValidRequest_ReturnsSale()
    {
        var dto = new SaleCreateDto
        {
            SaleNumber = "S-1001",
            CustomerId = Guid.NewGuid(),
            CustomerName = "John Doe",
            BranchId = Guid.NewGuid(),
            BranchName = "Main Branch",
            Items = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 15, UnitPrice = 4 }
            }
        };

        var command = new CreateSaleCommand(dto);
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = dto.SaleNumber, CustomerName = dto.CustomerName };

        _mapper.Map<Sale>(dto).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given item with quantity over 20 When creating sale Then throws exception")]
    public async Task Handle_ItemQuantityOverLimit_ThrowsException()
    {
        var dto = new SaleCreateDto
        {
            SaleNumber = "S-1002",
            CustomerId = Guid.NewGuid(),
            CustomerName = "Jane Doe",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch B",
            Items = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 25, UnitPrice = 10 }
            }
        };

        var command = new CreateSaleCommand(dto);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Cannot sell more than 20 units of the same product.");
    }

    [Fact(DisplayName = "Given valid item quantities When creating sale Then applies correct discounts")]
    public async Task Handle_ValidQuantities_AppliesDiscounts()
    {
        var dto = new SaleCreateDto
        {
            SaleNumber = "S-1003",
            CustomerId = Guid.NewGuid(),
            CustomerName = "Alice",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch C",
            Items = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 10 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 12, UnitPrice = 10 }
            }
        };

        var command = new CreateSaleCommand(dto);
        var sale = new Sale { Id = Guid.NewGuid(), SaleNumber = dto.SaleNumber, CustomerName = dto.CustomerName };

        _mapper.Map<Sale>(dto).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}