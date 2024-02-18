using System.Media;

namespace Pexeso
{
    public partial class Pexeso : Form
    {
        public Button prvniStisknuteTlacitko;
        public Button druheStisknuteTlacitko;
        private int _cas;
        private int _pocetTahu = 1;
        private SoundPlayer _soundPlayer_vyhra;
        private SoundPlayer _soundPlayer_dobre;
        private SoundPlayer _soundPlayer_spatne;
        public Pexeso()
        {
            InitializeComponent();

            _soundPlayer_vyhra = new SoundPlayer(Properties.Resources.tadaa_47995);
            _soundPlayer_dobre = new SoundPlayer(Properties.Resources.success_1_6297);
            _soundPlayer_spatne = new SoundPlayer(Properties.Resources.negative_beeps_6008);
            Button[] buttons = new Button[16];
            int i = 0;

            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    buttons[i] = c as Button;
                    i++;
                }
            }

            zamichej(buttons);
        }

        private void pexeso_Click(object sender, EventArgs e)
        {
            // p�etypov�n� na button
            Button button = (Button)sender;



            //kdy� je na tla��tko kliknut� provede se:
            if (button != null)

            {   // pokud je text tla��tka ji� �ern� - na tla��tko u� bylo kliknut� - nic se ned�je
                if (button.ForeColor == Color.Black)
                    return;
                //  prvn� tla��tko
                if (prvniStisknuteTlacitko == null)
                {
                    prvniStisknuteTlacitko = button;
                    prvniStisknuteTlacitko.ForeColor = Color.Black;
                    return;
                }
                // druh� tla��tko

                if (druheStisknuteTlacitko == null)
                {
                    druheStisknuteTlacitko = button;
                    druheStisknuteTlacitko.ForeColor = Color.Black;
                }



                //po��t�n� tah�
                _pocetTahu++;
                casovac.Start();
                // kdy� jsou ob� tla��tka stisknut� - porovn� se text
                if (prvniStisknuteTlacitko != null && druheStisknuteTlacitko != null)
                {   // rovnaj� se - resetuj� se hodnoty
                    if (prvniStisknuteTlacitko.Text == druheStisknuteTlacitko.Text)
                    {
                        _soundPlayer_dobre.Play();
                        prvniStisknuteTlacitko = null;
                        druheStisknuteTlacitko = null;

                    }

                    else
                    {
                        _soundPlayer_spatne.Play();
                        otoceni.Start();
                    }

                    zkontroluj_viteze();
                }
            }

        }
        private void zkontroluj_viteze()
        {   //projde v�echny tla��tka typu button
            foreach (Button button in Controls.OfType<Button>())
            {
                if (button.ForeColor != Color.Black)
                {

                    return; // Pokud alespo� jedno tla��tko nen� �ern�, nen� v�hra
                }
            }
            //zastav� se �asova� a vyp�e se hl�ka
            casovac.Stop();
            _soundPlayer_vyhra.Play();
            Close.Enabled = true;
            Reset.Enabled = true;

            MessageBox.Show($"Vyhr�l jsi za {_cas} sekund, za pou�it� {_pocetTahu} tah�.");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {  //po��t�n� �asu
            _cas++;
        }
        static void zamichej(Button[] poleTlacitek)
        {
            //deklarace prom�nn� n
            int n = poleTlacitek.Length - 1;
            // vytvo�en� random gener�toru
            Random generator = new Random();
            // dokud je n v�t�� ne� nula cyklus
            while (n > 0)
            { // v�b�r n�hodn�ho prvku od 0 do n
                int nahodnyPrvek = generator.Next(0, n);
                //deklarace do�asnn� prom�nn�, kter� se rovn� prvku na pozici n
                string docasnaPromenna = poleTlacitek[n].Text;
                //na pozici n um�st�n� ��sla z pozice n�hodn� vybran�ho prvku
                poleTlacitek[n].Text = poleTlacitek[nahodnyPrvek].Text;
                //na pozici n�hodn� vybran�ho prvku um�st�n� ��sla z pozice n = dokon�en� p�ehozen�
                poleTlacitek[nahodnyPrvek].Text = docasnaPromenna;
                // sn�en� n o 1
                n--;
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Reset_Click(object sender, EventArgs e)
        {  
            foreach (Button button in Controls.OfType<Button>())
            {
                button.ForeColor = button.BackColor;

            }
            prvniStisknuteTlacitko = null;
            druheStisknuteTlacitko = null;
            _pocetTahu = 1;
            
            Close.Enabled = false;
            Reset.Enabled = false;
            _cas = 0;
            Button[] buttons = new Button[16];
            int i = 0;

            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    buttons[i] = c as Button;
                    i++;
                }
            }

            zamichej(buttons);

        }
        private void otoceni_Tick(object sender, EventArgs e)
        {
            otoceni.Stop();
            prvniStisknuteTlacitko.ForeColor = prvniStisknuteTlacitko.BackColor;
            druheStisknuteTlacitko.ForeColor = druheStisknuteTlacitko.BackColor;


            prvniStisknuteTlacitko = null;
            druheStisknuteTlacitko = null;


        }
    }
}