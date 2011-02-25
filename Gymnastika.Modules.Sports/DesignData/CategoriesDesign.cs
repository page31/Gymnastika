﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gymnastika.Modules.Sports.Models;
using System.Collections.ObjectModel;

namespace Gymnastika.Modules.Sports.DesignData
{
    public class CategoriesDesign
    {
        public CategoriesDesign()
        {
            Categories = new CategoryCollection()
            {
                new SportsCategory()
                {
                    Name = "s"
                }
            };
        }
       public CategoryCollection Categories { get; set; }
    }
}
