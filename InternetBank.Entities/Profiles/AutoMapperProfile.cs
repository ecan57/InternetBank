using AutoMapper;
using InternetBank.Entities.Concrete;
using InternetBank.Entities.DTO;

namespace InternetBank.Entities.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterAccountDTO, Account>();
            CreateMap<UpdateAccountDTO, Account>();
            CreateMap<Account, GetAccountDTO>();
            CreateMap<TransactionRequestDTO, Transaction>();
        }
    }
}
