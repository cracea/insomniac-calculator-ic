using MediatR;
using Microsoft.AspNetCore.Mvc;

using Insomniac.Calculator.API.Models;
using Insomniac.Calculator.Services.Abstractions.Repository;
using Insomniac.Calculator.Services.Mediators;

namespace Insomniac.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class CalculatorController(
        IHistoryDataService historyService,
        IPostalCodeDataService postalCodeService,
        IMediator mediator,
        ILogger<CalculatorController> logger) : ControllerBase
    {
        private const int _historyTablePageLength = 10;

        private readonly IHistoryDataService _historyService = historyService;
        private readonly IPostalCodeDataService _postalCodeService = postalCodeService;
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<CalculatorController> _logger = logger;

        [HttpPost("calculate-tax")]
        public async Task<ActionResult<CalculateResultDto>> Calculate(CalculateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a mediator command
            var command = new CalculateTaxCommand(
                request.PostalCode,
                request.Income
            );

            // Handler the mediator command
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError("Calculator mediator handler returned invalid response");
                return NotFound("Calculation could not be completed.");
            }

            return Ok(new CalculateResultDto(result));
        }

        [HttpGet("history")]
        public async Task<ActionResult<PaginatedResultDto<CalculatorHistoryDto>>> History([FromQuery] int skip = 0, [FromQuery] int take = _historyTablePageLength)
        {
            if (skip < 0)
                skip = 0;

            if (take <= 0)
                take = _historyTablePageLength;

            var history = await _historyService.GetPaginatedHistoryAsync(skip, take);

            return Ok(new PaginatedResultDto<CalculatorHistoryDto>()
            {
                Items = history.Items.Select(i => new CalculatorHistoryDto(i)).ToList(),
                TotalCount = history.TotalCount,
                Page = history.Page,
                PageSize = history.PageSize
            });
        }

        [HttpGet("postal-codes")]
        public async Task<ActionResult<List<PostalCodeDto>>> PostalCodes()
        {
            var postalCodes = await _postalCodeService.GetPostalCodesAsync();

            return Ok(postalCodes.Select(pc => new PostalCodeDto(pc)).ToList());
        }
    }
}