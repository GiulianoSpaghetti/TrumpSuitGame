namespace TrumpSuitGame;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
#if ANDROID
        Title = App.GetResource(Resource.String.informazioni);
#else
        Title = $"{App.d["Informazioni"]}";
#endif
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri("https://github.com/numerunix/TrumpSuitGame"));
    }

}