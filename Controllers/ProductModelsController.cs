using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraftLtd.Data;
using KhumaloCraftLtd.Models;
using KhumaloCraftLtd.Data.Services;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Security.Claims;


namespace KhumaloCraftLtd.Controllers
{
    public class ProductModelsController : Controller
    {
        private readonly IProductsService _ProductService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPurchaseService _PurchaseService;
        //private readonly SearchService _SearchService;

        public ProductModelsController(IProductsService productsService, IWebHostEnvironment webHostEnvironment, IPurchaseService purchaseService)
        {
            _ProductService = productsService;
            _webHostEnvironment = webHostEnvironment;
            _PurchaseService = purchaseService;
            //_SearchService = searchService;
        }

        // GET: ProductModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _ProductService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> MyItems()
        {
            var applicationDbContext = _ProductService.GetAll();
            var prod = applicationDbContext.Where(l => l.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking();

            return View("MyItems", prod);
        }

        // GET: ProductModels/Create
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: ProductModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsVM product)
        {
            if (product.Image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string fileName = product.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.Image.CopyTo(fileStream);
                }

                var productObj = new ProductModel
                {
                    productName = product.productName,
                    Price = product.Price,
                    Category = product.Category,
                    Availability = product.Availability,
                    Description = product.Description,
                    
                    IdentityUserId = product.IdentityUserId,
                    ImagePath = fileName,
                };
                await _ProductService.Add(productObj);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ProductModels/Details/5

        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var productModel = await _ProductService.GetById(id);
                if (productModel == null)
                {
                    return NotFound();
                }

                return View(productModel);
            }
        [HttpPost]
        public async Task<ActionResult> Purchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return View("PurchaseForm", purchase); 
            }

            try
            {
                await _PurchaseService.Add(purchase);

             
                var product = await _ProductService.GetById(purchase.productId.Value);
               

               
                

               
                product.Availability -= purchase.quantity;

                
                await _ProductService.SaveChanges();

                
                return View("Details", product);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View("Error");
                
            }
           
            
        }
        public async Task<ActionResult> AddStock(int id, int Quantity)
        {
            var product = await _ProductService.GetById(id);
           product.Availability += Quantity;
            await _ProductService.SaveChanges();
            return View("Details", product);
        }



       /* public async Task<IActionResult> Search(string query)
        {

            var results = await _SearchService.SearchProducts(query);
            return View();
        }*/



        /*  



           // GET: ProductModels/Edit/5
           public async Task<IActionResult> Edit(int? id)
           {
               if (id == null)
               {
                   return NotFound();
               }

               var productModel = await _context.Products.FindAsync(id);
               if (productModel == null)
               {
                   return NotFound();
               }
               ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", productModel.IdentityUserId);
               return View(productModel);
           }

           // POST: ProductModels/Edit/5
           // To protect from overposting attacks, enable the specific properties you want to bind to.
           // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
           [HttpPost]
           [ValidateAntiForgeryToken]
           public async Task<IActionResult> Edit(int id, [Bind("Id,productName,Price,Category,Availability,Description,ImagePath,IdentityUserId")] ProductModel productModel)
           {
               if (id != productModel.Id)
               {
                   return NotFound();
               }

               if (ModelState.IsValid)
               {
                   try
                   {
                       _context.Update(productModel);
                       await _context.SaveChangesAsync();
                   }
                   catch (DbUpdateConcurrencyException)
                   {
                       if (!ProductModelExists(productModel.Id))
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
               ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", productModel.IdentityUserId);
               return View(productModel);
           }

           // GET: ProductModels/Delete/5
           public async Task<IActionResult> Delete(int? id)
           {
               if (id == null)
               {
                   return NotFound();
               }

               var productModel = await _context.Products
                   .Include(p => p.User)
                   .FirstOrDefaultAsync(m => m.Id == id);
               if (productModel == null)
               {
                   return NotFound();
               }

               return View(productModel);
           }

           // POST: ProductModels/Delete/5
           [HttpPost, ActionName("Delete")]
           [ValidateAntiForgeryToken]
           public async Task<IActionResult> DeleteConfirmed(int id)
           {
               var productModel = await _context.Products.FindAsync(id);
               if (productModel != null)
               {
                   _context.Products.Remove(productModel);
               }

               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
           }

           private bool ProductModelExists(int id)
           {
               return _context.Products.Any(e => e.Id == id);
           }*/
    }
}
