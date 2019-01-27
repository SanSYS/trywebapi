using Microsoft.EntityFrameworkCore;

public class StoriesRepository: DbContext{
    public DbSet<Story> stories { get; set; }
    public DbSet<StoryItem> storyitems {get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseNpgsql("Host=172.21.13.70;Database=storiesdb;Username=postgres;Password=1");
}