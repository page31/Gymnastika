﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gymnastika.Modules.Meals.Controllers
{
    public interface ILoadDataController
    {
        bool IsLoaded { get; }
        void Load();
        void LoadCategoryData();
        void LoadSubCategoryData();
        void LoadFoodData();
        void LoadNutritionalElementData();
        void LoadIntroductionData();
        void LoadDietPlanData();
        void LoadSubDietPlanData();
        void LoadDietPlanItemData();
    }
}