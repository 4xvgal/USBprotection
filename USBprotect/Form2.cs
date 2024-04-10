using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USBprotect
{
    public partial class Form2 : Form
    {
        private string message; // 클래스 레벨 변수로 메시지 저장

        public Form2(string message)
        {
            InitializeComponent();
            this.message = message; // 생성자에서 전달된 메시지를 저장
            this.Load += Form2_Load; // Load 이벤트에 핸들러 연결
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show(message);
        }
    }
 }

