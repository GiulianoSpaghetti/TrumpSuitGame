using TrumpSuitGame;

namespace TrumpSuitGame;

public partial class AppShell : Shell
{


    public static Boolean aggiorna = false;
    public AppShell()
    {
    InitializeComponent();
    #if ANDROID
        scapplicazione.Title = App.GetResource(Resource.String.applicazione);
        scopzioni.Title = App.GetResource(Resource.String.opzioni);
        scinformazioni.Title = App.GetResource(Resource.String.informazioni);
    #else
        scapplicazione.Title="Application";
        scopzioni.Title="Options";
        scinformazioni.Title="Informations";
    #endif
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
{
    string current = args.Current.Location.ToString();
    if (current is "//Main")
        if (aggiorna)
        {
            MainPage.main.AggiornaOpzioni();
            aggiorna = false;
        }
    base.OnNavigated(args);

}
}
