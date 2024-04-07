﻿using System;
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
            }
            else
            {
                MessageBox.Show("USB 디바이스를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //1. >>버튼 클릭시 승인과정 거침
            //2. 승인되지 않을 시 메시지 박스로 승인되지 않았다고 알림
            //3. 어떤 정보를 불러와서 리스트 박스에 보이게할지 결정
        }
    }
}
