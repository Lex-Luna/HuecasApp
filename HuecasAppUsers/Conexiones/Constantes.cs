using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuecasAppUsers.Conexiones
{
    public class Constantes
    {
        public static FirebaseClient firebase = new FirebaseClient("https://huecasapp-adf08-default-rtdb.firebaseio.com/");
        public const string WebapyFirebase = "AIzaSyDkLJ-fJ-mx41aCbCtC5P6x4G8VqIrZCEY";
        public static string storage = "huecasapp-d8da1.appspot.com";
        
        public const string GoogleMapsApiKey = "AIzaSyDkbzFWQC_jmqn6NY1_F92aOLVfoKbrRr8"; 
        
    }

}

