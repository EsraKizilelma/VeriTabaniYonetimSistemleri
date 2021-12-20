using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace veritabanideneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; " +
            "port=5432; Database=eczane; user ID=postgres; password=b191210040");
        
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            DateTime time;
            baglanti.Open();
            NpgsqlCommand ekle = new NpgsqlCommand("insert into ilac(ilacid,ilacadi,skt,stokadet,tedarikci,alisfiyati,satisfiyati)" +
                " values (@ilacid,@ilacadi,@skt,@stokadet,@tedarikci,@alisfiyati,@satisfiyati)", baglanti);
            ekle.Parameters.AddWithValue("@ilacid", int.Parse(textBoxilacid.Text));
            ekle.Parameters.AddWithValue("@ilacadi", textBoxIlacAdi.Text);
            ekle.Parameters.AddWithValue("@skt", textBoxTETT.Text);
            ekle.Parameters.AddWithValue("@stokadet", int.Parse(textBoxStokAdet.Text));
            ekle.Parameters.AddWithValue("@tedarikci", textBoxFirmaAdi.Text);
            ekle.Parameters.AddWithValue("@alisfiyati", int.Parse(textBoxAlisFiyati.Text));
            ekle.Parameters.AddWithValue("@satisfiyati", int.Parse(textBoxSatisfiyati.Text));
            ekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ilac kaydı başarılı bir sekilde gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void buttonListele_Click(object sender, EventArgs e)
        {
            string sorgu_ilac = "select * from ilac";
            NpgsqlDataAdapter datadapter = new NpgsqlDataAdapter(sorgu_ilac, baglanti);
            DataSet dataset = new DataSet();
            datadapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand sil = new NpgsqlCommand("delete from ilac where ilacid=@ilacid", baglanti);
            sil.Parameters.AddWithValue("@ilacid", int.Parse(textBoxilacid.Text));
            sil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand guncelle = new NpgsqlCommand("update ilac set ilacadi = @ilacadi, skt = @skt, stokadet = @stokadet," +
                " tedarikci = @tedarikci, alisfiyati = @alisfiyati, satisfiyati = @satisfiyati where ilacid = @ilacid", baglanti);
            guncelle.Parameters.AddWithValue("@ilacid", int.Parse(textBoxilacid.Text));
            guncelle.Parameters.AddWithValue("@ilacadi", textBoxIlacAdi.Text);
            guncelle.Parameters.AddWithValue("@skt", textBoxTETT.Text);
            guncelle.Parameters.AddWithValue("@stokadet", int.Parse(textBoxStokAdet.Text));
            guncelle.Parameters.AddWithValue("@tedarikci", textBoxFirmaAdi.Text);
            guncelle.Parameters.AddWithValue("@alisfiyati", int.Parse(textBoxAlisFiyati.Text));
            guncelle.Parameters.AddWithValue("@satisfiyati", int.Parse(textBoxSatisfiyati.Text));
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void buttonAra_Click(object sender, EventArgs e)
        {
            if(textBoxilacid.Text != string.Empty)
            {
                baglanti.Open();
                string sorgu = "select*from ilac where ilacid =" + textBoxilacid.Text;
                NpgsqlDataAdapter araID = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet dataset = new DataSet();
                araID.Fill(dataset);
                dataGridView1.DataSource = dataset.Tables[0];
                baglanti.Close();
            }
            if (textBoxIlacAdi.Text != string.Empty)
            {
                baglanti.Open();
                string sorgu = "select*from ilac where ilacid =" + textBoxIlacAdi.Text;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                baglanti.Close();
            }
        }
    }
    
}
