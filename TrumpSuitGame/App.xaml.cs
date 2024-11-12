﻿using System.Globalization;

namespace TrumpSuitGameGeordi
{
    public partial class App : Application
    {
        public static string piattaforma;
        public static ResourceDictionary d;
        public static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public App()
        {
            InitializeComponent();
            try
            {
                d = Resources[CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary;

            }
            catch (Exception ex)
            {
                d = Resources["en"] as ResourceDictionary;
            }
            piattaforma = DeviceInfo.Current.Model;
            if (piattaforma == "System Product Name")
                piattaforma = "Windows " + DeviceInfo.Current.VersionString;
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
