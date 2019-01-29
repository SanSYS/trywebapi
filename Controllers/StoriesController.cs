using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[Route("api/stories")]
public class StoriesController: ControllerBase 
{
    [HttpGet("all")]
    public List<Story> GetAll() {
        return new StoriesRepository().stories.ToList();
    }

    [HttpGet("items/{id}")]
    public List<StoryItem> GetItems(int id){
        return new StoriesRepository()
                .storyitems
                .Where(p => p.storyid == id)
                .ToList();
    }

    [HttpPost("add")]
    public IActionResult Add(string title){
        var rep = new StoriesRepository();
        
        // достать последний ид из бд
        int lastId = rep.stories.Max(p => p.id);

        // добавить новую историю
        rep.stories.Add(new Story{
            id = lastId + 1,
            title = title
        });

        // сохранить изменения в БД
        rep.SaveChanges();

        return Redirect("/add.html");
    }

    [HttpPost("items/{storyId}/add")]
    public IActionResult AddItem(int storyId, string text){
        var rep = new StoriesRepository();
        
        // достать последний ид из бд
        int lastId = rep.storyitems.Max(p => p.id);

        // добавить новый элемент в историю
        rep.storyitems.Add(new StoryItem{
            id = lastId + 1,
            text = text,
            storyid = storyId
        });

        // сохранить изменения в БД
        rep.SaveChanges();

        return Redirect("/add.html");
    }
}