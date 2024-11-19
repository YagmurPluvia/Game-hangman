using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Adam_Asmaca
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] harfler = { "a", "b", "c", "ç", "d", "e", "f", "g", "ğ", "h", "ı", "i", "j", "k", "l", "m", "n", "o", "ö", "p", "r", "s", "ş", "t", "u", "ü", "v", "y", "z" };
        adam_Asmaca_Islemleri cl_adam_asmaca;
        String kelime;
        String[] seçilen_harfler;
        int can_hakkı = 7;
        int kalan_hakkımız;
        int width, height;
        private void Form1_Load(object sender, EventArgs e)
        {
            width = pictureBox1.Width;
            height = pictureBox1.Height;
            kalan_hakkımız = can_hakkı;
            seçilen_harfler = new string[0];
            cl_adam_asmaca = new adam_Asmaca_Islemleri();
        }
        private void kelime_Seç()
        {
            adam_Asmaca_Islemleri.yeni_Kelime kelimemiz_ = cl_adam_asmaca.kelime_al();
            label_ktg.Text = "Bu bir " + kelimemiz_.kategori;
            kelime = kelimemiz_.kelime;
            foreach (char harf in kelime.ToCharArray())
            {
                label_kel.Text = label_kel.Text + "_ ";
            }
            button1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label_soylen.Visible = true;
            label_kel.Visible = true;
            label_ktg.Visible = true;
            button2.Visible = true;
            kelime_Seç();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string veri = textBox1.Text;
            bool buldu = false;
            bool soylendi = false;
            foreach (var item in seçilen_harfler)
            {
                if (veri.ToLower() == item.ToLower())
                {
                    soylendi = true;
                    MessageBox.Show("Bu harfi daha önce söylediniz.");
                }
            }
            if (!soylendi)
            {
                for (int i = 0; i < harfler.Length; i++)
                {
                    if (veri.ToLower() == harfler[i])
                    {
                        buldu = true;
                        kelimede_bul(veri);
                        break;
                    }
                }
                if (!buldu)
                {
                    MessageBox.Show("29 harften birini giriniz.");
                }
                güncelle();
            }
            int index = label_kel.Text.IndexOf("_");
            if (index == -1)
            {
                sonraki_bolum();
            }
        }
        private void kelimede_bul(String harf)
        {
            bool dogrumu = false;
            char[] dizi = kelime.ToCharArray();
            for (int a = 0; a < dizi.Length; a++)
            {
                if (dizi[a].ToString() != "")
                {
                    if (harf == dizi[a].ToString().ToLower())
                    {
                        dogrumu = true;
                        label_kel.Text = label_kel.Text.Remove(a * 2, 1);
                        label_kel.Text = label_kel.Text.Insert(a * 2, harf).ToUpper();
                    }
                }
            }
            if (!dogrumu)
            {
                kalan_hakkımız--;
                pictureBox1.Invalidate();
            }
            ekle_harf(harf);
        }
        private void güncelle()
        {
            label_soylen.Text = "";
            foreach (string item in seçilen_harfler)
            {
                label_soylen.Text = label_soylen.Text + item + " ";
            }
        }
        private void ekle_harf(string harf)
        {
            string[] a = new string[seçilen_harfler.Length + 1];
            for (int i = 0; i < seçilen_harfler.Length; i++)
            {
                a[i] = seçilen_harfler[i];
            }
            a[a.Length - 1] = harf;
            seçilen_harfler = a;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen kalem = new Pen(Color.Black, 8);
            if (kalan_hakkımız < can_hakkı)
            {
                e.Graphics.DrawLine(kalem, width / 10, height / 15, width / 10, height / 15 * 14);
                e.Graphics.DrawLine(kalem, width / 10, height / 15, width / 2, height / 15);
            }
            if (kalan_hakkımız < can_hakkı - 1)
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 15, width / 2, height / 15 * 3);
                e.Graphics.DrawEllipse(kalem, width / 2 - width / 10, height / 5, width / 5, height / 10);
            }
            if (kalan_hakkımız < can_hakkı - 2)//gövde
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 3, width / 2, height / 10 * 6);
            }
            if (kalan_hakkımız < can_hakkı - 3)//sağ kol 
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 3, width / 2 + width / 10, height / 10 * 3 + height / 5);
            }
            if (kalan_hakkımız < can_hakkı - 4)//kol
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 3, width / 2 - width / 10, height / 10 * 3 + height / 5);
            }
            if (kalan_hakkımız < can_hakkı - 5)//sağ ayak
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 6, width / 2 + width / 10, height / 10 * 6 + height / 10);
            }
            if (kalan_hakkımız < can_hakkı - 6)//sol ayak
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 6, width / 2 - width / 10, height / 10 * 6 + height / 10);
                oyun_bitti();
            }
        }
        private void sonraki_bolum()
        {
            temizle();
            kelime_Seç();
        }
        public void oyun_bitti()
        {
            MessageBox.Show("Oyun Bitti. Dogru kelime : " + kelime);
            button1.Visible = true;
            textBox1.Visible = false;
            button2.Visible = false;
            temizle();

        }
        private void temizle()
        {
            seçilen_harfler = new string[0];
            kalan_hakkımız = can_hakkı;
            pictureBox1.Invalidate();
            label_ktg.Text = "";
            label_kel.Text = "";
            label_soylen.Text = "";
            textBox1.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    class adam_Asmaca_Islemleri
    {

        private List<yeni_Kelime> yeni_kelime_listesi;
        private List<yeni_Kelime> kelime_listesi;
        public adam_Asmaca_Islemleri()
        {
            data_dosyası_al();
        }
        public struct yeni_Kelime
        {
            public string kelime;
            public string kategori;
        }
        public yeni_Kelime kelime_al()
        {
            Random rastg = new Random();
            int a = rastg.Next(0, kelime_listesi.Count);
            yeni_Kelime kelimemiz = kelime_listesi[a];
            return kelimemiz;
        }
        private yeni_Kelime yeni_Kelime_Olustur(string kelime, string kategori)
        {
            yeni_Kelime kelimemiz = new yeni_Kelime();
            kelimemiz.kelime = kelime;
            kelimemiz.kategori = kategori;
            return kelimemiz;
        }
        private void data_dosyası_al()
        {
            yeni_kelime_listesi = new List<yeni_Kelime>();
            StreamReader oku = new StreamReader(@"Data.txt", Encoding.Default);
            string veri;
            while ((veri = oku.ReadLine()) != null)
            {
                if (veri != "")
                {
                    yeni_kelime_listesi.Add(yeni_Kelime_Olustur(veri.Split(',')[0], veri.Split(',')[1]));
                }
            }
            kelime_listesi = yeni_kelime_listesi;
        }
    }
}
