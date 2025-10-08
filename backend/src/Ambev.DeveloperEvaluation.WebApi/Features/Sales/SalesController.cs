using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers.Create;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers.Delete;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller responsável pelas operações relacionadas a vendas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova venda.
        /// </summary>
        /// <param name="dto">Objeto contendo os dados da venda.</param>
        /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
        /// <returns>Retorna o objeto da venda criada com status 201 Created.</returns>
        [HttpPost]
        public async Task<ActionResult<SaleResponseDto>> Create([FromBody] SaleCreateDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateSaleCommand(dto);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Create), new { saleNumber = result.SaleNumber }, result);
        }

        /// <summary>
        /// Obtém uma venda pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da venda (GUID).</param>
        /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
        /// <returns>Retorna a venda encontrada ou 404 caso não exista.</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSaleQuery(id), cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Retorna todas as vendas cadastradas.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
        /// <returns>Lista de vendas com status 200 OK.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllSalesQuery(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Deleta uma venda pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da venda (GUID).</param>
        /// <param name="cancellationToken">Token para cancelamento da requisição.</param>
        /// <returns>Status 204 NoContent se deletado, 404 NotFound caso não exista.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var success = await _mediator.Send(new DeleteSaleCommand(id), cancellationToken);
            return success ? NoContent() : NotFound();
        }
    }
}
