using MotoGear.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotoGear.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ICartService cartService;

        public ShoppingCartController(ICartService CartService)
        {
            this.cartService = CartService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var model = cartService.GetCartItems(this.HttpContext);
            return View();
        }

        public ActionResult AddToShoppingCart(string Id)
        {
            cartService.AddToShoppingCart(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromShoppingCart(string Id)
        {
            cartService.RemoveFromShoppingCart(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult CartSummary()
        {
            var shoppingCartSummary = cartService.GetCartSummary(this.HttpContext);

            return PartialView(shoppingCartSummary);
        }
    }
}