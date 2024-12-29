using System;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Presentation.Controllers;

[Controller]
[Route("[controller]")]
public class AccountController : Controller
{
    public AccountController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }
}