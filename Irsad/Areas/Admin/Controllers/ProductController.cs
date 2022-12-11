using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Irsad.Data;
using Irsad.Models.Entities;
using Irsad.Models.ViewModels;
using Irsad.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Irsad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(p => p.Category);
            return View(await appDbContext.ToListAsync());
        }

       
       
        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,ImageUrl,CategoryId,Id,TagIds,Count")] AddProductVM model)
        {
            if (ModelState.IsValid)
            {
                var path = await UploadFileHelper.UploadFile(model.ImageUrl);
                Product product = new()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Count = model.Count,
                    ImageUrl = path,
                    CategoryId = model.CategoryId,
                };
                _context.Add(product);
                await _context.SaveChangesAsync();
                foreach (var tag in model.TagIds)
                {
                    _context.ProductTags.Add(new ProductTag { TagId = tag, ProductId = product.Id });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");
            return View(model);
        }
        public static int? pId=0;
        public static string imgpath = null;
        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            pId = id;
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            imgpath = product.ImageUrl;
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");
            
            List<int> ids=new();
            foreach (var item in _context.ProductTags)
            {
                if (item.ProductId == id)
                {
                    ids.Add(item.TagId);
                }
            }
            var model = new AddProductVM()
            {
                Name = product.Name,
                Price = product.Price,
                Count=product.Count,
                CategoryId = product.CategoryId,
                ImageUrl = null,
                TagIds=ids.ToArray()
            };
            return View(model);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Price,ImageUrl,CategoryId,TagIds,Count")] AddProductVM model,IFormFile file)
        {
            Product product1 = null;
            if (ModelState.ErrorCount <= 1)
            {
                try
                {
                    if (model.ImageUrl == null)
                    {
                        product1 = new Product()
                        {
                            Id = (int)pId,
                            Name = model.Name,
                            Price = model.Price,
                            Count = model.Count,
                            CategoryId = model.CategoryId,
                            ImageUrl = imgpath
                        };

                    }
                    else
                    {
                        product1 = new Product()
                        {
                            Id = (int)pId,
                            Name = model.Name,
                            Price = model.Price,
                            Count = model.Count,
                            CategoryId = model.CategoryId,
                            ImageUrl = await UploadFileHelper.UploadFile(model.ImageUrl)
                        };
                    }
                    _context.Update(product1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists((int)pId))
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
            
            var product = await _context.Products.FindAsync((int)pId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            ViewBag.Tags = new MultiSelectList(_context.Tags, "Id", "Name");
            
            List<int> ids = new();
            foreach (var item in _context.ProductTags)
            {
                if (item.ProductId == (int)pId)
                {
                    ids.Add(item.TagId);
                }
            }
            var model1 = new AddProductVM()
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                CategoryId = product.CategoryId,
                ImageUrl = null,
                TagIds = ids.ToArray()
            };
            return View(model1);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.Id == id);
        }
    }
}
