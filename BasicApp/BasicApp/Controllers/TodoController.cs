using Microsoft.EntityFrameworkCore;

public static class TodoController
{
    // CREATE
    public static async Task<IResult> CreateTodo(Todo data, AppDbContext db)
    {
        if (await db.Todos.AnyAsync(t => t.Id == data.Id))
        {
            return Results.Conflict("Todo Id already exists");
        }

        if (await db.Todos.AnyAsync(t => t.Title == data.Title))
        {
            return Results.Conflict("Todo Title already exists");
        }

        db.Todos.Add(data);
        await db.SaveChangesAsync();

        return Results.Created($"/api/todos/{data.Id}", new
        {
            message = "Todo added successfully",
            data
        });
    }

    // GET ALL
    public static async Task<IResult> GetAllTodos(AppDbContext db)
    {
        return Results.Ok(new
        {
            message = "Todos fetched successfully",
            data = await db.Todos.ToListAsync()
        });
    }

    // GET BY ID
    public static async Task<IResult> GetTodoById(int id, AppDbContext db)
    {
        var todo = await db.Todos.FirstOrDefaultAsync(t => t.Id == id);

        if (todo is null)
        {
            return Results.NotFound(new
            {
                message = $"Todo not found with id {id}"
            });
        }

        return Results.Ok(new
        {
            message = "Todo fetched successfully",
            data = todo
        });
    }

    // UPDATE
    public static async Task<IResult> UpdateTodoById(int id, UpdateTodo data, AppDbContext db)
    {
        var todo = await db.Todos.FindAsync(id);

        if (todo is null)
        {
            return Results.NotFound(new
            {
                message = $"Todo not found with id {id}"
            });
        }

        if (data.Title is not null)
            todo.Title = data.Title;

        if (data.IsCompleted.HasValue)
            todo.IsCompleted = data.IsCompleted.Value;

        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            message = "Todo updated successfully",
            data = todo
        });
    }

    // DELETE
    public static async Task<IResult> DeleteTodoById(int id, AppDbContext db)
    {
        var todo = await db.Todos.FindAsync(id);

        if (todo is null)
        {
            return Results.NotFound(new
            {
                message = $"Todo not found with id {id}"
            });
        }

        db.Todos.Remove(todo);
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            message = "Todo deleted successfully",
            data = todo
        });
    }
}