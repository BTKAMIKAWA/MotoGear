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
        IRepository<Customer> customers;
        IShoppingCartService shoppingCartService;
        IOrderService orderService;
        public ShoppingCartController(IShoppingCartService ShoppingCartService, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.shoppingCartService = ShoppingCartService;
            this.orderService = OrderService;
            this.customers = Customers;
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

        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ZipCode=customer.ZipCode
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }           
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var shoppingCartItems = shoppingCartService.GetShoppingCartItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;

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