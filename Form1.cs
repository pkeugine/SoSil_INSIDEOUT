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
using System.Globalization;

namespace Simulation_Form
{
    public partial class Form1 : Form
    {
        enum GameState
        {
            Main_Game,
            Prolog_Game,
            Play_Game,
            Pre_Game1,
            In_Game1, // 광운대역
            Mini_Game1,
            Pre_Game2,
            In_Game2, // 새빛관
            Mini_Game2,
            Pre_Game3,
            In_Game3, // 참빛관
            Mini_Game3,
            Pre_Game4_1,
            In_Game4_1, // 도서관
            MIni_Game4_1,
            Pre_Game4_2,
            In_Game4_2, // 식당
            Mini_Game4_2,
            Pre_Game4_3,
            In_Game4_3, // CDP 특강
            Mini_Game4_3,
            Pre_Game5,
            In_Game5, // 비마관
            Mini_Game5,
            Pre_Game6,
            In_Game6, // 개강 총회
            Mini_Game6,
            End_Game // 게임 종료
        };
        private Button btn_Main;
        private Button btn_Play;

        private Button btn_before;
        private Button btn_next;

        private Button btn_Yes;
        private Button btn_No;

        private Label lbl_User;
        private TextBox txt_Name;

        private Label lbl_Name;
        private Label lbl_Text;
        private Label lbl_Dialog;

        private Button btn_select1;
        private Button btn_select2;
        private Button btn_select3;

        private TextBox txt_Answer;

        private PictureBox Picbox_select;


        private Random generateRandom;
        private DateTime current = DateTime.Now;
        private PictureBox Picbox_Character; // 캐릭터 표시 Picturebox
        private string[] text_Value; // 시나리오 전체 Text
        private int text_read; // 시나리오 현재 line
        private int text_line; // 시나리오 전체 line

        private int mini_game_count = 10;
        private int false_count = 0;

        private int mini5_game_count = 0;
        private int picnum;
        bool startCreate = false;

        private SoundPlayer wp; // Sound 도입
        GameState game_mode = GameState.Main_Game; // GameState 이용한 EventHandler Recycle


        List<Student> sList = new List<Student>();
        int son_choice = 0; // which question will you choose for son game?

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            generateRandom = new Random(current.Millisecond);
            lbl_Gamename.Parent = Picbox_Background;
            lbl_Gamename.BackColor = Color.Transparent;
            lbl_Gamename.BringToFront();

            //Picbox_SchoolImage.Image = Properties.Resources.NPC2;
            Picbox_SchoolImage.Parent = Picbox_Background;
            btn_Start.Parent = Picbox_Background;
            btn_Prolog.Parent = Picbox_Background;
            btn_Exit.Parent = Picbox_Background;

            btn_Start.FlatStyle = FlatStyle.Flat;
            btn_Prolog.FlatStyle = FlatStyle.Flat;
            btn_Exit.FlatStyle = FlatStyle.Flat;

            game_mode = GameState.Main_Game;

            text_Value = System.IO.File.ReadAllLines(@"..\..\시나리오.txt");
            text_line = text_Value.Length;
            text_read = 0;

            sList.Add(new Student("승수", 201010, 'M', 20, 8, "파주", true, 20, true, "유진", -1));
            sList.Add(new Student("유진", 201013, 'M', 20, 0, "서울", true, 2, true, "승수", -1));
            sList.Add(new Student("세현", 201046, 'M', 20, 1, "부산", false, 4, false, "", 0));
            //상민 == female!!! remember~~	
            sList.Add(new Student("상민", 201017, 'F', 22, 10, "춘천", false, 5, false, "", 0));
            sList.Add(new Student("승희", 201077, 'F', 21, 0, "대전", false, 9, false, "", 0));
            sList.Add(new Student("승현", 181022, 'M', 23, 3, "서울", false, 6, false, "보람", 2));
            sList.Add(new Student("경동", 201070, 'M', 19, 9, "대구", false, 10, true, "상민", 1));
            sList.Add(new Student("보람", 181051, 'F', 22, 10, "인천", true, 8, true, "승현", 2));
            sList.Add(new Student("효제", 181044, 'M', 22, 9, "제주", false, 3, false, "", 0));
            sList.Add(new Student("강민", 161022, 'M', 26, 5, "서울", true, 1, false, "", 0));

            sList.Add(new Student("주인", 201001, 'M', 20, 15, "서울", true, 0, false, "", 0));
        }

        // =======================INIT======================
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            game_mode = GameState.End_Game;
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
        // =======================PROLOG======================
        private void btn_Prolog_Click(object sender, EventArgs e)
        {
            game_mode = GameState.Prolog_Game;
            btn_Start.Hide();
            btn_Prolog.Hide();
            btn_Exit.Hide();
            lbl_Gamename.Hide();
            Picbox_SchoolImage.Hide();
            Picbox_Background.Image = Properties.Resources.Prolog;

            if (btn_Main == null)
            {

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


            int locX = btn_Main.Location.X;
            int locY = btn_Main.Location.Y;
            int dX = (btn_Main.Width / 2) + locX;
            int dY = (btn_Main.Height / 2) + locY;

            Point Mouse = new Point(dX, dY);
            Cursor.Position = this.PointToScreen(Mouse);
        }


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
            game_mode = GameState.Main_Game;
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

        // =======================GAME SETTING======================

        private void btn_Start_MouseMove(object sender, MouseEventArgs e)
        {
            btn_Start.FlatAppearance.BorderSize = 3;
        }

        private void btn_Start_MouseLeave(object sender, EventArgs e)
        {
            btn_Start.FlatAppearance.BorderSize = 1;
            btn_Start.BackColor = Color.Transparent;
        }


        private void btn_Start_Click(object sender, EventArgs e)
        {
            game_mode = GameState.Play_Game;
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

                int locX = btn_Play.Location.X;
                int locY = btn_Play.Location.Y;
                int dX = (btn_Play.Width / 2) + locX;
                int dY = (btn_Play.Height / 2) + locY;

                Point Mouse = new Point(dX, dY);
                Cursor.Position = this.PointToScreen(Mouse);
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
                txt_Name.Focus();
                return;
            }
            else
            {
                Pre_Game1();
                text_Value[3] = text_Value[3].Replace("나는", txt_Name.Text + "...");
                //EUGINE PARK ADDED THIS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //SET MAIN CHARACTER's NAME
                //sList[10].SetName(text_Value);
            }
        }

