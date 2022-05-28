﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using store.Models;
using store.Data;

namespace store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db;
        private UserManager<IdentityUser> userManager;
        public HomeController(ILogger<HomeController> logger,
                                ApplicationDbContext db,
                                UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this.db = db;
            this.userManager = userManager;
            this.userManager = userManager;
        }
        public IActionResult SeedData(int? arg1, int? arg2){
            // return this.Content($"OK arg1={arg1}, arg2={arg2}");
            if(db.Users.FirstOrDefault(u=>u.Id == "001") == null)
                userManager.CreateAsync(new Customer() {
                    Id = "001", UserName = "customer1@example.com",
                    EmailConfirmed = true }, "Ws9Pp59TjWW6kN8:").Wait();
            if(db.Users.FirstOrDefault(u=>u.Id == "002") == null)
                userManager.CreateAsync(new Customer() {
                Id = "002", UserName = "customer2@example.com",
                EmailConfirmed = true }, "Ws9Pp59TjWW6kN8:").Wait();
            if(db.Users.FirstOrDefault(u=>u.Id == "003") == null)
                userManager.CreateAsync(new Employee {
                Id = "003", UserName = "employee@example.com",
                EmailConfirmed = true }, "Ws9Pp59TjWW6kN8:").Wait();
            if(db.Invoices.FirstOrDefault(u=>u.Id == 1) == null){
                db.Add( new Invoice(){ Id = 1, Date = DateTime.Now,
                    IssuedByGuid = "003", IssuedForGuid="001",
                    Items = new List<InvoiceItem>(){
                        new InvoiceItem(){Id=1, Name="Product 1",
                            Amount=2f, Price=23.4f, VAT=23},
                        new InvoiceItem(){Id=2, Name="Product 2",
                            Amount=1.3f, Price=3.4f, VAT=8}
                    }
                });
                db.SaveChanges();
                }
            return this.RedirectToAction("Index", "Customer");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
