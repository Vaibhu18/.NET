using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add DB
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Add TODO
app.MapPost("/api/todos", async (Todo data, AppDbContext db) => await TodoController.CreateTodo(data, db));

// Get All TODOS
app.MapGet("/api/todos", async (AppDbContext db) => await TodoController.GetAllTodos(db));

// Get TODO by ID
app.MapGet("/api/todos/{id}", async (int id, AppDbContext db) => await TodoController.GetTodoById(id, db));

// Update TODO
app.MapPatch("/api/todos/{id}", async (int id, UpdateTodo data, AppDbContext db) => await TodoController.UpdateTodoById(id, data, db));

// Delete TODO
app.MapDelete("/api/todos/{id}", async (int id, AppDbContext db) => await TodoController.DeleteTodoById(id, db));

app.Run();
public record UpdateTodo(string? Title, bool? IsCompleted);