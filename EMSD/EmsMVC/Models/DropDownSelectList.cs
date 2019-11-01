using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EmsEntities;
using EmsDAL;
using EmsBAL;

namespace EmsMVC.Models
{
    public class DropDownSelectList
    {
        public SelectList DepartmentList()
        {
            SelectList items;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                items = unitOfWork.DepartmentRepository.GetAll().Select(d => new { d.Id, d.Name }).ToSelectList();
            }
            return items;
        }
    }
}