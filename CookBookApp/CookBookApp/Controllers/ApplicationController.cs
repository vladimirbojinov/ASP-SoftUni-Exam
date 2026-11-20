using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookBookApp.Web.Controllers;

[Authorize]
public class ApplicationController : Controller
{
    protected string? GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

    protected string? GetUserEmail()
        => User.FindFirstValue(ClaimTypes.Email);

    protected bool IsUserAuthenticated
        => User.Identity?.IsAuthenticated ?? false;
}