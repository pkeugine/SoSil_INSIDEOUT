using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_Baseballgame
{
    public partial class Form1 : Form
    {

        int answer = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("게임이 시작됩니다!");
            Random rand = new Random();
            int rnum = rand.Next(10, 99);
            answer = rnum;
            MessageBox.Show("두자리 정수가 생성되었습니다. 맞혀보세요");
           
        }
        
        private void Result_button_Click(object sender, EventArgs e)
        {

                if (answer == (Convert.ToInt32(textBox1.Text)))
                {
                    MessageBox.Show("정답입니다! 포인트를 획득하셨습니다");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("오답입니다. 계속 시도해보세요");
                }
                
           
        }
        
        private void Show_Ans_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("정답은 '확인'칸을 누르면 나옵니다. 이후 포인트는 자동 소멸되며 게임은 자동 종료됩니다");
            MessageBox.Show(this.answer.ToString());
            this.Close();
        }

        private void RESET_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Box1_CheckedChanged(object sender, EventArgs e)
        {
            if (answer >= 20)
                MessageBox.Show("넵");
            else
                MessageBox.Show("아니오");
        }

        private void Box2_CheckedChanged(object sender, EventArgs e)
        {
            if (answer >= 40)
                MessageBox.Show("넵");
            else
                MessageBox.Show("아니오");
        }

        private void Box3_CheckedChanged(object sender, EventArgs e)
        {
            if (answer >= 60)
                MessageBox.Show("넵");
            else
                MessageBox.Show("아니오");
        }

        private void Box4_CheckedChanged(object sender, EventArgs e)
        {
            if (answer >= 80)
                MessageBox.Show("넵");
            else
                MessageBox.Show("아니오");
        }

        private void Box5_CheckedChanged(object sender, EventArgs e)
        {
            for (int k = 2; k <= answer / 2; k++)
            {
                if (answer % k == 0)
                {
                    MessageBox.Show("아니오");
                    return;
                }
            }

            MessageBox.Show("넵");
        }

        private void Box7_CheckedChanged(object sender, EventArgs e)
        {
            if (answer % 4 == 0)
                MessageBox.Show("넵");
            else
                MessageBox.Show("아니오");

        }

        private void Box6_CheckedChanged(object sender, EventArgs e)
        {
            if (answer % 3 == 0)
                MessageBox.Show("넵");
            else
                MessageBox.Show("아니오");
        }
    }
}
