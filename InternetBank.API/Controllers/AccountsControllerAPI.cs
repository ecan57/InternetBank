using AutoMapper;
using InternetBank.Business.Abstract;
using InternetBank.Core.Utities;
using InternetBank.Data.Abstract;
using InternetBank.Entities.Concrete;
using InternetBank.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InternetBank.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsControllerAPI : ControllerBase
    {
        private IMapper _mapper;
        private IAccountService _accountService;
        private IAccountRepository _accountRepository;

        public AccountsControllerAPI(IMapper mapper, IAccountService accountService, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountService = accountService;
            _accountRepository = accountRepository;
        }
        [HttpPost]
        [Route("yeni_hesap_kaydı")]
        public IActionResult RegisterNewAccount([FromBody] RegisterAccountDTO newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);
            //map
            var account = _mapper.Map<Account>(newAccount);
            return Ok(_accountService.Create(account, newAccount.Password, newAccount.ConfirmPassword));
        }

        [HttpGet]
        [Route("id_göre_hesap_getir")]
        public IActionResult GetAccountById(int Id)
        {
            var account = _accountService.GetById(Id);
            var getAccountDTO = _mapper.Map<GetAccountDTO>(account);
            return Ok(getAccountDTO);
        }

        [HttpGet]

        [Route("bütün_hesapları_getir")]
        public IActionResult GetAllAccounts()
        {
            var allAccounts = _accountService.GetAllAccounts();
            var getCleanedAccounts = _mapper.Map<IList<GetAccountDTO>>(allAccounts);
            return Ok(getCleanedAccounts);
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] Authenticate model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var authResult = _accountService.Authenticate(model.TCNo, model.Password);
            if (authResult == null) return Unauthorized("Geçersiz Kimlik Bilgileri");
            return Ok(authResult);
        }

        [HttpGet]

        [Route("fake_hesapları_getir")]
        public List<Account> GetFakeAccounts()
        {
            return _accountRepository.GetAccounts().ToList();
        }
    }
}
