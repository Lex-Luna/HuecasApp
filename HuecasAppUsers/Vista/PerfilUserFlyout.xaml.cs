using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HuecasAppUsers.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilUserFlyout : ContentPage
    {
        public ListView ListView;

        public PerfilUserFlyout()
        {
            InitializeComponent();

            BindingContext = new PerfilUserFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class PerfilUserFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<PerfilUserFlyoutMenuItem> MenuItems { get; set; }

            public PerfilUserFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<PerfilUserFlyoutMenuItem>(new[]
                {
                    new PerfilUserFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new PerfilUserFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new PerfilUserFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new PerfilUserFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new PerfilUserFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}