        /* wav 재생 함수 */
        private void playSubwaySound()
        {
            wp = new SoundPlayer(Properties.Resources.Subway);
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
        private void Pre_Game1()
        {
            game_mode = GameState.Pre_Game1;
            playSubwaySound();

            btn_Start.Hide();
            lbl_User.Hide();
            txt_Name.Hide();
            btn_Play.Hide();
            lbl_Gamename.Hide();
            Picbox_SchoolImage.Hide();
            this.ClientSize = new System.Drawing.Size(800, 600);
            Picbox_Background.Image = Properties.Resources.Main;
            Pre_Game1_Controller();
        }

        // Creates button before and next , scenario play by File IO
        private void Pre_Game1_Controller()
        {
            lbl_Text = new System.Windows.Forms.Label();
            lbl_Text.Location = new System.Drawing.Point(100, 150);
            lbl_Text.Name = "lbl_Text";
            lbl_Text.Size = new System.Drawing.Size(600, 40);
            lbl_Text.Text = text_Value[0];
            text_read = 1;
            this.Controls.Add(lbl_Text);
            lbl_Text.BackColor = System.Drawing.Color.Transparent;
            lbl_Text.Parent = Picbox_Background;
            lbl_Text.BringToFront();
            lbl_Text.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));

            btn_before = new Button();
            btn_before.Location = new System.Drawing.Point(550, 500);
            btn_before.Name = "btn_before";
            btn_before.Size = new System.Drawing.Size(100, 50);
            btn_before.Text = "이전";
            this.Controls.Add(btn_before);
            btn_before.BackColor = System.Drawing.Color.Transparent;
            btn_before.Parent = Picbox_Background;
            btn_before.FlatStyle = FlatStyle.Flat;
            btn_before.FlatAppearance.BorderSize = 1;
            btn_before.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
            btn_before.ForeColor = System.Drawing.Color.DeepPink;
            btn_before.BringToFront();
            btn_before.Click += new System.EventHandler(this.btn_before_Click);
            btn_before.MouseLeave += new System.EventHandler(this.btn_before_MouseLeave);
            btn_before.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_before_MouseMove);

            btn_next = new Button();
            btn_next.Location = new System.Drawing.Point(660, 500);
            btn_next.Name = "btn_next";
            btn_next.Size = new System.Drawing.Size(100, 50);
            btn_next.Text = "다음";
            this.Controls.Add(btn_next);
            btn_next.BackColor = System.Drawing.Color.Transparent;
            btn_next.Parent = Picbox_Background;
            btn_next.FlatStyle = FlatStyle.Flat;
            btn_next.FlatAppearance.BorderSize = 1;
            btn_next.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
            btn_next.ForeColor = System.Drawing.Color.DeepSkyBlue;
            btn_next.BringToFront();
            btn_next.Click += new System.EventHandler(this.btn_next_Click);
            btn_next.MouseLeave += new System.EventHandler(this.btn_next_MouseLeave);
            btn_next.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_next_MouseMove);

            int locX = btn_next.Location.X;
            int locY = btn_next.Location.Y;
            int dX = (btn_next.Width / 2) + locX;
            int dY = (btn_next.Height / 2) + locY;

            Point Mouse = new Point(dX, dY);
            Cursor.Position = this.PointToScreen(Mouse);
        }

        private void btn_before_MouseLeave(object sender, EventArgs e)
        {
            btn_before.FlatAppearance.BorderSize = 1;
            btn_before.BackColor = Color.Transparent;
        }
        private void btn_next_MouseLeave(object sender, EventArgs e)
        {
            btn_next.FlatAppearance.BorderSize = 1;
            btn_next.BackColor = Color.Transparent;
        }
        private void btn_before_MouseMove(object sender, EventArgs e)
        {
            btn_before.FlatAppearance.BorderSize = 3;
        }
        private void btn_next_MouseMove(object sender, EventArgs e)
        {
            btn_next.FlatAppearance.BorderSize = 3;
        }

        private void btn_before_Click(object sender, EventArgs e)
        {
            switch (game_mode)
            {
                case GameState.Pre_Game1:
                    {
                        lbl_Text.Invoke(new MethodInvoker(delegate ()
                        {
                            if (text_read > 0)
                            {
                                lbl_Text.Text = text_Value[--text_read];
                            }
                            else
                            {
                                text_read = 0;
                                lbl_Text.Text = text_Value[text_read];
                            }
                        }));
                        break;
                    }
                case GameState.In_Game1:
                    {
                        lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                        {
                            if (text_read > 14)
                            {
                                lbl_Dialog.Text = text_Value[--text_read].Substring(1);
                                if (text_Value[text_read].Substring(0, 1) == "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.승수;
                                }
                            }
                            else
                            {
                                text_read = 14;
                                lbl_Dialog.Text = text_Value[text_read].Substring(1);
                                if (text_Value[text_read].Substring(0, 1) == "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.승수;
                                }                            
                            }
                        }));
                        break;
                    }
                case GameState.Mini_Game1:
                    {
                        break;
                    }
                case GameState.Mini_Game2:
                    {
                        if (text_read <= 557 && text_read !=535)
                        {
                            if (text_read == 516) Picbox_Background.Image = Properties.Resources.Drinkingplace;
                            lbl_Dialog.Text = text_Value[--text_read].Substring(1);
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; }
                                else { Picbox_Character.Image = Properties.Resources.누구; }
                            }));
                        }
                        else if (text_read < 557 && text_read == 535)
                        {
                            lbl_Dialog.Text = text_Value[536].Substring(1);
                            for(int i=0; i<2; i++) sList[10].Drink();
                            text_read = 537;
                        }
                        else if (text_read == 558)
                        {
                            if(son_choice == 0) { son_choice=1; lbl_Dialog.Text = text_Value[557].Substring(1);}
                            else if (son_choice == 1) {son_choice=2; lbl_Dialog.Text = text_Value[558].Substring(1);}
                            else if (son_choice == 2) {son_choice=3; lbl_Dialog.Text = text_Value[559].Substring(1);}
                            else if (son_choice == 3) {son_choice=4; lbl_Dialog.Text = text_Value[560].Substring(1);}
                            else if (son_choice == 4) {son_choice=5; lbl_Dialog.Text = text_Value[561].Substring(1);}
                            else if (son_choice == 5) {son_choice=6; lbl_Dialog.Text = text_Value[562].Substring(1);}
                            else if (son_choice == 6) {son_choice=7; lbl_Dialog.Text = text_Value[563].Substring(1);}
                            else if (son_choice == 7) {son_choice=8; lbl_Dialog.Text = text_Value[564].Substring(1);}
                            else if (son_choice == 8) {son_choice=0; lbl_Dialog.Text = text_Value[565].Substring(1);}
                            //else if (son_choice == 9) {son_choice=0; lbl_Dialog.Text = text_Value[566].Substring(1);}

                        }
                        break;
                    }
                case GameState.In_Game2:
                    {
                        break;
                    }
                case GameState.In_Game3:
                    {
                        break;
                    }
                case GameState.In_Game4_1:
                    {
                        break;
                    }
                case GameState.In_Game4_2:
                    {
                        break;
                    }
                case GameState.In_Game4_3:
                    {
                        break;
                    }
            }

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            switch (game_mode)
            {
                case GameState.Pre_Game1:
                    {

                        if (text_read < 14)
                        {
                            lbl_Text.Invoke(new MethodInvoker(delegate ()
                            {
                                lbl_Text.Text = text_Value[text_read++];
                            }));
                        }
                        else
                        {
                            lbl_Text.Invoke(new MethodInvoker(delegate ()
                            {
                                text_read = 14;
                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                wp.Stop();
                                //Mini_Game2();
                                In_Game1();
                            }));
                        }
                        break;
                    }
                case GameState.In_Game1:
                    {
                        if (text_read < 32)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0,1)== "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                    lbl_Name.Text = txt_Name.Text;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.승수;
                                    lbl_Name.Text = "승수";
                                }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                            }));
                        }
                        else
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                Mini_Game1();
                            }));
                        }
                        break;
                    }
                case GameState.Mini_Game1:
                    {
                        In_Game2();
                        break;
                    }
                case GameState.Mini_Game2:
                    {
                        if (text_read < 758 && text_read !=735)
                        {
                            if (text_read == 717) Picbox_Background.Image = Properties.Resources.DrinkingGame;
                            if (text_read == 734)
                            {
                                btn_next.Text = "당연";
                                btn_before.Text = "아니";
                            }
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = "승수"; }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = "유진"; }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = "세현"; }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = "상민"; }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = "승희"; }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = "승현"; }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = "경동"; }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = "보람"; }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = "효제"; }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = "강민"; }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);

                                if(text_read == 757) {btn_before.Text = "교체"; btn_next.Text = "사용"; }
                            }));
                        }
                        else if (text_read < 757 && text_read == 735)
                        {
                            lbl_Dialog.Text = text_Value[735].Substring(1);
                            text_read = 737;
                        }
                        else if (text_read == 758)
                        {
                            if(son_choice == 0) {}
                            else if (son_choice == 1) {}
                            else if (son_choice == 2) {}
                            else if (son_choice == 3) {}
                            else if (son_choice == 4) {}
                            else if (son_choice == 5) {}
                            else if (son_choice == 6) {}
                            else if (son_choice == 7) {}
                            else if (son_choice == 8) {}
                            else if (son_choice == 9) {}

                        }
                        break;
                    }
                case GameState.In_Game2:
                    {
                        if (text_read < 151)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = sList[0].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = sList[1].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = sList[2].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = sList[3].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = sList[4].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = sList[5].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = sList[6].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = sList[7].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = sList[8].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = sList[9].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                            }));
                        } else 
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                In_Game3();
                            }));
                        }
                        break;
                    }
                case GameState.In_Game3:
                    {
                        if (text_read < 241)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = sList[0].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = sList[1].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = sList[2].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = sList[3].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = sList[4].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = sList[5].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = sList[6].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = sList[7].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = sList[8].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = sList[9].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                            }));
                        } else 
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                In_Game4_1();
                            }));
                        }
                        break;
                    }
                case GameState.In_Game4_1:
                    {
                        if (text_read < 338)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = sList[0].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = sList[1].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = sList[2].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = sList[3].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = sList[4].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = sList[5].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = sList[6].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = sList[7].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = sList[8].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = sList[9].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                            }));
                        }
                        else
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                In_Game4_2();
                            }));
                        }
                        break;
                    }
                case GameState.In_Game4_2:
                    {
                        if (text_read < 427)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = sList[0].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = sList[1].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = sList[2].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = sList[3].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = sList[4].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = sList[5].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = sList[6].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = sList[7].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = sList[8].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = sList[9].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                            }));
                        }
                        else
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                In_Game4_3();
                            }));
                        }
                        break;
                    }
                case GameState.In_Game4_3:
                    {
                        if (text_read < 533)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = sList[0].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = sList[1].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = sList[2].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = sList[3].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = sList[4].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = sList[5].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = sList[6].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = sList[7].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = sList[8].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = sList[9].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                            }));
                        }
                        else
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                In_Game5();
                            }));
                        }
                        break;
                    }
                case GameState.In_Game5:
                    {
                        if (text_read < 631)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = sList[0].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = sList[1].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = sList[2].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = sList[3].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = sList[4].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = sList[5].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = sList[6].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = sList[7].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = sList[8].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = sList[9].Name(); }
                                else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                            }));
                        }
                        else
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                // Mini_Game2();
                                Mini_Game5();
                            }));
                        }
                        break;
                    }
                case GameState.Mini_Game5:
                    {
                        if (text_read <= 773)
                        {
                            if (text_Value[text_read].Substring(0, 1) == "U")
                            {
                                Picbox_Character.Image = Properties.Resources.NPC1;
                                lbl_Name.Text = txt_Name.Text;
                            }
                            else
                            {
                                Picbox_Character.Image = Properties.Resources.강민;
                                lbl_Name.Text = sList[9].Name();
                            }
                            lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                        }
                        else if (text_read == 774)
                        {
                            if (text_Value[text_read].Substring(0, 1) == "U")
                            {
                                Picbox_Character.Image = Properties.Resources.NPC1;
                                lbl_Name.Text = txt_Name.Text;
                            }
                            else
                            {
                                Picbox_Character.Image = Properties.Resources.강민;
                                lbl_Name.Text = sList[9].Name();
                            }
                            lbl_Dialog.Text = text_Value[text_read++].Substring(2);
                            btn_next.Invoke(new MethodInvoker(delegate ()
                            {
                                btn_next.Text = "시작";
                            }));
                            btn_next.Hide();
                            btn_select1.Show();
                            btn_select2.Show();
                            //btn_select3.Show();
                        }
                        else if (text_read > 774 &&  text_read < 780)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {

                                if (text_Value[text_read].Substring(0, 1) == "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                    lbl_Name.Text = txt_Name.Text;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.강민;
                                    lbl_Name.Text = sList[9].Name();
                                }
                                                                                             

                                if (text_Value[text_read - 1].Substring(1, 1) == "교")
                                {
                                    if (txt_Answer.Text == "페가수스")
                                    {
                                        mini5_game_count++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("오답입니다.");
                                    }
                                    txt_Answer.Clear();

                                }
                                else if (text_Value[text_read - 1].Substring(1, 1) == "이")
                                {
                                    if (txt_Answer.Text== "부대찌개")
                                    {
                                        mini5_game_count++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("오답입니다.");
                                    }
                                    Picbox_select.Show();
                                    txt_Answer.Clear();
                                }

                                lbl_Dialog.Text = text_Value[text_read++].Substring(1);


                            }));

                        }
                        else if(text_read <781)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read - 1].Substring(1, 1) == "친")
                                {
                                    switch (picnum)
                                    {
                                        case 1:
                                            {
                                                if (txt_Answer.Text == "경동")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 2:
                                            {
                                                if (txt_Answer.Text == "보람")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 3:
                                            {
                                                if (txt_Answer.Text == "상민")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 4:
                                            {
                                                if (txt_Answer.Text == "세현")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 5:
                                            {
                                                if (txt_Answer.Text == "승수")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 6:
                                            {
                                                if (txt_Answer.Text == "승현")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 7:
                                            {
                                                if (txt_Answer.Text == "승희")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 8:
                                            {
                                                if (txt_Answer.Text == "유진")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                        case 9:
                                            {
                                                if (txt_Answer.Text == "효제")
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("오답입니다.");
                                                }
                                                break;
                                            }
                                    }
                                    text_read = 699;
                                    if (text_Value[text_read].Substring(0, 1) == "U") { Picbox_Character.Image = Properties.Resources.NPC1; lbl_Name.Text = txt_Name.Text; }
                                    else if (text_Value[text_read].Substring(0, 1) == "A") { Picbox_Character.Image = Properties.Resources.승수; lbl_Name.Text = "승수"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "B") { Picbox_Character.Image = Properties.Resources.유진; lbl_Name.Text = "유진"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "C") { Picbox_Character.Image = Properties.Resources.세현; lbl_Name.Text = "세현"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "D") { Picbox_Character.Image = Properties.Resources.상민; lbl_Name.Text = "상민"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "E") { Picbox_Character.Image = Properties.Resources.승희; lbl_Name.Text = "승희"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "F") { Picbox_Character.Image = Properties.Resources.승현; lbl_Name.Text = "승현"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "G") { Picbox_Character.Image = Properties.Resources.경동; lbl_Name.Text = "경동"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "H") { Picbox_Character.Image = Properties.Resources.보람; lbl_Name.Text = "보람"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "I") { Picbox_Character.Image = Properties.Resources.효제; lbl_Name.Text = "효제"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "J") { Picbox_Character.Image = Properties.Resources.강민; lbl_Name.Text = "강민"; }
                                    else if (text_Value[text_read].Substring(0, 1) == "(") { Picbox_Character.Image = Properties.Resources.교수님; lbl_Name.Text = ""; }
                                    else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
                                    //lbl_Dialog.Text = text_Value[text_read++].Substring(1);

                                    lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                                    {
                                        btn_select1.Hide();
                                        btn_select2.Hide();
                                        btn_select3.Hide();
                                        Picbox_select.Hide();
                                        txt_Answer.Hide();
                                        Mini_Game2();
                                    }));
                                    btn_next.Invoke(new MethodInvoker(delegate ()
                                    {
                                        btn_next.Text = "다음";
                                    }));
                                }
                               

                            }));
                            break;
                        }                    
                        else
                        {
                            
                            
                        }
                        break;
                    }
            }

        }

        // =======================IN GAME1======================
        private void In_Game1()
        {
            game_mode = GameState.In_Game1;
            Picbox_Background.Image = Properties.Resources.Station;
            In_Game1_Controller();
        }

        // Creates Character Image, Dialog Labels
        private void In_Game1_Controller()
        {
            btn_before.BringToFront();
            btn_next.BringToFront();
           
            lbl_Text.Text = text_Value[13];
            lbl_Text.ForeColor = Color.Red;
            lbl_Text.Location = new Point(130, 50);

            lbl_Dialog = new System.Windows.Forms.Label();
            lbl_Dialog.Location = new System.Drawing.Point(150, 510);
            lbl_Dialog.Name = "lbl_Dialog";
            lbl_Dialog.Size = new System.Drawing.Size(400, 40);
            lbl_Dialog.Text = text_Value[14].Substring(1);
            lbl_Dialog.ForeColor = Color.White;
            this.Controls.Add(lbl_Dialog);
            lbl_Dialog.BackColor = System.Drawing.Color.Transparent;
            lbl_Dialog.Parent = Picbox_Background;
            lbl_Dialog.BringToFront();
            lbl_Dialog.Font = new System.Drawing.Font("맑은 고딕", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));

            lbl_Name = new System.Windows.Forms.Label();
            lbl_Name.Location = new System.Drawing.Point(150, 450);
            lbl_Name.Name = "lbl_Name";
            lbl_Name.Size = new System.Drawing.Size(400, 40);
            lbl_Name.Text = txt_Name.Text;
            lbl_Name.ForeColor = Color.White;
            this.Controls.Add(lbl_Name);
            lbl_Name.BackColor = System.Drawing.Color.Black;
            lbl_Name.Parent = Picbox_Background;
            lbl_Name.BringToFront();
            lbl_Name.Font = new System.Drawing.Font("맑은 고딕", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
            
            Picbox_Character = new PictureBox();
            Picbox_Character.Image = Properties.Resources.NPC1;
            Picbox_Character.Location = new System.Drawing.Point(0, 450);
            Picbox_Character.Name = "Picbox_Character";
            Picbox_Character.Size = new System.Drawing.Size(150, 150);
            Picbox_Character.Parent = Picbox_Background;
            //this.Controls.Add(Picbox_Character);
            Picbox_Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            Picbox_Character.BackColor = System.Drawing.Color.Transparent;
            Picbox_Character.TabIndex = 0;
            Picbox_Character.BringToFront();
            Picbox_Character.TabStop = false;

            int locX = btn_next.Location.X;
            int locY = btn_next.Location.Y;
            int dX = (btn_next.Width / 2) + locX;
            int dY = (btn_next.Height / 2) + locY;

            Point Mouse = new Point(dX, dY);
            Cursor.Position = this.PointToScreen(Mouse);
        }

        // =======================MINI GAME1======================
        private void Mini_Game1()
        {
            game_mode = GameState.Mini_Game1;
            btn_before.Hide();
            btn_next.Hide();
            Mini_Game1_Controller();
        }
        private void Mini_Game1_Controller()
        {
            btn_Yes = new Button();
            btn_Yes.Location = new System.Drawing.Point(550, 500);
            btn_Yes.Name = "btn_Yes";
            btn_Yes.Size = new System.Drawing.Size(100, 50);
            btn_Yes.Text = "O";
            this.Controls.Add(btn_Yes);
            btn_Yes.BackColor = System.Drawing.Color.Transparent;
            btn_Yes.Parent = Picbox_Background;
            btn_Yes.FlatStyle = FlatStyle.Flat;
            btn_Yes.FlatAppearance.BorderSize = 1;
            btn_Yes.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
            btn_Yes.ForeColor = System.Drawing.Color.DeepSkyBlue;
            btn_Yes.BringToFront();
            btn_Yes.Click += new System.EventHandler(this.btn_YesNo_Click);
            btn_Yes.MouseLeave += new System.EventHandler(this.btn_Yes_MouseLeave);
            btn_Yes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_Yes_MouseMove);

            btn_No = new Button();
            btn_No.Location = new System.Drawing.Point(660, 500);
            btn_No.Name = "btn_No";
            btn_No.Size = new System.Drawing.Size(100, 50);
            btn_No.Text = "X";
            this.Controls.Add(btn_No);
            btn_No.BackColor = System.Drawing.Color.Transparent;
            btn_No.Parent = Picbox_Background;
            btn_No.FlatStyle = FlatStyle.Flat;
            btn_No.FlatAppearance.BorderSize = 1;
            btn_No.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(15)));
            btn_No.ForeColor = System.Drawing.Color.DeepPink;
            btn_No.BringToFront();
            btn_No.Click += new System.EventHandler(this.btn_YesNo_Click);
            btn_No.MouseLeave += new System.EventHandler(this.btn_No_MouseLeave);
            btn_No.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_No_MouseMove);

            text_read = 33;
            lbl_Dialog.Text = text_Value[text_read++].Substring(2);
            /* 사진 회전 기능
            btn_before.BringToFront();
            btn_next.BringToFront();

            lbl_Dialog.Text = text_Value[499].Substring(1);
            */
        }
        private void btn_Yes_MouseLeave(object sender, EventArgs e)
        {
            btn_Yes.FlatAppearance.BorderSize = 1;
            btn_Yes.BackColor = Color.Transparent;
        }
        private void btn_Yes_MouseMove(object sender, EventArgs e)
        {
            btn_Yes.FlatAppearance.BorderSize = 3;
        }
        private void btn_No_MouseLeave(object sender, EventArgs e)
        {
            btn_No.FlatAppearance.BorderSize = 1;
            btn_No.BackColor = Color.Transparent;
        }
        private void btn_No_MouseMove(object sender, EventArgs e)
        {
            btn_No.FlatAppearance.BorderSize = 3;
        }
        private void btn_YesNo_Click(object sender, EventArgs e)
        {
            switch (game_mode)
            {
                case GameState.Mini_Game1:
                    {
                        if (text_read < 43)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.승수;
                                }
                                Button YN = sender as Button;
                                if (YN.Text == "X" && text_Value[text_read - 1].Substring(1, 1) == "N")
                                {
                                    if (mini_game_count < 10)
                                    {
                                        mini_game_count++;
                                    }

                                }
                                else if (YN.Text == "O" && text_Value[text_read - 1].Substring(1, 1) == "Y")
                                {
                                    if (mini_game_count < 10)
                                    {
                                        mini_game_count++;
                                    }

                                }
                                else // 오답
                                {
                                    mini_game_count--;
                                    false_count++;
                                    lbl_Text.Invoke(new MethodInvoker(delegate ()
                                    {
                                        lbl_Text.Text = text_Value[13].Replace("50분", "5" + false_count.ToString() + "분");
                                    }));
                                    MessageBox.Show("안타깝지만 오답입니다!");
                                }
                                lbl_Dialog.Text = text_Value[text_read++].Substring(2);
                            }));
                        }
                        else // 게임종료
                        {
                            //MessageBox.Show(mini_game_count.ToString());
                            if (false_count < 5)
                            {
                                sList[0].Enjoy();
                                lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                                {
                                    lbl_Dialog.Text = text_Value[43].Substring(1);
                                    btn_Yes.Hide();
                                    btn_No.Hide();
                                    btn_next.Show();
                                }));

                            }
                            else
                            {
                                sList[0].Hate();
                                lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                                {
                                    lbl_Dialog.Text = text_Value[44].Substring(1);
                                    btn_Yes.Hide();
                                    btn_No.Hide();
                                    btn_next.Show();
                                }));

                            }

                        }
                        break;
                    }
            }
        }

        // =======================MINI GAME2======================
        private void Mini_Game2()
        {
            text_read = 700;
            game_mode = GameState.Mini_Game2;
            Picbox_Background.Image = Properties.Resources.Drinkingplace;
            Mini_Game2_Controller();

            //대사    U      A    B    C   E   I    H    J
            //번호    10     0    1    2   4   8    7    9
            //       주인공 승수 유진 세현 승희 효제 보람 강민
            //나이    20     20  20   20   21  22   19   26
            //성별    남     남   남  남    여  남   여   남
            //지역    서울  파주 서울 부산 대전 제주 인천 서울
            //통학    네     네  네   노   노   노   네   네
            //애인    노     노  노   노   노   노   네   노
            //주량    15     8   0    1    0   9    10   5
            //상태    0      5   0    2    0   4     8   4
            //현황    


            // 초기 음주 상태.
            for (int i = 0; i <= 5; i++) sList[0].Drink();
            for (int i = 0; i <= 2; i++) sList[2].Drink();
            for (int i = 0; i <= 4; i++) sList[8].Drink();
            for (int i = 0; i <= 8; i++) sList[7].Drink();
            for (int i = 0; i <= 4; i++) sList[9].Drink();

            // 즐겁게 놀며 나를 계속 먹임
            for (int i = 0; i < 8; i++) sList[10].Drink();
            // 주인공 9잔

            //상민이가 여자인 것을 모르면 추가 1잔
            

            // 정신차리고 승희를 지키자!

            //"나보다 나이 많은 사람 접어" -> 승희 아웃 (안됨)
            //"서울이 고향인 사람 접어" -> 주인공 한 잔, 유진 아웃, 강민 한 잔
            //"고향 이름에 받침 없으면 접어" -> 승수 한잔, 효제 한 잔
            //"나보다 나이 많은 남자 접어" -> 효제 한 잔 , 강민 한 잔
            //"나이 25 이상이면 접어" -> 강민 한 잔
            //"애인 있으면 접어" -> 보람 한 잔
            //"이름 보람이면 접어" -> 보람 한 잔
            //"아직 20살 미만이면 접어" -> 보람 한 잔
            //"학번 190000 미만 접어" -> 강민 한 잔 보람 한 잔


            //보람, 강민, 그리고 주인공 모두 필름이 존재할 때
            //while((sList[7].Film==true || sList[9].File==true) && sList[10].Film==true) {
                
            //}
            //if(sList[10].Film==false)
            //{
            //    this.Close();
            //}
        }
        private void Mini_Game2_Controller()
        {
            btn_before.BringToFront();
            btn_next.BringToFront();

            lbl_Dialog.Text = text_Value[699].Substring(1);
            Picbox_Character.Image = Properties.Resources.NPC1;
        }

        // =======================IN GAME2======================
        private void In_Game2()
        {
            text_read = 100;
            lbl_Dialog.Text = text_Value[99].Substring(1);
            game_mode = GameState.In_Game2;

            lbl_Text.Hide();

            Bitmap temp = Properties.Resources.Saebit;
            temp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Picbox_Background.Image = temp;
            Picbox_Character.Image = Properties.Resources.NPC1;
            change_name();
        }
        private void In_Game3()
        {
            text_read = 200;
            lbl_Dialog.Text = text_Value[199].Substring(1);
            game_mode = GameState.In_Game3;
            Picbox_Background.Image = Properties.Resources.Chambit;
            Picbox_Character.Image = Properties.Resources.NPC1;
            change_name();
        }
        private void In_Game4_1() 
        {
            text_read = 300;
            lbl_Dialog.Text = text_Value[299].Substring(1);
            game_mode = GameState.In_Game4_1;
            Picbox_Background.Image = Properties.Resources.Library;
            Picbox_Character.Image = Properties.Resources.NPC1;
            change_name();
        }
        private void In_Game4_2()
        {
            text_read = 400;
            lbl_Dialog.Text = text_Value[399].Substring(1);
            game_mode = GameState.In_Game4_2;
            Picbox_Background.Image = Properties.Resources.Restaurant;
            Picbox_Character.Image = Properties.Resources.승희;
            change_name();
        }
        private void In_Game4_3()
        {
            text_read = 500;
            lbl_Dialog.Text = text_Value[499].Substring(1);
            game_mode = GameState.In_Game4_3;
            Picbox_Background.Image = Properties.Resources.Ceedeepee;
            Picbox_Character.Image = Properties.Resources.승희;
            change_name();
        }
        private void In_Game5()
        {
            text_read = 600;
            lbl_Dialog.Text = text_Value[599].Substring(1);
            game_mode = GameState.In_Game5;
            Picbox_Background.Image = Properties.Resources.Bima;
            Picbox_Character.Image = Properties.Resources.NPC1;
            change_name();
        }

        private void change_name()
        {
            if (text_Value[text_read + 1].Substring(0, 1) == "U") { lbl_Name.Text = txt_Name.Text; }
            else if (text_Value[text_read + 1].Substring(0, 1) == "A") { lbl_Name.Text = sList[0].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "B") { lbl_Name.Text = sList[1].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "C") { lbl_Name.Text = sList[2].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "D") { lbl_Name.Text = sList[3].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "E") { lbl_Name.Text = sList[4].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "F") { lbl_Name.Text = sList[5].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "G") { lbl_Name.Text = sList[6].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "H") { lbl_Name.Text = sList[7].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "I") { lbl_Name.Text = sList[8].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "J") { lbl_Name.Text = sList[9].Name(); }
            else if (text_Value[text_read + 1].Substring(0, 1) == "(") { lbl_Name.Text = ""; }
            else { Picbox_Character.Image = Properties.Resources.누구; lbl_Name.Text = ""; }
        }

        private void Mini_Game5()
        {
            Mini_Game5_Controller();
        }

        private void Mini_Game5_Controller()
        {
            game_mode = GameState.Mini_Game5;
            text_read = 767;
            int num = generateRandom.Next(1, 9);
            picnum = num;
            btn_select1 = new Button();
            btn_select1.BackColor = System.Drawing.Color.Transparent;
            btn_select1.Parent = Picbox_Background;
            btn_select1.FlatStyle = FlatStyle.Flat;
            btn_select1.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(40)));
            btn_select1.ForeColor = System.Drawing.Color.DarkCyan;
            btn_select1.Location = new System.Drawing.Point(150, 350);
            btn_select1.Name = "btn_select1";
            this.Controls.Add(btn_select1);
            btn_select1.Size = new System.Drawing.Size(150, 50);
            btn_select1.Text = "광운대역";
            btn_select1.BringToFront();
            btn_select1.Click += new System.EventHandler(this.btn_selects_Click);
            btn_select1.MouseLeave += new System.EventHandler(this.btn_selects_MouseLeave);
            btn_select1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_selects_MouseMove);

            btn_select2 = new Button();
            btn_select2.BackColor = System.Drawing.Color.Transparent;
            btn_select2.Parent = Picbox_Background;
            btn_select2.FlatStyle = FlatStyle.Flat;
            btn_select2.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(40)));
            btn_select2.ForeColor = System.Drawing.Color.DarkCyan;
            btn_select2.Location = new System.Drawing.Point(350, 350);
            btn_select2.Name = "btn_select2";
            this.Controls.Add(btn_select2);
            btn_select2.Size = new System.Drawing.Size(150, 50);
            btn_select2.Text = "석계역";
            btn_select2.BringToFront();
            btn_select2.Click += new System.EventHandler(this.btn_selects_Click);
            btn_select2.MouseLeave += new System.EventHandler(this.btn_selects_MouseLeave);
            btn_select2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_selects_MouseMove);

            btn_select3 = new Button();
            btn_select3.BackColor = System.Drawing.Color.Transparent;
            btn_select3.Parent = Picbox_Background;
            btn_select3.FlatStyle = FlatStyle.Flat;
            btn_select3.Font = new System.Drawing.Font("맑은 고딕", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(40)));
            btn_select3.ForeColor = System.Drawing.Color.DarkCyan;
            btn_select3.Location = new System.Drawing.Point(550, 350);
            btn_select3.Name = "btn_select3";
            this.Controls.Add(btn_select3);
            btn_select3.Size = new System.Drawing.Size(150, 50);
            btn_select3.Text = "";
            btn_select3.BringToFront();
            btn_select3.Click += new System.EventHandler(this.btn_selects_Click);
            btn_select3.MouseLeave += new System.EventHandler(this.btn_selects_MouseLeave);
            btn_select3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_selects_MouseMove);

            txt_Answer = new System.Windows.Forms.TextBox();
            txt_Answer.Location = new System.Drawing.Point(510, 510);
            txt_Answer.Name = "txt_Answer";
            txt_Answer.Size = new System.Drawing.Size(100, 450);
            this.Controls.Add(txt_Answer);
            txt_Answer.BringToFront();
            txt_Answer.Focus();

            Picbox_select = new PictureBox();
            switch (num)
            {
                case 1:
                    Picbox_select.Image = Properties.Resources.경동;
                    break;
                case 2:
                    Picbox_select.Image = Properties.Resources.보람;
                    break;
                case 3:
                    Picbox_select.Image = Properties.Resources.상민;
                    break;
                case 4:
                    Picbox_select.Image = Properties.Resources.세현;
                    break;
                case 5:
                    Picbox_select.Image = Properties.Resources.승수;
                    break;
                case 6:
                    Picbox_select.Image = Properties.Resources.승현;
                    break;
                case 7:
                    Picbox_select.Image = Properties.Resources.승희;
                    break;
                case 8:
                    Picbox_select.Image = Properties.Resources.유진;
                    break;
                case 9:
                    Picbox_select.Image = Properties.Resources.효제;
                    break;
            }
            Picbox_select.Location = new Point(350, 150);
            Picbox_select.Name = "Picbox_select";
            Picbox_select.Size = Picbox_Character.Size;
            Picbox_select.Parent = Picbox_Background;
            Picbox_select.SizeMode = PictureBoxSizeMode.StretchImage;
            Picbox_select.BringToFront();

            Picbox_select.Hide();
            btn_select1.Hide();
            btn_select2.Hide();
            btn_select3.Hide();
            txt_Answer.Hide();

        }
        private void btn_selects_MouseLeave(object sender, EventArgs e)
        {
            Button selected = sender as Button;

            switch(selected.Name)
            {
                case "btn_select1":
                    {
                        btn_select1.FlatAppearance.BorderSize = 1;
                        btn_select1.BackColor = Color.Transparent;
                        break;
                    }
                case "btn_select2":
                    {
                        btn_select2.FlatAppearance.BorderSize = 1;
                        btn_select2.BackColor = Color.Transparent;
                        break;
                    }
                case "btn_select3":
                    {
                        btn_select3.FlatAppearance.BorderSize = 1;
                        btn_select3.BackColor = Color.Transparent;
                        break;
                    }

            }
        }
        private void btn_selects_MouseMove(object sender, EventArgs e)
        {
            Button selected = sender as Button;

            switch (selected.Name)
            {
                case "btn_select1":
                    {
                        btn_select1.FlatAppearance.BorderSize = 3;
                        break;
                    }
                case "btn_select2":
                    {
                        btn_select2.FlatAppearance.BorderSize = 3;
                        break;
                    }

                case "btn_select3":
                    {
                        btn_select3.FlatAppearance.BorderSize = 3;
                        break;
                    }
            }

        }
        private void btn_selects_Click(object sender, EventArgs e)
        {
            switch (game_mode)
            {
                case GameState.Mini_Game5:
                    {
                        if (text_read < 778)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0, 1) == "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.강민;
                                }
                                Button YN = sender as Button;
                                if (text_Value[text_read - 1].Substring(1, 1) == "S")
                                {
                                    if (YN.Name == "btn_select2")
                                    {
                                        mini5_game_count++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("오답입니다.");
                                    }
                                    btn_select1.Text = "6";
                                    btn_select2.Text = "7";
                                    btn_select3.Text = "8";
                                    btn_select3.Show();
                                }
                                else if (text_Value[text_read - 1].Substring(1, 1) == "8")
                                {
                                    if (YN.Name == "btn_select3")
                                    {
                                        mini5_game_count++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("오답입니다.");
                                    }
                                    btn_select1.Text = "7";
                                    btn_select2.Text = "14";
                                    btn_select3.Text = "15";
                                }
                                else if (text_Value[text_read - 1].Substring(1, 1) == "4")
                                {
                                    if (YN.Name == "btn_select2")
                                    {
                                        mini5_game_count++;
                                    }
                                    else
                                    {
                                        MessageBox.Show("오답입니다.");
                                    }
                                    btn_select1.Hide();
                                    btn_select2.Hide();
                                    btn_select3.Hide();
                                    btn_next.Show();
                                    btn_next.Text = "제출";
                                    txt_Answer.Show();
                                    txt_Answer.Focus();
                                }

                                if (text_Value[text_read].Substring(0, 1) == "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                    lbl_Name.Text = txt_Name.Text;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.강민;
                                    lbl_Name.Text = sList[9].Name();
                                }
                                
                                if(text_read<777)
                                {
                                    lbl_Dialog.Text = text_Value[text_read++].Substring(2);
                                }
                                else
                                {
                                    lbl_Dialog.Text = text_Value[text_read++].Substring(1);
                                }

                            }));
                        }
                        break;
                    }
            }
        }

    }

}
