using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Shopping_Web.Models;
using Shopping_Web.Models.ViewModels;
using Shopping_Web.Repository;

namespace Shopping_Web.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;
        public CartController(DataContext context)
        {
            _dataContext = context;
        }
        public IActionResult Index()
        {
            List<CartItemModel > cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(c => c.Price * c.Quantity),

            };
            return View(cartVM);
        }
        public IActionResult Checkout()
        {
            return View("~/Views/Checkout/Index.cshtml");
        }
        public async Task<IActionResult> Add(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItems = cart.Where(c =>c.ProductId == Id).FirstOrDefault();
            if(cartItems == null)
            {
                cart.Add(new CartItemModel(product));
            }else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.setJson("Cart", cart);
            TempData["success"] = "Add item to cart successfully";
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Descrease(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if(cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
            }
            else
            {
                cart.RemoveAll(p =>p.ProductId==Id);
            }
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.setJson("Cart", cart);
            }
            TempData["success"] = "Descrease item to cart successfully";

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity += 1;
            }
           
            HttpContext.Session.setJson("Cart", cart);
            TempData["success"] = "Increase item to cart successfully";


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            
            if (cart !=null)
            {
                cart.RemoveAll(item => item.ProductId == Id);
                HttpContext.Session.setJson("Cart", cart);
            }
            TempData["success"] = "Remove item to cart successfully";

            return RedirectToAction("Index");
        }
    }
}
