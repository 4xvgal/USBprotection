﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            SelectQuery query = new SelectQuery("Win32_PnPEntity");
            query.Condition = "PNPClass='USB'"; // USB 클래스에 해당하는 디바이스만 검색

            //생성자에는 'SelectQurey' 객체 전달해서 원하는 쿼리 실행
            //"Win32_PnPEntity"클래스에서 USB 장치 검색하는 쿼리 실행
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            List<string> deviceList = new List<string>();

            foreach (ManagementObject usbDevice in searcher.Get())
            {
                deviceList.Add(usbDevice["DeviceID"].ToString()); // 디바이스 ID를 리스트에 추가
            }

            // 리스트에 있는 디바이스 정보를 리스트박스에 표시
            foreach (string device in deviceList)
            {
                //listBox1.Items.Add(device);
            }
            // 예시
            //listBox1.Items.Add("Device1"); 
            //listBox1.Items.Add("Device2"); 
            //listBox1.Items.Add("Device3"); 
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
            
            //3. 어떤 정보를 불러와서 리스트 박스에 보이게할지 결정
        }
    }
}
