using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UsbSecurity
{
    public partial class RequestManagementForm : Form
    {
        private PermitRequestApprove requestApprove;
        private PermitRequestDelete requestDelete;
        private PermitRequestInquiry requestInquiry;
        private List<PermitRequestEnt> permitRequests;

        public RequestManagementForm()
        {
            InitializeComponent();
            requestApprove = new PermitRequestApprove();
            requestDelete = new PermitRequestDelete();
            requestInquiry = new PermitRequestInquiry();
            permitRequests = requestInquiry.GetRequests();
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            listBox1.Items.Clear();
            var deviceNameCount = new Dictionary<string, int>();

            if (permitRequests.Count == 0)
            {
                MessageBox.Show("현재 허용 요청이 존재하지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var request in permitRequests)
            {
                if (!deviceNameCount.ContainsKey(request.DeviceName))
                {
                    deviceNameCount[request.DeviceName] = 1;
                }
                else
                {
                    deviceNameCount[request.DeviceName]++;
                }

                string displayName = request.DeviceName;
                if (deviceNameCount[request.DeviceName] > 1)
                {
                    displayName += " (" + deviceNameCount[request.DeviceName] + ")";
                }
                listBox1.Items.Add(displayName);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                approve_btn.Enabled = true;
                delete_btn.Enabled = true;

                var selectedRequest = permitRequests[selectedIndex];
                label5.Text = selectedRequest.Requester;
                label6.Text = selectedRequest.RequestTime.ToString("yyyy-MM-dd HH:mm:ss");
                label7.Text = selectedRequest.Reason;
            }
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void approve_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                var result = MessageBox.Show("허용하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        requestApprove.ApproveRequest(selectedIndex);
                        PermitRequestEnt.SaveApprovedRequest(permitRequests[selectedIndex]);
                        permitRequests.RemoveAt(selectedIndex);
                        PopulateListBox();
                        MessageBox.Show("요청이 승인되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("요청 승인 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                var result = MessageBox.Show("삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        requestDelete.RemoveRequest(selectedIndex);
                        permitRequests.RemoveAt(selectedIndex);
                        PopulateListBox();
                        MessageBox.Show("요청이 삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("요청 삭제 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void home_btn_Click(object sender, EventArgs e)
        {
            this.Hide(); // 현재 폼 숨기기
            MainForm.Instance.Show();
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
                PopulateListBox();  // 리스트 박스 다시 채우기
                label5.Text = ""; // 요청자 초기화
                label6.Text = ""; // 요청 일시 초기화
                label7.Text = ""; // 요청 사유 초기화
        }
    }
}
