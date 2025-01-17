using System;
using BrainCells.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Presentation.Controllers;

[Route("[controller]")]
[Controller]
public class ErrorController : Controller
{
    [Route("{code}")]
    [HttpGet]
    public IActionResult Index(int code)
    {
        StatusCodeViewModel model = new StatusCodeViewModel{
                Code = 26,
                Message = "Hmm..!",
        };
        switch(code)
        {
            case StatusCodes.Status404NotFound:
                model.Code = 404;
                model.Message = "Page Not Found!";
                break;
            case StatusCodes.Status403Forbidden:
                model.Code = 403;
                model.Message = "Forbidden!";
                break;  
        }
        return View("Index", model);
    } 
}