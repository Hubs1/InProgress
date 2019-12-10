using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpty.Models;//Student

namespace CoreEmpty.ViewModels
{
    public class HomeDetails
    {
        public Student Student { get; set; }
        public string PageTitle { get; set; }
    }
}
