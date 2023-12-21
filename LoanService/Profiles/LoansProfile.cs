using AutoMapper;
using LoanService.Dtos;
using LoanService.Models;

namespace LoanService.Profiles
{
    public class LoansProfile : Profile
    {
        public LoansProfile()
        {
            CreateMap<Loan, LoanReadDto>();
            CreateMap<LoanCreateDto, Loan>();
            CreateMap<LoanUpdateDto, Loan>();
            
        }
    }
}
