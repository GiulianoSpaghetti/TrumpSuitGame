using CommunityToolkit.Maui.Alerts;
using org.altervista.numerone.framework;
using System.Collections.ObjectModel;
using System.Threading;

namespace TrumpSuitGame;

public partial class OpzioniPage : ContentPage
{
    private bool briscolaDaPunti;
    private bool avvisaTalloneFinito;
    private UInt16 secondi;
    private UInt16 livello;
    private Stream s;
    public OpzioniPage()
    {
        InitializeComponent();
        livello = (UInt16)Preferences.Get("livello", 3);
        txtNomeUtente.Text = Preferences.Get("nomeUtente", "numerone");
        txtCpu.Text = Preferences.Get("nomeCpu", "numerona");
        secondi = (UInt16)Preferences.Get("secondi", 5);
        txtSecondi.Text = secondi.ToString();
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        swAvvisaTallone.IsToggled = avvisaTalloneFinito;
        swCartaBriscola.IsToggled = briscolaDaPunti;
        pkrlivello.SelectedIndex = livello - 1;
        Title = $"{App.Dictionary["Opzioni"]}";
        opNomeCpu.Text = $"{App.Dictionary["NomeCpu"]}: ";
        opNomeUtente.Text= $"{App.Dictionary["NomeUtente"]}: ";
        lbSecondi.Text= $"{App.Dictionary["secondi"]}";
        lbLivello.Text = $"{App.Dictionary["IlLivelloE"]}";
        lbCartaBriscola.Text= $"{App.Dictionary["BriscolaDaPunti"]}";
        lbAvvisaTallone.Text= $"{App.Dictionary["AvvisaTallone"]}";
        btnOk.Text= $"{App.Dictionary["Salva"]}";
        lblMazzi.Text = $"{App.Dictionary["MazzoAlternativo"]}";
        ObservableCollection<string> _mazzi = new ObservableCollection<string>();
        try
        {
            s=FileSystem.OpenAppPackageFileAsync("Mazzi\\Gatti\\0.png").Result;
            _mazzi.Add("Gatti");
            s.Close();
        }
        catch (AggregateException ex)
        {
         
        }
        _mazzi.Add("Napoletano");
        try
        {
            s = FileSystem.OpenAppPackageFileAsync("Mazzi\\Siciliano\\0.png").Result;
            _mazzi.Add("Siciliano");
            s.Close();
        }
        catch (AggregateException ex)
        {

        }
        try
        {
            s = FileSystem.OpenAppPackageFileAsync("Mazzi\\Trevigiano\\0.png").Result;
            _mazzi.Add("Trevigiano");
            s.Close();
        }
        catch (AggregateException ex)
        {

        }
        pkrmazzi.ItemsSource = _mazzi;
        string mazzo = Preferences.Get("mazzo", "Napoletano");
        pkrmazzi.SelectedItem = mazzo;

    }

    public async void OnOk_Click(Object source, EventArgs evt)
    {
	UInt16 sec;
        Preferences.Set("nomeUtente", txtNomeUtente.Text);
        Preferences.Set("nomeCpu", txtCpu.Text);
        briscolaDaPunti = swCartaBriscola.IsToggled;
        Preferences.Set("briscolaDaPunti", briscolaDaPunti);
        avvisaTalloneFinito = swAvvisaTallone.IsToggled;
        Preferences.Set("avvisaTalloneFinito", avvisaTalloneFinito);

        try
        {
            sec = UInt16.Parse(txtSecondi.Text);
        }
        catch (FormatException ex)
        {
            await Snackbar.Make($"{App.Dictionary["ValoreNonValido"]}").Show(App.cancellationTokenSource.Token);
            return;
        }
        catch (OverflowException ex)
        {
            await Snackbar.Make($"{App.Dictionary["ValoreNonValido"]}").Show(App.cancellationTokenSource.Token);
            return;
        }
        if (sec <1 || sec > 10)
        {
            await Snackbar.Make($"{App.Dictionary["ValoreNonValido"]}").Show(App.cancellationTokenSource.Token);
            txtSecondi.Text = ( (UInt16) Preferences.Get("secondi", 5)).ToString();
            return;
        }
        secondi=sec;
        Preferences.Set("secondi", secondi);
        Preferences.Set("livello", pkrlivello.SelectedIndex + 1);
        Preferences.Set("mazzo", pkrmazzi.SelectedItem==null?"Naèpoletano": pkrmazzi.SelectedItem.ToString());
#if ANDROID
        AppShell.aggiorna = true;
#else
        AppShellWindows.aggiorna = true;
#endif
        await Shell.Current.GoToAsync("//Main");
    }

}
