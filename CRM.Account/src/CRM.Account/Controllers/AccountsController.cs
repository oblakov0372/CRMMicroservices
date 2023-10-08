using CRM.Account.AccountManagement;
using CRM.Account.Dtos;
using CRM.Account.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Account.Controllers
{
  [Controller]
  [Route("accounts")]
  public class AccountsController : ControllerBase
  {
    private readonly IAccountManagementService _accountManagementService;
    public AccountsController(IAccountManagementService accountManagementService)
    {
      _accountManagementService = accountManagementService;
    }
    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<AccountDto>> GetAllAsync()
    {
      return await _accountManagementService.GetAllAsync();
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditStatusAsync([FromBody] Role role, Guid id)
    {
      var editedAccount = await _accountManagementService.EditStatusAsync(id, role);

      return Ok(editedAccount);
    }
    [HttpPost("registration")]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateAccountDto createModel)
    {
      var accountDto = await _accountManagementService.RegisterAsync(createModel);
      if (accountDto == null)
        return BadRequest(new { message = "User with this user name or email alreay registered" });

      return Ok(new { accountDto, message = "User was registered!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginAccountDto loginModel)
    {
      var accountDto = await _accountManagementService.LoginAsync(loginModel);
      if (accountDto == null)
        return BadRequest(new { message = "Incorrect password or username" });

      return Ok(new { accountDto, message = "Log In successfuly" });
    }

  }
}