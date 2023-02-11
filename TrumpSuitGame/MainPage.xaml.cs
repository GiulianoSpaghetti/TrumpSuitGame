using org.altervista.numerone.framework;

namespace TrumpSuitGame;

public partial class MainPage : ContentPage
{
    private static Giocatore g, cpu, primo, secondo, temp;
    private static Mazzo m;
    private static Carta c, c1, briscola;
    private static bool aggiornaNomi = false;
    private static UInt16 secondi = 5;
    private static bool avvisaTalloneFinito = true, briscolaDaPunti = false;
    private static IDispatcherTimer t;
    private TapGestureRecognizer gesture;
    private ElaboratoreCarteBriscola e;
    public MainPage()
    {
        this.InitializeComponent();
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        secondi = (UInt16)Preferences.Get("secondi", 5);

        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        m = new Mazzo(e);
        Carta.Inizializza(40, CartaHelperBriscola.GetIstanza(e));
                g = new Giocatore(new GiocatoreHelperUtente(), Preferences.Get("nomeUtente", ""), 3);
        cpu = new Giocatore(new GiocatoreHelperCpu(ElaboratoreCarteBriscola.GetCartaBriscola()), Preferences.Get("nomeCpu", ""), 3);
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
        PuntiCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{cpu.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {cpu.GetPunteggio()}";
        PuntiUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{g.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {g.GetPunteggio()}";
        NelMazzoRimangono.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_nel_mazzo_rimangono)}{m.GetNumeroCarte()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_nel_mazzo_rimangono)}";
        CartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.il_seme_di_briscola_e)}: {briscola.GetSemeStr()}";
        
        VisualizzaImmagine(Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola()).GetID(), 4, 4, false);

        t = Dispatcher.CreateTimer();
        t.Interval = TimeSpan.FromSeconds(secondi);
        t.Tick += (s, e) =>
        {
            if (aggiornaNomi)
            {
                NomeUtente.Text = g.GetNome();
                NomeCpu.Text = cpu.GetNome();
                aggiornaNomi = false;
            }
            Informazioni.Text = "";
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
            PuntiCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{cpu.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {cpu.GetPunteggio()}";
            PuntiUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{g.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {g.GetPunteggio()}";
            if (AggiungiCarte())
            {
                NelMazzoRimangono.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_nel_mazzo_rimangono)}{m.GetNumeroCarte()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_nel_mazzo_rimangono)}";
                CartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.il_seme_di_briscola_e)}: {briscola.GetSemeStr()}";
                if (m.GetNumeroCarte() == 0)
                {
                    ((Image)this.FindByName(Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola()).GetID())).IsVisible = false;
                    NelMazzoRimangono.IsVisible = false;
                    if (avvisaTalloneFinito)
                        Informazioni.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.il_mazzo_e_finito)}";
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
                        Informazioni.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_cpu_gioca)}{cpu.GetCartaGiocata().GetValore() + 1}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_cpu_gioca)}{App.GetResource(TrumpSuitGame.Resource.String.briscola)}";
                    else if (cpu.GetCartaGiocata().GetPunteggio() > 0)
                        Informazioni.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_cpu_gioca)}{cpu.GetCartaGiocata().GetValore() + 1}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_cpu_gioca)}{cpu.GetCartaGiocata().GetSemeStr()}";
                }

            }
            else
            {
                Navigation.PushAsync(new GreetingsPage(g, cpu));
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

    private void OnInfo_Click(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InfoPage());
    }

    private void  NuovaPartita()
    {
        e = new ElaboratoreCarteBriscola(briscolaDaPunti);
        m = new Mazzo(e);
        briscola = Carta.GetCarta(ElaboratoreCarteBriscola.GetCartaBriscola());
        g = new Giocatore(new GiocatoreHelperUtente(), g.GetNome(), 3);
        cpu = new Giocatore(new GiocatoreHelperCpu(ElaboratoreCarteBriscola.GetCartaBriscola()), cpu.GetNome(), 3);
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
        PuntiCpu.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{cpu.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {cpu.GetPunteggio()}";
        PuntiUtente.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_punti)}{g.GetNome()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_punti)}: {g.GetPunteggio()}";
        NelMazzoRimangono.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.prefisso_nel_mazzo_rimangono)}{m.GetNumeroCarte()}{App.GetResource(TrumpSuitGame.Resource.String.suffisso_nel_mazzo_rimangono)}";
        NelMazzoRimangono.IsVisible = true;
        CartaBriscola.Text = $"{App.GetResource(TrumpSuitGame.Resource.String.il_seme_di_briscola_e)}: {briscola.GetSemeStr()}";
        CartaBriscola.IsVisible = true;
        primo = g;
        secondo = cpu;
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
    private static bool AggiungiCarte()
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
        Image img = (Image)Sender;
        t.Start();
        GiocaUtente(img);
        if (secondo == cpu)
            GiocaCpu();
    }

    public static void AggiornaOpzioni()
    {
        g.SetNome(Preferences.Get("nomeUtente", ""));
        cpu.SetNome(Preferences.Get("nomeCpu", ""));
        secondi = (UInt16)Preferences.Get("secondi", 5);
        avvisaTalloneFinito = Preferences.Get("avvisaTalloneFinito", true);
        briscolaDaPunti = Preferences.Get("briscolaDaPunti", false);
        t.Interval = TimeSpan.FromSeconds(secondi);
        aggiornaNomi = true;
    }
}