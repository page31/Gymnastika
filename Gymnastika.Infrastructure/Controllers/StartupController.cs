﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Gymnastika.Views;
using Gymnastika.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Gymnastika.Common;
using Microsoft.Practices.Prism.Events;
using Gymnastika.Common.Events;
using Gymnastika.Common.Services;
using Microsoft.Practices.Prism.Modularity;
using Gymnastika.Common.Models;
using Gymnastika.Common.Session;

namespace Gymnastika.Controllers
{
    public class StartupController : IStartupController
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;

        public StartupController(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Run()
        {
            RegisterDependencies();
            RegisterStartupViewWithRegion();
            SubscribeEvents();
        }

        protected void RegisterDependencies()
        {
            _container
                .RegisterType<IUserService, UserService>()
                .RegisterType<ISessionManager, SessionManager>(new ContainerControlledLifetimeManager());

            //Register Views
            _container
                .RegisterType<IStartupView, StartupView>(new ContainerControlledLifetimeManager())
                .RegisterType<IMainView, MainView>(new ContainerControlledLifetimeManager())
                .RegisterType<ICreateNewUserView, CreateNewUserView>();

            //Register ViewModels
            _container
                .RegisterType<StartupViewModel>()
                .RegisterType<MainViewModel>()
                .RegisterType<CreateNewUserViewModel>();
        }

        protected void RegisterStartupViewWithRegion()
        {
            _regionManager.RegisterViewWithRegion(
                    RegionNames.DisplayRegion, () => _container.Resolve<IStartupView>());
        }

        protected void SubscribeEvents()
        {
            _container
                .Resolve<IEventAggregator>()
                .GetEvent<LogOnSuccessEvent>()
                .Subscribe(OnUserLogOnSuccess);
        }

        private void OnUserLogOnSuccess(UserModel user)
        {
            IMainView view = _container.Resolve<IMainView>();
            IStartupView startupView = _container.Resolve<IStartupView>();
            IRegion displayRegion = _regionManager.Regions[RegionNames.DisplayRegion];

            displayRegion.Remove(startupView);
            displayRegion.Add(view);
            displayRegion.Activate(view);

            LoadModules();
        }

        private void LoadModules()
        {
            IModuleCatalog moduleCatelog = _container.Resolve<IModuleCatalog>();
            IModuleManager moduleManager = _container.Resolve<IModuleManager>();

            foreach (var moduleInfo in moduleCatelog.Modules)
            {
                moduleManager.LoadModule(moduleInfo.ModuleName);
            }
        }
    }
}