using org.altervista.numerone.framework;

namespace TrumpSuitGame;

public partial class GreetingsPage : ContentPage
{
    private static Giocatore g, cpu;
    private static Mazzo m;
    private static UInt128 partite;
    private static UInt16 puntiUtente, puntiCpu;
	public GreetingsPage(Giocatore gi, Giocatore cp, GiocatoreHelperCpu helper, Mazzo mazzo, UInt16 vecchiPuntiUtente, UInt16 vecchiPuntiCpu, UInt128 NumeroPartite)
	{
		InitializeComponent();
#if ANDROID
        Title = App.GetResource(Resource.String.gioco_finito);
#else
        Title = $"{App.d["PartitaFinita"]}";
#endif
        String s, s1;
        g = gi;
        cpu= cp;
        m = mazzo;
        partite = NumeroPartite;
        puntiUtente = (UInt16) (vecchiPuntiUtente+g.GetPunteggio());
        puntiCpu = (UInt16) (vecchiPuntiCpu + cpu.GetPunteggio());
        if (puntiUtente == puntiCpu)
#if ANDROID
            s = App.GetResource(TrumpSuitGame.Resource.String.gioco_pareggiato);
#else
            s = $"{App.d["PartitaPatta"]}";
#endif
        else
        {
            if (puntiUtente > puntiCpu)
#if ANDROID
                s = App.GetResource(TrumpSuitGame.Resource.String.hai_vinto);
#else
                s = $"{App.d["HaiVinto"]}";
#endif
            else
#if ANDROID
                s = App.GetResource(TrumpSuitGame.Resource.String.hai_perso);
            s = $"{s} {App.GetResource(TrumpSuitGame.Resource.String.per)} {Math.Abs(puntiUtente - puntiCpu)} {App.GetResource(TrumpSuitGame.Resource.String.punti)}";
        }
        if (NumeroPartite%2==1)
            s1=App.GetResource(TrumpSuitGame.Resource.String.nuovo_gioco);
        else {
            s1=App.GetResource(TrumpSuitGame.Resource.String.start_second_game);
        }
        fpRisultrato.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.gioco_finito)} {s} {s1}?";
        btnNo.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.no)}";
        btnShare.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.condividi)}";
#else
                s = $"{App.d["HaiPerso"]}";
            s = $"{s} {App.d["per"]} {Math.Abs(puntiUtente - puntiCpu)} {App.d["punti"]}";
        }
        if (NumeroPartite % 2 == 1)
            s1 = App.d["EffettuaNuovaPartita"] as string;
        else
        {
            s1 = App.d["EffettuaSecondaPartita"] as string;
        }
        fpRisultrato.Text = $"{App.d["PartitaFinita"]}. {s}. {s1}?";
        btnNo.Text = $"{App.d["No"]}";
        btnShare.Text = $"{App.d["Condividi"]}";
#endif
        btnShare.IsEnabled = helper.GetLivello() == 3;
        btnShare.IsVisible = NumeroPartite%2==1;

    }


    private async void OnFPShare_Click(object sender, EventArgs e)
    {
#if ANDROID
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text={App.GetResource(TrumpSuitGame.Resource.String.with_the_game)}%20{partite+1}%20{g.GetNome()}%20{App.GetResource(TrumpSuitGame.Resource.String.versus)}%20{cpu.GetNome()}%20{App.GetResource(TrumpSuitGame.Resource.String.is_finished)}%20{puntiUtente}%20{App.GetResource(TrumpSuitGame.Resource.String.at)}%20{puntiCpu}%20{App.GetResource(TrumpSuitGame.Resource.String.on_platform)}%20{App.piattaforma}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FTrumpSuitGame"));
#else
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text={App.d["ColGioco"]}%20{partite+1}%20{g.GetNome()}%20{App.d["contro"]}%20{cpu.GetNome()}%20{App.d["efinito"]}%20{puntiUtente}%20{App.d["a"]}%20{puntiCpu}%20{App.d["piattaforma"]}%20{App.piattaforma}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FTrumpSuitGame"));
#endif
        btnShare.IsEnabled = false;
    }

    private void OnCancelFp_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}
