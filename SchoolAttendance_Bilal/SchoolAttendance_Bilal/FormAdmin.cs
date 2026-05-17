using System;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        public string namaAdmin;
        public string roleUser;
        public int IdPetugas = 0;

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FDashboard() { TopLevel = false, TopMost = true }, PNLKONTEN);
            LBadmin.Text = namaAdmin;
            LBrole.Text = "- " + roleUser;
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FDashboard() { TopLevel = false, TopMost = true }, PNLKONTEN);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            FAbsensi abs = new FAbsensi() { TopLevel = false, TopMost = true };
            abs.IdPetugas = IdPetugas;
            abs.IsReadOnly = true;
            KFBilal.UntukFormBilal(abs, PNLKONTEN);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FLaporan() { TopLevel = false, TopMost = true }, PNLKONTEN);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FKelas() { TopLevel = false, TopMost = true }, PNLKONTEN);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FSiswa() { TopLevel = false, TopMost = true }, PNLKONTEN);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FGuru() { TopLevel = false, TopMost = true }, PNLKONTEN);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            PNLSIDE.Visible = !PNLSIDE.Visible;
        }

        private void FormAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult Setuju = MessageBox.Show("Apakah ingin logout?", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Setuju == DialogResult.Yes)
            {
                Login Flogin = new Login();
                Flogin.Show();
                this.Hide();
            }
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FBilalHal10() { TopLevel = false, TopMost = true }, PNLKONTEN);
        }
    }
}
