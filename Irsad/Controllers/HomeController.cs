using Irsad.Data;
using Irsad.Models;
using Irsad.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Diagnostics;
using X.PagedList;

namespace Irsad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger,AppDbContext context,UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult GetAll()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View(_context.Products.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Search(string? pattern)
        {
            var list = await _context.Products.Where(p => p.Name.Contains(pattern)).ToListAsync();
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Add(int? id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product != null && product.Count>0)
            {
                product.Count--;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                var ord = new UserProduct()
                {
                    ProductId = (int)id,
                    UserId = _userManager.GetUserId(HttpContext.User)
                };
                _context.UserProducts.Add(ord);
                await _context.SaveChangesAsync();
                return RedirectToAction("Order");
            }
            return RedirectToAction("GetAll");
        }
        [Authorize]
        public async Task<IActionResult> Remove(int? id)
        {
            var prd = (await _context.Products.FirstOrDefaultAsync(p => p.Id == id));
            if (prd != null )
            {
                
                prd.Count++;
                _context.Products.Update(prd);
                await _context.SaveChangesAsync();
                var ord = await _context.UserProducts.FirstOrDefaultAsync(up => up.ProductId == (int)id);
                 _context.UserProducts.Remove(ord);
                await _context.SaveChangesAsync();
                return RedirectToAction("Order");
            }
            return RedirectToAction("GetAll");
        }
        public async Task<IActionResult> SearchTag(int? id)
        {
            var list = await _context.ProductTags.Include(pt => pt.Product).Where(pt => pt.TagId == id).Select(pt => pt.Product).ToListAsync();

            return View(list);
        }
        [Authorize]
        public IActionResult Order()
        {
            var lst = _context.UserProducts.Include(p => p.User).Where(u => u.UserId == _userManager.GetUserId(HttpContext.User)).Select(p => p.Product).ToList();
            double total = 0;
            foreach (var item in lst)
            {
                total+=item.Price;
            }
            ViewBag.Total = total;
            return View(lst);
        }
        public IActionResult Buy()
        {
            var lst = _context.UserProducts.Where(u => u.UserId == _userManager.GetUserId(HttpContext.User));
            foreach (var item in lst)
            {
                _context.UserProducts.Remove(item);
            }
            _context.SaveChanges();
            return RedirectToAction("Order");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}