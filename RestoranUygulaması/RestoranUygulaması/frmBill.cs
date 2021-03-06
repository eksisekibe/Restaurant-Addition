using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestoranUygulaması
{
    public partial class frmBill : Form
    {
        public frmBill()
        {
            InitializeComponent();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Çıkmak istediğinizden emin misiniz?", "UYARI !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        cSiparis cs = new cSiparis();
        private void frmBill_Load(object sender, EventArgs e)
        {
            if (cGenel._ServisTurNo == 1)
            {

                lblAdisyonId.Text = cGenel._AdisyonId;
                txtIndirimTutari.TextChanged += new EventHandler(txtIndirimTutari_TextChanged);
                cs.getByOrder(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));
                if (lvUrunler.Items.Count>0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);
                    }
                    lblToplamTutar.Text = string.Format("{0:0.000}", toplam);
                    lblOdenecek.Text = string.Format("{0:0.000}", toplam);


                }
                gbIndirim.Visible = true;

                txtIndirimTutari.Clear();
            }
        }

        private void txtIndirimTutari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtIndirimTutari.Text)<Convert.ToDecimal(lblToplamTutar.Text))
                {
                    try
                    {
                        lblIndirim.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtIndirimTutari));
                    }
                    catch (Exception)
                    {

                        lblIndirim.Text = String.Format("{0:0.000}", 0);

                    }
                }
                else
                {
                    MessageBox.Show("İndirim Tutarı Tutardan Fazla Olamaz!!!");
                }
            }
            catch (Exception)
            {

                lblIndirim.Text = String.Format("{0:0.000}", 0);
            }
        }

        private void chkIndirim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndirim.Checked)
            {
                gbIndirim.Visible = true;
            }
            else
            {
                gbIndirim.Visible = false;

            }
        }

        private void lblIndirim_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblIndirim.Text)>0)
            {
                decimal odenecek = 0;
                lblOdenecek.Text = lblToplamTutar.Text;
                odenecek = Convert.ToDecimal(lblOdenecek.Text) - Convert.ToDecimal(lblIndirim.Text);
                lblOdenecek.Text = string.Format("{0:0.000}", odenecek);
            }

            decimal kdv = Convert.ToDecimal(lblOdenecek.Text)*18/100;
            lblKdv.Text = string.Format("{0:0.000}", kdv);

        }
    }
}
