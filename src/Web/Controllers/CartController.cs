using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyNetwork.ApplicationCore.Constants;
using PharmacyNetwork.ApplicationCore.Entities;
using PharmacyNetwork.ApplicationCore.Interfaces;
using PharmacyNetwork.Infrastructure.Data;
using PharmacyNetwork.Web.Extensions;
using PharmacyNetwork.Web.Models;
using PharmacyNetwork.Web.ViewModels;

namespace PharmacyNetwork.Web.Controllers
{
    [Authorize(Roles = AuthorizationConstants.Roles.USERS)]
    public class CartController : Controller
    {
        private readonly PharmacyNetworkContext _context;

        public CartController(PharmacyNetworkContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cartList = HttpContext.Session.Get<List<CartItem>>("Cart");
            return View(cartList);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ReserveMedItemsViewModel viewModel)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            if (!ModelState.IsValid) return View(new ReserveMedItemsViewModel() { Items = cart });

            var nowDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            
            foreach (var item in cart)
            {
                await _context.Database.ExecuteSqlRawAsync($"EXEC reserve_med_item '{nowDateTime}', {item.MedicalItemId}, {item.PharmacyId}, {item.Count}, '{viewModel.Telephone}'");
            }

            ClearCart();

            return RedirectToAction("Details", "Purchases", new { id = 31 }); // TODO: REVIEW
        }

        public IActionResult Reserve()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            ReserveMedItemsViewModel viewModel = new ReserveMedItemsViewModel()
            {
                Items = cart
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ConfirmPurchase()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            var nowDateTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");

            var query =
                $"INSERT INTO purchase(pharm_id, purch_date, purch_amount, purch_discount_percent) " +
                $"VALUES({cart[0].PharmacyId}, '{nowDateTime}', 0, 0);";

            await _context.Database.ExecuteSqlRawAsync(query);

            var purchases = _context.Purchase.ToList();
            var idPurchase = purchases.First(p => p.PurchDate.ToString("yyyyMMdd HH:mm:ss") == nowDateTime).PurchId;

            foreach (var item in cart)
            {
                await _context.Database.ExecuteSqlRawAsync($"EXEC add_purchase_in_check {item.MedicalItemId}, {idPurchase}, {item.Count}, {item.PharmacyId};");
            }

            ClearCart();

            return RedirectToAction("Details", "Purchases", new {id = idPurchase});
        }

        public IActionResult AddToCart(int medItemId, decimal medItemPrice, int pharmId, int count)
        {
            var cartItem = new CartItem()
            {
                MedicalItemId = medItemId,
                MedItemPrice = medItemPrice,
                PharmacyId = pharmId,
                Count = count
            };

            var cartList = HttpContext.Session.Get<List<CartItem>>("Cart");

            if (cartList == null)
            {
                cartList = new List<CartItem> { cartItem };
            }
            else
            {
                if (cartList.Any(i => i.PharmacyId == cartItem.PharmacyId && i.MedicalItemId == cartItem.MedicalItemId))
                {
                    cartList.Find(i => i.PharmacyId == cartItem.PharmacyId && i.MedicalItemId == cartItem.MedicalItemId).Count += cartItem.Count;
                }
                else
                {
                    cartList.Add(cartItem);
                }
            }

            HttpContext.Session.Set("Cart", cartList);

            return RedirectToAction(nameof(Index), "PharmacyWharehouses");
        }

        public IActionResult DeleteFromCart(int id)
        {
            var cartList = HttpContext.Session.Get<List<CartItem>>("Cart");
            
            cartList.RemoveAll(i => i.MedicalItemId == id);

            HttpContext.Session.Set("Cart", cartList);

            return RedirectToAction(nameof(Index));
        }

        private void ClearCart()
        {
            var cartList = HttpContext.Session.Get<List<CartItem>>("Cart");
            cartList.Clear();
            HttpContext.Session.Set("Cart", cartList);
        }
    }
}
