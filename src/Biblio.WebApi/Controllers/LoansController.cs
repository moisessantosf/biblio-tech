using Biblio.Application.Interfaces;
using Biblio.Domain.Entities;
using Biblio.Infrastructure.Messaging;
using Biblio.Infrastructure.Messaging.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IMessagingService _mockMessagingService;

        public LoansController(ILoanService loanService, IMessagingService mockMessagingService)
        {
            _loanService = loanService;
            _mockMessagingService = mockMessagingService;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> CreateLoan(RequestCreateLoanDTO loan)
        {
            try
            {
                var createdLoan = await _loanService.CreateLoan(loan);
                var loanMessage = new LoanMessage
                {
                    BookId = createdLoan.BookId,
                    CustomerId = createdLoan.CustomerId,
                    LoanDate = createdLoan.LoanDate,
                };
                await _mockMessagingService.SendMessageAsync(loanMessage);

                return CreatedAtAction(nameof(GetLoan), new { id = createdLoan.Id }, createdLoan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private object GetLoan()
        {
            throw new NotImplementedException();
        }

        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnLoan(int id)
        {
            try
            {
                var returnedLoan = await _loanService.ReturnLoan(id);

                var returnMessage = new ReturnMessage
                {
                    BookId = returnedLoan.BookId,
                    CustomerId = returnedLoan.CustomerId,
                    ReturnDate = returnedLoan.ReturnDate,
                };
                await _mockMessagingService.SendMessageAsync(returnMessage);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Loan>>> SearchLoans([FromQuery] int? bookId, [FromQuery] int? customerId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var loans = await _loanService.SearchLoans(bookId, customerId, startDate, endDate);
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            try
            {
                var loan = await _loanService.GetLoanById(id);
                if (loan == null)
                {
                    return NotFound();
                }

                return loan;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
