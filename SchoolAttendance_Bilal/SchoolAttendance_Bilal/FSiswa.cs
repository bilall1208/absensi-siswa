using System;
using System.Data;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FSiswa : Form
    {
        public FSiswa()
        {
            InitializeComponent();
        }

        private void IsiComboKelas()
        {
            cmbFilterKelas.Items.Clear();
            cmbFilterKelas.Items.Add("-- Semua Kelas --");
            DB.crud("SELECT IDK, Kelas FROM tkelas ORDER BY IDK");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                cmbFilterKelas.Items.Add(brs["Kelas"].ToString());
            cmbFilterKelas.SelectedIndex = 0;
        }

        public void tampildata()
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud("SELECT * FROM tsiswa INNER JOIN tkelas on tsiswa.IDK = tkelas.IDK");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                guna2DataGridView1.Rows.Add("" + brs["NISN"], "" + brs["Nama"], "" + brs["Alamat"], "" + brs["Kelas"]);
        }

        private void FilterData()
        {
            guna2DataGridView1.Rows.Clear();

            string searchText    = guna2TextBox1.Text.Trim();
            string selectedKelas = cmbFilterKelas.SelectedItem?.ToString() ?? "-- Semua Kelas --";

            string query = "SELECT * FROM tsiswa INNER JOIN tkelas on tsiswa.IDK = tkelas.IDK WHERE 1=1";

            if (!string.IsNullOrEmpty(searchText))
                query += $" AND Nama LIKE '%{searchText}%'";

            if (selectedKelas != "-- Semua Kelas --")
                query += $" AND tkelas.Kelas = '{selectedKelas}'";

            DB.crud(query);
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                guna2DataGridView1.Rows.Add("" + brs["NISN"], "" + brs["Nama"], "" + brs["Alamat"], "" + brs["Kelas"]);
        }

        private void FSiswa_Load(object sender, EventArgs e)
        {
            IsiComboKelas();
            tampildata();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FTambahDataSiswa FTDS = new FTambahDataSiswa();
            FTDS.Perbarui = false;
            FTDS.ShowDialog();
            tampildata();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void cmbFilterKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void guna2DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            guna2DataGridView1.Cursor = Cursors.Hand;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;
            string nisn = guna2DataGridView1.Rows[baris].Cells[0].Value?.ToString() ?? "";
            DataGridViewRow BarisDipilih = guna2DataGridView1.Rows[baris];

            if (kolom == 4)
            {
                FTambahDataSiswa FTDS = new FTambahDataSiswa();
                FTDS.Perbarui    = true;
                FTDS.NISNPilihan = nisn;
                FTDS.IsiData(
                    nisn,
                    BarisDipilih.Cells["Nama"].Value?.ToString() ?? "",
                    BarisDipilih.Cells["Kelas"].Value?.ToString() ?? "",
                    BarisDipilih.Cells["Alamat"].Value?.ToString() ?? "");
                FTDS.ShowDialog();
                tampildata();
            }
            if (kolom == 5)
            {
                DialogResult konfirmasi = MessageBox.Show(
                    $"Apakah anda yakin hapus siswa dengan NISN `{nisn}`?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (konfirmasi == DialogResult.OK)
                {
                    DB.crud($"DELETE FROM tsiswa WHERE NISN='{nisn}'");
                    tampildata();
                }
            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            cmbFilterKelas.SelectedIndex = 0;
            tampildata();
        }
    }
}
