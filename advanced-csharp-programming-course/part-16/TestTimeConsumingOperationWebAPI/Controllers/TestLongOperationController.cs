﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestTimeConsumingOperationWebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class TestLongOperationController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.Delay(5000);

        return Content("Web API Long Operation Completed");
    }
}