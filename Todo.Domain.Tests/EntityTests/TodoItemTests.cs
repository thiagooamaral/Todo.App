using System;
using Todo.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Todo.Domain.Tests.EntityTests
{
    [TestClass]
    public class TodoItemTests
    {
        private readonly TodoItem _validTodo = new TodoItem("Título da tarefa", DateTime.Now, "Thiago Amaral");

        [TestMethod]
        public void Dado_um_novo_todo_o_mesmo_nao_pode_ser_concluido()
        {
            Assert.AreEqual(_validTodo.Done, false);
        }
    }
}