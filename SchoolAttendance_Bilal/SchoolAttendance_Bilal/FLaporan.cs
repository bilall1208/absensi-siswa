using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FLaporan : Form
    {
        public int IDKelas = -1;

        public FLaporan()
        {
            InitializeComponent();
        }

        private void FLaporan_Load(object sender, EventArgs e)
        {
            guna2DateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            guna2DateTimePicker2.Value = DateTime.Now;
            MuatLaporan();
        }

        private void MuatLaporan()
        {
            string tglMulai   = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            string tglSelesai = guna2DateTimePicker2.Value.ToString("yyyy-MM-dd");

            if (guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal selesai!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            guna2DataGridView1.Rows.Clear();

            string filterKelas = IDKelas >= 0 ? $"AND k.IDK = {IDKelas}" : "";

            DB.crud(
                $"SELECT ta.Tanggal, k.Kelas, " +
                $"SUM(td.Hadir) AS Hadir, SUM(td.Sakit) AS Sakit, " +
                $"SUM(td.Izin) AS Izin, SUM(td.Alpa) AS Alpa " +
                $"FROM tabsensi ta " +
                $"INNER JOIN tdetailabsensi td ON ta.IDAbsen = td.IDAbsen " +
                $"INNER JOIN tsiswa s ON td.NISN = s.NISN " +
                $"INNER JOIN tkelas k ON s.IDK = k.IDK " +
                $"WHERE ta.Tanggal BETWEEN '{tglMulai}' AND '{tglSelesai}' " +
                $"AND s.NISN != '' AND s.IDK >= 0 {filterKelas} " +
                $"GROUP BY ta.Tanggal, k.IDK, k.Kelas " +
                $"ORDER BY ta.Tanggal, k.IDK"
            );

            if (DB.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data absensi pada rentang tanggal tersebut.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                guna2DataGridView1.Rows.Add(
                    Convert.ToDateTime(brs["Tanggal"]).ToString("dd-MM-yyyy"),
                    brs["Kelas"].ToString(),
                    Convert.ToInt32(brs["Hadir"]),
                    Convert.ToInt32(brs["Sakit"]),
                    Convert.ToInt32(brs["Izin"]),
                    Convert.ToInt32(brs["Alpa"])
                );
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MuatLaporan();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter   = "CSV File (*.csv)|*.csv";
            sfd.FileName = $"Laporan_Absensi_{guna2DateTimePicker1.Value:yyyyMMdd}_sd_{guna2DateTimePicker2.Value:yyyyMMdd}";
            sfd.Title    = "Simpan Laporan";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("sep=;");
                sb.AppendLine("Tanggal;Kelas;Hadir;Sakit;Izin;Alpa");

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    string kelas = row.Cells["Column3"].Value?.ToString() ?? "";
                    if (string.IsNullOrWhiteSpace(kelas)) continue;

                    string tanggal = row.Cells["Column1"].Value?.ToString() ?? "";
                    string hadir   = row.Cells["Column4"].Value?.ToString() ?? "0";
                    string sakit   = row.Cells["Column5"].Value?.ToString() ?? "0";
                    string izin    = row.Cells["Column6"].Value?.ToString() ?? "0";
                    string alpa    = row.Cells["Column7"].Value?.ToString() ?? "0";

                    sb.AppendLine($"\"{tanggal}\";{kelas};{hadir};{sakit};{izin};{alpa}");
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));

                MessageBox.Show($"Laporan berhasil disimpan ke:\n{sfd.FileName}",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan file.\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
