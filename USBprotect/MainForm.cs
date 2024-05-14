﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbSecurity;

namespace USBprotect
{
    public partial class MainForm : Form
    {
        private static MainForm _instance;
        private DeviceMonitor deviceMonitor = new DeviceMonitor();  

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect,int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public MainForm()
        {
            InitializeComponent();
            tbg.FlatStyle = FlatStyle.Flat;  // 버튼 스타일
            tbg.FlatAppearance.BorderSize = 0;  // 테두리 없애기
            tbg.BackColor = Color.Transparent;
            tbg.Enabled = true;
            _instance = this;

            deviceMonitor.Start(); // USB 장치 감시 시작
        }


        public static MainForm Instance
        {
            get { return _instance; }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 30, 30));
            panel2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 30, 30));
            panel3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel3.Width, panel3.Height, 70, 70));
            panel4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel4.Width, panel4.Height, 30, 30));
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 15, 15));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 15, 15));
            button3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 15, 15));
            button4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button4.Width, button4.Height, 15, 15));
            button5.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button5.Width, button5.Height, 15, 15));

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.BorderSize = 0;

                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 새 폼 생성
            Form AllowBlOCKForm = new AllowBlockForm();  // Form2는 새로 열 폼의 클래스 이름입니다.

            // 새 폼이 닫힐 때 애플리케이션 종료 설정
            AllowBlOCKForm.Closed += (s, args) => this.Close();

            // 새 폼 표시
            AllowBlOCKForm.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();  // 폼 표시
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;  // 트레이 아이콘 숨기기
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;  // 종료 이벤트 취소
                this.Hide();      // 폼 숨기기
                notifyIcon1.Visible = true;  // 트레이 아이콘 표시
            }
        }

        private void 종료_Click(object sender, EventArgs e)
        {
            deviceMonitor.Stop(); // USB 장치 감시 중지
            Application.Exit();
        }

        private void 최대화_Click(object sender, EventArgs e)
        {
            this.Show();  // 폼을 보여줍니다.
            this.WindowState = FormWindowState.Maximized;  // 폼을 최대화 상태로 변경
            notifyIcon1.Visible = false;  // 트레이 아이콘을 숨깁니다.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            deviceMonitor.Stop(); // USB 장치 감시 중지   
            Application.Exit();
        }
    }
    
}