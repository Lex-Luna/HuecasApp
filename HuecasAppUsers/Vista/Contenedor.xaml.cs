﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contenedor : Xamarin.Forms.TabbedPage
    {
        public Contenedor()
        {
            InitializeComponent();
            //On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}