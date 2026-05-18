using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class TodoService
{
    private readonly MyDbContext context;
    private readonly IHttpContextAccessor httpContextAccessor;

    public TodoService(MyDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        this.httpContextAccessor = httpContextAccessor;
    }


    // Get current UserId from JWT
    private Guid GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userId!);
    }

    // Get current UserRole from JWT
    private string GetUserRole()
    {
        return httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value!;
    }
    // ✅ ADD TODO
    public async Task<Todo> AddTodoAsync(Todo todo)
    {
        todo.UserId = GetUserId();
        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();
        return todo;
    }

    // ✅ Get Todos
    public async Task<List<Todo>> GetTodosAsync()
    {
        var role = GetUserRole();
        var userId = GetUserId();
        Console.WriteLine(role);
        if (role == "Admin")
        {
            return await context.Todos.Include(t => t.User).ToListAsync();
        }
        return await context.Todos.Where(t => t.UserId == userId).ToListAsync();
    }
}