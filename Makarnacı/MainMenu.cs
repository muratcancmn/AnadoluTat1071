using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Makarnacı
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            
        }
        OrderMenu GiveOrder;
        OrderDetails OrderInformation;
        AddPasta AddPasta;
        AddExtraItem extra;
       
        System.Media.SoundPlayer ses = new System.Media.SoundPlayer("Ali-Atay-Anadolu-Tat-1071-Radyo-Jingle-_Voice-Over_-_-Ölümlü-Dünya-Film-Müzikleri-_-SİNEMALARDA.wav");
        void HideForms()
        {
            OrderInformation.Hide();
            GiveOrder.Hide();
            AddPasta.Hide();
            extra.Hide();
            pictureBox1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            btnSes.Visible = false;
            btnSesKes.Visible = false;
            ses.Stop();
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {
            GiveOrder = new OrderMenu();
          OrderInformation = new OrderDetails();
            AddPasta = new AddPasta();
            extra = new AddExtraItem();
           
        }
   


        private void siparişOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            GiveOrder.MdiParent = MainMenu.ActiveForm;
            GiveOrder.Dock = DockStyle.Fill;
            HideForms();
            GiveOrder.Show();
        }

        private void siparişBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OrderInformation.MdiParent = MainMenu.ActiveForm;
            OrderInformation.Dock = DockStyle.Fill;
            HideForms();
            OrderInformation.Show();
        }

        private void makarnaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            AddPasta.MdiParent = MainMenu.ActiveForm;
            AddPasta.Dock = DockStyle.Fill;
            HideForms();
            AddPasta.Show();
        }

        private void extraMalzemeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            extra.MdiParent = MainMenu.ActiveForm;
            extra.Dock = DockStyle.Fill;
            HideForms();
            extra.Show();
        }

        private void btnSes_Click(object sender, EventArgs e)
        {
           
            ses.Play();
        }

        private void btnSesKes_Click(object sender, EventArgs e)
        {
            ses.Stop();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Anadolu Tat 1071'den ayrılıyor musunuz :( ", "Soru :", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("Makarnasız kalmayın :(");
            }
            else
            {
                MessageBox.Show("Teşekkür ederizz");
                e.Cancel = true;
            }
        }
    }
}
