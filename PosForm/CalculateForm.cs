using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace PosForm
{
    public partial class CalculateForm : Form
    {
        Table table;
        Sales sale;
         public CalculateForm(Table table,Sales sale)
        {
            InitializeComponent();
            this.table = table;
            this.sale = sale;
        }

        Image round_table = Image.FromFile("round_table.png");

        private ListViewItem FindListViewItem(string menuName)
        {
            // 리스트뷰에서 해당 메뉴를 찾아 반환합니다.
            foreach (ListViewItem item in listView_Order.Items)
            {
                if (item.Text == menuName)
                {
                    return item;
                }
            }

            return null; // 해당 메뉴를 찾지 못한 경우
        }
       

        //결제창이 로드될때 CSV파일에서 결제정보를 읽어와서 리스트뷰에 표시
        private void CalculateForm_Load(object sender, EventArgs e)
        {
            int seatIndex = table.SeatNumber; // Table 객체에서 좌석 인덱스 가져오기
            string csvFilePath = $"table{seatIndex}.csv"; // seatIndex에 따른 CSV 파일 경로 생성

            // 해당 좌석의 CSV 파일이 존재하는지 확인
            if (File.Exists(csvFilePath))
            {
                // CSV 파일이 존재하면 파일에서 데이터를 읽어와 ListView에 표시
                List<string[]> paymentData = File.ReadAllLines(csvFilePath)
                                                .Select(line => line.Split(','))
                                                .ToList();

                foreach (string[] paymentInfo in paymentData)
                {
                    ListViewItem item = new ListViewItem(paymentInfo);
                    listView_Order.Items.Add(item); // ListView에 결제 정보 추가

                    // 각 메뉴별 수량을 CSV 파일에서 가져온 데이터로 초기화
                    switch (paymentInfo[0]) // 예시로 메뉴명이 첫 번째 항목으로 저장된 것으로 가정
                    {
                        case "후라이드치킨":
                            table.FriedCount = int.Parse(paymentInfo[1]); // CSV 파일에서 후라이드치킨 수량을 가져와 초기화
                            table.FriedSum();
                            break;
                        case "양념치킨":
                            table.RedCount = int.Parse(paymentInfo[1]); 
                            table.RedSum();
                            break;
                        case "간장치킨":
                            table.SoyCount = int.Parse(paymentInfo[1]); 
                            table.SoySum();
                            break;
                        case "깐풍치킨":
                            table.KkanpungCount = int.Parse(paymentInfo[1]); 
                            table.KkanpungSum();
                            break;
                        case "탕수치킨":
                            table.TangsuCount = int.Parse(paymentInfo[1]); 
                            table.TangsuSum();
                            break;
                        case "어니언치킨":
                            table.OnionCount = int.Parse(paymentInfo[1]); 
                            table.OnionSum();
                            break;
                        case "감자튀김":
                            table.FrenchCount = int.Parse(paymentInfo[1]); 
                            table.FrenchSum();
                            break;
                        case "똥집튀김":
                            table.GizzardCount = int.Parse(paymentInfo[1]);
                            table.GizzardSum();
                            break;
                        case "소주":
                            table.SojuCount = int.Parse(paymentInfo[1]); 
                            table.SojuSum();
                            break;
                        case "맥주":
                            table.BeerCount = int.Parse(paymentInfo[1]); 
                            table.BeerSum();
                            break;
                        case "음료수(소)":
                            table.SmallDrinkCount = int.Parse(paymentInfo[1]); 
                            table.SmallDrinkSum();
                            break;
                        case "음료수(대)":
                            table.BigDrinkCount = int.Parse(paymentInfo[1]); 
                            table.BigDrinkSum();
                            break;

                    }
                }
            }

            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        //------------------------------------------------------------ 메뉴  버튼 -----------------------------------------------------------
        private void btn_fried_Click(object sender, EventArgs e)
        {
            string menuName = btn_fried.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                //이미 주문되어 있을경우
                table.FriedCount++; //메뉴 수량 증가
                existingItem.SubItems[1].Text = (table.FriedCount).ToString(); //수량표시
                table.FriedSum(); //메뉴 가격 계산
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Friedsum); //가격표시


            }
            else
            {
                //첫 주문일 경우
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.FriedCount)); //리스트뷰에 수량표시
                item.SubItems.Add(("₩") + Convert.ToString(table.FriedSum())); //가격 표시

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum()); //결제금액 표시
        }
        private void btn_red_Click(object sender, EventArgs e)
        {
            string menuName = btn_red.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.RedCount++;
                existingItem.SubItems[1].Text = (table.RedCount).ToString();
                table.RedSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Redsum);

            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.RedCount)); 
                item.SubItems.Add(("₩") + Convert.ToString(table.RedSum())); 

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_soy_Click(object sender, EventArgs e)
        {
            string menuName = btn_soy.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.SoyCount++;
                existingItem.SubItems[1].Text = (table.SoyCount).ToString();
                table.SoySum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Soysum);

            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.SoyCount)); 
                item.SubItems.Add(("₩") + Convert.ToString(table.SoySum())); 

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_kkanpung_Click(object sender, EventArgs e)
        {
            string menuName = btn_kkanpung.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.KkanpungCount++;
                existingItem.SubItems[1].Text = (table.KkanpungCount).ToString();
                table.KkanpungSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Kkanpungsum);

            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.KkanpungCount)); 
                item.SubItems.Add(("₩") + Convert.ToString(table.KkanpungSum())); 

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_tangsu_Click(object sender, EventArgs e)
        {
            string menuName = btn_tangsu.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.TangsuCount++;
                existingItem.SubItems[1].Text = (table.TangsuCount).ToString();
                table.TangsuSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Tangsusum);

            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.TangsuCount));
                item.SubItems.Add(("₩") + Convert.ToString(table.TangsuSum())); 

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_onion_Click(object sender, EventArgs e)
        {
            string menuName = btn_onion.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.OnionCount++;
                existingItem.SubItems[1].Text = (table.OnionCount).ToString();
                table.OnionSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Onionsum);

            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.OnionCount)); 
                item.SubItems.Add(("₩") + Convert.ToString(table.OnionSum()));

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_french_Click(object sender, EventArgs e)
        {
            string menuName = btn_french.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.FrenchCount++;
                existingItem.SubItems[1].Text = (table.FrenchCount).ToString();
                table.FrenchSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Frenchsum);


            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.FrenchCount));
                item.SubItems.Add(("₩") + Convert.ToString(table.FrenchSum()));

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }
        private void btn_gizzard_Click(object sender, EventArgs e)
        {
            string menuName = btn_gizzard.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.GizzardCount++;
                existingItem.SubItems[1].Text = (table.GizzardCount).ToString();
                table.GizzardSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Gizzardsum);


            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.GizzardCount));
                item.SubItems.Add(("₩") + Convert.ToString(table.GizzardSum()));

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }
        private void btn_beer_Click(object sender, EventArgs e)
        {
            string menuName = btn_beer.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.BeerCount++;
                existingItem.SubItems[1].Text = (table.BeerCount).ToString();
                table.BeerSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Beersum);


            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.BeerCount));
                item.SubItems.Add(("₩") + Convert.ToString(table.BeerSum()));

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_soju_Click(object sender, EventArgs e)
        {
            string menuName = btn_soju.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.SojuCount++;
                existingItem.SubItems[1].Text = (table.SojuCount).ToString();
                table.SojuSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.Sojusum);


            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.SojuCount));
                item.SubItems.Add(("₩") + Convert.ToString(table.SojuSum()));

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_smalldrink_Click(object sender, EventArgs e)
        {
            string menuName = btn_smalldrink.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.SmallDrinkCount++;
                existingItem.SubItems[1].Text = (table.SmallDrinkCount).ToString();
                table.SmallDrinkSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.SmallDrinksum);


            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.SmallDrinkCount));
                item.SubItems.Add(("₩") + Convert.ToString(table.SmallDrinkSum()));

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }

        private void btn_bigdrink_Click(object sender, EventArgs e)
        {
            string menuName = btn_bigdrink.Text;

            // 리스트뷰에서 해당 메뉴를 찾아봅니다.
            ListViewItem existingItem = FindListViewItem(menuName);

            if (existingItem != null)
            {
                // 이미 리스트뷰에 해당 메뉴가 있으면 수량만 증가시킵니다.
                table.BigDrinkCount++;
                existingItem.SubItems[1].Text = (table.BigDrinkCount).ToString();
                table.BigDrinkSum();
                existingItem.SubItems[2].Text = ("₩") + Convert.ToString(table.BigDrinksum);


            }
            else
            {
                // 새로운 ListViewItem을 생성하고 아이템을 추가합니다.
                ListViewItem item = new ListViewItem(menuName);
                item.SubItems.Add(Convert.ToString(table.BigDrinkCount));
                item.SubItems.Add(("₩") + Convert.ToString(table.BigDrinkSum()));

                // 리스트뷰에 아이템 추가
                listView_Order.Items.Add(item);
            }
            textBox_cash.Text = Convert.ToString(table.Sum());
        }
