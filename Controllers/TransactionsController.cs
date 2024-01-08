using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TransactionRecordApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace TransactionRecordApp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionContext _transactionContext;

        public TransactionsController(TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }

        [AllowAnonymous]
        public IActionResult List()
        {
            var transactions = _transactionContext.Transactions
                .Include(o => o.TransactionType)
                .OrderBy(o => o.CompanyName)
                .ToList();

            return View(transactions);
        }
    }
}
