using System.Globalization;

namespace TrumpSuitGame;

public partial class App : Application
{
    private static string piattaforma;
    public static string Piattaforma
    {
        get => piattaforma;
    }
    private static ResourceDictionary dic;
    public static ResourceDictionary d
    {
        get => dic;
    }
    public static readonly CancellationTokenSource cancellationTokenSource= new CancellationTokenSource();

    public App()
    {
        InitializeComponent();
        try
        {
            dic = Resources[CultureInfo.CurrentCulture.TwoLetterISOLanguageName] as ResourceDictionary;

        }
        catch (Exception ex)
        {
            dic = Resources["en"] as ResourceDictionary;
        }
        piattaforma = DeviceInfo.Current.Model;
        if (piattaforma == "System Product Name")
            piattaforma = "Windows " + DeviceInfo.Current.VersionString;

    }

#if ANDROID
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
#else        
    protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShellWindows());
        }
#endif
}