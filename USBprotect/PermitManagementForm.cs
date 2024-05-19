using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using USBprotect.PermitRequest;

namespace USBprotect
{
    public partial class PermitManagementForm : Form
    {
        private PermitRequestManagement _permitRequestManagement;
        private List<PermitRequest.PermitRequest> permitRequests; // 허용 요청을 저장할 리스트

        public PermitManagementForm()
        {
            InitializeComponent();
            _permitRequestManagement = new PermitRequestManagement();
            permitRequests = _permitRequestManagement.GetRequests(); // 리스트 초기화
            PopulateListBox(); // 로드된 요청으로 리스트 박스 채우기
        }

        // 리스트 박스를 허가 요청으로 채우는 메서드
        private void PopulateListBox()
        {
            listBox1.Items.Clear(); // 리스트 박스에 있는 항목들 지우기
            var deviceNameCount = new Dictionary<string, int>(); // 디바이스 이름과 갯수를 저장할 딕셔너리

            // 각 허용 요청에 대해 반복
            foreach (var request in permitRequests)
            {
                // 디바이스 이름의 갯수 업데이트
                if (!deviceNameCount.ContainsKey(request.DeviceName))
                {
                    deviceNameCount[request.DeviceName] = 1;
                }
                else
                {
                    deviceNameCount[request.DeviceName]++;
                }

                // 디바이스 이름과 갯수를 포함한 표시 이름 생성
                string displayName = request.DeviceName;
                // 디바이스 이름이 중복되는 경우에만 숫자를 표시
                if (deviceNameCount[request.DeviceName] > 1)
                {
                    displayName += " (" + deviceNameCount[request.DeviceName] + ")";
                }
                listBox1.Items.Add(displayName); // 표시 이름을 리스트 박스에 추가
            }
        }


        // 리스트 박스에서 선택한 인덱스 변경 이벤트 핸들러
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        // 폼을 닫는 버튼 클릭 이벤트 핸들러
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // 폼 닫기
        }

        // 선택한 요청을 삭제하는 버튼 클릭 이벤트 핸들러
        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex; // 선택한 아이템의 인덱스 가져오기

            // 아이템이 선택되었는지 확인
            if (selectedIndex != -1)
            {
                string message = "정말로 삭제하시겠습니까?"; // 삭제 확인 메시지
                string caption = "요청 삭제 확인"; // 메시지 박스 제목
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // 삭제 확인 대화상자 표시

                // 사용자가 삭제를 확인한 경우
                if (result == DialogResult.Yes)
                {
                    // 리스트에서 선택한 요청 삭제
                    _permitRequestManagement.RemoveRequest(selectedIndex);
                    // 리스트 박스 갱신
                    permitRequests = _permitRequestManagement.GetRequests();
                    PopulateListBox();
                }
            }
            else
            {
                // 삭제할 요청이 선택되지 않은 경우 메시지 표시
                MessageBox.Show("삭제할 요청을 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}