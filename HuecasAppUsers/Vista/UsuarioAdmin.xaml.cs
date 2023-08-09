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
        #region Constructor
        public UsuarioAdmin()
        {
            InitializeComponent();
        }
        #endregion
        #region Variables
        public ObservableCollection<EncuestaM> listaEncueta1;
        #endregion
        #region Objetos
        public ObservableCollection<EncuestaM> LisEncueta1
        {
            get => listaEncueta1;
            set
            {
                listaEncueta1 = value;
                OnPropertyChanged(); Debug.WriteLine("LisEncuestaRecomendados changed");
            }
        }
        #endregion
        #region Procesos
        public async Task MostrarMisEncuestas(string idUser)
        {
            EncuestaD f = new EncuestaD();
            var encuestas = await f.MostEncuestaIdUser(idUser);
            LisEncueta1 = new ObservableCollection<EncuestaM>(encuestas);
        }
        #endregion
        #region Comandos

        #endregion
    }
}