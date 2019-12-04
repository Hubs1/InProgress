using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmpty.Models
{
    public class MockStudentRepository : IStudentRepository // MockStudentRepository provide implementation for IStudentRepository
    {
        private List<Student> _studentList;
        public MockStudentRepository() // shortcut: ctor + tab twice
        {
            _studentList = new List<Student>()
            {
                new Student(){Id=1, Name="Ace", Email="a@student.com"},
                new Student(){Id=2, Name="Boy", Email="b@student.com"},
                new Student(){Id=3, Name="Crop", Email="c@student.com"},
                new Student(){Id=4, Name="Dish", Email="d@student.com"}
            }; // initialise private field studentList with hard-coded data

        }
        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(e => e.Id == id);
            throw new NotImplementedException();
        }
    }
}