//-----------------------------------------------------------------------------------------------

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        
        //메뉴 주문 버튼
        private void btn_order_Click(object sender, EventArgs e)
        {
            int seatIndex = table.SeatNumber; // Table 객체에서 좌석 인덱스 가져오기
            string csvFilePath = $"table{seatIndex}.csv"; // seatIndex에 따른 CSV 파일 경로 생성


            // 리스트뷰의 각 아이템을 순회하며 CSV 파일에 데이터를 저장
            using (StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                foreach (ListViewItem item in listView_Order.Items)
                {
                    string line = $"{item.SubItems[0].Text},{item.SubItems[1].Text},{item.SubItems[2].Text}"; //메뉴명, 수량, 금액
                    sw.WriteLine(line); // 리스트뷰에 있는 결제 정보를 CSV 파일에 추가
                }
            }

            // CSV 파일에서 메뉴명을 저장할 리스트를 생성합니다.
            List<string> menuNames = new List<string>();

            // CSV 파일의 각 줄을 읽어옵니다.
            foreach (string line in File.ReadAllLines(csvFilePath))
            {
                // 각 줄을 쉼표(,)를 기준으로 분할하여 배열로 저장합니다.
                string[] columns = line.Split(',');

                // 열의 길이가 1 이상인 경우에만 처리합니다.
                if (columns.Length > 0)
                {
                    // 각 줄의 첫 번째 열에 있는 데이터(메뉴명)를 리스트에 추가합니다.
                    menuNames.Add(columns[0]); // 메뉴명 가져오기
                }
            }

            string buttonName = $"btn_seat{seatIndex}";

            // BusinessForm에서 해당 버튼 찾기
            BusinessForm businessForm = Application.OpenForms.OfType<BusinessForm>().FirstOrDefault();

            if (businessForm != null)
            {
                System.Windows.Forms.Button seatButton = businessForm.Controls.Find(buttonName, true).FirstOrDefault() as System.Windows.Forms.Button;

                if (seatButton != null)
                {
                    seatButton.Image = null;
                    seatButton.Text = string.Join(", ", menuNames); //주문 메뉴를 테이블 버튼에 표시
                    seatButton.BackColor = Color.DeepSkyBlue; // 버튼 배경색을 파란색으로 설정
                }
            }
            MessageBox.Show("주문이 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();    

        }

        private void listView_Order_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //현금 결제 버튼
        private void btn_cash_Click(object sender, EventArgs e)
        {
            if (listView_Order.Items.Count == 0)
                MessageBox.Show("메뉴를 선택해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DialogResult dlr = MessageBox.Show("현금결제 하시겠습니까?", "현금 결제", MessageBoxButtons.YesNo);
                switch (dlr)
                {
                    case DialogResult.Yes:

                        listView_Order.Items.Clear();
                        sale.friedsum += table.Friedsum; //테이블의 메뉴별 결제 금액을 총 매출을 저장하는 sale객체의 필드들에 저장함
                        sale.redsum += table.Redsum;
                        sale.soysum += table.Soysum;
                        sale.kkanpungsum += table.Kkanpungsum;
                        sale.tangsusum += table.Tangsusum;
                        sale.onionsum += table.Onionsum;
                        sale.frenchsum += table.Frenchsum;
                        sale.gizzardsum += table.Gizzardsum;
                        sale.sojusum += table.Sojusum;
                        sale.smalldrinksum += table.SmallDrinksum;
                        sale.bigdrinksum += table.BigDrinksum;

                        table.FriedCount = 1; //메뉴별 수량 초기화
                        table.Friedsum = 0; //테이블에 저장돤 메뉴별 결제 금액 초기화
                        table.RedCount = 1;
                        table.Redsum = 0;
                        table.SoyCount = 1;
                        table.Soysum = 0;
                        table.TangsuCount = 1;
                        table.Tangsusum = 0;
                        table.KkanpungCount = 1;
                        table.Kkanpungsum = 0;
                        table.OnionCount = 1;
                        table.Onionsum = 0;
                        table.FrenchCount = 1;
                        table.Frenchsum = 0;
                        table.GizzardCount = 1;
                        table.Gizzardsum = 0;
                        table.SojuCount = 1;
                        table.Sojusum = 0;
                        table.BeerCount = 1;
                        table.Beersum = 0;
                        table.SmallDrinkCount = 1;
                        table.SmallDrinksum = 0;
                        table.BigDrinkCount = 1;
                        table.BigDrinksum = 0;
                        sale.Amount(); // 총 매출 계산
                        textBox_cash.Text = null; //결제금액 표시 초기화
                        Close();

                        int seatIndex = table.SeatNumber;
                        string buttonName = $"btn_seat{seatIndex}";
                        //// 현재 열려있는 폼 중 BusinessForm 타입의 폼을 찾아서 가져옴
                        BusinessForm businessForm = Application.OpenForms.OfType<BusinessForm>().FirstOrDefault();

                        string csvFilePath = $"table{seatIndex}.csv";
                        if (File.Exists(csvFilePath)) // CSV 파일이 존재하면
                        {
                            try
                            {
                                File.Delete(csvFilePath); //삭제
                                MessageBox.Show("결제가 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"결제 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        if (businessForm != null)
                        {
                            System.Windows.Forms.Button seatButton = businessForm.Controls.Find(buttonName, true).FirstOrDefault() as System.Windows.Forms.Button;

                            if (seatButton != null)
                            {
                                seatButton.Image = round_table; //테이블 이미지 표시
                                seatButton.Text = ""; //테이블 텍스트 초기화
                                seatButton.BackColor = Color.Transparent; //테이블 버튼 색상 초기화
                            }
                        }
                        break;

                }
            }
        }

        //카드결제 버튼
        private void btn_card_Click(object sender, EventArgs e)
        {
            if (listView_Order.Items.Count == 0)
                MessageBox.Show("메뉴를 선택해주세요", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DialogResult dlr = MessageBox.Show("카드결제 하시겠습니까?", "카드 결제", MessageBoxButtons.YesNo);
                switch (dlr)
                {
                    case DialogResult.Yes:
                        listView_Order.Items.Clear();
                        sale.friedsum += table.Friedsum;
                        sale.redsum += table.Redsum;
                        sale.soysum += table.Soysum;
                        sale.kkanpungsum += table.Kkanpungsum;
                        sale.tangsusum += table.Tangsusum;
                        sale.onionsum += table.Onionsum;
                        sale.frenchsum += table.Frenchsum;
                        sale.gizzardsum += table.Gizzardsum;
                        sale.sojusum += table.Sojusum;
                        sale.smalldrinksum += table.SmallDrinksum;
                        sale.bigdrinksum += table.BigDrinksum;
                        table.FriedCount = 1;
                        table.Friedsum = 0;
                        table.RedCount = 1;
                        table.Redsum = 0;
                        table.SoyCount = 1;
                        table.Soysum = 0;
                        table.TangsuCount = 1;
                        table.Tangsusum = 0;
                        table.KkanpungCount = 1;
                        table.Kkanpungsum = 0;
                        table.OnionCount = 1;
                        table.Onionsum = 0;
                        table.FrenchCount = 1;
                        table.Frenchsum = 0;
                        table.GizzardCount = 1;
                        table.Gizzardsum = 0;
                        table.SojuCount = 1;
                        table.Sojusum = 0;
                        table.BeerCount = 1;
                        table.Beersum = 0;
                        table.SmallDrinkCount = 1;
                        table.SmallDrinksum = 0;
                        table.BigDrinkCount = 1;
                        table.BigDrinksum = 0;
                        sale.Amount();
                        textBox_cash.Text = null;

                        int seatIndex = table.SeatNumber;
                        string csvFilePath = $"table{seatIndex}.csv"; // seatIndex에 따른 CSV 파일 경로 생성
                        if (File.Exists(csvFilePath))
                        {
                            try
                            {
                                File.Delete(csvFilePath);
                                MessageBox.Show("결제가 완료되었습니다..", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"파일 삭제 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        string buttonName = $"btn_seat{seatIndex}"; //테이블 번호에 따라 버튼을 제어하기 위함
                        BusinessForm businessForm = Application.OpenForms.OfType<BusinessForm>().FirstOrDefault();

                        if (businessForm != null)
                        {
                            System.Windows.Forms.Button seatButton = businessForm.Controls.Find(buttonName, true).FirstOrDefault() as System.Windows.Forms.Button;

                            if (seatButton != null) //좌석 버튼 설정
                            {
                                seatButton.Image = round_table;
                                seatButton.Text = "";
                                seatButton.BackColor = Color.Transparent;
                            }
                        }
                        Close();

                        break;
                }
            }
        }

        private void textBox_cash_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_CancelAll_Click(object sender, EventArgs e)
        {
            //메인메뉴
            table.FriedCount = 1;
            table.Friedsum = 0;

            table.RedCount = 1;
            table.Redsum = 0;

            table.SoyCount = 1;
            table.Soysum = 0;

            table.KkanpungCount = 1;
            table.Kkanpungsum = 0;

            table.TangsuCount = 1;
            table.Tangsusum = 0;

            table.OnionCount = 1;
            table.Onionsum = 0;

            //사이드메뉴
            table.FrenchCount = 1;
            table.Frenchsum = 0;

            table.GizzardCount = 1;
            table.Gizzardsum = 0;

            //주류 및 음료수
            table.SojuCount = 1;
            table.Sojusum = 0;

            table.BeerCount = 1;
            table.Beersum = 0;

            table.SmallDrinkCount = 1;
            table.SmallDrinksum = 0;

            table.BigDrinkCount = 1;
            table.BigDrinksum = 0;

            listView_Order.Items.Clear();

            // 총금액을 0으로 초기화
            textBox_cash.Text = Convert.ToString(table.Sum());

        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            if (listView_Order.SelectedItems.Count > 0)  //선택된 항목이 있으면
            {
                // 선택된 모든 항목을 리스트에 추가하고 동시에 제거
                foreach (ListViewItem selectedItem in listView_Order.SelectedItems)
                {
                    switch (selectedItem.Text)
                    {
                        case "후라이드치킨":
                            table.FriedCount = 1; //메뉴별 수량 초기화
                            table.Friedsum = 0; //테이블에 저장돤 메뉴별 결제 금액 초기화
                            break;
                        case "양념치킨":
                            table.RedCount = 1;
                            table.Redsum = 0;
                            break;
                        case "간장치킨":
                            table.SoyCount = 1;
                            table.Soysum = 0;
                            break;
                        case "깐풍치킨":
                            table.KkanpungCount = 1;
                            table.Kkanpungsum = 0;
                            break;
                        case "탕수치킨":
                            table.TangsuCount = 1;
                            table.Tangsusum = 0;
                            break;
                        case "어니언치킨":
                            table.OnionCount = 1;
                            table.Onionsum = 0;
                            break;
                        case "감자튀김":
                            table.FrenchCount = 1;
                            table.Frenchsum = 0;
                            break;
                        case "똥집튀김":
                            table.GizzardCount = 1;
                            table.Gizzardsum = 0;
                            break;
                        case "소주":
                            table.SojuCount = 1;
                            table.Sojusum = 0;
                            break;
                        case "맥주":
                            table.BeerCount = 1;
                            table.Beersum = 0;
                            break;
                        case "음료수(소)":
                            table.SmallDrinkCount = 1;
                            table.SmallDrinksum = 0;
                            break;
                        case "음료수(대)":
                            table.BigDrinkCount = 1;
                            table.BigDrinksum = 0;
                            break;
                        default:
                            break;
                    }

                    // 선택된 항목들 제거
                    listView_Order.Items.Remove(selectedItem);
                }

                textBox_cash.Text = Convert.ToString(table.Sum());
            }
        }

       
    }
}
