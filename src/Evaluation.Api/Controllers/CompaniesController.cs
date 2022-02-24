using Evaluation.Api.Controllers.Base;
using Evaluation.Api.Handlers.Queries;
using Evaluation.Api.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Api.Controllers;

[Route("v1/companies")]
[ApiController]
public class CompaniesController : EvaluationApiControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get a Company by the Id.
    /// </summary>
    /// <param name="id">The Company's Id.</param>
    /// <returns>The Company details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Company))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var company = await _mediator.Send(new GetCompanyQuery(id));

        if (company == null)
        {
            return ResourceNotFound(id);
        }

        return Ok(company);
    }
}
