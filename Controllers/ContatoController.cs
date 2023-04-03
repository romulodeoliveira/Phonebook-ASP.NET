using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactRegister.Controllers
{
    [Route("[controller]")]
    public class ContatoController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("Update")]
        public IActionResult Update()
        {
            return View();
        }

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