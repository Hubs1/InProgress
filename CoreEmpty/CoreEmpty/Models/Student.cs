using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmpty.Models
{
    public class Student
    {
        public int Id { get; set; } // shortcut: prop + press tab key twice
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
