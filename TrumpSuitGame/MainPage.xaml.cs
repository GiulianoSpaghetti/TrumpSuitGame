using CommunityToolkit.Maui.Alerts;
using org.altervista.numerone.framework;
using System.Reflection;
using Snackbar = CommunityToolkit.Maui.Alerts.Snackbar;

namespace TrumpSuitGame;

public partial class MainPage : ContentPage
{
    private static Giocatore g, cpu, primo, secondo, temp;
    private static Mazzo m;
    private static Carta c, c1, briscola;
    private static bool aggiornaNomi = false, primoUtente=true;
    private static UInt16 secondi = 5;
    private static bool avvisaTalloneFinito = true, briscolaDaPunti = false;
    private static IDispatcherTimer t;
    private static TapGestureRecognizer gesture;
    private static ElaboratoreCarteBriscola e;
    private static GiocatoreHelperCpu helper;
    private static CancellationTokenSource cancellationTokenSource;
    public static MainPage main;
    public MainPage()
    {
        this.InitializeComponent();
        main = this;
        cancellationTokenSource = new CancellationTokenSource();
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        secondi = (UInt16)Preferences.Get("secondi", 5);
        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        m = new Mazzo(e);
        Carta.Inizializza(40, CartaHelperBriscola.GetIstanza(e));
        g = new Giocatore(new GiocatoreHelperUtente(), Preferences.Get("nomeUtente", "numerone"), 3);
        switch (Preferences.Get("livello", 3))
        {
            case 1: helper = new GiocatoreHelperCpu0(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
            case 2: helper = new GiocatoreHelperCpu1(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
            default: helper = new GiocatoreHelperCpu2(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
        }
        cpu = new Giocatore(helper, Preferences.Get("nomeCpu", "numerona"), 3);
        primo = g;
        secondo = cpu;
        briscola = Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola());
        gesture = new TapGestureRecognizer();
        gesture.Tapped += Image_Tapped;
        for (UInt16 i = 0; i < 3; i++)
        {
            g.AddCarta(m);
            cpu.AddCarta(m);

        }
        VisualizzaImmagine(g.GetID(0), 1, 0, true);
        VisualizzaImmagine(g.GetID(1), 1, 1, true);
        VisualizzaImmagine(g.GetID(2), 1, 2, true);

        NomeUtente.Text = g.GetNome();
        NomeCpu.Text = cpu.GetNome();
#if ANDROID
        PuntiCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{cpu.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {cpu.GetPunteggio()}";
        PuntiUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{g.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {g.GetPunteggio()}";
        NelMazzoRimangono.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_nel_mazzo_rimangono)}{m.GetNumeroCarte()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_nel_mazzo_rimangono)}";
        CartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.il_seme_di_briscola_e)}: {briscola.GetSemeStr()}";
        Level.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.level)}: {helper.GetLivello()}";
#else
        PuntiCpu.Text = $"{cpu.GetNome()} points: {cpu.GetPunteggio()}";
        PuntiUtente.Text = $"{g.GetNome()} points: {g.GetPunteggio()}";
        NelMazzoRimangono.Text = $"There are still {m.GetNumeroCarte()} cards in the deck";
        CartaBriscola.Text = $"The trump suit is: {briscola.GetSemeStr()}";
        Level.Text = $"The level is: {helper.GetLivello()}";
#endif
        VisualizzaImmagine(Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola()).GetID(), 4, 4, false);

        t = Dispatcher.CreateTimer();
        t.Interval = TimeSpan.FromSeconds(secondi);
        t.Tick += (s, e) =>
        {
            string snack = "";
            if (aggiornaNomi)
            {
                NomeUtente.Text = g.GetNome();
                NomeCpu.Text = cpu.GetNome();
                aggiornaNomi = false;
            }
            c = primo.GetCartaGiocata();
            c1 = secondo.GetCartaGiocata();
            ((Image)this.FindByName(c.GetID())).IsVisible = false;
            ((Image)this.FindByName(c1.GetID())).IsVisible = false;
            if ((c.CompareTo(c1) > 0 && c.StessoSeme(c1)) || (c1.StessoSeme(briscola) && !c.StessoSeme(briscola)))
            {
                temp = secondo;
                secondo = primo;
                primo = temp;
            }

            primo.AggiornaPunteggio(secondo);
#if ANDROID
            PuntiCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{cpu.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {cpu.GetPunteggio()}";
            PuntiUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{g.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {g.GetPunteggio()}";
#else
            PuntiCpu.Text = $"{cpu.GetNome()} points: {cpu.GetPunteggio()}";
            PuntiUtente.Text = $"{g.GetNome()} points: {g.GetPunteggio()}";
#endif
            if (AggiungiCarte())
            {
#if ANDROID
                NelMazzoRimangono.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_nel_mazzo_rimangono)}{m.GetNumeroCarte()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_nel_mazzo_rimangono)}";
                CartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.il_seme_di_briscola_e)}: {briscola.GetSemeStr()}";
