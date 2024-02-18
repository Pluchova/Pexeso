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
            // pøetypování na button
            Button button = (Button)sender;



            //když je na tlaèítko kliknuté provede se:
            if (button != null)

            {   // pokud je text tlaèítka již èerný - na tlaèítko už bylo kliknuté - nic se nedìje
                if (button.ForeColor == Color.Black)
                    return;
                //  první tlaèítko
                if (prvniStisknuteTlacitko == null)
                {
                    prvniStisknuteTlacitko = button;
                    prvniStisknuteTlacitko.ForeColor = Color.Black;
                    return;
                }
                // druhé tlaèítko

                if (druheStisknuteTlacitko == null)
                {
                    druheStisknuteTlacitko = button;
                    druheStisknuteTlacitko.ForeColor = Color.Black;
                }



                //poèítání tahù
                _pocetTahu++;
                casovac.Start();
                // když jsou obì tlaèítka stisknutá - porovná se text
                if (prvniStisknuteTlacitko != null && druheStisknuteTlacitko != null)
                {   // rovnají se - resetují se hodnoty
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
        {   //projde všechny tlaèítka typu button
            foreach (Button button in Controls.OfType<Button>())
            {
                if (button.ForeColor != Color.Black)
                {

                    return; // Pokud alespoò jedno tlaèítko není èerné, není výhra
                }
            }
            //zastaví se èasovaè a vypíše se hláška
            casovac.Stop();
            _soundPlayer_vyhra.Play();
            Close.Enabled = true;
            Reset.Enabled = true;

            MessageBox.Show($"Vyhrál jsi za {_cas} sekund, za použití {_pocetTahu} tahù.");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {  //poèítání èasu
            _cas++;
        }
        static void zamichej(Button[] poleTlacitek)
        {
            //deklarace promìnné n
            int n = poleTlacitek.Length - 1;
            // vytvoøení random generátoru
            Random generator = new Random();
            // dokud je n vìtší než nula cyklus
            while (n > 0)
            { // výbìr náhodného prvku od 0 do n
                int nahodnyPrvek = generator.Next(0, n);
                //deklarace doèasnné promìnné, která se rovná prvku na pozici n
                string docasnaPromenna = poleTlacitek[n].Text;
                //na pozici n umístìní èísla z pozice náhodnì vybraného prvku
                poleTlacitek[n].Text = poleTlacitek[nahodnyPrvek].Text;
                //na pozici náhodnì vybraného prvku umístìní èísla z pozice n = dokonèené pøehození
                poleTlacitek[nahodnyPrvek].Text = docasnaPromenna;
                // snížení n o 1
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