using CookBookApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookBookApp.Web.Controllers;

public class HomeController : ApplicationController
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode = 500)
    {
        int code = statusCode;
        Response.StatusCode = code;

        (ViewData["Title"], ViewData["Message"]) = code switch
        {
            400 => ("Bad request", "The request could not be understood. Please check the address or form data and try again."),
            403 => ("Access denied", "You don't have permission to view this page."),
            404 => ("Page not found", "We couldn't find the page you were looking for."),
            _ => ("Error", "Unexpected error occurred. Please contact support!.")
        };

        ViewData["StatusCode"] = code;

        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
