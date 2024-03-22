# Code For Basic Database

## Model

```cs
public class User
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string UserName { get; set; } = "Giga Chad";
    
    public byte[] Image { get; set; }= DefaultImage();
    private static byte[] DefaultImage()
    {
        // Provide logic to load a default image from a file or resource
        // For example:
        // return File.ReadAllBytes("default_image.png");
        return new byte[0]; // Placeholder default image
    }
    public List<Challenge> Challenges { get; set; }
}


public class Challenge
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = "Challenge";
    
    [Required]
    public int DayCount { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    public List<ToDo> ToDos { get; set; }
    
    [Required]
    public Guid UserId { get; set; } // Foreign key
    
    public User User { get; set; } // Navigation property
}

public class ToDo
{
    [Key]
    public Guid Id { get; set; }
    
    public bool IsCompleted { get; set; }
    
    [Required]
    public string Description { get; set; } = "Exercise";
    
    public DateTime CompletionDate { get; set; }
    public Guid ChallengeId { get; set; } // Foreign key
    
    public Challenge Challenge { get; set; } // Navigation property
}
```

## DBContext or Repositiory

```cs

public class Repository : DbContext
{
    public Repository(DbContextOptions<Repository> opts) : base(opts)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<ToDo> ToDos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Seed Users
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "Default",
            Image = GetDefaultImage(),
        };
        var chlng = new Challenge
        {
            Id = Guid.NewGuid(),
            Name = "Streak Visitors",
            DayCount = 7,
            StartDate = DateTime.Now,
            UserId = user.Id
        };
        var todoOne = new ToDo
        {
            Id = Guid.NewGuid(),
            Description = "Task 1",
            ChallengeId = chlng.Id
        };
        var todoTwo = new ToDo
        {
            Id = Guid.NewGuid(),
            Description = "Task 2",
            ChallengeId = chlng.Id
        };
        #endregion

        modelBuilder.Entity<User>().HasData(user);

        // Seed Challenges
        modelBuilder.Entity<Challenge>().HasData(chlng);

        // Seed ToDos
        modelBuilder.Entity<ToDo>().HasData(todoOne,todoTwo);

        base.OnModelCreating(modelBuilder);
    }

    private byte[] GetDefaultImage()
    {
        // Provide logic to load a default image from a file or resource
        // For example:
        // return File.ReadAllBytes("default_image.png");
        return new byte[0]; // Placeholder default image
    } 
}

```

## In Case of Design Flaws

```cs

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Repository>
{
    public Repository CreateDbContext(string[] args)
    {  
        var connectionString = "Data Source=mydatabase.db";

        var optionsBuilder = new DbContextOptionsBuilder<Repository>();
        optionsBuilder.UseSqlite(connectionString);

        return new Repository(optionsBuilder.Options);
    }
}
```


> Setting up Connection String and using it

```cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Streak.Data;

public class Program
{
    public static void Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("SQLiteConnection");

        var options = new DbContextOptionsBuilder<Repository>()
            .UseSqlite(connectionString)
            .Options;

        using (var context = new Repository(options))
        {
            // Perform CRUD operations here
        }
    }
}

```

> Setup Connection string in appsettings.json

```json
{
  "ConnectionStrings": {
    "SQLiteConnection": "Data Source=mydatabase.db"
  }
}

```

## DotNet Command in Order

> Add SqLite to Project

```cmd
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

> EF Design And Tools Command

```cmd
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

> Add Migration to DB And Check Is It Designed Valid

```cmd
dotnet ef migrations add InitialCreate
```

> For Specific DbContext and Specific Folder location for Migration

```cmd
dotnet ef migrations add InitialCreate --context Repository --output-dir Migrations
```

> Update Database

```cmd
dotnet ef database update
```

```cmd
dotnet ef database update --context Repository
```

> Using Faker-cs Library For Populating Data

```cmd
dotnet add package Faker.Net
```

```cs
using Microsoft.EntityFrameworkCore;
namespace CRUD;
public class UserGeneration
{
    public void FillData()
    {

        var options = new DbContextOptionsBuilder<Repository>()
                    .UseSqlite("Data Source=mydatabase.db")
                    .Options;
        Console.Clear();
        using (var context = new Repository(options))
        {
            for (var i = 0; i < 100; i++)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = Faker.Name.FullName(),
                };
                var userEntity = context.Users.Add(user);
                context.SaveChanges();
                // Create a new challenge 
                Console.WriteLine($"{i} User Generated with id {user.Id}");

                for (var k = 0; k < Faker.RandomNumber.Next(1, 7); k++)
                {
                    var chlng = new Challenge
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Lorem.Sentence(1),
                        DayCount = Faker.RandomNumber.Next(365),
                        StartDate = Faker.Identification.DateOfBirth(),
                        UserId = userEntity.Entity.Id
                    };
                    var challengeEntity = context.Challenges.Add(chlng);
                    context.SaveChanges();
                    Console.WriteLine($">>>> {k} Challenge Generated with id {chlng.Id}");
                    for (var j = 0; j < Faker.RandomNumber.Next(5, 10); j++)
                    {
                        var todo = new ToDo
                        {
                            Id = Guid.NewGuid(),
                            Description = Faker.Lorem.Paragraph(1),
                            ChallengeId = challengeEntity.Entity.Id
                        };
                        context.Add(todo);
                        Console.WriteLine($">>>>>>>>>>>>> {j} ToDoFor Generated with id {todo.Id}");
                    }
                }
            }
        }
    }
}
```

## This Rerlated To Blazor Specific

```cs
var connectionString = "Data Source=mydatabase.db";
 
builder.Services.AddDbContext<Repository>(options =>
 options.UseSqlite(connectionString));
```
