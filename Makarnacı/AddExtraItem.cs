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
    public partial class AddExtraItem : Form
    {
   
        public AddExtraItem()
        {
            InitializeComponent();
            
        }
        string sosAdı = "";
        int sosFiyatı = 0;
        void SosEkle()
        {
            try
            {
                if (txtSosAdı.Text!="")
                {
                    sosAdı = txtSosAdı.Text;
                    sosFiyatı = (int)nmnSosFiyat.Value;
                    if (sosFiyatı != 0)
                    {
                        OrderMenu.order.extraMalzeme.Add(sosAdı);
                        OrderMenu.order.extraMalzemeFiyatı.Add(sosFiyatı);
                        OrderMenu.order.sosEkleme.Add(new CheckBox() { Text = sosAdı, AutoSize = true, Checked = false });
                        OrderMenu.order.flow.Controls.Add(OrderMenu.order.sosEkleme[OrderMenu.order.sosEkleme.Count - 1]);
                        MessageBox.Show("Sosunuz eklenmiştir");
                    }
                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir fiyat giriniz");
                    }
                }

                else
                {
                    MessageBox.Show("lütfen geçerli bir isim giriniz");
                }
          
            }
            catch (Exception )
            {
                MessageBox.Show("lütfen geçerli bir isim giriniz");
            }
            
            }

            private void btnSos_Click(object sender, EventArgs e)
            {
                SosEkle();
               
            }
        }
    }
