namespace TrumpSuitGameGeordi
{
    public partial class AppShell : Shell
    {
        public static Boolean aggiorna = false;
        public AppShell()
        {
            InitializeComponent();
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
}
