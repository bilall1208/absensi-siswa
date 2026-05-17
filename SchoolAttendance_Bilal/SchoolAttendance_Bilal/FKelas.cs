using System;
using System.Data;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FKelas : Form
    {
        public FKelas()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud("SELECT * FROM tkelas");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                guna2DataGridView1.Rows.Add("" + brs["IDK"], "" + brs["Kelas"], "" + brs["Wali_Kelas"]);
        }

        private void FKelas_Load(object sender, EventArgs e)
        {
            tampildata();
            DB.crud("SELECT COUNT(*) AS Total FROM tkelas");
            guna2HtmlLabel4.Text = DB.ds.Tables[0].Rows[0]["Total"].ToString();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud($"SELECT * FROM tkelas WHERE Kelas LIKE '%{guna2TextBox1.Text}%' || Wali_Kelas LIKE '%{guna2TextBox1.Text}%'");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                guna2DataGridView1.Rows.Add("" + brs["IDK"], "" + brs["Kelas"], "" + brs["Wali_Kelas"]);
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;
            string idkel = guna2DataGridView1.Rows[baris].Cells[0].Value?.ToString() ?? "";
            DataGridViewRow BarisDipilih = guna2DataGridView1.Rows[baris];

            if (kolom == 3)
            {
                FTambahDataKelas FTDK = new FTambahDataKelas();
                FTDK.Perbarui = true;
                FTDK.IDK = idkel;
                FTDK.IsiData(
                    idkel,
                    BarisDipilih.Cells["Kelas"].Value?.ToString() ?? "",
                    BarisDipilih.Cells["Wali_Kelas"].Value?.ToString() ?? "");
                FTDK.ShowDialog();
                tampildata();
            }
            if (kolom == 4)
            {
                DialogResult konfirmasi = MessageBox.Show(
                    $"Apakah anda yakin hapus kelas dengan IDK `{idkel}`?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (konfirmasi == DialogResult.OK)
                {
                    DB.crud($"DELETE FROM tkelas WHERE IDK='{idkel}'");
                    tampildata();
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FTambahDataKelas FTDK = new FTambahDataKelas();
            FTDK.Perbarui = false;
            FTDK.ShowDialog();
            tampildata();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            tampildata();
        }
    }
}
