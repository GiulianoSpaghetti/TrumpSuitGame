namespace TrumpSuitGame;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
        Title = App.GetResource(Resource.String.informazioni);
	}
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri("https://github.com/numerunix/TrumpSuitGame"));
    }

}