using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[Route("api/stories")]
public class StoriesController: ControllerBase 
{
    [HttpGet("all")]
    public List<Story> GetAll() {
        return Repository.Stories;
    }

    [HttpGet("items/{id}")]
    public List<StoryItem> GetItems(int id){
        return Repository
                .StoryItems
                .Where(p => p.StoryId == id)
                .ToList();
    }

    public class Story
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class StoryItem
    {
        public string Text { get; set; }
        
        public int StoryId { get; set; }
    }

    static class Repository{
        public static List<Story> Stories = new List<Story>{
            new Story{
                Id = 1,
                Title = "Сегодня в кино"
            },
            new Story{
                Id = 2,
                Title = "Скидки на продукты"
            },
            new Story{
                Id = 3,
                Title = "Новые налоги"
            }
        };

        public static List<StoryItem> StoryItems = new List<StoryItem> {
            new StoryItem{ StoryId = 1, Text = "Интерстеллар" },
            new StoryItem{ StoryId = 1, Text = "Звёздные врата" },
            new StoryItem{ StoryId = 1, Text = "Друзья" },
            new StoryItem{ StoryId = 1, Text = "Друзья 2" },
            new StoryItem{ StoryId = 1, Text = "Друзья 3" },

            new StoryItem{ StoryId = 2, Text = "Пятёрочка - молоко 0%" },
            new StoryItem{ StoryId = 2, Text = "Лента - мёд 2%" },
            new StoryItem{ StoryId = 2, Text = "Бары - всё 50%" },
            new StoryItem{ StoryId = 2, Text = "Рестораны - всё 50%" },
            new StoryItem{ StoryId = 2, Text = "Flight - всё 20%" },
        };
    }
}