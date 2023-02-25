namespace TrumpSuitGame;

public partial class App : Application
{
	public static string piattaforma;
    public App()
    {
        InitializeComponent();
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            piattaforma = "Android";
            MainPage = new AppShell();
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            piattaforma = "Windows NT";
            MainPage = new AppShellWindows();
        }
        else
        {
            piattaforma = "unkonwn platform";
            MainPage = new AppShell();
        }
    }

#if ANDROID
    public static System.String GetResource(int id)
    {
        return Android.App.Application.Context.Resources.GetString(id);

    }
#endif
}
