using org.altervista.numerone.framework;

namespace TrumpSuitGame;

public partial class OpzioniPage : ContentPage
{
    private static bool briscolaDaPunti;
    private static bool avvisaTalloneFinito;
    private static UInt16 secondi;
    private static UInt16 livello;
	public OpzioniPage()
	{
		InitializeComponent();
        livello = (UInt16)Preferences.Get("livello", 3);
        txtNomeUtente.Text = Preferences.Get("nomeUtente", "numerone");
        txtCpu.Text = Preferences.Get("nomeCpu", "numerona");
        secondi = (UInt16) Preferences.Get("secondi", 5);
        txtSecondi.Text=secondi.ToString();
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        cbAvvisaTallone.IsChecked = avvisaTalloneFinito;
        cbCartaBriscola.IsChecked = briscolaDaPunti;
        pkrlivello.SelectedIndex = livello - 1;
#if ANDROID
        Title = App.GetResource(Resource.String.opzioni);
        opNomeCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.nome_cpu)}: ";
        opNomeUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.nome_utente)}: ";
        lbSecondi.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.secondi)}";
        lbLivello.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.level)}: ";
        lbCartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.carta_briscola)}";
        lbAvvisaTallone.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.avvisa_tallone)}";
        btnOk.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.salva)}";
#else
        Title = "Options";
        opNomeCpu.Text="CPU Name: ";
        opNomeUtente.Text="Username: ";
        lbSecondi.Text="Seconds during the which show the plays";
        lbLivello.Text = "The level is: ";
        lbCartaBriscola.Text="The card designating the trump suit can give points";
        lbAvvisaTallone.Text="Alerts when the deck ends";
        btnOk.Text="Save";
#endif
    }

    public async void OnOk_Click(Object source, EventArgs evt)
    {
        Preferences.Set("nomeUtente", txtNomeUtente.Text);
        Preferences.Set("nomeCpu", txtCpu.Text);
        if (cbCartaBriscola.IsChecked == false)
            briscolaDaPunti = false;
        else
            briscolaDaPunti = true;
        Preferences.Set("briscolaDaPunti", briscolaDaPunti);
        if (cbAvvisaTallone.IsChecked == false)
            avvisaTalloneFinito = false;
        else
            avvisaTalloneFinito = true;
        Preferences.Set("avvisaTalloneFinito", avvisaTalloneFinito);

        try
        {
            secondi = UInt16.Parse(txtSecondi.Text);
        }
        catch (FormatException ex)
        {
#if ANDROID
            txtSecondi.Text = App.GetResource(TrumpSuitGame.Resource.String.valore_non_valido);
            return;
        }
        if (secondi > 10)
        {
            txtSecondi.Text = App.GetResource(TrumpSuitGame.Resource.String.valore_troppo_alto);
#else
            txtSecondi.Text="Invalid rvalue";
#endif
            return;
        }
        Preferences.Set("secondi", secondi);
        Preferences.Set("livello", pkrlivello.SelectedIndex + 1);
#if WINDOWS
        AppShellWindows.aggiorna = true;
#else
        AppShell.aggiorna = true;
#endif
        await Shell.Current.GoToAsync("//Main");
    }

}