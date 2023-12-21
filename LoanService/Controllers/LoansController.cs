using Microsoft.AspNetCore.Mvc;
using LoanService.Data;
using LoanService.Models;
using AutoMapper;
using LoanService.Dtos;
using LoanService.SyncDataServices.Http;

namespace LoanService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepo _repo;
        private readonly IMapper _mapper;
        private readonly IBookDataClient _bookDataClient;

        public LoansController(ILoanRepo repo, IMapper mapper, IBookDataClient bookDataClient)
        {
            _repo = repo;
            _mapper = mapper;
            _bookDataClient = bookDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LoanReadDto>> GetLoans()
        {
            Console.WriteLine("--> Getting all loans ");
            return Ok(_mapper.Map<IEnumerable<LoanReadDto>>(_repo.GetAllLoans()));
        }

        [HttpGet("{id}", Name = "GetLoanByID")]
        public ActionResult<LoanReadDto> GetLoanByID(int id)
        {
            var loan = _repo.GetLoanByID(id);
            return loan == null ? NotFound() : Ok(_mapper.Map<LoanReadDto>(loan));
        }

        public ActionResult<IEnumerable<LoanReadDto>> GetLoansByUserID(int userId)
        {
            var loan = _repo.GetLoansByUser(userId);
            return loan == null ? NotFound() : Ok(_mapper.Map<IEnumerable<LoanReadDto>>(loan));
        }

        [HttpPost]
        public async Task<ActionResult<LoanReadDto>> AddLoan(LoanCreateDto loanDto) 
        {
            
            try
            {
                var loanModel = _mapper.Map<Loan>(loanDto);
                int availableCount = await _bookDataClient.GetBookCountAsync(loanModel.BookId);

                if (availableCount <= 0) 
                {
                    return Conflict("There are no more available copies of this book.");
                }
                await _bookDataClient.UpdateBookCount(loanModel.BookId, --availableCount);
                _repo.AddLoan(loanModel);
                _repo.SaveChanges();
                

                var loanReadDto = _mapper.Map<LoanReadDto>(loanModel);
                return CreatedAtRoute(nameof(GetLoanByID), new { id = loanReadDto.LoanId }, loanReadDto);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }      
        }
        [HttpPut("{loanId}")]
        public IActionResult UpdateLoan(int loanId, LoanUpdateDto loanDto)
        {
            if (loanId != loanDto.LoanId)
            {
                return BadRequest("Mismatched Loan ID.");
            }

            try
            {
                _repo.UpdateLoan(loanDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }


}
