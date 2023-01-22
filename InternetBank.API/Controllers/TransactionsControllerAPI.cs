using AutoMapper;
using InternetBank.Business.Abstract;
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
    public class TransactionsControllerAPI : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private ITransactionRepository _transactionRepository;
        private IMapper _mapper;

        public TransactionsControllerAPI(ITransactionService transactionService, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("yeni_işlem_oluştrma")]
        public IActionResult CreateNewTransation([FromBody] TransactionRequestDTO transaction)
        {
            if (!ModelState.IsValid) return BadRequest(transaction);

            var transactionRequest = _mapper.Map<Transaction>(transaction);

            return Ok(_transactionService.CreateNewTransaction(transactionRequest));
        }

        [HttpPost]
        [Route("para_yatırma")]
        public IActionResult Deposit(string tcNo, string accountNumber, decimal amount, string password)
        {
            return Ok(_transactionService.Deposit(tcNo, accountNumber, amount, password));
        }
        [HttpPost]
        [Route("para_transferi")]
        public IActionResult FundsTransfer(string tcNo, string fromAccount, string toAccount, decimal amount, string password)
        {
            if (fromAccount.Equals(toAccount)) return BadRequest("Aynı hesaptan aynı hesaba para transfer edilemez.");

            return Ok(_transactionService.FundsTransfer(tcNo, fromAccount, toAccount, amount, password));
        }

        [HttpPost]
        [Route("para_çekme")]
        public IActionResult Withdraw(string tcNo, string accountNumber, decimal amount, string password)
        {
            //try check validity of accountNumber
            //if (!Regex.IsMatch(accountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("Hesap numarası 11 haneli olmalıdır.");

            return Ok(_transactionService.Withdraw(tcNo, accountNumber, amount, password));

        }

        [HttpGet]

        [Route("işlemleri_getir")]
        public List<Transaction> GetTransactions()
        {
            return _transactionService.GetAllTransactions().ToList();
        }


        [HttpGet]

        [Route("fake_işlemleri_getir")]
        public List<Transaction> GetFakeTransactions()
        {
            return _transactionRepository.GetTransactions().ToList();
        }
    }
}
