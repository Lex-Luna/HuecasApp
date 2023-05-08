using HuecasAppUsers.VistaModelo;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilUser : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PerfilUser()
        {
            InitializeComponent();
            BindingContext = new PerfilUserVM(Navigation);
        }

        private void Cerra_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}