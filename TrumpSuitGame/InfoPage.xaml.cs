namespace TrumpSuitGameGeordi;

public partial class InfoPage : ContentPage
{
    public InfoPage()
    {
        InitializeComponent();
        Title = $"{App.d["Informazioni"]}";
        dedica.Text = $"{App.d["dedica"]}";
    }
    private async void OnSito_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(new Uri("https://github.com/numerunix/TrumpSuitGame"));
    }

}