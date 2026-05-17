using System;
using System.Data;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FTambahDataKelas : Form
    {
        public FTambahDataKelas()
        {
            InitializeComponent();
            tampilwali();
        }

        public void tampilwali()
        {
            cmbWakel.Items.Clear();
            cmbWakel.Items.Add("-- Nama Wali Kelas --");
            DB.crud("SELECT IDP, Nama FROM tpetugas WHERE role = 'Guru' ORDER BY Nama");
            foreach (DataRow item in DB.ds.Tables[0].Rows)
                cmbWakel.Items.Add(item["IDP"] + " - " + item["Nama"]);
            cmbWakel.SelectedIndex = 0;
        }

        public string IDK { get; set; }
        public bool Perbarui { get; set; } = false;

        private string _idpWali = "";

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtIDK.Text == "" || txtNamaKls.Text == "" || string.IsNullOrEmpty(_idpWali))
            {
                MessageBox.Show("Semua field harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idk      = txtIDK.Text.Trim();
            string kls      = txtNamaKls.Text.Trim();
            string namaWali = cmbWakel.SelectedItem?.ToString() ?? "";

            if (Perbarui)
            {
                DB.crud($"UPDATE tkelas SET IDK='{idk}', Kelas='{kls}', Wali_Kelas='{namaWali}', IDP={_idpWali} WHERE IDK='{IDK}'");
                MessageBox.Show("Data kelas berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DB.crud($"INSERT INTO tkelas (IDK, Kelas, Wali_Kelas, IDP) VALUES ('{idk}', '{kls}', '{namaWali}', {_idpWali})");
                MessageBox.Show("Data kelas berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        public void IsiData(string idk, string kls, string wali)
        {
            txtIDK.Text     = idk;
            txtNamaKls.Text = kls;
            foreach (var item in cmbWakel.Items)
            {
                string[] parts = item.ToString().Split('-');
                string namaItem = parts[parts.Length - 1].Trim();
                if (item.ToString().Contains(wali) || wali.Contains(namaItem))
                {
                    cmbWakel.SelectedItem = item;
                    break;
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbWakel_DropDownClosed(object sender, EventArgs e)
        {
            AmbilIdpWali();
        }

        private void cmbWakel_SelectedIndexChanged(object sender, EventArgs e)
        {
            AmbilIdpWali();
        }

        private void AmbilIdpWali()
        {
            try
            {
                if (cmbWakel.SelectedIndex > 0)
                {
                    string[] parts = cmbWakel.SelectedItem.ToString().Split('-');
                    _idpWali = parts[0].Trim();
                }
                else
                {
                    _idpWali = "";
                }
            }
            catch { _idpWali = ""; }
        }
    }
}
