using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBprotect.InternalFunction;
using UsbSecurity;

namespace USBprotect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Form1의 Load 이벤트에 이벤트 핸들러 추가
            this.Load += Form1_Load;

         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // UsbDeviceWatcher 인스턴스 생성 및 시작
            UsbDeviceWatcher watcher = new UsbDeviceWatcher();
            watcher.Start();

            // FormEventBase에 Form1 인스턴스 설정
            UnauthorizedUsbFormEvent feb = new UnauthorizedUsbFormEvent();
            feb.GetForm(this);

            
        }

        public void OpenForm2(string message)
        {
            Form2 form2 = Form2.GetInstance(message);
            form2.Show();
        }   
    }
}

