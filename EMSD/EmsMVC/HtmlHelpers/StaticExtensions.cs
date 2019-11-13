using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;//DescriptionAttribute
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmsMVC
{
    public static class StaticExtensions
    {
        public static SelectList ToSelectList(this IEnumerable list)
        {
            return new SelectList(list);
        }
    }
}