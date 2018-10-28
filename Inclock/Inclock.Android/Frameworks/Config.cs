﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Inclock.BL.SqlLite;
[assembly: Xamarin.Forms.Dependency(typeof(Inclock.Droid.Frameworks.Config))]
namespace Inclock.Droid.Frameworks
{
    public class Config : IConfig
    {
        public string StringConnection => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
    }
}