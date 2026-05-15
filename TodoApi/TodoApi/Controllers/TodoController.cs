using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddTodo(Todo todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return Ok(todo);
    }

    [HttpGet]
    public IActionResult GetTodos()
    {
        return Ok(_context.Todos.ToList());
    }
}