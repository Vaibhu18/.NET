using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize] // all endpoints required login
public class TodoController : ControllerBase
{
    private readonly TodoService service;
    public TodoController(TodoService service)
    {
        this.service = service;
    }

    // ✅ Add Todo (Admin & User)
    [HttpPost]
    public async Task<ActionResult<Todo>> AddTodo(Todo todo)
    {
        var result = await service.AddTodoAsync(todo);
        return Ok(result);
    }

    // ✅ Get Todos
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetTodos()
    {
        var result = await service.GetTodosAsync();
        return Ok(result);
    }

}