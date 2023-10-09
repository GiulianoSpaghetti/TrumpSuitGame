using System.Globalization;

namespace TrumpSuitGame;

public partial class App : Application
{
    public static string piattaforma;
    public static ResourceDictionary d;
    public static readonly CancellationTokenSource cancellationTokenSource= new CancellationTokenSource();

    public App()
    {
        InitializeComponent();
        try
        {
            d = Resources[CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary;

        }
        catch (Exception ex)
        {
            d = Resources["it"] as ResourceDictionary;
        }
#if WINDOWS
        MainPage = new AppShellWindows();
#else
        MainPage = new AppShell();
#endif
        piattaforma = DeviceInfo.Current.Model;
        if (piattaforma == "System Product Name")
            piattaforma = "Windows " + DeviceInfo.Current.VersionString;

    }

#if ANDROID
    public static System.String GetResource(int id)
    {
        return Android.App.Application.Context.Resources.GetString(id);

    }
#endif
}