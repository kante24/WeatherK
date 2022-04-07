﻿using System;
using WeatherK.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherK
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CurrentWeatherPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
