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


    // ürün bilgisi ekle 

    public partial class OrderMenu : Form
    {
        public static OrderMenu order = new OrderMenu();
        public List<string> yemekler; // diğer formlardan makarna adı almamaı sağlayan satır
        public List<int> yemekFiyatı; // diğer formlardan makarna fiyatı almamı sağlayan satır
        public List<string> extraMalzeme;  // diğer formlardan sos adı sağlayan satır
        public List<int> extraMalzemeFiyatı; // diğer formlardan sos fiyatı almamı sağlayan satır
        public ComboBox makarnaCombo; // diğer formlardan comboboxıma veri almamı sağlayan satır
        public FlowLayoutPanel flow; // diğer formlardan flowlayoutuma control eklememi sağlayan satır
        public List<CheckBox> sosEkleme; // diğer formlardan checkbox almamı sağlayan satır

        public OrderMenu()
        {
            InitializeComponent();
            order = this;
            yemekler = makarnalar;
            yemekFiyatı = makarnaFiyat;
            makarnaCombo = comboBox1;
            extraMalzeme = extraSoslar;
            extraMalzemeFiyatı = extraSoslarFiyat;
            flow = flowLayoutPanel2;
            sosEkleme = soslar;
        }
        RadioButton rd;
        CheckBox cx;
        Label lb;

        List<CheckBox> soslar = new List<CheckBox>();
        List<RadioButton> porsiyonlar = new List<RadioButton>();
        List<string> makarnalar = new List<string>();
        List<int> makarnaFiyat = new List<int>();
        List<string> extraSoslar = new List<string>();
        List<int> extraSoslarFiyat = new List<int>();
        List<int> BoyutTutar = new List<int>();
        ExtraItem sos = new ExtraItem();
        BoyutEnum boyut = new BoyutEnum();
        PastaEnum makarna = new PastaEnum();
        int toplamTutar = 0;// siparişi kaydenene kadar tutulan tutar
        int araTutar = 0;// her bir sipariş için tutulan ara tutar
        int ciroTutar = 0;// sipariş kaydedildekten sonra ciroyu tutan tutar
        int ciroSos = 0;// Sipariş detayları kısmında
        bool isPastaChosen = true, isPortionChosen = true, isExtraChosen = true;
        int orderCount = 0;// Sipariş sayısını tutan değer 
        int orderAmount = 0;// kaç adet seçtiğimi tutan değer
        int orderAmountCount = 0;// sipariş detayları kısmında kaç adet ürün satıldığını tutan değer 

        private void OrderMenu_Load(object sender, EventArgs e)
        {
            Pasta();
            lb = new Label();
            flowLayoutPanel1.Controls.Add(lb);
            lb.Text = "Boyut Seçiniz";
            Sizes();

            lb = new Label();
            flowLayoutPanel2.Controls.Add(lb);
            lb.Text = "EXTRA MALZEME SEÇİNİZ";

            ExtraItems();

        }
        #region PastaLoad
        /// <summary>
        /// Makarnalarımı loadda flowlayouta ekleyip fiyat ve isimlerini de listelere atan method
        /// </summary>
        void Pasta()
        {

            foreach (string pasta in Enum.GetNames(typeof(Pasta)))
            {
                comboBox1.Items.Add(pasta);

                makarnalar.Add(pasta);
            }
            int[] pastaFiyatları = (int[])Enum.GetValues(typeof(Pasta));
            foreach (int fiyat in pastaFiyatları)
            {
                makarnaFiyat.Add(fiyat);
            }
        }
        #endregion

        #region ExtraLoad
        /// <summary>
        ///  Extra soslarımı loadda flowlayouta ekleyip fiyat ve isimlerini de listelere atan method
        /// </summary>
        void ExtraItems()
        {
            int[] sosFiyatları = (int[])Enum.GetValues(typeof(Extra));
            foreach (int fiyat in sosFiyatları)
            {
                extraSoslarFiyat.Add(fiyat);
            }
            int i = 0;
            foreach (string check in Enum.GetNames(typeof(Extra)))
            {
                cx = new CheckBox();
                cx.Name = check;
                cx.Text = check + " + " + extraSoslarFiyat[i] + " ₺";
                cx.AutoSize = true;
                soslar.Add(cx);
                this.Controls.Add(cx);
                extraSoslar.Add(check);
                flowLayoutPanel2.Controls.Add(cx);
                i++;
            }
           
        }
        #endregion

        #region SizeLoad
        /// <summary>
        /// Boyutları loadda flowlayouta ekleyip fiyatlarını da listeye atan method
        /// </summary>
        void Sizes()
        {
            foreach (int item in Enum.GetValues(typeof(Boyut)))
            {
                BoyutTutar.Add(item);
            }
            int i = 0;
            foreach (string s in Enum.GetNames(typeof(Boyut)))
            {
                rd = new RadioButton();
                rd.Name = s;
                rd.Text = s + " + " + BoyutTutar[i] + " ₺";
                rd.AutoSize = true;
                porsiyonlar.Add(rd);
                this.Controls.Add(rd);
                flowLayoutPanel1.Controls.Add(rd);
                i++;
            }
        }
        #endregion

        #region Amount
        /// <summary>
        /// Adet sayımı değiştiren method ref olarak orderAmount parametresi alır.Bunu değiştirerek sipariş adedini tutar.
        /// </summary>
        /// <param name="orderAmount"></param>
        void Amount(ref int orderAmount)
        {
            //PastaEnum x = new PastaEnum();
            //x.orderAmount = (int)numericUpDown1.Value;
            orderAmount = (int)numericUpDown1.Value;
            orderAmountCount += orderAmount;

        }
        #endregion

        #region ExtraPrice
        /// <summary>
        /// Extrasoslarımın hangisinin seçildiğini bulup fiyatını ve adını yazan method
        /// </summary>
        void ExtraPrice()
        {
          
            try
            {
                int j = 0;
                foreach (CheckBox cxx in soslar)
                {
                    if (cxx.Checked == true)
                    {
                        sos.ExtraItemPrice += extraSoslarFiyat[j];
                        listBox1.Items.Add(cxx.Text + " Birim Fiyatı : " + extraSoslarFiyat[j] + "₺");
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region PortionPrice
        /// <summary>
        /// Boyutlarımın hangisinin seçildiğini bulup o değeri tutan method
        /// </summary>
        void PortionPrice()
        {
            try
            {

                for (int i = 0; i < porsiyonlar.Count; i++)
                {
                    if (porsiyonlar[i].Checked == true)
                    {
                        boyut.PortionPrice = (int)Enum.Parse(typeof(Boyut), Enum.GetNames(typeof(Boyut))[i]);
                        listBox1.Items.Add(Enum.Parse(typeof(Boyut), Enum.GetNames(typeof(Boyut))[i]) + "  Porsiyon" + "     Artı Fiyat " + boyut.PortionPrice + "₺");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region PastaPrice
        /// <summary>
        /// Hnagi makarnanın seçildiğini anlayan , fiyatını ve ismini yazan method
        /// </summary>
        void PastaPrice()
        {
            try
            {

                for (int i = 0; i < makarnalar.Count; i++)
                {
                    if (comboBox1.Text == makarnalar[i])
                    {
                        makarna.PastaPrice = makarnaFiyat[i] + boyut.PortionPrice;
                        listBox1.Items.Add(makarnalar[i] + "   Makarnazınızın Birim Fiyatı  :  " + makarnaFiyat[i] + "₺");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region IfItsChecked
        /// <summary>
        /// Controllerimin seçilip seçilmediğini kontrol eden method
        /// </summary>
        void IfItsChecked()
        {
            for (int i = 0; i < porsiyonlar.Count; i++)
            {
                if (porsiyonlar[i].Checked == true)
                {
                    isPortionChosen = true;
                    break;
                }
                else
                {
                    isPortionChosen = false;
                }
            }
            for (int i = 0; i < makarnalar.Count; i++)
            {
                if (comboBox1.Text == makarnalar[i])
                {
                    isPastaChosen = true;
                    break;
                }
                else
                {
                    isPastaChosen = false;
                }
            }
            for (int i = 0; i < soslar.Count; i++)
            {
                if (soslar[i].Checked == true)
                {
                    isExtraChosen = true;
                    break;
                }
                else
                {
                    isExtraChosen = false;
                }
            }
        }
        #endregion

        #region PriceHolders
        /// <summary>
        /// Birden fazla yerde fiyat hesaplaması olacağı için bunu method haline getirdim
        /// </summary>
        void PriceHolders()
        {
            lblTutar.Text = toplamTutar.ToString() + "₺";
            listBox1.Items.Add("Ara Fiyat =  " + araTutar + "₺");
            ciroTutar += araTutar;
            ciroSos += sos.ExtraItemPrice;
            orderCount++;
            sos.ExtraItemPrice = 0;
        }
        #endregion

        #region Control
        /// <summary>
        /// Controllerimin seçilip seçilmeme durumuna göre işlemler yapan method
        /// </summary>
        void Control()
        {
            IfItsChecked();

            Amount(ref orderAmount);
            if (orderAmount == 0)
            {
                if (isPastaChosen == false & isPortionChosen == false & sos.ExtraItemPrice == 0)
                {
                    MessageBox.Show("Lütfen bir sipariş veriniz");
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir adet giriniz");
                }
            }
            else
            {
                if (isPastaChosen == true & isPortionChosen == true & isExtraChosen == true)
                {
                    listBox1.Items.Add(orderAmount + " Adet");
                    PortionPrice();
                    PastaPrice();
                    listBox1.Items.Add("Extra Malzemeleriniz  :");
                    ExtraPrice();
                    toplamTutar += (makarna.PastaPrice + sos.ExtraItemPrice) * orderAmount;
                    araTutar = (makarna.PastaPrice + sos.ExtraItemPrice) * orderAmount;
                    PriceHolders();

                }
                else if (isPastaChosen == true & isPortionChosen == true & isExtraChosen == false)
                {
                    listBox1.Items.Add(orderAmount + " Adet");
                    PortionPrice();
                    PastaPrice();
                    listBox1.Items.Add("Extra Malzemeleriniz  :");
                    toplamTutar += makarna.PastaPrice * orderAmount;
                    araTutar = makarna.PastaPrice * orderAmount;
                    PriceHolders();
                }
                else if (isPortionChosen == false & isPastaChosen == true)
                {
                    MessageBox.Show("Lütfen bir porsiyon  seçiniz");
                }
                else if (isPastaChosen == false & isPortionChosen == false & isExtraChosen == true)
                {
                    listBox1.Items.Add(orderAmount + " Adet");
                    ExtraPrice();
                    listBox1.Items.Add("Extra Malzemeleriniz  :");
                    toplamTutar += sos.ExtraItemPrice * orderAmount;
                    lblTutar.Text = toplamTutar.ToString();
                    PriceHolders();
                }
                else if (isPastaChosen == false & isPortionChosen == true)
                {
                    MessageBox.Show("Lütfen bir makarna  seçiniz");
                }
                else if (orderAmount != 0 & isPastaChosen == false & isPortionChosen == false)
                {
                    MessageBox.Show("Lütfen Makarna ve ya ekstra sos seçiniz");
                }
            }
        }
        #endregion

        #region ButonSave
        void ButonKaydet()
        {
            try
            {
                if (listBox1.Items == null)
                {
                    MessageBox.Show("Geçerli bir sipariş vermediniz");
                }
                else
                {
                    foreach (object obj in listBox1.Items)
                    {
                        OrderDetails.orderDetails.lx.Items.Add(obj);
                    }
                    OrderDetails.orderDetails.Ciro.Text = ciroTutar.ToString() + "₺";
                    OrderDetails.orderDetails.siparişSayısı.Text = orderCount.ToString();
                    OrderDetails.orderDetails.ExtraMalzeme.Text = ciroSos.ToString();
                    OrderDetails.orderDetails.Amount.Text = orderAmountCount.ToString();
                    listBox1.Items.Clear();
                    lblTutar.Text = "";
                    sos.ExtraItemPrice = 0;
                    boyut.PortionPrice = 1;
                    makarna.PastaPrice = 0;
                    toplamTutar = 0;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen düzgün sipariş giriniz");
            }
        }
        #endregion

        #region ButonSaveControl
        /// <summary>
        /// Siparişlerimi kaydederken doğrulurunu kontrol ederek alan method
        /// </summary>
        void KaydetKontrol()
        {
            if (toplamTutar != 0)
            {
                if (MessageBox.Show("Siparişinizden emin misiniz? ", "Soru :", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ButonKaydet();
                    MessageBox.Show("Afiyet olsuuun");
                }
                else;
            }
            else
            {
                MessageBox.Show("Lütfen Bir Sipariş Giriniz");
            }
        }
        #endregion

        #region PastaPicture
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "SalçalıMakarna":
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile("5e1c0d5518c77313c849901f.jpg");
                    lblMakarnaFiyat.Text = "Salçalı Makarna Fiyatı : 20₺";
                    return;
                case "PestoSosluMakarna":
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile("indir.jpg");
                    lblMakarnaFiyat.Text = "Pesto Soslu Makarna Fiyatı : 25₺";
                    return;
                case "KöriSosluMakarna":
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile("Kori-Soslu-Makarna-88.jpg");
                    lblMakarnaFiyat.Text = "Köri Soslu Makarna Fiyatı : 30₺";
                    return;
                case "SosisliMakarna":
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile("sosisli-makarna.jpg");
                    lblMakarnaFiyat.Text = "Sosisli Makarna Fiyatı : 35₺";
                    return;
                case "KıymalıMakarna":
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile("kıymalıMakarna.jpg");
                    lblMakarnaFiyat.Text = "Kıymalı Makarna Fiyatı : 40₺";
                    return;
                default:
                    pictureBox1.Visible = false;
                    lblMakarnaFiyat.Text = "";
                    return;
            }
          
        }
        #endregion
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            KaydetKontrol();
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            Control();
        }
    }
}
