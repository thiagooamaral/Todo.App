using System;
using Todo.Domain.Entities;
using Todo.Domain.Respositories;
using System.Collections.Generic;

namespace Todo.Domain.Tests.Repositories
{
    public class FakeTodoRepository : ITodoRepository
    {
        public void Create(TodoItem todo) {}

        public IEnumerable<TodoItem> GetAll(string user) =>
            throw new NotImplementedException();

        public IEnumerable<TodoItem> GetAllDone(string user) =>
            throw new NotImplementedException();

        public IEnumerable<TodoItem> GetAllUndone(string user) =>
            throw new NotImplementedException();

        public TodoItem GetById(Guid id, string user) =>
            new TodoItem("Título Teste 01", DateTime.Now, "Thiago Amaral");

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done) =>
            throw new NotImplementedException();

        public void Update(TodoItem todo) {}
    }
}