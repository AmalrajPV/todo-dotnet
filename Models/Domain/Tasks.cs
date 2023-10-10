using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Domain
{
    public class Tasks
    {
        public int Id {get; set;}
        public required string Task {get; set;}

        public string? Category { get; set; }

        public bool Status { get; set; } = false;

        public int Priority { get; set; }

    }
}