using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.RegisterSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.ListSaleItems;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.v1.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.v1.RegisterSale;
using Ambev.DeveloperEvaluation.Application.Sales.v1.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSales;
using Ambev.DeveloperEvaluation.Application.Sales.v1.ListSaleItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.v1;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/sales")]
[ApiVersion("1.0")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers a new sale by processing the provided RegisterSaleRequest.
    /// </summary>
    /// <param name="request">The RegisterSaleRequest containing the data for the new sale to be registered.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An IActionResult representing the outcome of the operation.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<RegisterSaleRequest>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterSale([FromBody] RegisterSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new RegisterSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<RegisterSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<RegisterSaleResponse>
        {
            Success = true,
            Message = "Sale registered successfully",
            Data = _mapper.Map<RegisterSaleResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a sale.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to be deleted.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An IActionResult representing the outcome of the deletion operation.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest(id);
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        });
    }

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="request">The UpdateSaleRequest data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An IActionResult representing the outcome of the update operation.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale updated successfully"
        });
    }

    /// <summary>
    /// List sales
    /// </summary>
    /// <param name="request">The ListSalesRequest data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An paginate list of sales.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListSales([FromQuery] ListSalesRequest request, CancellationToken cancellationToken)
    {
        var validator = new ListSalesRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<ListSalesCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        var responses = _mapper.Map<SalesResponse[]>(response.Sales);

        var paginatedList = new PaginatedList<SalesResponse>(responses.ToList(), response.salesCount, request.Page, request.Size);
        return OkPaginated(paginatedList);
    }

    /// <summary>
    /// List sales items
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An List of sales items.</returns>
    [HttpGet("{id}/items")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListSalesItems([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new ListSaleItemsRequest(id);
        var validator = new ListSaleItemsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<ListSaleItemsCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        var items = _mapper.Map<ListSaleItemsResponse[]>(response.Items);

        return Ok(new ListItemsResponse(items));
    }
}
