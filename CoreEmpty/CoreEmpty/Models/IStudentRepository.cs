    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmpty.Models
{
    public interface IStudentRepository //creates for managing class with database
    {
        Student GetStudent(int id); // declaration of this method
    }
}
