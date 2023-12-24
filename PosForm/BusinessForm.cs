using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PosForm
{

    public partial class BusinessForm : Form
    {
        Table[] tables = new Table[8];
        Sales sale = new Sales();
        public BusinessForm()
        {
            InitializeComponent();
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i] = new Table(i + 1); // 좌석 번호는 1부터 시작
                LoadChickenPricesFromCSV("menu.csv", tables[i]);

            }

        }

        
        private void BusinessForm_Load(object sender, EventArgs e)
        {
            
        }

        //클릭한 버튼의 이름에서 좌석번호만 가져와서 좌석번호에 해당하는 테이블 객체를 인수로 결제폼 실행
        private void btn_seat_Click(object sender, EventArgs e)
        {
            //클릭한 버튼을 명시적으로 버튼 객체로 캐스팅하여 클릭된 버튼에 접근
            System.Windows.Forms.Button clickedButton = sender as System.Windows.Forms.Button; 

            // 버튼의 이름에서 "btn_seat"을 제거하고, 남은 숫자만 가져와 정수로 변환 ->좌석번호
            if (int.TryParse(clickedButton.Name.Replace("btn_seat", ""), out int seatIndex))
            {
                // 좌석번호가 테이블 배열의 인덱스 범위 내에 있는지 확인
                if (seatIndex >= 0 && seatIndex < tables.Length)
                {
                    //tables클래스 객체배열을 인수로 결제폼 실행
                    OpenCalculateForm(tables[seatIndex]);
                }
                else
                {
                    MessageBox.Show("유효하지 않은 좌석 번호입니다.");
                }
            }
        }

        private void OpenCalculateForm(Table table)
        {
            // CalculateForm 인스턴스 생성
            CalculateForm clm = new CalculateForm(table, sale);

            // 결제 창 열기
            clm.ShowDialog();
        }


        //치킨 가격을 menu.csv에서 읽어옴
        private void LoadChickenPricesFromCSV(string filePath, Table table)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.Default);

                foreach (string line in lines)
                {
                    // 각 줄에서 메뉴 이름과 가격을 추출
                    string[] fields = line.Split(',');
                    
                        string menuName = fields[0].Trim(); // 공백제거로 메뉴 이름 가져옴
                        int menuPrice;

                        // 두 번째 열에서 가격을 추출
                        if (int.TryParse(fields[1].Trim(), out menuPrice))
                        {
                            // Table 클래스의 변수들의 값으로 설정합니다.
                            SetChickenPrice(menuName, menuPrice, table);
                        }
                        
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"메뉴 파일을 읽는 도중 오류가 발생했습니다: {ex.Message}");
            }
        }


        // 메뉴이름에 따라 메뉴가격을 설정하는 메소드
        private void SetChickenPrice(string menuName, int menuPrice, Table table)
        {
            switch (menuName)
            {
                case "후라이드치킨":
                    table.FriedChicken = menuPrice;
                    break;
                case "양념치킨":
                    table.RedChicken = menuPrice;
                    break;
                case "간장치킨":
                    table.SoyChicken = menuPrice;
                    break;
                case "깐풍치킨":
                    table.KkanpungChicken = menuPrice;
                    break;
                case "탕수치킨":
                    table.TangsuChicken = menuPrice;
                    break;
                case "어니언치킨":
                    table.OnionChicken = menuPrice;
                    break;
                case "감자튀김":
                    table.FrenchFries = menuPrice;
                    break;
                case "똥집튀김":
                    table.GizzardFries = menuPrice;
                    break;
                case "소주":
                    table.Soju = menuPrice;
                    break;
                case "맥주":
                    table.Beer = menuPrice;
                    break;
                case "음료수(소)":
                    table.SmallDrink = menuPrice;
                    break;
                case "음료수(대)":
                    table.BigDrink = menuPrice;
                    break;

            }
        }

        private void btn_seat1_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[0]); 
        }

        private void btn_seat2_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[1]);
        }

        private void btn_seat3_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[2]);
        }

        private void btn_seat4_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[3]);
        }

        private void btn_seat5_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[4]);
        }

        private void btn_seat6_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[5]);
        }

        private void btn_seat7_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[6]);
        }

        private void btn_seat8_Click(object sender, EventArgs e)
        {
            OpenCalculateForm(tables[7]);
        }


        //매출을 sales.csv에 저장하는 메소드
        void AddDataToCSV(string filePath, string date, params int[] sales)
        {
            if (!File.Exists(filePath))
            {
                string header = "날짜,후라이드치킨,양념치킨,간장치킨,깐풍치킨,탕수치킨,어니언치킨,감자튀김,똥집튀김,소주,맥주,음료수(소),음료수(대),총매출";
                File.WriteAllText(filePath, header + Environment.NewLine, Encoding.UTF8);
            }

            // 데이터를 CSV 파일에 추가
            using (StreamWriter writer = new StreamWriter(filePath, true, new UTF8Encoding(true)))
            {
                string line = $"{date},{string.Join(",", sales)}";
                writer.WriteLine(line);
            }
        }

        private void BusinessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filePath = "sale.csv";
            DateTime currentDate = DateTime.Now;

            // 날짜를 원하는 형식으로 포맷팅
            string formattedDate = currentDate.ToString("yyyy-MM-dd");

            AddDataToCSV(filePath, formattedDate, sale.friedsum, sale.redsum, sale.soysum, sale.kkanpungsum, sale.tangsusum, sale.onionsum, sale.frenchsum, sale.gizzardsum, sale.sojusum, sale.beersum, sale.smalldrinksum, sale.bigdrinksum, sale.amount);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
