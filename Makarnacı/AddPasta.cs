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
    
    public partial class AddPasta : Form
    {
       
        public AddPasta()
        {
            InitializeComponent();
        }
        string newPasta="";
        int newPastaPrice=0;
        void PastaAdd()
        {
            if (txtMakarna.Text!="")
            {
                newPasta = txtMakarna.Text;
                newPastaPrice = (int)nmFiyat.Value;
                if (newPastaPrice!=0)
                {
                    OrderMenu.order.yemekler.Add(newPasta);
                    OrderMenu.order.yemekFiyatı.Add(newPastaPrice);
                    OrderMenu.order.makarnaCombo.Items.Add(newPasta);
                    MessageBox.Show("Makarnanız eklenmiştir");
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir fiyat giriniz");

                }
   
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir isim giriniz");
            }
            

        }

        private void btnAddPasta_Click(object sender, EventArgs e)
        {
            PastaAdd();
      

        }
    }
}
