using System;
using System.Data;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FGuru : Form
    {
        public FGuru()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud("SELECT * FROM tpetugas");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                guna2DataGridView1.Rows.Add("" + brs["IDP"], "" + brs["Nama"], "" + brs["username"], "" + brs["password"], "" + brs["role"]);
        }

        private void FGuru_Load(object sender, EventArgs e)
        {
            tampildata();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;
            string idp = guna2DataGridView1.Rows[baris].Cells[0].Value?.ToString() ?? "";
            DataGridViewRow BarisDipilih = guna2DataGridView1.Rows[baris];

            if (kolom == 5)
            {
                FTambahDataGuru FTDG = new FTambahDataGuru();
                FTDG.Perbarui = true;
                FTDG.IDP = idp;
                FTDG.IsiData(
                    BarisDipilih.Cells["Nama"].Value?.ToString() ?? "",
                    BarisDipilih.Cells["Username"].Value?.ToString() ?? "",
                    BarisDipilih.Cells["Password"].Value?.ToString() ?? "",
                    BarisDipilih.Cells["Role"].Value?.ToString() ?? "");
                FTDG.ShowDialog();
                tampildata();
            }
            if (kolom == 6)
            {
                DialogResult konfirmasi = MessageBox.Show(
                    $"Apakah anda yakin hapus guru dengan ID `{idp}`?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (konfirmasi == DialogResult.OK)
                {
                    DB.crud($"DELETE FROM tpetugas WHERE IDP='{idp}'");
                    tampildata();
                }
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud($"SELECT * FROM tpetugas WHERE Nama LIKE '%{guna2TextBox1.Text}%' OR username LIKE '%{guna2TextBox1.Text}%'");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                guna2DataGridView1.Rows.Add("" + brs["IDP"], "" + brs["Nama"], "" + brs["username"], "" + brs["password"], "" + brs["role"]);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FTambahDataGuru FTDG = new FTambahDataGuru();
            FTDG.Perbarui = false;
            FTDG.ShowDialog();
            tampildata();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            tampildata();
        }
    }
}
