﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inclock.View.master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class menuMaster : ContentPage
    {
        public ListView ListView;

        public menuMaster()
        {
            InitializeComponent();

            BindingContext = new ViewModels.MenuViewModel();
            ListView = MenuItemsListView;
        }

    }
}