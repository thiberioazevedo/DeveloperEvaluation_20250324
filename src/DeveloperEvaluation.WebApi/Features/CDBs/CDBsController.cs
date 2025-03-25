using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DeveloperEvaluation.WebApi.Common;
using DeveloperEvaluation.WebApi.Features.CDBs.CreateCDB;
using DeveloperEvaluation.WebApi.Features.CDBs.GetCDB;
using DeveloperEvaluation.WebApi.Features.CDBs.DeleteCDB;
using DeveloperEvaluation.Application.CDBs.CreateCDB;
using DeveloperEvaluation.Application.CDBs.GetCDB;
using DeveloperEvaluation.Application.CDBs.DeleteCDB;
using DeveloperEvaluation.WebApi.Features.CDBs.ListCDBs;
using DeveloperEvaluation.Application.CDBs.ListCDBs;
using DeveloperEvaluation.Domain.Repositories;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using DeveloperEvaluation.Common.MessageBroker.Services;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.WebApi.MessageBroker.Services;
using MassTransit;

namespace DeveloperEvaluation.WebApi.Features.CDBs;

[ApiController]
[Route("api/[controller]")]
public class CDBsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IMessageBrokerPublisher _messageBrokerPublisher;

    public CDBsController(IMediator mediator, IMapper mapper, IMessageBrokerPublisher messageBrokerPublisher)
    {
        _mediator = mediator;
        _mapper = mapper;
        _messageBrokerPublisher = messageBrokerPublisher;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCDBResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> CreateCDB([FromBody] CreateCDBRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCDBRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        await _messageBrokerPublisher.PublishMessageAsync(request, RabbitMQQueues.CreateCDBQueue);

        return Created(string.Empty, new ApiResponseWithData<CreateCDBResponse>
        {
            Success = true,
            Message = "Create CDB sent to queue successfully."
        });

        /*
        var command = _mapper.Map<CreateCDBCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return Created(string.Empty, new ApiResponseWithData<CreateCDBResponse>
        {
            Success = true,
            Message = "CDB created successfully",
            Data = _mapper.Map<CreateCDBResponse>(response)
        });
        */
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCDBResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetCDB([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCDBRequest { Id = id };
        var validator = new GetCDBRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCDBCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<GetCDBResponse>(response));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<ListCDBsCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> ListCDBs([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? searchText, [FromQuery] string? columnOrder, [FromQuery] bool asc, CancellationToken cancellationToken)
    {
        var request = new ListCDBsRequest { PageNumber = pageNumber, PageSize = pageSize, SearchText = searchText, ColumnOrder = columnOrder, Asc = asc };
        var validator = new ListCDBsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<ListCDBsCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        var paginatedList = _mapper.Map<PaginatedList<CDBResponse>>(response);
        return Ok(paginatedList);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> DeleteCDB([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCDBRequest { Id = id };
        var validator = new DeleteCDBRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCDBCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok("CDB deleted successfully");
    }
}
