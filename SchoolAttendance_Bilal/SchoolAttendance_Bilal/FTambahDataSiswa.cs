using System;
using System.Data;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FTambahDataSiswa : Form
    {
        public FTambahDataSiswa()
        {
            InitializeComponent();
        }

        public string NISNPilihan { get; set; }
        public bool Perbarui { get; set; } = false;

        private string _kelasPilihan = "";

        private void IsiComboKelas()
        {
            cmbKelas.Items.Clear();
            DB.crud("SELECT IDK, Kelas FROM tkelas ORDER BY IDK");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                cmbKelas.Items.Add(brs["Kelas"].ToString());
            cmbKelas.SelectedIndex = 0;
        }

        private void FTambahDataSiswa_Load(object sender, EventArgs e)
        {
            IsiComboKelas();

            if (!string.IsNullOrEmpty(_kelasPilihan))
                cmbKelas.SelectedItem = _kelasPilihan;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void IsiData(string nis, string nm, string kls, string alt)
        {
            txtNISN.Text  = nis;
            txtNama.Text  = nm;
            _kelasPilihan = kls;
            txtAlamat.Text = alt;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtNISN.Text == "" || txtNama.Text == "" || cmbKelas.SelectedIndex == -1 || txtAlamat.Text == "")
            {
                MessageBox.Show("Semua field harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nisn   = txtNISN.Text;
            string nama   = txtNama.Text;
            string alamat = txtAlamat.Text;

            DB.crud($"SELECT IDK FROM tkelas WHERE Kelas='{cmbKelas.SelectedItem}'");
            string idk = DB.ds.Tables[0].Rows[0]["IDK"].ToString();

            if (Perbarui)
            {
                DB.crud($"UPDATE tsiswa SET NISN='{nisn}', Nama='{nama}', Alamat='{alamat}', IDK='{idk}' WHERE NISN='{NISNPilihan}'");
                MessageBox.Show("Data siswa berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DB.crud($"INSERT INTO tsiswa VALUES('{nisn}', '{nama}', '{alamat}', '{idk}')");
                MessageBox.Show("Data siswa berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
