namespace TrumpSuitGame;

public partial class AppShell : Shell
{

    public static Boolean aggiorna=false;
    public AppShell()
    {
        InitializeComponent();
        scapplicazione.Title = App.GetResource(Resource.String.applicazione);
        scopzioni.Title = App.GetResource(Resource.String.opzioni);
        scinformazioni.Title = App.GetResource(Resource.String.informazioni);
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        string current = args.Current.Location.ToString();
        if (current is "//Main")
            if (aggiorna)
            {
                MainPage.AggiornaOpzioni();
                aggiorna = false;
            }
            base.OnNavigated(args);

    }
}
