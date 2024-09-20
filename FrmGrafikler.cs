﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Seçim_Verileriyle_Parti_İstatistik_Analizi
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ERGBAB8;Initial Catalog=DbSecimProje;Integrated Security=True");

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //ilçe Adlarını Comboboxa Çekme 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE ", baglanti);
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            baglanti.Close();

            //Grafiğe Toplam Sonuçları Getirme 

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select SUM(APARTI), SUM(APARTI), SUM(BPARTI),SUM(CPARTI), SUM(DPARTI), SUM(EPARTI)" +
                "FROM TBLILCE", baglanti);

            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A Parti", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B Parti", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C Parti", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D Parti", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E Parti", dr2[4]);
                

            }
            baglanti.Close();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLILCE WHERE ILCEAD=@P1 ", baglanti);
            komut.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                PbA.Value = int.Parse(dr[2].ToString());
                PbB.Value = int.Parse(dr[3].ToString());
                PbC.Value = int.Parse(dr[4].ToString());
                PbD.Value = int.Parse(dr[5].ToString());
                PbE.Value = int.Parse(dr[6].ToString());

                LblA.Text = dr[2].ToString();
                LblB.Text = dr[3].ToString();
                lblC.Text = dr[4].ToString();
                LblD.Text = dr[5].ToString();
                LblE.Text = dr[6].ToString();

               

            }
            baglanti.Close();

        }
    }
}