using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace T1_Bilal_XIRPLA
{
    public partial class FAbsensi : Form
    {
        public int IdPetugas = 0;
        public int IDKelas = -1;
        public bool IsReadOnly = false;

        private Dictionary<string, int> nisnToIdAbsen = new Dictionary<string, int>();
        private readonly int[] kolomCheckbox = { 4, 5, 6, 7 };

        public FAbsensi()
        {
            InitializeComponent();
        }

        private void FAbsensi_Load(object sender, EventArgs e)
        {
            guna2ComboBox2.Items.Add("-- Semua Kelas --");
            DB.crud("SELECT IDK, Kelas FROM tkelas ORDER BY IDK");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                guna2ComboBox2.Items.Add(brs["Kelas"].ToString());

            if (IDKelas >= 0)
            {
                DB.crud($"SELECT Kelas FROM tkelas WHERE IDK = {IDKelas}");
                if (DB.ds.Tables[0].Rows.Count > 0)
                    guna2ComboBox2.SelectedItem = DB.ds.Tables[0].Rows[0]["Kelas"].ToString();
                guna2ComboBox2.Enabled = false;
            }
            else
            {
                guna2ComboBox2.SelectedIndex = 0;
            }

            MuatData();

            if (IsReadOnly)
            {
                guna2Button2.Visible = false;
                guna2DataGridView1.ReadOnly = true;
            }
        }

        private void MuatData()
        {
            string tanggal      = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            string kelasDipilih = guna2ComboBox2.SelectedItem?.ToString() ?? "";
            bool semuaKelas     = (kelasDipilih == "-- Semua Kelas --" || string.IsNullOrEmpty(kelasDipilih));
            string searchText   = guna2TextBox1.Text.Trim();

            guna2DataGridView1.Rows.Clear();
            nisnToIdAbsen.Clear();

            string filterKelas      = semuaKelas ? "" : $"AND k.Kelas = '{kelasDipilih}'";
            string filterNama       = string.IsNullOrEmpty(searchText) ? "" : $"AND s.Nama LIKE '%{searchText}%'";
            string whereKelasInsert = semuaKelas
                ? "WHERE s.NISN != '' AND s.IDK >= 0"
                : $"WHERE k.Kelas = '{kelasDipilih}' AND s.NISN != '' AND s.IDK >= 0";
            string filterNamaInsert = string.IsNullOrEmpty(searchText) ? "" : $"AND s.Nama LIKE '%{searchText}%'";

            DB.crud($"SELECT s.NISN, s.Nama, k.Kelas FROM tsiswa s " +
                    $"INNER JOIN tkelas k ON s.IDK = k.IDK " +
                    $"{whereKelasInsert} {filterNamaInsert} " +
                    $"ORDER BY k.IDK, s.Nama");

            if (DB.ds.Tables[0].Rows.Count == 0)
            {
                guna2Button2.Text      = "Simpan";
                guna2Button2.FillColor = System.Drawing.Color.DarkBlue;
                return;
            }

            var daftarSiswa = new List<(string nisn, string nama, string kelas)>();
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
                daftarSiswa.Add((brs["NISN"].ToString(), brs["Nama"].ToString(), brs["Kelas"].ToString()));

            DB.crud($"SELECT ta.IDAbsen, td.NISN, td.Hadir, td.Sakit, td.Izin, td.Alpa " +
                    $"FROM tabsensi ta " +
                    $"INNER JOIN tdetailabsensi td ON ta.IDAbsen = td.IDAbsen " +
                    $"INNER JOIN tsiswa s ON td.NISN = s.NISN " +
                    $"INNER JOIN tkelas k ON s.IDK = k.IDK " +
                    $"WHERE ta.Tanggal = '{tanggal}' {filterKelas} {filterNama}");

            var dataAbsen = new Dictionary<string, (int idAbsen, bool hadir, bool sakit, bool izin, bool alpa)>();
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                string nisn = brs["NISN"].ToString();
                dataAbsen[nisn] = (
                    Convert.ToInt32(brs["IDAbsen"]),
                    Convert.ToBoolean(brs["Hadir"]),
                    Convert.ToBoolean(brs["Sakit"]),
                    Convert.ToBoolean(brs["Izin"]),
                    Convert.ToBoolean(brs["Alpa"])
                );
            }

            bool adaYangSudahAbsen = false;

            foreach (var siswa in daftarSiswa)
            {
                if (dataAbsen.TryGetValue(siswa.nisn, out var absen))
                {
                    nisnToIdAbsen[siswa.nisn] = absen.idAbsen;
                    guna2DataGridView1.Rows.Add(siswa.nisn, siswa.nama, siswa.kelas, tanggal,
                        absen.hadir, absen.sakit, absen.izin, absen.alpa);
                    adaYangSudahAbsen = true;
                }
                else
                {
                    guna2DataGridView1.Rows.Add(siswa.nisn, siswa.nama, siswa.kelas, tanggal,
                        false, false, false, false);
                }
            }

            guna2Button2.Text      = adaYangSudahAbsen ? "Update" : "Simpan";
            guna2Button2.FillColor = adaYangSudahAbsen
                ? System.Drawing.Color.DarkGreen
                : System.Drawing.Color.DarkBlue;
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            MuatData();
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MuatData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MuatData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;

            if (!System.Array.Exists(kolomCheckbox, k => k == kolom)) return;

            guna2DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            bool nilaiSekarang = Convert.ToBoolean(guna2DataGridView1.Rows[baris].Cells[kolom].Value);

            if (nilaiSekarang)
            {
                foreach (int k in kolomCheckbox)
                {
                    if (k != kolom)
                        guna2DataGridView1.Rows[baris].Cells[k].Value = false;
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk disimpan!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                bool hadir = Convert.ToBoolean(row.Cells["Hadir"].Value);
                bool sakit = Convert.ToBoolean(row.Cells["Sakit"].Value);
                bool izin  = Convert.ToBoolean(row.Cells["Izin"].Value);
                bool alpa  = Convert.ToBoolean(row.Cells["Alpa"].Value);

                if (!hadir && !sakit && !izin && !alpa)
                {
                    string nama = row.Cells["Nama"].Value?.ToString() ?? "";
                    MessageBox.Show($"Status absensi untuk siswa '{nama}' belum dipilih!",
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string tanggal    = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
            bool modeUpdate   = nisnToIdAbsen.Count > 0;

            if (!modeUpdate)
            {
                var kelasGroups = new Dictionary<string, List<DataGridViewRow>>();
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    string kelas = row.Cells["Kelas"].Value?.ToString() ?? "";
                    if (!kelasGroups.ContainsKey(kelas))
                        kelasGroups[kelas] = new List<DataGridViewRow>();
                    kelasGroups[kelas].Add(row);
                }

                foreach (var group in kelasGroups)
                {
                    DB.crud($"INSERT INTO tabsensi (Tanggal, Idp) VALUES ('{tanggal}', {IdPetugas})");
                    DB.crud("SELECT MAX(IDAbsen) AS IDAbsen FROM tabsensi");
                    int idAbsenBaru = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["IDAbsen"]);

                    foreach (DataGridViewRow row in group.Value)
                    {
                        string nisn = row.Cells["NIS"].Value?.ToString() ?? "";
                        int h = Convert.ToBoolean(row.Cells["Hadir"].Value) ? 1 : 0;
                        int s = Convert.ToBoolean(row.Cells["Sakit"].Value) ? 1 : 0;
                        int iz = Convert.ToBoolean(row.Cells["Izin"].Value) ? 1 : 0;
                        int al = Convert.ToBoolean(row.Cells["Alpa"].Value) ? 1 : 0;

                        DB.crud($"INSERT INTO tdetailabsensi (IDAbsen, NISN, Hadir, Sakit, Izin, Alpa) " +
                                $"VALUES ({idAbsenBaru}, '{nisn}', {h}, {s}, {iz}, {al})");
                    }
                }

                MessageBox.Show("Absensi berhasil disimpan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var kelasIdAbsen = new Dictionary<string, int>();

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    string nisn  = row.Cells["NIS"].Value?.ToString() ?? "";
                    string kelas = row.Cells["Kelas"].Value?.ToString() ?? "";
                    if (nisnToIdAbsen.ContainsKey(nisn) && !kelasIdAbsen.ContainsKey(kelas))
                        kelasIdAbsen[kelas] = nisnToIdAbsen[nisn];
                }

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    string nisn  = row.Cells["NIS"].Value?.ToString() ?? "";
                    string kelas = row.Cells["Kelas"].Value?.ToString() ?? "";
                    int h  = Convert.ToBoolean(row.Cells["Hadir"].Value) ? 1 : 0;
                    int s  = Convert.ToBoolean(row.Cells["Sakit"].Value) ? 1 : 0;
                    int iz = Convert.ToBoolean(row.Cells["Izin"].Value)  ? 1 : 0;
                    int al = Convert.ToBoolean(row.Cells["Alpa"].Value)  ? 1 : 0;

                    if (nisnToIdAbsen.TryGetValue(nisn, out int idAbsen))
                    {
                        DB.crud($"UPDATE tdetailabsensi SET " +
                                $"Hadir={h}, Sakit={s}, Izin={iz}, Alpa={al} " +
                                $"WHERE IDAbsen={idAbsen} AND NISN='{nisn}'");
                    }
                    else
                    {
                        if (!kelasIdAbsen.TryGetValue(kelas, out int idAbsenKelas))
                        {
                            DB.crud($"INSERT INTO tabsensi (Tanggal, Idp) VALUES ('{tanggal}', {IdPetugas})");
                            DB.crud("SELECT MAX(IDAbsen) AS IDAbsen FROM tabsensi");
                            idAbsenKelas = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["IDAbsen"]);
                            kelasIdAbsen[kelas] = idAbsenKelas;
                        }

                        DB.crud($"INSERT INTO tdetailabsensi (IDAbsen, NISN, Hadir, Sakit, Izin, Alpa) " +
                                $"VALUES ({idAbsenKelas}, '{nisn}', {h}, {s}, {iz}, {al})");
                    }
                }

                MessageBox.Show("Absensi berhasil disimpan!", "Sukses",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MuatData();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            MuatData();
        }
    }
}