#else
                NelMazzoRimangono.Text = $"There are still {m.GetNumeroCarte()} cards in the deck";
                CartaBriscola.Text = $"The trump suit is: {briscola.GetSemeStr()}";
#endif
                if (m.GetNumeroCarte() == 0)
                {
                    ((Image)this.FindByName(Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola()).GetID())).IsVisible = false;
                    NelMazzoRimangono.IsVisible = false;
                    if (avvisaTalloneFinito)
#if ANDROID
                        snack += $"{App.GetResource(TrumpSuitGame.Resource.String.il_mazzo_e_finito)}\n";
#else
                        snack += "The deck is finished\n";
#endif
                }
                for (UInt16 i = 0; i < g.GetNumeroCarte(); i++)
                {
                    VisualizzaImmagine(g.GetID(i), 1, i, true);
                    ((Image)this.FindByName("Cpu" + i)).IsVisible = true;
                }
                if (cpu.GetNumeroCarte() == 2)
                    Cpu2.IsVisible = false;
                if (cpu.GetNumeroCarte() == 1)
                    Cpu1.IsVisible = false;

                if (primo == cpu)
                {
                    GiocaCpu();
                    if (cpu.GetCartaGiocata().StessoSeme(briscola))
#if ANDROID
                        snack += $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_cpu_gioca)}{cpu.GetCartaGiocata().GetValore() + 1}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_cpu_gioca)}{App.GetResource(TrumpSuitGame.Resource.String.briscola)}\n";
#else
                        snack += $"The CPU has played the {cpu.GetCartaGiocata().GetValore() + 1} of trump\n";
#endif
                    else if (cpu.GetCartaGiocata().GetPunteggio() > 0)
#if ANDROID
                        snack += $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_cpu_gioca)}{cpu.GetCartaGiocata().GetValore() + 1}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_cpu_gioca)}{cpu.GetCartaGiocata().GetSemeStr()}\n";
#else
                        snack += $"The CPU has played the {cpu.GetCartaGiocata().GetValore() + 1} of {cpu.GetCartaGiocata().GetSemeStr()}\n";
