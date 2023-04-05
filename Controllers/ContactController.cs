using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactRegister.Models;
using ContactRegister.Repositories;

namespace ContactRegister.Controllers
{
    [Route("[controller]")]
    public class ContactController : Controller
    {

        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // Index

        [HttpGet("Contact")]
        public IActionResult Index()
        {
            return View();
        }

        // Create

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(ContactModel contact)
        {
            _contactRepository.ToAdd(contact);
            return RedirectToAction("Index");
        }

        // Update

        [HttpGet("Update")]
        public IActionResult Update()
        {
            return View();
        }

        // Delete

        [HttpGet("ConfirmDeletion")]
        public IActionResult ConfirmDeletion()
        {
            return View();
        }

        [HttpGet("Delete")]
        public IActionResult Delete()
        {
            return View();
        }
    }
}