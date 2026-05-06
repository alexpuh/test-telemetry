using Microsoft.AspNetCore.Mvc;
using webserv.Dto;
using System.Diagnostics;

namespace webserv.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticlesController(ILogger<ArticlesController> logger) : ControllerBase
{
    private DtoArticle[] articles = Enumerable.Range(1, 5).Select(index => new DtoArticle
        {
            Id = index,
            Name = Guid.NewGuid().ToString(),
        })
        .ToArray();
    
    private static readonly ActivitySource ActivitySource = new("webserv.ArticlesController");

    [HttpGet]
    public IEnumerable<DtoArticle> Get()
    {
        using var activity = ActivitySource.StartActivity("GetArticles", ActivityKind.Internal);
        logger.LogInformation("Getting articles");
        
        activity?.SetTag("articles.operation", "fetch");
        activity?.AddEvent(new ActivityEvent("Starting to generate articles"));
        
        
        activity?.SetTag("articles.count", articles.Length);
        activity?.AddEvent(new ActivityEvent("Articles generated"));
        
        logger.LogInformation("Returning {ArticleCount} articles", articles.Length);
        
        activity?.SetStatus(ActivityStatusCode.Ok);
        
        return articles;
    }

    [HttpGet("{id:int}")]
    public ActionResult<DtoArticle> GetById([FromRoute] int id)
    {
        logger.LogInformation("Getting article: [{ArticleId}]", id);

        var article = articles.FirstOrDefault(a => a.Id == id);
        if (article == null)
        {
            return NotFound();
        }
        logger.LogInformation("Returning {ArticleName} article", article.Name);
        
        return article;
    }
}
