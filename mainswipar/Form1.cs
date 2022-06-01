using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraT;

namespace mainswipar
{
    public partial class Form1 : Form
    {
        public Button[,] pct = new Button[30, 30];
        public Tabla crez = new Tabla(30);
        public int size;
        public bool Terminat = false;
        public int butoaneafis = 0;
        public int bombe = 30;
        public bool start = true;

        //schimbare


        public Form1()
        {
            InitializeComponent();
            
        }

        public void reset()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    pct[i, j].Hide();
                    crez.Grid[i, j].Steag = false;
                }
            }
            crez.wipe();
            button4.Show();
            button2.Show();
            button3.Show();
            button4.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            start = true;
            butoaneafis = 0;
        }

        public void pornire()
        {
            crez.Size = size;
            crez.PlaceBombs(bombe);
            //crez.GenerareTabla();
            aranjare();
        }

        public void aranjare()
        {
            int bs = panel1.Width / size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    pct[i, j] = new Button();
                    pct[i, j].Width = bs;
                    pct[i, j].Height = bs;
                    panel1.Controls.Add(pct[i, j]);
                    pct[i, j].Location = new Point(i * bs, j * bs);
                    //pct[i, j].Click += pct_click;
                    pct[i, j].MouseUp += pct_mouseup;
                    pct[i, j].Tag = new Point(i, j);
                    
                    
                    //pct[i, j].FlatStyle = FlatStyle.Flat;
                    //pct[i, j].FlatAppearance.BorderColor = Color.Purple;
                    //pct[i, j].FlatAppearance.BorderSize = 1;


                }
            }
        }

        private void pct_mouseup(object sender, MouseEventArgs e)
        {
            Button ButonApasat = (Button)sender;
            Point locatie = (Point)ButonApasat.Tag;

            int x = locatie.X;
            int y = locatie.Y;
            if (e.Button == MouseButtons.Right)
            {
                if (crez.Grid[x, y].Steag == false) { 
                pct[x, y].BackgroundImage = Image.FromFile("../../Image/flag.png");
                pct[x, y].BackgroundImageLayout = ImageLayout.Stretch;
                crez.Grid[x, y].Steag = true;
                }
                else
                {
                    pct[x, y].BackgroundImage = null;
                    crez.Grid[x, y].Steag = false;
                }
            }
            else if(e.Button == MouseButtons.Left&&crez.Grid[x,y].Steag==false)
            {

                butoaneafis++;
                //primul buton apasat

                while (start == true)
                {
                    if (crez.Grid[x, y].Bomba == false && crez.Grid[x, y].vecin == 0)
                        start = false;
                    else
                    {
                        crez.wipe();
                        crez.PlaceBombs(bombe);
                        //crez.GenerareTabla();
                    }
                }

                //actualizare text
                pct[x, y].Enabled = false;

                if (crez.Grid[x, y].vecin == 0)
                    CautareZero(x, y);

                if (crez.Grid[x, y].Bomba == true)
                {
                    pct[x, y].BackColor = Color.Red;
                }
                else
                {
                    pct[x, y].BackColor = Color.Green;
                    pct[x, y].Text = $"{crez.Grid[x, y].vecin}";
                    crez.Grid[x,y].Steag = false;
                    pct[x,y].BackgroundImage = null;
                }

                //Terminare
                if (crez.Grid[x, y].Bomba == true)
                {
                    afis();
                    MessageBox.Show("lose");
                    reset();

                }
                if (size * size - bombe == butoaneafis)
                {
                    afis();
                    MessageBox.Show("win");
                    reset();
                }

            }
        }

        public void afis()
        {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        pct[i, j].BackgroundImage = null;
                        if (crez.Grid[i, j].Bomba == true)
                            pct[i, j].BackColor = Color.Red;
                        else
                        {
                            pct[i, j].BackColor = Color.Green;
                            pct[i, j].Text = Convert.ToString(crez.Grid[i, j].vecin);
                        }
                    }
                }
        }

        public void CautareZero(int i, int j)
        {

            if (j - 1 >= 0 && j - 1 < size)
                if (pct[i, j - 1].Enabled == true)
                {
                    pct[i, j - 1].Enabled = false;
                    butoaneafis++;
                    pct[i, j - 1].BackColor = Color.Green;
                    pct[i, j - 1].Text = $"{crez.Grid[i, j - 1].vecin}";
                    crez.Grid[i, j - 1].Steag = false;
                    pct[i,j-1].BackgroundImage = null;
                    if (crez.Grid[i, j - 1].vecin == 0)
                        CautareZero(i, j - 1);

                }
            if (j + 1 >= 0 && j + 1 < size)
                if (pct[i, j + 1].Enabled == true)
                {
                    pct[i, j + 1].Enabled = false;
                    butoaneafis++;
                    pct[i, j + 1].BackColor = Color.Green;
                    crez.Grid[i, j + 1].Steag = false;
                    pct[i, j + 1].BackgroundImage = null;
                    pct[i, j + 1].Text = $"{crez.Grid[i, j + 1].vecin}";
                    if (crez.Grid[i, j + 1].vecin == 0)
                        CautareZero(i, j + 1);

                }
            if (i - 1 >= 0 && i - 1 < size)
                if (pct[i - 1, j].Enabled == true)
                {
                    pct[i - 1, j].Enabled = false;
                    butoaneafis++;
                    pct[i - 1, j].BackColor = Color.Green;
                    crez.Grid[i-1, j].Steag = false;
                    pct[i-1, j].BackgroundImage = null;
                    pct[i - 1, j].Text = $"{crez.Grid[i - 1, j].vecin}";
                    if (crez.Grid[i - 1, j].vecin == 0)
                        CautareZero(i - 1, j);

                }
            if (i + 1 >= 0 && i + 1 < size)
                if (pct[i + 1, j].Enabled == true)
                {
                    pct[i + 1, j].Enabled = false;
                    butoaneafis++;
                    pct[i + 1, j].BackColor = Color.Green;
                    crez.Grid[i+1, j].Steag = false;
                    pct[i+1, j ].BackgroundImage = null;
                    pct[i + 1, j].Text = $"{crez.Grid[i + 1, j].vecin}";
                    if (crez.Grid[i + 1, j].vecin == 0)
                        CautareZero(i + 1, j);

                }
            if (i + 1 >= 0 && i + 1 < size && j + 1 >= 0 && j + 1 < size)
                if (pct[i + 1, j + 1].Enabled == true)
                {
                    pct[i + 1, j + 1].Enabled = false;
                    butoaneafis++;
                    pct[i + 1, j + 1].BackColor = Color.Green;
                    crez.Grid[i + 1, j+1].Steag = false;
                    pct[i + 1, j+1].BackgroundImage = null;
                    pct[i + 1, j + 1].Text = $"{crez.Grid[i + 1, j + 1].vecin}";
                    if (crez.Grid[i + 1, j + 1].vecin == 0)
                        CautareZero(i + 1, j + 1);

                }
            if (i + 1 >= 0 && i + 1 < size && j - 1 >= 0 && j - 1 < size)
                if (pct[i + 1, j - 1].Enabled == true)
                {
                    pct[i + 1, j - 1].Enabled = false;
                    butoaneafis++;
                    pct[i + 1, j - 1].BackColor = Color.Green;
                    crez.Grid[i + 1, j - 1].Steag = false;
                    pct[i + 1, j - 1].BackgroundImage = null;
                    pct[i + 1, j - 1].Text = $"{crez.Grid[i + 1, j - 1].vecin}";
                    if (crez.Grid[i + 1, j - 1].vecin == 0)
                        CautareZero(i + 1, j - 1);

                }
            if (i - 1 >= 0 && i - 1 < size && j + 1 >= 0 && j + 1 < size)
                if (pct[i - 1, j + 1].Enabled == true)
                {
                    pct[i - 1, j + 1].Enabled = false;
                    butoaneafis++;
                    pct[i - 1, j + 1].BackColor = Color.Green;
                    crez.Grid[i - 1, j + 1].Steag = false;
                    pct[i - 1, j + 1].BackgroundImage = null;
                    pct[i - 1, j + 1].Text = $"{crez.Grid[i - 1, j + 1].vecin}";
                    if (crez.Grid[i - 1, j + 1].vecin == 0)
                        CautareZero(i - 1, j + 1);

                }
            if (i - 1 >= 0 && i - 1 < size && j - 1 >= 0 && j - 1 < size)
                if (pct[i - 1, j - 1].Enabled == true)
                    {
                        pct[i - 1, j - 1].Enabled = false;
                        butoaneafis++;
                        pct[i - 1, j - 1].BackColor = Color.Green;
                    crez.Grid[i - 1, j - 1].Steag = false;
                    pct[i - 1, j - 1].BackgroundImage = null;
                    pct[i - 1, j - 1].Text = $"{crez.Grid[i - 1, j - 1].vecin}";
                        if (crez.Grid[i - 1, j - 1].vecin == 0)
                            CautareZero(i - 1, j - 1);

                    }
        }

        //private void pct_click(object sender, EventArgs e)
        //{
        //    //contorizare butaone afisate
        //    butoaneafis++;
        //    //Se afla linia si coloana butonului
        //    Button ButonApasat = (Button)sender;
        //    Point locatie = (Point)ButonApasat.Tag;

        //    int x = locatie.X;
        //    int y = locatie.Y;

        //    Cell loccur = crez.Grid[x, y];
        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            afis();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bombe = 100;
            size = 22;
            button4.Hide();
            button2.Hide();
            button3.Hide();
            button4.Enabled= false;
            button2.Enabled= false;
            button3.Enabled= false;
            pornire();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bombe = 10;
            size = 9;
            button4.Hide();
            button2.Hide();
            button3.Hide();
            button4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            pornire();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bombe = 41;
            size = 16;
            button4.Hide();
            button2.Hide();
            button3.Hide();
            button4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            pornire();
        }
    }
}
