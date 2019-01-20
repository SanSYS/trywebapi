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
        return new Repository().stories.ToList();
    }

    [HttpGet("items/{id}")]
    public List<StoryItem> GetItems(int id){
        return new Repository()
                .storyitems
                .Where(p => p.storyid == id)
                .ToList();
    }

    public class Story
    {
        public int id { get; set; }

        public string title { get; set; }
    }

    public class StoryItem
    {
        public int id { get; set; }

        public string text { get; set; }
        
        public int storyid { get; set; }
    }

    class Repository: DbContext{
        public DbSet<Story> stories { get; set; }
        public DbSet<StoryItem> storyitems {get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.UseNpgsql("Host=172.21.13.70;Database=storiesdb;Username=postgres;Password=1");
    }
}