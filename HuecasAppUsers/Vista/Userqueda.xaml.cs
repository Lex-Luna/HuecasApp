using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Userqueda : ContentPage
    {
        public Userqueda()
        {
            InitializeComponent();
            BindingContext = this;
            Task.Run(async () =>
            {
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
        private async Task IrDetalleQueda(UsuarioM p)
        {
            try
            {
                await Navigation.PushAsync(new DetalleQueda(p));
            }
            catch (Exception e)
            {

                Debug.WriteLine("Error: No se pudo tomar el ID " + e);

            }
        }
        public ICommand IrDetalleQuedaCommand => new Command<UsuarioM>(async (p) => await IrDetalleQueda(p));

    }
}