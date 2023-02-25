using org.altervista.numerone.framework;

namespace TrumpSuitGame;

public partial class GreetingsPage : ContentPage
{
    private Giocatore g, cpu;
	public GreetingsPage(Giocatore g, Giocatore cpu)
	{
		InitializeComponent();
#if ANDROID
        Title = App.GetResource(Resource.String.gioco_finito);
#else
        Title = "Game over";
#endif
        String s;
        this.g = g;
        this.cpu= cpu;
        if (g.GetPunteggio() == cpu.GetPunteggio())
#if ANDROID
            s = App.GetResource(TrumpSuitGame.Resource.String.gioco_pareggiato);
#else
            s = "Game is drown";
#endif
        else
        {
            if (g.GetPunteggio() > cpu.GetPunteggio())
#if ANDROID
                s = App.GetResource(TrumpSuitGame.Resource.String.hai_vinto);
#else
                s = "You won";
#endif
            else
#if ANDROID
                s = App.GetResource(TrumpSuitGame.Resource.String.hai_perso);
            s = $"{s} {App.GetResource(TrumpSuitGame.Resource.String.per)} {Math.Abs(g.GetPunteggio() - cpu.GetPunteggio())} {App.GetResource(TrumpSuitGame.Resource.String.punti)}";
        }
        fpRisultrato.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.gioco_finito)} {s} {App.GetResource(TrumpSuitGame.Resource.String.nuovo_gioco)}";
        btnNo.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.no)}";
        btnShare.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.condividi)}";
#else
                s = "You loose";
            s = $"{s} for {Math.Abs(g.GetPunteggio() - cpu.GetPunteggio())} points";
        }
        fpRisultrato.Text = $"The game is over. {s} Do you want to play again?";
        btnNo.Text = "No";
        btnShare.Text = "Share";
#endif
    }


    private async void OnFPShare_Click(object sender, EventArgs e)
    {
#if ANDROID
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text={App.GetResource(TrumpSuitGame.Resource.String.with_the_game)}{g.GetNome()}%20{App.GetResource(TrumpSuitGame.Resource.String.versus)}%20{cpu.GetNome()}%20{App.GetResource(TrumpSuitGame.Resource.String.is_finished)}%20{g.GetPunteggio()}%20{App.GetResource(TrumpSuitGame.Resource.String.at)}%20{cpu.GetPunteggio()}%20{App.GetResource(TrumpSuitGame.Resource.String.on_platform)}%20{App.piattaforma}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FTrumpSuitGame"));
#else
        await Launcher.Default.OpenAsync(new Uri($"https://twitter.com/intent/tweet?text=Whith%20the%20game%20Trump%20Suit%20Game%20the%20match%20{g.GetNome()}%20versus%20{cpu.GetNome}%20os%20finished%20{g.GetPunteggio()}%20at{cpu.GetPunteggio()}%20on%20platform%20{App.piattaforma}))&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2FTrumpSuitGame"));
#endif
        btnShare.IsEnabled = false;
    }

    private void OnCancelFp_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}
