using MotoGear.Core.Contracts;
using MotoGear.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotoGear.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        IShoppingCartService shoppingCartService;
        IOrderService orderService;
        public ShoppingCartController(IShoppingCartService ShoppingCartService, IOrderService OrderService)
        {
            this.shoppingCartService = ShoppingCartService;
            this.orderService = OrderService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var model = shoppingCartService.GetShoppingCartItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToShoppingCart(string Id)
        {
            shoppingCartService.AddToShoppingCart(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromShoppingCart(string Id)
        {
            shoppingCartService.RemoveFromShoppingCart(this.HttpContext, Id);

            return RedirectToAction("Index");
        }

        public PartialViewResult ShoppingCartSummary()
        {
            var shoppingCartSummary = shoppingCartService.GetShoppingCartSummary(this.HttpContext);

            return PartialView(shoppingCartSummary);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var shoppingCartItems = shoppingCartService.GetShoppingCartItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            //process payment

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, shoppingCartItems);
            shoppingCartService.ClearShoppingCart(this.HttpContext);

            return RedirectToAction("ThankYou", new { OrderId = order.Id });
        }

        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}