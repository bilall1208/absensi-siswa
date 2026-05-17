using System;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FTambahDataGuru : Form
    {
        public bool Perbarui = false;
        public string IDP = "";

        private string _rolePilihan = "";

        public FTambahDataGuru()
        {
            InitializeComponent();
        }

        private void IsiComboRole()
        {
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("Admin");
            guna2ComboBox1.Items.Add("Guru");
            guna2ComboBox1.SelectedIndex = 0;
        }

        private void FTambahDataGuru_Load(object sender, EventArgs e)
        {
            IsiComboRole();

            if (!string.IsNullOrEmpty(_rolePilihan))
                guna2ComboBox1.SelectedItem = _rolePilihan;

            this.Text       = Perbarui ? "Update Data Guru" : "Tambah Data Guru";
            guna2Button1.Text = Perbarui ? "Update" : "Simpan";
        }

        public void IsiData(string nm, string usr, string pw, string role)
        {
            guna2TextBox1.Text = nm;
            guna2TextBox2.Text = usr;
            guna2TextBox3.Text = pw;
            _rolePilihan = role;
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Nama tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox2.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text))
            {
                MessageBox.Show("Username tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox2.Focus();
                return;
            }
            if (guna2ComboBox1.SelectedItem == null)
            {
                MessageBox.Show("Pilih role!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2ComboBox1.Focus();
                return;
            }

            string nama     = guna2TextBox1.Text;
            string username = guna2TextBox2.Text;
            string password = guna2TextBox3.Text;
            string role     = guna2ComboBox1.SelectedItem.ToString();

            if (Perbarui)
            {
                if (string.IsNullOrWhiteSpace(password))
                    DB.crud($"UPDATE tpetugas SET Nama='{nama}', username='{username}', role='{role}' WHERE IDP='{IDP}'");
                else
                    DB.crud($"UPDATE tpetugas SET Nama='{nama}', username='{username}', password='{password}', role='{role}' WHERE IDP='{IDP}'");

                MessageBox.Show("Data guru berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    guna2TextBox3.Focus();
                    return;
                }

                DB.crud($"INSERT INTO tpetugas (Nama, username, password, role) VALUES ('{nama}', '{username}', '{password}', '{role}')");
                MessageBox.Show("Data guru berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
