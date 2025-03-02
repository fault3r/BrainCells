using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BrainCells.Application.Common;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.TodoRepository;
using BrainCells.Application.Services.TodoRepository.Dto;
using BrainCells.Presentation.Models.Account.ViewModels;
using BrainCells.Presentation.Models.Todo.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrainCells.Presentation.Controllers;

[Authorize]
[Route("[controller]")]
[Controller]
public class TodoController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITodoRepository _todoRepository;
        private readonly IValidator<AddListViewModel> _addListValidator;


    public TodoController(IAccountRepository accountRepository,
        ITodoRepository todoRepository,
            IValidator<AddListViewModel> addListValidator)
    {
        _accountRepository = accountRepository;
        _todoRepository = todoRepository;
            _addListValidator = addListValidator;
    }

    private async Task setAccount()
    {
        var account = await _accountRepository.GetAccountAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        ViewData["Account"] = new AccountViewModel{
            Id = account.Id,
            Email = account.Email,
            Role = account.Role,
            Name = account.Name,
            Picture = account.Picture,
        };
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await setAccount();
        return View("Index");
    }

    [Route("AddList")]
    [HttpGet]
    public async Task<IActionResult> AddList()
    {
        await setAccount();
        return View("AddList");
    }

    [Route("AddList")]
    [HttpPost]
    public async Task<IActionResult> AddList([FromForm]AddListViewModel list)
    {
        ModelState.Clear();
        var validate = _addListValidator.Validate(list);
        if(validate.IsValid)
        {
            var result = await _todoRepository.AddListAsync(new TodoListDto{
                Name = list.Name,
                Description = list.Description,
                Color = list.Color,
                Picture = list.Picture,
                DefaultPicture = list.DefaultPicture,
            });
            if(result.Success)
            {
                list = new AddListViewModel(); 
                ViewData["MessageType"] = AppConsts.SUCCESS;
            }
            else
                ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("AddList", result.Message);
        }
        else
        {
            ViewData["MessageType"] = AppConsts.WARNING;
            ModelState.AddFluentResult(validate);
        }
        await setAccount();
        return View("AddList", list);
    }
}