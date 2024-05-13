using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbSecurity;


namespace UsbSecurity
{
    public partial class BlockingManagement : Form
    {

        public BlockingManagement()
        {
            InitializeComponent();

            foreach (var device in USBinfo.BlackListDevices)
            {
               // 리스트 박스에 블랙리스트 장치들을 추가
                listBox1.Items.Add(device.DeviceName);
            }

            foreach(var newitem in listBox1.Items)
            {
                // newitem 캐스팅
                USBinfo item = newitem as USBinfo;
                USBinfo.WhiteListDevices.Add(item); 
            }
            
        }

      
        private void BlockingManagement_Load(object sender, EventArgs e)
        {

        }
    }
}
