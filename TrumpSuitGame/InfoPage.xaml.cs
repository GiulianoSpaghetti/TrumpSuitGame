namespace TrumpSuitGame;

public partial class InfoPage : ContentPage
{
    public readonly Uri uri = new Uri("https://github.com/GiulianoSpaghetti/TrumpSuitGame");

    public InfoPage()
	{
		InitializeComponent();
        Title = $"{App.Dictionary["Informazioni"]}";
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(uri);
    }

}
