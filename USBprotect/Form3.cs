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
        private List<PermitRequestManagement.PermitRequest> permitRequests;

        public Form3()
        {
            InitializeComponent();
            permitRequests = new List<PermitRequestManagement.PermitRequest>();
            LoadRequests();
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            listBox1.Items.Clear();
            var deviceNameCount = new Dictionary<string, int>();

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

                string displayName = request.DeviceName + " (" + deviceNameCount[request.DeviceName] + ")";
                listBox1.Items.Add(displayName);
            }
        }

        private void LoadRequests()
        {
            string filePath = "PermitRequests.xml";

            if (File.Exists(filePath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestManagement.PermitRequest>));
                    using (FileStream fileStream = File.OpenRead(filePath))
                    {
                        permitRequests = (List<PermitRequestManagement.PermitRequest>)serializer.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading permit requests: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("PermitRequests.xml file not found.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex != -1)
            {
                var selectedRequest = permitRequests[selectedIndex];

                label5.Text = selectedRequest.Requester;
                label6.Text = selectedRequest.RequestTime.ToString("yyyy-MM-dd HH:mm:ss");
                label7.Text = selectedRequest.Reason;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveRequests()
        {
            string filePath = "PermitRequests.xml";

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestManagement.PermitRequest>));
                using (FileStream fileStream = File.Create(filePath))
                {
                    serializer.Serialize(fileStream, permitRequests);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving permit requests: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex != -1)
            {
                string message = "정말로 삭제하시겠습니까?";
                string caption = "요청 삭제 확인";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Remove selected request from list
                    permitRequests.RemoveAt(selectedIndex);
                    // Update XML file
                    SaveRequests();
                    // Refresh list box
                    PopulateListBox();
                }
            }
            else
            {
                MessageBox.Show("삭제할 요청을 선택해주세요.", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
