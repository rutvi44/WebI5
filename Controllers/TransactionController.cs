using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using TransactionRecordApp.Models;

namespace TransactionRecordApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionContext _transactionContext;

        public TransactionController (TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.TransactionTypes = _transactionContext.TransactionType.OrderBy(t => t.Name).ToList();
            return View("Edit", new Transaction());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _transactionContext.Transactions.Add(transaction);
                _transactionContext.SaveChanges();
                return RedirectToAction("List", "Transactions");
            }
            else
            {
                ViewBag.Action = "Add";
                ViewBag.TransactionTypes = _transactionContext.TransactionType.OrderBy(t => t.Name).ToList();
                return View("Edit", transaction);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var transaction = _transactionContext.Transactions.Find(id);
            ViewBag.TransactionTypes = _transactionContext.TransactionType.OrderBy(t => t.Name).ToList();
            return View(transaction);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _transactionContext.Transactions.Update(transaction);
                _transactionContext.SaveChanges();
                return RedirectToAction("List", "Transactions");
            }
            else
            {
                ViewBag.Action = "Edit";
                ViewBag.TransactionTypes = _transactionContext.TransactionType.OrderBy(t => t.Name).ToList();
                return View(transaction);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var transaction = _transactionContext.Transactions.Find(id);
            return View(transaction);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Transaction transaction)
        {
            _transactionContext.Transactions.Remove(transaction);
            _transactionContext.SaveChanges();
            return RedirectToAction("List", "Transactions");
        }
    }
}
