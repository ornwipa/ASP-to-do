using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    /// <summary>
    /// Since this is an interface, there isn't any actual code here, 
    /// just the definition (or method signature) of the GetIncompleteItemsAsync method.
    /// This method requires no parameters and returns a Task<TodoItem[]>
    /// </summary>
    public interface ITodoItemService
    {
        /// <summary>
        /// The Task type is similar to a future or a promise, and
        /// it's used here because this method will be asynchronous.
        /// The method may not be able to return the list of to-do items right away 
        /// because it needs to go talk to the database first.
        /// </summary>
        Task<TodoItem[]> GetIncompleteItemsAsync();
        Task<bool> AddItemAsync(TodoItem newItem);
    }
}