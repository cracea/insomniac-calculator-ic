using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Insomniac.Calculator.Web.Models;
using Insomniac.Calculator.Web.Services.Abstractions;
using Insomniac.Calculator.Web.Services.Models;

namespace Insomniac.Calculator.Web.Controllers
{
    public class CalculatorController(ICalculatorHttpService calculatorHttpService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vm = await GetCalculatorViewModelAsync();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CalculateRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var vm = await GetCalculatorViewModelAsync(request);
                return View(vm);
            }

            try
            {
                var result = await calculatorHttpService.CalculateTaxAsync(new CalculateRequest
                {
                    PostalCode = request.PostalCode,
                    Income = request.Income
                });

                ViewBag.CalculationResult = result.Tax;
            }
            catch (HttpRequestException)
            {
                ViewBag.CalculationError = "Failed to connect to the server. Please try again later.";
            }
            catch (Exception)
            {
                ViewBag.CalculationError = "An unexpected error occurred. Please contact support.";
            }

            var vmWithErrors = await GetCalculatorViewModelAsync(request);
            return View(vmWithErrors);
        }

        public IActionResult History()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> HistoryTableData([FromQuery] int start = 0, [FromQuery] int length = 10)
        {
            if (start < 0)
            {
                return BadRequest(new { Error = "The 'start' parameter must be greater than or equal to 0" });
            }

            if (length <= 0)
            {
                return BadRequest(new { Error = "The 'length' parameter must be greater than 0." });
            }

            var paginatedResult = await calculatorHttpService.GetHistoryAsync(start, length);

            return Json(new
            {
                recordsTotal = paginatedResult?.TotalCount ?? 0,
                recordsFiltered = paginatedResult?.TotalCount ?? 0,
                data = paginatedResult?.Items ?? []
            });
        }

        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
        {
            var postalCodes = await calculatorHttpService.GetPostalCodesAsync();
            return new CalculatorViewModel
            {
                PostalCodes = new SelectList(postalCodes, "Code", "Code"),
                Income = request?.Income ?? 0,
                PostalCode = request?.PostalCode ?? string.Empty
            };
        }
    }
}