using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioAdmin : ContentPage
    {
        public UsuarioAdmin()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                BindingContext = this;
                await MostrarUsuarios();
            }).Wait();
        }
        ObservableCollection<UsuarioM> lisUsuarios;
        public ObservableCollection<UsuarioM> LisUsuarios
        {
            get => lisUsuarios;
            set
            {
                lisUsuarios = value;
                OnPropertyChanged(); Debug.WriteLine("LisEncuestaRecomendados changed");
            }
        }
        public async Task MostrarUsuarios()
        {
            var f = new UsuarioD();
            var usuario = await f.MostUsuario();
            LisUsuarios = new ObservableCollection<UsuarioM>(usuario);
            
        }

    }
}