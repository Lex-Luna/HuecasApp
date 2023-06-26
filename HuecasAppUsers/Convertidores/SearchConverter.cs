using HuecasAppUsers.Modelo;
using HuecasAppUsers.Vista;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HuecasAppUsers.Convertidores
{
    public class SearchConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var searchText = (string)value;
            var allItems = (List < EncuestaM>)parameter;

            var filteredItems = allItems
                .Where(item => item.NomLocal.Contains(searchText)).ToList();
            return filteredItems;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
