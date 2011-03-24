﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gymnastika.Modules.Sports.ViewModels;

namespace Gymnastika.Modules.Sports.Views
{
    /// <summary>
    /// Interaction logic for PlanDetailView.xaml
    /// </summary>
    public partial class PlanDetailView : UserControl
    {
        public PlanDetailView()
        {
            InitializeComponent();
        }

        public ISportsPlanViewModel ViewModel
        {
            set { this.DataContext = value; }
            get { return DataContext as ISportsPlanViewModel; }
        }
    }
}
