using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PosForm
{
    public partial class CashupForm : Form
    {
        private DataTable salesDataTable;
        public CashupForm()
        {
            InitializeComponent();

        }
       

        private void CashupForm_Load(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart;

            string csvFilePath = "sale.csv";

            // CSV 파일에서 데이터를 읽어와 DataTable에 저장
            DataTable dataTable = ReadCsvToDataTable(csvFilePath);

            // DataGridView에 데이터 바인딩
            DataView dv = new DataView(dataTable);
            dv.RowFilter = $"[날짜] = #{selectedDate.ToString("yyyy-MM-dd")}#";

            // DataGridView에 데이터 바인딩
            dataGridView1.DataSource = dv.ToTable();
        }

        private DataTable ReadCsvToDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // CSV 파일을 읽어와서 DataTable에 저장
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string[] headers = reader.ReadLine().Split(',');

                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    while (!reader.EndOfStream)
                    {
                        string[] rows = reader.ReadLine().Split(','); //한줄 읽는데 ,로 구분해서 배열로 저장
                        DataRow dataRow = dataTable.NewRow(); //새로운 행 생성

                        for (int i = 0; i < headers.Length; i++) //헤더의 개수만큼 반복
                        {
                            dataRow[i] = rows[i]; //배열에 저장된 데이터를 datarow에 저장
                        }

                        dataTable.Rows.Add(dataRow); //datatable에 추가
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return dataTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
