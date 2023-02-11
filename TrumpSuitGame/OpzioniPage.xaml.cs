namespace TrumpSuitGame;

public partial class OpzioniPage : ContentPage
{
    private bool briscolaDaPunti;
    private bool avvisaTalloneFinito;
    private UInt16 secondi;
	public OpzioniPage()
	{
		InitializeComponent();
        Title = App.GetResource(Resource.String.opzioni);
        txtNomeUtente.Text = Preferences.Get("nomeUtente", "");
        txtCpu.Text = Preferences.Get("nomeCpu", "");
        secondi = (UInt16) Preferences.Get("secondi", 5);
        txtSecondi.Text=secondi.ToString();
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        opNomeCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.nome_cpu)}: ";
        opNomeUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.nome_utente)}: ";
        Secondi.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.secondi)}";
        lbCartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.carta_briscola)}";
        lbAvvisaTallone.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.avvisa_tallone)}";
        btnOk.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.salva)}";
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
            txtSecondi.Text = App.GetResource(TrumpSuitGame.Resource.String.valore_non_valido);
            return;
        }
        if (secondi > 10)
        {
            txtSecondi.Text = App.GetResource(TrumpSuitGame.Resource.String.valore_troppo_alto);
            return;
        }
        Preferences.Set("secondi", secondi);
        AppShell.aggiorna = true;
        await Shell.Current.GoToAsync("//Main");
    }
}