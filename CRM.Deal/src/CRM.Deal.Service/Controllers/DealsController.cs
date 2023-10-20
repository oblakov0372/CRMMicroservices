using System.Security.Claims;
using CRM.Deal.Service.DealManagement;
using CRM.Deal.Service.Dtos;
using CRM.TelegramUser.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Deal.Service.Controllers
{
  [Controller]
  [Route("deals")]
  [Authorize]
  public class DealsController : ControllerBase
  {
    private readonly IDealManagementService dealManagementService;
    public DealsController(IDealManagementService dealManagementService)
    {
      this.dealManagementService = dealManagementService;
    }
    protected Guid GetUserId()
    {
      return Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetDealsAsync([FromQuery] DealParameters parameters)
    {
      (IEnumerable<DealDto> deals, int totalPages) = await dealManagementService.GetAllDealsAsync(parameters);
      return Ok(new { deals, totalPages });
    }

    [HttpGet("GetDealsByLoggedUser")]
    public async Task<IActionResult> GetDealsByLoggedUser([FromQuery] DealParameters parameters)
    {
      var userId = GetUserId();
      (IEnumerable<DealDto> deals, int totalPages) = await dealManagementService.GetAllDealsByUserIdAsync(userId, parameters);
      return Ok(new { deals, totalPages });

    }

    [HttpPost]
    public async Task<IActionResult> PostDealAsync([FromBody] CreateDealModel model)
    {
      var creatorId = GetUserId();
      var createdDeal = await dealManagementService.CreateDealAsync(model, creatorId);
      if (createdDeal == null)
        return BadRequest();
      return Ok(createdDeal);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDealAsync(Guid id, [FromBody] UpdateDealModel model)
    {
      var updatedById = GetUserId();
      var result = await dealManagementService.UpdateDealAsync(model, id, updatedById);
      if (result)
        return Ok();
      return BadRequest();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDealAsync(Guid id)
    {
      var result = await dealManagementService.DeleteDealAsync(id);
      if (result)
        return Ok();
      return BadRequest();
    }
  }
}