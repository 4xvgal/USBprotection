using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using USBprotect.PermitRequest;

namespace USBprotect
{
    public partial class Form3 : Form
    {
        private List<PermitRequestManagement.PermitRequest> permitRequests; // 허용 요청을 저장할 리스트

        public Form3()
        {
            InitializeComponent();
            permitRequests = new List<PermitRequestManagement.PermitRequest>(); // 리스트 초기화
            LoadRequests(); // XML 파일에서 허용 요청 로드
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
                string displayName = request.DeviceName + " (" + deviceNameCount[request.DeviceName] + ")";
                listBox1.Items.Add(displayName); // 표시 이름을 리스트 박스에 추가
            }
        }

        // XML 파일에서 허용 요청 로드하는 메서드
        private void LoadRequests()
        {
            string filePath = "PermitRequests.xml"; // 파일 경로

            // 파일이 존재하는지 확인
            if (File.Exists(filePath))
            {
                try
                {
                    // XML 파일을 허용 요청 리스트로 역직렬화
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestManagement.PermitRequest>));
                    using (FileStream fileStream = File.OpenRead(filePath))
                    {
                        permitRequests = (List<PermitRequestManagement.PermitRequest>)serializer.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    // 로딩 실패시 에러 메시지 표시
                    MessageBox.Show("허가 요청을 불러오는 중 오류가 발생했습니다: " + ex.Message);
                }
            }
            else
            {
                // 파일이 없는 경우 메시지 표시
                MessageBox.Show("PermitRequests.xml 파일을 찾을 수 없습니다.");
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

        // 허용 요청을 XML 파일에 저장하는 메서드
        private void SaveRequests()
        {
            string filePath = "PermitRequests.xml"; // 파일 경로

            try
            {
                // 허용 요청 리스트를 XML로 직렬화하여 파일에 저장
                XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestManagement.PermitRequest>));
                using (FileStream fileStream = File.Create(filePath))
                {
                    serializer.Serialize(fileStream, permitRequests);
                }
            }
            catch (Exception ex)
            {
                // 저장 실패시 에러 메시지 표시
                MessageBox.Show("허가 요청을 저장하는 중 오류가 발생했습니다: " + ex.Message);
            }
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
                    permitRequests.RemoveAt(selectedIndex);
                    // XML 파일 업데이트
                    SaveRequests();
                    // 리스트 박스 갱신
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
