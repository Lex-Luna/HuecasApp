using HuecasAppUsers.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using HuecasAppUsers.Datos;
using Firebase.Auth;
using HuecasAppUsers.Conexiones;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            //LoginOk();
            InitializeComponent();
            BindingContext = new LoginVM(Navigation);
            
        }
        
       
    }
}