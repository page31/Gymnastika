﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gymnastika.Modules.Meals.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Gymnastika.Data;
using Gymnastika.Modules.Meals.Services;

namespace Gymnastika.Modules.Meals.ViewModels
{
    public class FoodItemViewModel : NotificationObject
    {
        private decimal _amount;
        private decimal _calories;
        private string _changeMyFavoriteButtonContent;
        private ICommand _deleteFoodFromPlanCommand;
        private ICommand _changeMyFavoriteCommand;
        private ICommand _showFoodDetailCommand;

        public FoodItemViewModel(Food food)
        {
            Food = food;
            Amount = 100;
            ChangeMyFavoriteButtonContent = "+ 收藏";
        }

        public Food Food { get; set; }

        public string Name
        {
            get { return Food.Name; }
        }

        public string SmallImageUri
        {
            get { return Food.SmallImageUri; }
        }

        public string MiddleImageUri
        {
            get { return Food.MiddleImageUri; }
        }

        public IEnumerable<NutritionalElement> NutritionalElements { get; set; }

        public decimal Calorie
        {
            get { return Decimal.Round(Food.Calorie); }
        }

        public IList<NutritionalElement> Nutritions { get; set; }

        public decimal Amount
        {
            get 
            {
                return Decimal.Round(_amount); 
            }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged("Amount");
                    Calories = Calorie * Amount / 100;
                    Nutritions = new List<NutritionalElement>();
                    var workEnvironment = ServiceLocator.Current.GetInstance<IWorkEnvironment>();
                    var foodService = ServiceLocator.Current.GetInstance<IFoodService>();
                    using (var scope = workEnvironment.GetWorkContextScope())
                    {
                        NutritionalElements = foodService.NutritionalElementProvider.GetNutritionalElements(Food);
                        for (int i = 0; i < NutritionalElements.ToList().Count; i++)
                        {
                            Nutritions.Add(new NutritionalElement
                            {
                                Name = NutritionalElements.ToList()[i].Name,
                                Value = NutritionalElements.ToList()[i].Value * Amount / 100
                            });
                        }
                    }
                    OnDietPlanSubListPropertyChanged();
                }
            }
        }

        public decimal Calories
        {
            get
            {
                return _calories;
            }
            set
            {
                if (_calories != value)
                {
                    _calories = value;
                    RaisePropertyChanged("Calories");
                    //OnDietPlanSubListPropertyChanged();
                }
            }
        }

        public string ChangeMyFavoriteButtonContent
        {
            get
            {
                return _changeMyFavoriteButtonContent;
            }
            set
            {
                if (_changeMyFavoriteButtonContent != value)
                {
                    _changeMyFavoriteButtonContent = value;
                    RaisePropertyChanged("ChangeMyFavoriteButtonContent");
                }
            }
        }

        public ICommand DeleteFoodFromPlanCommand
        {
            get
            {
                if (_deleteFoodFromPlanCommand == null)
                    _deleteFoodFromPlanCommand = new DelegateCommand(OnDeleteFoodFromPlan);

                return _deleteFoodFromPlanCommand;
            }
        }

        public ICommand ChangeMyFavoriteCommand
        {
            get
            {
                if (_changeMyFavoriteCommand == null)
                    _changeMyFavoriteCommand = new DelegateCommand(OnChangeMyFavorite);

                return _changeMyFavoriteCommand;
            }
        }

        public ICommand ShowFoodDetailCommand
        {
            get
            {
                if (_showFoodDetailCommand == null)
                    _showFoodDetailCommand = new DelegateCommand(ShowFoodDetail);

                return _showFoodDetailCommand;
            }
        }

        public event EventHandler DeleteFoodFromPlan;

        public event EventHandler ChangeMyFavorite;

        public event EventHandler DietPlanSubListPropertyChanged;

        private void OnDeleteFoodFromPlan()
        {
            if (DeleteFoodFromPlan != null)
                DeleteFoodFromPlan(this, new EventArgs());
        }

        private void OnChangeMyFavorite()
        {
            if (ChangeMyFavorite != null)
                ChangeMyFavorite(this, new EventArgs());
        }

        private void OnDietPlanSubListPropertyChanged()
        {
            if (DietPlanSubListPropertyChanged != null)
                DietPlanSubListPropertyChanged(this, new EventArgs());
        }

        private void ShowFoodDetail()
        {
            IFoodDetailViewModel foodDetailViewModel = ServiceLocator.Current.GetInstance<IFoodDetailViewModel>();
            foodDetailViewModel.Food = Food;
            foodDetailViewModel.Initialize();
            foodDetailViewModel.View.ShowView();
        }
    }
}
