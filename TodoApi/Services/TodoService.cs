using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Models.Entities;

namespace TodoApi.Services
{
    public class TodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoDto>> GetAllAsync()
        {
            var entities = await _context.Todos.ToListAsync();
            return entities.Select(MapToDto);
        }

        public async Task<TodoDto?> GetAsync(int id)
        {
            var entity = await _context.Todos.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TodoDto> CreateAsync(CreateTodoDto createTodo)
        {
            var entity = new TodoEntity
            {
                Name = createTodo.Name,
                Description = createTodo.Description,
                Status = createTodo.Status,
                Date = DateTime.Parse(createTodo.Date).ToUniversalTime(),
                Assignee = createTodo.Assignee,
                Creator = createTodo.Creator
            };

            _context.Todos.Add(entity);
            await _context.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTodoDto updatedTodo)
        {
            var entity = await _context.Todos.FindAsync(id);
            if (entity == null) return false;

            entity.Name = updatedTodo.Name;
            entity.Description = updatedTodo.Description;
            entity.Status = updatedTodo.Status;
            entity.Date = DateTime.Parse(updatedTodo.Date).ToUniversalTime();
            entity.Assignee = updatedTodo.Assignee;
            entity.Creator = updatedTodo.Creator;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int id, PatchTodoDto patchTodo)
        {
            var entity = await _context.Todos.FindAsync(id);
            if (entity == null) return false;

            // Only update fields that were provided (not null)
            if (patchTodo.Name != null)
                entity.Name = patchTodo.Name;

            if (patchTodo.Description != null)
                entity.Description = patchTodo.Description;

            if (patchTodo.Status != null)
                entity.Status = patchTodo.Status;

            if (patchTodo.Date != null)
                entity.Date = DateTime.Parse(patchTodo.Date).ToUniversalTime();

            if (patchTodo.Assignee != null)
                entity.Assignee = patchTodo.Assignee;

            if (patchTodo.Creator != null)
                entity.Creator = patchTodo.Creator;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Todos.FindAsync(id);
            if (entity == null) return false;

            _context.Todos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static TodoDto MapToDto(TodoEntity entity)
        {
            return new TodoDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Status = entity.Status,
                Date = entity.Date.ToString("yyyy-MM-dd"),
                Assignee = entity.Assignee,
                Creator = entity.Creator
            };
        }
    }
}