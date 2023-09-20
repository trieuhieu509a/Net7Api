using Microsoft.AspNetCore.Identity;

namespace TodoList.Api.Data
{
    public class TodoListDbContextSeed
    {
        private readonly IPasswordHasher<Entities.User> _passwordHasher = new PasswordHasher<Entities.User>();

        public async Task SeedAsync(TodoListDbContext context, ILogger<TodoListDbContextSeed> logger, int? retry = 0)
        {
            if (!context.Tasks.Any())
            {
                var user = new Entities.User()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "admin",
                    Email = "admin@localhost",
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "admin");

                context.Users.Add(user);
            }

            if (!context.Tasks.Any())
            {
                context.Tasks.Add( new Entities.Task()
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 1",
                    CreatedDate = DateTime.UtcNow,
                    Priority = Models.Enums.Priority.High,
                    Status = Models.Enums.Status.InProgress
                });
            }

            await context.SaveChangesAsync();

        }
    }
}
