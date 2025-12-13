using Microsoft.AspNetCore.Mvc;
using webserv.Dto;

namespace webserv.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticlesController(ILogger<ArticlesController> logger) : ControllerBase
{
    [HttpGet]
    public IEnumerable<DtoArticle> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new DtoArticle
        {
            Id = index,
            Name = Guid.NewGuid().ToString(),
        })
        .ToArray();
    }
}
