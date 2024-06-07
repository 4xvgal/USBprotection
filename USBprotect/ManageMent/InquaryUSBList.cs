using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UsbSecurity
{
    public class InquaryUSBList
    {
        AllowBlockForm abform;
        public InquaryUSBList(AllowBlockForm abform)
        {
            this.abform = abform;
        }

        public void inquaryList()
        {
            // 버튼 누르면 리스트 전체 순회 해서 데이터 다시 로드
            if (USBinfo.BlackListDevices != null && USBinfo.BlackListDevices.Count > 0)
            {
                abform.listBox1.Items.Clear();
                foreach (var device in USBinfo.BlackListDevices)
                {
                    abform.listBox1.Items.Add($"{device.DeviceName} / {device.PnpDeviceId}");
                }
            }
            else
            {
                // 리스트가 null이거나 비어있을 경우
                MessageBox.Show("블랙리스트 장치가 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (USBinfo.WhiteListDevices != null && USBinfo.WhiteListDevices.Count > 0)
            {
                abform.listBox2.Items.Clear();
                foreach (var device in USBinfo.WhiteListDevices)
                {
                    abform.listBox2.Items.Add($"{device.DeviceName} / {device.PnpDeviceId}");
                }
            }
            else
            {
                // 리스트가 null이거나 비어있을 경우
                MessageBox.Show("화이트리스트 장치가 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}