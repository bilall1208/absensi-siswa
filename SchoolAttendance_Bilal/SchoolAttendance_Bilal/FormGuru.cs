using System;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FormGuru : Form
    {
        public FormGuru()
        {
            InitializeComponent();
        }

        public string namaGuru;
        public string roleUser;
        public int IdPetugas = 0;
        public int IDKelas = -1;

        private FDashboard BuatDashboard()
        {
            FDashboard d = new FDashboard() { TopLevel = false, TopMost = true };
            d.IDKelas = IDKelas;
            return d;
        }

        private FAbsensi BuatAbsensi()
        {
            FAbsensi abs = new FAbsensi() { TopLevel = false, TopMost = true };
            abs.IdPetugas = IdPetugas;
            abs.IDKelas   = IDKelas;
            return abs;
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            PNLSIDE.Visible = !PNLSIDE.Visible;
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(new FBilalHal10() { TopLevel = false, TopMost = true }, PNLKONTEN);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(BuatDashboard(), PNLKONTEN);
            LBguru.Text  = namaGuru;
            LBrole.Text  = "- " + roleUser;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            DialogResult Setuju = MessageBox.Show("Apakah ingin logout?", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Setuju == DialogResult.Yes)
            {
                Login Flogin = new Login();
                Flogin.Show();
                this.Hide();
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(BuatDashboard(), PNLKONTEN);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(BuatAbsensi(), PNLKONTEN);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FLaporan lp = new FLaporan() { TopLevel = false, TopMost = true };
            lp.IDKelas = IDKelas;
            KFBilal.UntukFormBilal(lp, PNLKONTEN);
        }

        private void FormGuru_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            KFBilal.UntukFormBilal(BuatAbsensi(), PNLKONTEN);
        }
    }
}
