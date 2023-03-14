namespace TrumpSuitGame;

public partial class App : Application
{
	public static string piattaforma;
    public App()
    {
        InitializeComponent();
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
