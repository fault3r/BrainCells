using System;
using System.Security.Claims;
using BrainCells.Application.Common;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.TodoRepository;
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
        private readonly IValidator<EditListViewModel> _editListValidator;


    public TodoController(IAccountRepository accountRepository,
        ITodoRepository todoRepository,
            IValidator<AddListViewModel> addListValidator,
            IValidator<EditListViewModel> editListValidator)
    {
        _accountRepository = accountRepository;
        _todoRepository = todoRepository;
            _addListValidator = addListValidator;
            _editListValidator = editListValidator;
    }

    private async Task setAccount()
    {
        var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(accountId != null)
        {
            var account = await _accountRepository.GetAccountAsync(accountId);
            if(account != null)
                ViewData["Account"] = new AccountViewModel{
                    Id = account.Id,
                    Email = account.Email,
                    Role = account.Role,
                    Name = account.Name,
                    Picture = account.Picture,
                };
            else
                ViewData["Account"] = null;
        }
        else
            ViewData["Account"] = null;
    }
    
    private async Task setLists()
    {
        var tLists = await _todoRepository.GetListsAsync();
        var lists = tLists.Select(r => new ListViewModel{
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Color = r.Color,
            Picture = r.Picture,
        }).ToList();
        ViewData["Lists"] = lists as IEnumerable<ListViewModel>;
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await setLists();
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
            var result = await _todoRepository.AddListAsync(new AddListDto{
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

    [Route("EditList")]
    [HttpGet]
    public async Task<IActionResult> EditList([FromQuery]string id)
    {
        var tList = await _todoRepository.GetListsAsync(id);
        var list = tList.Select(r => new EditListViewModel{
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Color = r.Color,
            CurrentPicture = r.Picture,
        }).FirstOrDefault();
        await setAccount();
        return View("EditList", list);
    }

    [Route("EditList")]
    [HttpPost]
    public async Task<IActionResult> EditList([FromForm]EditListViewModel list)
    {
        ModelState.Clear();
        var validate = _editListValidator.Validate(list);
        if(validate.IsValid)
        {
            var result = await _todoRepository.EditListAsync(new EditListDto{
                Id = list.Id,
                Name = list.Name,
                Description = list.Description,
                Color = list.Color,
                Picture = list.Picture,
                DefaultPicture = list.DefaultPicture,
            });
            if(result.Success)
            {
                var tList = await _todoRepository.GetListsAsync(list.Id);
                list.CurrentPicture = tList.FirstOrDefault().Picture;
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
        return View("EditList", list);
    }

    [Route("DeleteList")]
    [HttpPost]
    public async Task<IActionResult> DeleteList([FromForm]string id, bool confirm)
    {
        ModelState.Clear();
        if(confirm)
        {
            var result = await _todoRepository.DeleteListAsync(id);
            if(result.Success)
                return await Index();
            else
                ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("DeleteList", result.Message);
        }
        else
        {
            ViewData["MessageType"] = AppConsts.ERROR;
            ModelState.AddModelError("DeleteList", "To continue with the deletion process you must confirm your intention to delete!");
        }
        return await EditList(id);
    }
}