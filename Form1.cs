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

        private Label lbl_User;
        private TextBox txt_Name;

        private Label lbl_Text;
        private Label lbl_Dialog;

        private PictureBox Picbox_Character; // 캐릭터 표시 Picturebox
        private string[] text_Value; // 시나리오 전체 Text
        private int text_read; // 시나리오 현재 line
        private int text_line; // 시나리오 전체 line
        bool startCreate = false;

        private SoundPlayer wp; // Sound 도입
        GameState game_mode = GameState.Main_Game; // GameState 이용한 EventHandler Recycle


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
        private void btn_before_MouseMove(object sender, EventArgs e)
        {
            btn_before.FlatAppearance.BorderSize = 3;
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
                                    Picbox_Character.Image = Properties.Resources.NPC2;
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
                                    Picbox_Character.Image = Properties.Resources.NPC2;
                                }                            
                            }
                        }));
                        break;
                    }
            }

        }

        private void btn_next_MouseLeave(object sender, EventArgs e)
        {
            btn_next.FlatAppearance.BorderSize = 1;
            btn_next.BackColor = Color.Transparent;
        }
        private void btn_next_MouseMove(object sender, EventArgs e)
        {
            btn_next.FlatAppearance.BorderSize = 3;
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
                                In_Game1();
                            }));
                        }
                        break;
                    }
                case GameState.In_Game1:
                    {
                        if (text_read < 27)
                        {
                            lbl_Dialog.Invoke(new MethodInvoker(delegate ()
                            {
                                if (text_Value[text_read].Substring(0,1)== "U")
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC1;
                                }
                                else
                                {
                                    Picbox_Character.Image = Properties.Resources.NPC2;
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
            }

        }

        // =======================IN GAME1======================
        private void In_Game1()
        {
            game_mode = GameState.In_Game1;
            Picbox_Background.Image = Properties.Resources.Station;
            wp.Stop();
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

        private void Mini_Game1()
        {

        }
    }
}
