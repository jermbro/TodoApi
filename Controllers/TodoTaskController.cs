using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("api/todotask")]
    public class TodoTaskController : Controller
    {
//        private readonly xprtestContext _context;
        private readonly TodoContext _context;

        public TodoTaskController(TodoContext context)
        {
            _context = context;

/*
            if (_context.TodoTask.Count() == 0)
            {
                _context.TodoTask.Add(new TodoTask { Name = "Item1" });
                _context.SaveChanges();
            }
 */
        }

        [HttpGet]
        public IEnumerable<TodoTask> GetAll()
        {
            return _context.TodoTask.ToList();
        }


        [HttpGet("{id}", Name = "GetTodoTask")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoTask.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoTask item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TodoTask.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodoTask", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.TodoTask.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoTask.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoTask.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoTask.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}