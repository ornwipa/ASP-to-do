using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        /// <summary>
        /// The constructor is a special method, called to create a new instance of a class
        /// </summary>
        /// <param name="todoItemService">To create the TodoController, 
        /// need to provide an object that matches the ITodoItemService interface.</param>
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IActionResult> Index()
        {
            // Get to-do items from database
            var items = await _todoItemService.GetIncompleteItemsAsync();

            // Put items into a model
            var model = new TodoViewModel()
            {
                Items = items
            };

            // Render view using the model            
            return View(model);
        }
    }
}