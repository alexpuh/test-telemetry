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
        logger.LogInformation("Getting articles");
        
        var articles = Enumerable.Range(1, 5).Select(index => new DtoArticle
        {
            Id = index,
            Name = Guid.NewGuid().ToString(),
        })
        .ToArray();
        
        logger.LogInformation("Returning {ArticleCount} articles", articles.Length);
        
        return articles;
    }
}
