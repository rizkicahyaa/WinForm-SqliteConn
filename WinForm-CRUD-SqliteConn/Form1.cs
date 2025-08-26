using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SQLite;

namespace WinForm_CRUD_SqliteConn
{
    public partial class Form1 : Form
    {
        private List<Mahasiswa> list = new List<Mahasiswa>();

        public class Mahasiswa
        {
            public string Nim { get; set; }
            public string Nama { get; set; }
            public string Kelas { get; set; }
        }


        public Form1()
        {
            InitializeComponent();
            InisialisasiListView();
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nim", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Kelas", 70, HorizontalAlignment.Center);
        }

        private void ResetForm()
        {
            txtNim.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            txtNim.Focus();
        }

        private void TampilkanData()
        {
            lvwMahasiswa.Items.Clear();

            using (var conn = GetOpenConnection())
            {
                string sql = "SELECT * FROM mahasiswa";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    int noUrut = 1;
                    while (reader.Read())
                    {
                        var item = new ListViewItem(noUrut.ToString());
                        item.SubItems.Add(reader["nim"].ToString());
                        item.SubItems.Add(reader["nama"].ToString());
                        item.SubItems.Add(reader["kelas"].ToString());
                        lvwMahasiswa.Items.Add(item);
                        noUrut++;
                    }
                }
            }
        }

        public static SQLiteConnection GetOpenConnection()
        {
            SQLiteConnection conn = null;
            try
            {
                string dbName = @"C:\Users\user\source\repos\WinForm-CRUD-SqliteConn\Database\DbMahasiswa.db";
                string connectionString = string.Format("Data Source ={0}; FailIfMissing = True", dbName);

                conn = new SQLiteConnection(connectionString);
                conn.Open(); // buka koneksi ke database
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            Mahasiswa mhs = new Mahasiswa();

            mhs.Nim = txtNim.Text;
            mhs.Nama = txtNama.Text;
            mhs.Kelas = txtKelas.Text;

            list.Add(mhs);

            var message = "Data berhasil disimpan.";

            MessageBox.Show(message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ResetForm();
        }

        private void btnTampilkan_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var index = lvwMahasiswa.SelectedIndices[0];
                list.RemoveAt(index);
                TampilkanData();
                MessageBox.Show("Data berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Pilih data yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnTesKoneksi_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = GetOpenConnection();

            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Koneksi ke database berhasil.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Koneksi ke database gagal.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                conn.Dispose();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void lvwMahasiswa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
