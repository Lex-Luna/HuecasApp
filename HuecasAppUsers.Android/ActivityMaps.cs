using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using HuecasAppUsers.Conexiones;
using Plugin.CurrentActivity;

[Application(Debuggable = true)]
[MetaData("com.google.andriod.maps.v2.API_KEY",
    Value = Constantes.GoogleMapsApiKey)]
public class ActivityMaps : Application
{
    ActivityMaps(IntPtr handle, JniHandleOwnership transer)
     : base(handle, transer)
    {

    }
    public override void OnCreate()
    {
        base.OnCreate();
        CrossCurrentActivity.Current.Init(this);
    }
}
