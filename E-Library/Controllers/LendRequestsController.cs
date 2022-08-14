using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Library.Models;
using Microsoft.AspNetCore.Http;

namespace E_Library.Controllers
{
    public class LendRequestsController : Controller
    {
        private readonly LMsSystemContext _context;
        public readonly IAccount _account;

        public LendRequestsController(LMsSystemContext context, IAccount account)
        {
            _context = context;
            _account = account;
        }

        // GET: LendRequests
        public async Task<IActionResult> Index()
        {
            var lMsSystemContext = _context.LendRequests.Include(l => l.Book).Include(l => l.User);
            return View(await lMsSystemContext.ToListAsync());
        }

        public ViewResult RequestToLend(int bookid)
        {
            
            var username = HttpContext.Session.GetString("UserName");
           
                var user = _account.getuserByname(username);

                LendRequest lendRequest = new LendRequest()
                {
                    LendStatus = "Requested",
                    LendDate = System.DateTime.Now,
                    BookId = bookid,
                    UserId = user.UserId,
                    Book = _context.Books.SingleOrDefault(b => b.BookId == bookid),
                    User = _context.Accounts.SingleOrDefault(u => u.UserId == user.UserId),

                };
                
                _context.LendRequests.Add(lendRequest);
                _context.SaveChanges();
            
            return View("Requested");
        }

        public ViewResult Approval(int lendid)
        {
            var lendedBook = _context.LendRequests.FirstOrDefault(b => b.LenId == lendid);
            lendedBook.LendStatus = "Approved";
            //_context.LendRequests.FirstOrDefault(b => b.LenId == lendid).LendStatus = "Approved";
            _context.LendRequests.FirstOrDefault(b => b.LenId == lendid).ReturnDate = _context.LendRequests.FirstOrDefault(b => b.LenId == lendid).LendDate.AddDays(14); //to add return date  for the user lent book at the time of admin approval . 
            _context.Books.SingleOrDefault(b => b.BookId == lendedBook.BookId).NoOfCopies--;
            _context.SaveChanges();
            return View("Approved");

            //return RedirectToAction("AdminDash", "LendRequests");

        }

        public ViewResult Reject(int lendid)
        {
            var lendedBook = _context.LendRequests.FirstOrDefault(b => b.LenId == lendid);
            lendedBook.LendStatus = "Rejected";
            //_context.LendRequests.FirstOrDefault(b => b.LenId == lendid).LendStatus = "Rejected";
            _context.Books.SingleOrDefault(b => b.BookId == lendedBook.BookId).NoOfCopies++;
            _context.SaveChanges();
            return View("Rejected");

        }


        public ActionResult ReturnBook(int id)
        {
            var lendedBook = _context.LendRequests.FirstOrDefault(b => b.LenId == id);
            lendedBook.LendStatus = "Returned";
            //_context.LendRequests.FirstOrDefault(b => b.LenId == lendid).LendStatus = "Returned";
            TimeSpan t = System.DateTime.Now - lendedBook.ReturnDate;
            lendedBook.FineAmount = t.Days - 14 > 0 ? (t.Days - 14) * 20 : 0;
            _context.SaveChanges();
            //return View("ReturnBook")
            return RedirectToAction("AllRequests", "LendRequests");

        }







        public async Task<IActionResult> AllRequests()
        {

            //var lMsSystemContext = _context.LendRequests.Include(l => l.Book).Include(l => l.User);
            //return View(await lMsSystemContext.ToListAsync());

            var username = HttpContext.Session.GetString("UserName");
            var user = _account.getuserByname(username);
            var User = _context.LendRequests.Where(u => u.UserId == user.UserId).Include(l =>l.Book).Include(l =>l.User);
            return View(await User.ToListAsync());


        }

        public async Task<IActionResult> IssuedRecords()
        {
            var username = HttpContext.Session.GetString("UserName");
            var user = _account.getuserByname(username);
            var User = _context.LendRequests.Where(u => u.UserId == user.UserId && u.LendStatus.Equals("Approved")).Include(l => l.Book).Include(l =>l.User);

            return View(await User.ToListAsync());
        }

        //public async Task<IActionResult> ReturnBook()
        //{
        //    var username = HttpContext.Session.GetString("UserName");
        //    var user = _account.getuserByname(username);
        //    var User = _context.LendRequests.Where(u => u.UserId == user.UserId && u.LendStatus.Equals("Approved")).Include(l => l.Book).Include(l => l.User);

        //    return View(await User.ToListAsync());
        //}




        // GET: LendRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendRequest = await _context.LendRequests
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LenId == id);
            if (lendRequest == null)
            {
                return NotFound();
            }

            return View(lendRequest);
        }

        // GET: LendRequests/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password");
            return View();
        }

        // POST: LendRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LenId,LendStatus,LendDate,ReturnDate,UserId,BookId,FineAmount")] LendRequest lendRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lendRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendRequest.BookId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password", lendRequest.UserId);
            return View(lendRequest);
        }
        public ViewResult AdminDash()
        {
            var viewdetails = _context.LendRequests.Where(l => l.LendStatus == "Requested").Include(l => l.Book).Include(l => l.User);
 
            return View(viewdetails.ToList());
        }

        // GET: LendRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendRequest = await _context.LendRequests.FindAsync(id);
            if (lendRequest == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendRequest.BookId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password", lendRequest.UserId);
            return View(lendRequest);
        }

        // POST: LendRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LenId,LendStatus,LendDate,ReturnDate,UserId,BookId,FineAmount")] LendRequest lendRequest)
        {
            if (id != lendRequest.LenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lendRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LendRequestExists(lendRequest.LenId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", lendRequest.BookId);
            ViewData["UserId"] = new SelectList(_context.Accounts, "UserId", "Password", lendRequest.UserId);
            return View(lendRequest);
        }

        // GET: LendRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendRequest = await _context.LendRequests
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LenId == id);
            if (lendRequest == null)
            {
                return NotFound();
            }

            return View(lendRequest);
        }

        // POST: LendRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lendRequest = await _context.LendRequests.FindAsync(id);
            _context.LendRequests.Remove(lendRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LendRequestExists(int id)
        {
            return _context.LendRequests.Any(e => e.LenId == id);
        }
    }
}
