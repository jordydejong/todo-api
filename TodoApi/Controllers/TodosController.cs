using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;
using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodosController(TodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Gets all todos
        /// </summary>
        /// <returns>A list of all todos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll() => Ok(await _todoService.GetAllAsync());

        /// <summary>
        /// Gets a specific todo by ID
        /// </summary>
        /// <param name="id">The todo ID</param>
        /// <returns>The requested todo</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoDto>> Get(int id)
        {
            var todo = await _todoService.GetAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        /// <summary>
        /// Creates a new todo
        /// </summary>
        /// <param name="todo">The todo to create</param>
        /// <returns>The created todo with generated ID</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<TodoDto>> Create([FromBody] CreateTodoDto todo)
        {
            var created = await _todoService.CreateAsync(todo);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing todo
        /// </summary>
        /// <param name="id">The todo ID to update</param>
        /// <param name="updatedTodo">The updated todo data</param>
        /// <returns>No content on success</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoDto updatedTodo)
        {
            var updated = await _todoService.UpdateAsync(id, updatedTodo);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Partially updates a todo
        /// </summary>
        /// <param name="id">The todo ID to update</param>
        /// <param name="patchTodo">The fields to update (only provided fields will be updated)</param>
        /// <returns>No content on success</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchTodoDto patchTodo)
        {
            var updated = await _todoService.PatchAsync(id, patchTodo);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a todo
        /// </summary>
        /// <param name="id">The todo ID to delete</param>
        /// <returns>No content on success</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _todoService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
