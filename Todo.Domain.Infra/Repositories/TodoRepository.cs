using System;
using System.Linq;
using Todo.Domain.Queries;
using Todo.Domain.Entities;
using Todo.Domain.Respositories;
using System.Collections.Generic;
using Todo.Domain.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Todo.Domain.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _context;

        public TodoRepository(DataContext context) => _context = context;

        // AsNoTracking: 
        // Utilizar para retornos, principalmente quando não houver manipulações do objeto
        // Por exemplo, quando for usar um retorno para um UPDATE/CREATE, não preciso usar
        // Mas quando for usar para retornos direto para tela, por exemplo, faz sentido usar

        // FirstOrDefault:
        // Caso não encontre um valor para retorno, retorna NULL
        // Não da Exception, diferentemente do First apenas

        // GetById:
        // Não usei o AsNoTracking pois uso ele para retornar o objeto na memória, para posterior comparação

        public void Create(TodoItem todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public void Update(TodoItem todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAll(string user) => 
            _context.Todos.AsNoTracking().Where(TodoQueries.GetAll(user)).OrderBy(x => x.Date);

        public IEnumerable<TodoItem> GetAllDone(string user) =>
            _context.Todos.AsNoTracking().Where(TodoQueries.GetAllDone(user)).OrderBy(x => x.Date);

        public IEnumerable<TodoItem> GetAllUndone(string user) =>
            _context.Todos.AsNoTracking().Where(TodoQueries.GetAllUndone(user)).OrderBy(x => x.Date);

        public TodoItem GetById(Guid id, string user) =>
            _context.Todos.FirstOrDefault(TodoQueries.GetById(id, user));

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done) =>
            _context.Todos.AsNoTracking().Where(TodoQueries.GetByPeriod(user, date, done)).OrderBy(x => x.Date);
    }
}