#endif
                    if (snack != "")
                        Snackbar.Make(snack).Show(cancellationTokenSource.Token);
                }

            }
            else
            {
                Navigation.PushAsync(new GreetingsPage(g, cpu, helper));
                NuovaPartita();
            }
            t.Stop();
        };
    }

    private void VisualizzaImmagine(String id, UInt16 i, UInt16 j, bool abilitaGesture)
    {
        Image img;
        img = (Image)this.FindByName(id);
        Applicazione.SetRow(img, i);
        Applicazione.SetColumn(img, j);
        img.IsVisible = true;
        if (abilitaGesture)
            img.GestureRecognizers.Add(gesture);
        else
            img.GestureRecognizers.Clear();

    }
    private void GiocaUtente(Image img)
    {
        UInt16 quale = 0;
        Image img1;
        for (UInt16 i = 1; i < g.GetNumeroCarte(); i++)
        {
            img1 = (Image)this.FindByName(g.GetID(i));
            if (img.Id == img1.Id)
                quale = i;
        }
        VisualizzaImmagine(g.GetID(quale), 2, 0, false);
        g.Gioca(quale);
    }

    private void NuovaPartita()
    {
        Image img;
        UInt16 level = (UInt16)Preferences.Get("livello", 3);
        if (level != helper.GetLivello()) {
#if ANDROID
            Snackbar.Make(App.GetResource(TrumpSuitGame.Resource.String.new_level_is_starting)).Show(cancellationTokenSource.Token);
#else
            Snackbar.Make("The level has changed, a new game is starting").Show(cancellationTokenSource.Token);
#endif
        }
        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        m = new Mazzo(e);
        briscola = Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola());
        g = new Giocatore(new GiocatoreHelperUtente(), g.GetNome(), 3);
        switch (level)
        {
            case 1: helper = new GiocatoreHelperCpu0(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
            case 2: helper = new GiocatoreHelperCpu1(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
            default: helper = new GiocatoreHelperCpu2(ElaboratoreCarteBriscola.GetCartaBriscola()); break;
        }
        cpu = new Giocatore(helper, cpu.GetNome(), 3);
        for (UInt16 i = 0; i < 40; i++)
        {
            img = (Image)this.FindByName(Carta.GetCarta(i).GetID());
            img.IsVisible = false;
        }
        for (UInt16 i = 0; i < 3; i++)
        {
            g.AddCarta(m);
            cpu.AddCarta(m);
        }
        VisualizzaImmagine(g.GetID(0), 1, 0, true);
        VisualizzaImmagine(g.GetID(1), 1, 1, true);
        VisualizzaImmagine(g.GetID(2), 1, 2, true);

        Cpu0.IsVisible = true;
        Cpu1.IsVisible = true;
        Cpu2.IsVisible = true;

#if ANDROID
        PuntiCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{cpu.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {cpu.GetPunteggio()}";
        PuntiUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{g.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {g.GetPunteggio()}";
        NelMazzoRimangono.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_nel_mazzo_rimangono)}{m.GetNumeroCarte()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_nel_mazzo_rimangono)}";
        CartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.il_seme_di_briscola_e)}: {briscola.GetSemeStr()}";
        Level.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.level)}: {helper.GetLivello()}";
#else
        PuntiCpu.Text = $"{cpu.GetNome()} points: {cpu.GetPunteggio()}";
        PuntiUtente.Text = $"{g.GetNome()} points: {g.GetPunteggio()}";
        NelMazzoRimangono.Text = $"There are still {m.GetNumeroCarte()} cards in the deck";
        CartaBriscola.Text = $"The trump suit is: {briscola.GetSemeStr()}";
        Level.Text = $"The level is: {helper.GetLivello()}";
#endif
        NelMazzoRimangono.IsVisible = true;
        CartaBriscola.IsVisible = true;
        primoUtente = !primoUtente;
        if (primoUtente)
        {
            primo = g;
            secondo = cpu;
        } else
        {
            primo = cpu;
            secondo = g;
            GiocaCpu();
        }
        VisualizzaImmagine(Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola()).GetID(), 4, 4, false);
        Applicazione.IsVisible = true;
    }
    private void GiocaCpu()
    {
        UInt16 quale = 0;
        Image img1 = Cpu0;
        if (primo == cpu)
            cpu.Gioca(0);
        else
            cpu.Gioca(0, g);
        quale = cpu.GetICartaGiocata();
        img1 = (Image)this.FindByName("Cpu" + quale);
        img1.IsVisible = false;
        VisualizzaImmagine(cpu.GetCartaGiocata().GetID(), 2, 2, false);
    }
    private bool AggiungiCarte()
    {
        try
        {
            primo.AddCarta(m);
            secondo.AddCarta(m);
        }
        catch (IndexOutOfRangeException e)
        {
            return false;
        }
        return true;
    }

    private void Image_Tapped(object Sender, EventArgs arg)
    {
        if (t.IsRunning)
            return;
        Image img = (Image)Sender;
        t.Start();
        GiocaUtente(img);
        if (secondo == cpu)
            GiocaCpu();
    }

    public void AggiornaOpzioni()
    {
        UInt16 level = (UInt16)Preferences.Get("livello", 3);
        g.SetNome(Preferences.Get("nomeUtente", ""));
        cpu.SetNome(Preferences.Get("nomeCpu", ""));
        secondi = (UInt16)Preferences.Get("secondi", 5);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        t.Interval = TimeSpan.FromSeconds(secondi);
        aggiornaNomi = true;
        if (level != helper.GetLivello())
            NuovaPartita();
    }

    private void OnNuovaPartita_Click(object sender, EventArgs evt)
    {
        NuovaPartita();
    }
    private void OnInfo_Click(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InfoPage());
    }


    private void OnOpzioni_Click(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OpzioniPage());
    }

    private void OnCancelFp_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}