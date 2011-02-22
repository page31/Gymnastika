﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gymnastika.Data;
using Gymnastika.Modules.Meals.Models;

namespace Gymnastika.Modules.Meals.Services
{
    public class FoodService : IFoodService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SubCategory> _subCategoryRepository;
        private readonly IRepository<Food> _foodRepository;
        private readonly IRepository<DietPlan> _DietPlanRepository;
        private readonly IRepository<SubDietPlan> _SubDietPlanRepository;

        public FoodService(IRepository<Category> categoryRepository,
            IRepository<SubCategory> subCategoryRepository,
            IRepository<Food> foodRepository,
            IRepository<DietPlan> DietPlanRepository,
            IRepository<SubDietPlan> SubDietPlanRepository)
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _foodRepository = foodRepository;
            _DietPlanRepository = DietPlanRepository;
            _SubDietPlanRepository = SubDietPlanRepository;
        }

        #region IFoodService Members

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.Fetch(c => true);
        }

        public Category GetCategory(int foodId)
        {
            return _categoryRepository.Get(_subCategoryRepository.Get(foodId).Id);
        }

        public SubCategory GetSubCategory(int foodId)
        {
            return _subCategoryRepository.Get(foodId);
        }

        public IEnumerable<Food> GetFoodsByName(string foodName)
        {
            return _foodRepository.Fetch(f => f.Name.Contains(foodName));
        }

        public IEnumerable<Food> GetFoodsInCount(int count)
        {
            return _foodRepository.Fetch(f => true, null, 0, count);
        }

        public IEnumerable<DietPlan> GetAllSavedDietPlansOfUser(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DietPlan> GetAllRecommendedDietPlans()
        {
            return _DietPlanRepository.Fetch(rdp => true);
        }

        public void CreateDietPlan(DietPlan dietPlan)
        {
            _DietPlanRepository.Create(dietPlan);
        }

        #endregion
    }
}