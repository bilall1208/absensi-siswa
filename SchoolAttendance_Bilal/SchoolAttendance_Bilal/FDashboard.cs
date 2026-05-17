using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace T1_Bilal_XIRPLA
{
    public partial class FDashboard : Form
    {
        public int IDKelas = -1;

        public FDashboard()
        {
            InitializeComponent();
        }

        private void FDashboard_Load(object sender, EventArgs e)
        {
            MuatDashboard();
        }

        private void MuatDashboard()
        {
            string hariIni = DateTime.Now.ToString("yyyy-MM-dd");
            bool isGuru = IDKelas >= 0;

            if (isGuru)
            {
                DB.crud($"SELECT COUNT(*) AS Total FROM tsiswa WHERE IDK = {IDKelas} AND NISN != ''");
                guna2HtmlLabel13.Text = DB.ds.Tables[0].Rows[0]["Total"].ToString();

                DB.crud($"SELECT Kelas FROM tkelas WHERE IDK = {IDKelas}");
                guna2HtmlLabel8.Text = DB.ds.Tables[0].Rows.Count > 0
                    ? DB.ds.Tables[0].Rows[0]["Kelas"].ToString() : "-";
                guna2HtmlLabel4.Text = "-";

                DB.crud($"SELECT COALESCE(SUM(td.Hadir), 0) AS TotalHadir, COUNT(td.NISN) AS TotalAbsen " +
                        $"FROM tdetailabsensi td " +
                        $"INNER JOIN tabsensi ta ON td.IDAbsen = ta.IDAbsen " +
                        $"INNER JOIN tsiswa s ON td.NISN = s.NISN " +
                        $"WHERE ta.Tanggal = '{hariIni}' AND s.IDK = {IDKelas}");
            }
            else
            {
                DB.crud("SELECT COUNT(*) AS Total FROM tsiswa WHERE NISN != ''");
                guna2HtmlLabel13.Text = DB.ds.Tables[0].Rows[0]["Total"].ToString();

                DB.crud("SELECT COUNT(*) AS Total FROM tpetugas WHERE role = 'Guru'");
                guna2HtmlLabel8.Text = DB.ds.Tables[0].Rows[0]["Total"].ToString();

                DB.crud("SELECT COUNT(*) AS Total FROM tkelas");
                guna2HtmlLabel4.Text = DB.ds.Tables[0].Rows[0]["Total"].ToString();

                DB.crud($"SELECT COALESCE(SUM(td.Hadir), 0) AS TotalHadir, COUNT(td.NISN) AS TotalAbsen " +
                        $"FROM tdetailabsensi td " +
                        $"INNER JOIN tabsensi ta ON td.IDAbsen = ta.IDAbsen " +
                        $"WHERE ta.Tanggal = '{hariIni}'");
            }

            int totalAbsen = 0;
            int totalHadir = 0;

            if (DB.ds.Tables[0].Rows.Count > 0)
            {
                totalAbsen = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["TotalAbsen"]);
                totalHadir = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["TotalHadir"]);
            }

            int totalTidakHadir = totalAbsen - totalHadir;
            double pctHadir = totalAbsen > 0 ? Math.Round((double)totalHadir / totalAbsen * 100, 1) : 0;
            double pctTidak = totalAbsen > 0 ? Math.Round(100 - pctHadir, 1) : 0;

            guna2HtmlLabel12.Text = pctHadir + "%";
            guna2HtmlLabel15.Text = pctTidak + "%";
            guna2HtmlLabel16.Text = "(" + totalHadir + " siswa)";
            guna2HtmlLabel10.Text = pctHadir + "%";

            IsiChart(totalHadir, totalTidakHadir);
        }

        private void IsiChart(int hadir, int tidakHadir)
        {
            chart2.Series["Series1"].Points.Clear();
            chart2.Series["Series1"].ChartType = SeriesChartType.Pie;
            chart2.Series["Series1"]["PieRadius"] = "4 0";

            if (hadir == 0 && tidakHadir == 0)
            {
                chart2.Series["Series1"].Points.AddXY("Belum Ada Data", 1);
                chart2.Series["Series1"].Points[0].Color = Color.LightGray;
                chart2.Series["Series1"].Points[0].LegendText = "Belum Ada Data";
            }
            else
            {
                int idxHadir = chart2.Series["Series1"].Points.AddXY("Hadir", hadir);
                chart2.Series["Series1"].Points[idxHadir].Color = Color.Green;
                chart2.Series["Series1"].Points[idxHadir].LegendText = "Hadir";

                int idxTidak = chart2.Series["Series1"].Points.AddXY("Tidak Hadir", tidakHadir);
                chart2.Series["Series1"].Points[idxTidak].Color = Color.Red;
                chart2.Series["Series1"].Points[idxTidak].LegendText = "Tidak Hadir";
            }

            chart2.Series["Series1"].IsValueShownAsLabel = false;
            chart2.ChartAreas["ChartArea1"].BackColor = Color.Transparent;
            chart2.BackColor = Color.Transparent;
            chart2.Legends["Legend1"].BackColor = Color.Transparent;
            chart2.Legends["Legend1"].Font = new Font("Microsoft YaHei UI", 10f);
        }

        private void FixLabelPosition(Control label, Control panel)
        {
            System.Drawing.Point oldPos = label.Location;
            label.Parent = panel;
            label.Location = new System.Drawing.Point(oldPos.X - panel.Location.X, oldPos.Y - panel.Location.Y);
            label.BackColor = Color.Transparent;
        }

        private void guna2Shapes6_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel6_Click(object sender, EventArgs e) { }
        private void chart2_Click(object sender, EventArgs e) { }
    }
}
