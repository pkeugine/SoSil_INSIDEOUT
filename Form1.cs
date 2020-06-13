using System;
using System.IO; // 파일 입출력
using System.Media; // 소리 재생
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation_Form
{
    public partial class Form1 : Form
    {
        private Button btn_Main;
        private Button btn_Play;

        private Button btn_A;
        private Button btn_B;

        private Label lbl_User;
        private TextBox txt_Name;

        bool startCreate = false;



        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            lbl_Gamename.Parent = Picbox_Background;
            lbl_Gamename.BackColor = Color.Transparent;
            lbl_Gamename.BringToFront();

            Picbox_SchoolImage.Parent = Picbox_Background;
            btn_Start.Parent = Picbox_Background;
            btn_Prolog.Parent = Picbox_Background;
            btn_Exit.Parent = Picbox_Background;

            btn_Start.FlatStyle = FlatStyle.Flat;
            btn_Prolog.FlatStyle = FlatStyle.Flat;
            btn_Exit.FlatStyle = FlatStyle.Flat;
        }

        /* 초기 화면 설정 */

        /* 버튼 컨트롤에 대한 마우스 On,Over,Click Event */

        /* 종료 */
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Exit_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Exit.FlatAppearance.BorderSize = 3;

        }
        private void btn_Exit_MouseLeave(object sender, EventArgs e)
        {
            btn_Exit.FlatAppearance.BorderSize = 1;
            btn_Exit.BackColor = Color.Transparent;
        }
        /* 프롤로그 */
        private void btn_Prolog_Click(object sender, EventArgs e)
        {
            btn_Start.Hide();
            btn_Prolog.Hide();
            btn_Exit.Hide();
            lbl_Gamename.Hide();
            Picbox_SchoolImage.Hide();
            Picbox_Background.Image = Properties.Resources.Prolog; 

            if (btn_Main == null)
            {
                /* 뒤로가기 버튼 동적으로 생성 */
                btn_Main = new Button();
                btn_Main.BackColor = System.Drawing.Color.Transparent;
                btn_Main.Parent = Picbox_Background;
                btn_Main.FlatStyle = FlatStyle.Flat;
                btn_Main.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(40)));
                btn_Main.ForeColor = System.Drawing.Color.DarkCyan;
                btn_Main.Location = new System.Drawing.Point(650, 350);
                btn_Main.Name = "btn_Main";
                btn_Main.Size = new System.Drawing.Size(150, 50);
                btn_Main.Text = "돌아가기";
                btn_Main.BringToFront();
                btn_Main.Click += new System.EventHandler(this.btn_Main_Click);
                btn_Main.MouseLeave += new System.EventHandler(this.btn_Main_MouseLeave);
                btn_Main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_Main_MouseMove);
            }
            else if (btn_Main.Visible == false)
            {
                btn_Main.Visible = true;
            }

            /* 뒤로가기 버튼으로 마우스 커서 위치 변경 */
            int locX = btn_Main.Location.X;
            int locY = btn_Main.Location.Y;
            int dX = (btn_Main.Width / 2) + locX;
            int dY = (btn_Main.Height / 2) + locY;

            Point Mouse = new Point(dX, dY);
            Cursor.Position = this.PointToScreen(Mouse);
        }

        /* 동적으로 생성된 뒤로가기 버튼 */
        private void btn_Main_MouseLeave(object sender, EventArgs e)
        {
            btn_Main.FlatAppearance.BorderSize = 1;
            btn_Main.BackColor = Color.Transparent;
        }
        private void btn_Main_MouseMove(object sender, EventArgs e)
        {
            btn_Main.FlatAppearance.BorderSize = 3;
        }
        private void btn_Main_Click(object sender, EventArgs e)
        {
            btn_Main.Visible = false;
            btn_Start.Show();
            btn_Prolog.Show();
            btn_Exit.Show();
            lbl_Gamename.Show();
            Picbox_SchoolImage.Show();
            Picbox_Background.Image = Properties.Resources.Background;        
        }


        private void btn_Prolog_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Prolog.FlatAppearance.BorderSize = 3;
        }

        private void btn_Prolog_MouseLeave(object sender, EventArgs e)
        {
            btn_Prolog.FlatAppearance.BorderSize = 1;
            btn_Prolog.BackColor = Color.Transparent;
        }

        /* 게임 시작 */
        private void btn_Start_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Start.FlatAppearance.BorderSize = 3;
        }

        private void btn_Start_MouseLeave(object sender, EventArgs e)
        {
            btn_Start.FlatAppearance.BorderSize = 1;
            btn_Start.BackColor = Color.Transparent;
        }

        /* 게임 시작 - 사용자로부터 이름 입력받기 */
        private void btn_Start_Click(object sender, EventArgs e)
        {
            MoveButton();
            btn_Prolog.Hide();
            btn_Exit.Hide();
            if (startCreate)
            {
                /* 동적으로 label, textBox, Button 생성 */
                lbl_User = new System.Windows.Forms.Label();
                lbl_User.Location = new System.Drawing.Point(470, 200);
                lbl_User.Name = "lbl_User";
                lbl_User.Size = new System.Drawing.Size(100, 40);
                lbl_User.Text = "이름 :";
                this.Controls.Add(lbl_User);
                lbl_User.BackColor = System.Drawing.Color.Transparent;
                lbl_User.Parent = Picbox_Background;
                lbl_User.BringToFront();
                lbl_User.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));

                txt_Name = new TextBox();
                txt_Name.Location = new Point(570, 210);
                txt_Name.Name = "txt_Name";
                this.Controls.Add(txt_Name);
                txt_Name.BringToFront();
                txt_Name.Focus();

                btn_Play = new Button();
                btn_Play.Location = new System.Drawing.Point(700, 200);
                btn_Play.Name = "btn_Main";
                btn_Play.Size = new System.Drawing.Size(100, 50);
                btn_Play.Text = "입장";
                this.Controls.Add(btn_Play);
                btn_Play.BackColor = System.Drawing.Color.Transparent;
                btn_Play.Parent = Picbox_Background;
                btn_Play.FlatStyle = FlatStyle.Flat;
                btn_Play.FlatAppearance.BorderSize = 1;
                btn_Play.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
                btn_Play.ForeColor = System.Drawing.Color.SkyBlue;
                btn_Play.BringToFront();
                btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
                btn_Play.MouseLeave += new System.EventHandler(this.btn_Play_MouseLeave);
                btn_Play.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_Play_MouseMove);
            }           

        }

        /* 동적으로 생성된 button EventHandler */
        private void btn_Play_MouseLeave(object sender, EventArgs e)
        {
            btn_Play.FlatAppearance.BorderSize = 1;
            btn_Play.BackColor = Color.Transparent;
        }
        private void btn_Play_MouseMove(object sender, EventArgs e)
        {
            btn_Play.FlatAppearance.BorderSize = 3;
        }
        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
            {
                MessageBox.Show("이름을 입력하세요");
                return;
            }
            else
            {
                /*
                playSubwaySound();

                btn_Start.Hide();
                lbl_User.Hide();
                txt_Name.Hide();
                btn_Play.Hide();
                lbl_Gamename.Hide();
                Picbox_SchoolImage.Hide();
                this.ClientSize = new System.Drawing.Size(800, 600);
                Picbox_Background.Image = Properties.Resources.Setting;
                */

                Game_Intro();
            }
        }

        /* wav 재생 함수 */
        private void playSubwaySound()
        {
            SoundPlayer wp = new SoundPlayer(Properties.Resources.Subway);
            wp.Play();
        }


        /* UI 환경 변경 */
        private void MoveButton()
        {
            int X = btn_Start.Left;
            int Y = btn_Start.Top;


            X = (lbl_Gamename.Left + lbl_Gamename.Right) / 2;
            Y = lbl_Gamename.Bottom + 10;

            btn_Start.Hide();
            btn_Start.Location = new Point(X, Y);

            btn_Start.Text = "플레이어 설정";
            btn_Start.Width = 350;
            btn_Start.Enabled = false;
            btn_Start.Show();
            btn_Start.FlatAppearance.BorderColor = Color.Pink;
            btn_Start.FlatAppearance.BorderSize = 5;
            startCreate = true;

        }

        // =======================GAME BEGINS======================
        private void Game_Intro()
        {
            playSubwaySound();

            btn_Start.Hide();
            lbl_User.Hide();
            txt_Name.Hide();
            btn_Play.Hide();
            lbl_Gamename.Hide();
            Picbox_SchoolImage.Hide();
            this.ClientSize = new System.Drawing.Size(800, 600);
            Picbox_Background.Image = Properties.Resources.Setting;

            Controller();
        }

        // Creates button A and B
        private void Controller()
        {
            btn_A = new Button();
            btn_A.Location = new System.Drawing.Point(700, 500);
            btn_A.Name = "btn_Main";
            btn_A.Size = new System.Drawing.Size(100, 50);
            btn_A.Text = "A";
            this.Controls.Add(btn_A);
            btn_A.BackColor = System.Drawing.Color.Transparent;
            btn_A.Parent = Picbox_Background;
            btn_A.FlatStyle = FlatStyle.Flat;
            btn_A.FlatAppearance.BorderSize = 1;
            btn_A.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
            btn_A.ForeColor = System.Drawing.Color.SkyBlue;
            btn_A.BringToFront();
            btn_A.Click += new System.EventHandler(this.btn_Play_Click);
            btn_A.MouseLeave += new System.EventHandler(this.btn_Play_MouseLeave);
            btn_A.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_Play_MouseMove);

            btn_B = new Button();
            btn_B.Location = new System.Drawing.Point(550, 500);
            btn_B.Name = "btn_Main";
            btn_B.Size = new System.Drawing.Size(100, 50);
            btn_B.Text = "B";
            this.Controls.Add(btn_B);
            btn_B.BackColor = System.Drawing.Color.Transparent;
            btn_B.Parent = Picbox_Background;
            btn_B.FlatStyle = FlatStyle.Flat;
            btn_B.FlatAppearance.BorderSize = 1;
            btn_B.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
            btn_B.ForeColor = System.Drawing.Color.SkyBlue;
            btn_B.BringToFront();
            btn_B.Click += new System.EventHandler(this.btn_Play_Click);
            btn_B.MouseLeave += new System.EventHandler(this.btn_Play_MouseLeave);
            btn_B.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_Play_MouseMove);
        }

    }
}
