using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
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
            LoadUSBDevices();
        }
        private void LoadUSBDevices()
        {
            // 여기에 저장된 USB 디바이스 정보를 불러오는 코드를 작성
            //SelectQuery query = new SelectQuery("Win32_PnPEntity");
            //query.Condition = "PNPClass='USB'"; // USB 클래스에 해당하는 디바이스만 검색

            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            //List<string> deviceList = new List<string>();

            //foreach (ManagementObject usbDevice in searcher.Get())
            //{
                //deviceList.Add(usbDevice["DeviceID"].ToString()); // 디바이스 ID를 리스트에 추가
           // }

            // 리스트에 있는 디바이스 정보를 리스트박스에 표시
            //foreach (string device in deviceList)
            //{
                //listBox1.Items.Add(device);
            //}
            // 예시
            listBox1.Items.Add("Device1"); 
            listBox1.Items.Add("Device2"); 
            listBox1.Items.Add("Device3"); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) // 아이템이 선택되었는지 확인
            {
                string selectedDevice = listBox1.SelectedItem.ToString(); // 선택된 디바이스
                listBox2.Items.Add(selectedDevice); // 승인된 디바이스 목록에 추가
                listBox1.Items.RemoveAt(listBox1.SelectedIndex); // 첫 번째 ListBox에서 선택된 디바이스 제거
                MessageBox.Show(selectedDevice + "가 승인되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("USB 디바이스를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //1. >>버튼 클릭시 승인과정 거침
            //2. 승인되지 않을 시 메시지 박스로 승인되지 않았다고 알림
            //3. 어떤 정보를 불러와서 리스트 박스에 보이게할지 결정


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

