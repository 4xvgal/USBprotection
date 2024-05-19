﻿using System;
using System.Linq;
using System.Windows.Forms;
using UsbSecurity;
using USBprotect.PermitRequest;

namespace USBprotect
{
    // 허용 요청 보내는 폼
    public partial class Form2 : Form       {
        private string hintText = "허용 사유를 입력하세요"; // 텍스트 상자의 힌트 텍스트
        private string message; // 초기 메시지
        private static Form2 instance; // Form2의 인스턴스
        private readonly PermitRequestManagement _requestList; // 허용 요청 리스트

        // 생성자
        public Form2(string message)
        {
            InitializeComponent();
            this.message = message;
            this.Load += Form2_Load; // 폼 로드 이벤트 핸들러 등록
            _requestList = new PermitRequestManagement(); // 허용 요청 리스트 초기화

            // USB 장치 정보를 가져와 label에 추가
            foreach (var id in ParsingUsbDevice.saveDeviceID)
            {
                label6.Text += id + "\n";
            }
        }

        // Form2 인스턴스 가져오는 메서드
        public static Form2 GetInstance(string message)
        {
            if (instance != null && !instance.IsDisposed)
            {
                instance.Close();
            }

            instance = new Form2(message);
            return instance;
        }

        // 폼 로드 시 이벤트 핸들러
        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show(message); // 초기 메시지 표시
            SetHintText(); // 힌트 텍스트 설정
        }

        // 힌트 텍스트 설정 메서드
        private void SetHintText()
        {
            textBox1.Text = hintText;
        }

        // 취소 버튼 클릭 이벤트 핸들러
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // 폼 닫기
        }

        // 허용 요청 버튼 클릭 이벤트 핸들러
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string deviceName = label6.Text.Trim();
                if (string.IsNullOrEmpty(deviceName))
                {
                    MessageBox.Show("USB 장치가 없습니다.");
                    return;
                }

                string requester = textBox2.Text; // 요청자
                string reason = textBox1.Text; // 사유
                DateTime requestTime = DateTime.Now; // 요청 시간

                _requestList.AddRequest(deviceName, requester, reason, requestTime); // 허용 요청 추가

                MessageBox.Show("요청이 전송되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // 텍스트 상자에 포커스가 들어간 경우 이벤트 핸들러
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == hintText)
            {
                textBox1.Text = ""; // 힌트 텍스트 삭제
            }
        }

        // 텍스트 상자에서 포커스가 빠져나간 경우 이벤트 핸들러
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                SetHintText(); // 텍스트 상자가 비어있으면 힌트 텍스트 설정
            }
        }
    }
}
