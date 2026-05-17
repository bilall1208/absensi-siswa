using System;
using System.Data;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DB.crud($"SELECT * FROM `tpetugas` WHERE username = '{txtUser.Text}' AND password = '{txtPass.Text}'");

                if (DB.ds.Tables[0].Rows.Count == 1)
                {
                    DataRow baris = DB.ds.Tables[0].Rows[0];
                    string nama = "" + baris["nama"];
                    string role = "" + baris["role"];
                    int idp = Convert.ToInt32(baris["IDP"]);

                    if (role == "Admin")
                    {
                        FormAdmin FA = new FormAdmin();
                        FA.namaAdmin = nama;
                        FA.roleUser = role;
                        FA.IdPetugas = idp;
                        FA.Show();
                        this.Hide();
                    }
                    else if (role == "Guru")
                    {
                        int idkelas = -1;
                        DB.crud($"SELECT IDK FROM tkelas WHERE IDP = {idp}");
                        if (DB.ds.Tables[0].Rows.Count > 0)
                            idkelas = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["IDK"]);

                        FormGuru FG = new FormGuru();
                        FG.namaGuru  = nama;
                        FG.roleUser  = role;
                        FG.IdPetugas = idp;
                        FG.IDKelas   = idkelas;
                        FG.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show($"Role '{role}' tidak dikenali. Hubungi administrator.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Username atau Password salah!", "Login Gagal",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Text = "";
                    txtPass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal terhubung ke database.\n{ex.Message}", "Error Koneksi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e) { }
    }
}
