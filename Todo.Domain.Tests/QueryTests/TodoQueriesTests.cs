using System;
using System.Linq;
using Todo.Domain.Queries;
using Todo.Domain.Entities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;

        public TodoQueryTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa 1", DateTime.Now, "usuarioA"));
            _items.Add(new TodoItem("Tarefa 2", DateTime.Now, "usuarioA"));
            _items.Add(new TodoItem("Tarefa 3", DateTime.Now, "thiagoamaral"));
            _items.Add(new TodoItem("Tarefa 4", DateTime.Now, "usuarioB"));
            _items.Add(new TodoItem("Tarefa 5", DateTime.Now, "thiagoamaral"));
        }

        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_apenas_do_usuario_thiagoamaral()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("thiagoamaral"));
            Assert.AreEqual(2, result.Count());
        }
    }
}