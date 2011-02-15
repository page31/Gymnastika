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
using Gymnastika.Modules.Sports.Views;

namespace Gymnastika.Modules.Sports.Views
{
    /// <summary>
    /// Interaction logic for SportView.xaml
    /// </summary>
    public partial class SportView : UserControl, ISportView
    {
        public SportView(ISportViewModel model)
        {
            ViewModel = model;
            InitializeComponent();
        }
        public ISportViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
            get
            {
                return this.DataContext as ISportViewModel;
            }
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            
        }
    }
}