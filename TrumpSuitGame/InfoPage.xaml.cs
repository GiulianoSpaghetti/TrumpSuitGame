namespace TrumpSuitGame;

public partial class InfoPage : ContentPage
{
    public readonly Uri uri = new Uri("https://github.com/GiulianoSpaghetti/TrumpSuitGame");

    private String s;
    public InfoPage()
	{
		InitializeComponent();
        Title = $"{App.Dictionary["Informazioni"]}";
        s = "";
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(uri);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Preferences.Get("mazzo", "Napoletano") == "Gatti")
        {
            s = $"Card are made by my camera, using the corners and the deck of a french deck taken from italian journal \"Carte\"";
        }
        else
            s = "Card images are the property of Modiano";

        Credits.Text = $"{s}, .NET and MAUI are properties of Microsoft Corporation";
    }
}
