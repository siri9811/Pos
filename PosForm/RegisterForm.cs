using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PosForm
{
    public partial class RegisterForm : Form
    {
        Product pdt = new Product();
        public RegisterForm()
        {
            InitializeComponent();
            LoadCsvToListView();
        }
        private void LoadCsvToListView()
        {
            // CSV 파일 경로
            string filePath = "Menu.csv"; // 적절한 파일 경로로 수정

            // 리스트뷰 초기화
            listView1.Items.Clear();

            // CSV 파일을 읽어올 때 사용할 StreamReader
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                // 나머지 줄을 읽어서 리스트뷰에 추가
                while (!sr.EndOfStream)
                {
                    string[] values = sr.ReadLine().Split(',');

                    // 리스트뷰 아이템 생성
                    ListViewItem item = new ListViewItem(values);

                    // 리스트뷰에 아이템 추가
                    listView1.Items.Add(item);
                }
            }
        }
        public void formClear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        public void AddMember(Product pdt)
        {
            string[] sitems = new string[] { pdt.Name, pdt.Price };
            ListViewItem lvi = new ListViewItem(sitems);
            listView1.Items.Add(lvi);
            listView1.EndUpdate();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {















        }

        private void btn_reg_Click(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            pdt.Name = textBox1.Text.ToString();
            pdt.Price = textBox2.Text.ToString();
            AddMember(pdt);
            formClear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void btn_mod_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
                listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Menu.csv");

            // 파일이 이미 존재하면 기존 데이터를 보존하고, 없다면 파일을 생성합니다.
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                foreach (ListViewItem lvi in listView1.Items)
                {
                    string name = lvi.SubItems[0].Text;
                    string price = lvi.SubItems[1].Text;

                    sw.WriteLine($"{name},{price}");
                }
            }

            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_mod_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
                listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;
            }
        }
    }
}
