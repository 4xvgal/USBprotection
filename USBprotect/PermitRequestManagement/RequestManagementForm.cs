using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using USBprotect.PermitRequest;

namespace USBprotect
{
    public partial class RequestManagementForm : Form
    {
        private PermitRequestApprove requestApprove; // 요청 허용 클래스 인스턴스
        private PermitRequestDelete requestDelete; // 요청 삭제 클래스 인스턴스
        private PermitRequestInquiry requestInquiry; // 요청 조회 클래스 인스턴스
        private List<PermitRequest.PermitRequestEnt> permitRequests; // 허용 요청 리스트

        public RequestManagementForm()
        {
            InitializeComponent();
            requestApprove = new PermitRequestApprove(); // 요청 허용 클래스 초기화
            requestDelete = new PermitRequestDelete(); // 요청 삭제 클래스 초기화
            requestInquiry = new PermitRequestInquiry(); // 요청 조회 클래스 초기화
            permitRequests = requestInquiry.GetRequests(); // 허용 요청 리스트 초기화
            PopulateListBox(); // 리스트 박스 채우기
        }

        private void PopulateListBox()   // 리스트 박스를 허가 요청으로 채우는 메서드
        {
            listBox1.Items.Clear(); // 리스트 박스에 있는 항목들 지우기
            var deviceNameCount = new Dictionary<string, int>(); // 디바이스 이름과 갯수를 저장할 딕셔너리

            foreach (var request in permitRequests) // 각 허용 요청에 대해 반복
            {
                if (!deviceNameCount.ContainsKey(request.DeviceName))   // 디바이스 이름의 갯수 업데이트
                {
                    deviceNameCount[request.DeviceName] = 1;
                }
                else
                {
                    deviceNameCount[request.DeviceName]++;
                }

                string displayName = request.DeviceName;       // 디바이스 이름과 갯수를 포함한 표시 이름 생성
                if (deviceNameCount[request.DeviceName] > 1)   // 디바이스 이름이 중복되는 경우에만 숫자를 표시
                {
                    displayName += " (" + deviceNameCount[request.DeviceName] + ")";
                }
                listBox1.Items.Add(displayName); // 표시 이름을 리스트 박스에 추가
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)     // 리스트 박스에서 선택한 인덱스 변경 이벤트 핸들러
        {
            int selectedIndex = listBox1.SelectedIndex; // 선택한 아이템의 인덱스 가져오기

            // 아이템이 선택되었는지 확인
            if (selectedIndex != -1)
            {
                // 선택한 허가 요청의 세부 정보 표시
                var selectedRequest = permitRequests[selectedIndex];
                label5.Text = selectedRequest.Requester;
                label6.Text = selectedRequest.RequestTime.ToString("yyyy-MM-dd HH:mm:ss");
                label7.Text = selectedRequest.Reason;
            }
        }

        private void button3_Click(object sender, EventArgs e)       // 삭제 버튼 클릭 이벤트 핸들러
        {
            try
            {
                int selectedIndex = listBox1.SelectedIndex; // 선택한 아이템의 인덱스 가져오기
                requestDelete.RemoveRequest(selectedIndex); // 선택한 요청 삭제
                permitRequests = requestInquiry.GetRequests(); // 업데이트된 요청 리스트 가져오기
                PopulateListBox(); // 리스트 박스 업데이트
                MessageBox.Show("요청이 삭제되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)   // 폼을 닫는 버튼 클릭 이벤트 핸들러
        {
            this.Close(); // 폼 닫기
        }

        private void button2_Click(object sender, EventArgs e)
        {
                try
                {
                    int selectedIndex = listBox1.SelectedIndex; // 선택한 아이템의 인덱스 가져오기
                    requestApprove.ApproveRequest(selectedIndex); // 선택한 요청 승인
                    permitRequests.RemoveAt(selectedIndex); // 허용 요청 리스트에서 제거
                    PopulateListBox(); // 리스트 박스 업데이트
                    MessageBox.Show("요청이 승인되었습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
        }
    }
}