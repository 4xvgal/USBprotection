using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbSecurity;

namespace USBprotect.SettingExportImport.form
{
    public partial class SettingExportImport : Form
    {
        public SettingExportImport()
        {
            InitializeComponent();
        }

        private void SettingExportImport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Export_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ExportDirTextBox.Text) && !string.IsNullOrEmpty(ExportDirTextBox.Text))
            {
                SettingImport importer = new SettingImport();
                ObservableCollection<USBinfo> usbInfos = importer.LoadFromDeviceFile(ExportDirTextBox.Text);

                if (usbInfos != null && usbInfos.Count > 0)
                {
                    SettingExport exporter = new SettingExport();
                    exporter.SaveToDeviceFile(ExportDirTextBox.Text, usbInfos);
                    MessageBox.Show($"Data exported successfully! Total {usbInfos.Count} device(s) information exported.", "Export Successful");
                }
                else
                {
                    MessageBox.Show("No data available to export.", "Export Error");
                }
            }
            else
            {
                MessageBox.Show("Please ensure both paths are selected.", "Export Error");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnBrowseExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportDirTextBox.Text = saveFileDialog.FileName;
            }
        }

        private void btnBrowseImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportDirTextBox.Text = openFileDialog.FileName;
            }
        }

        private void ImportDirTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Import_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ImportDirTextBox.Text))
            {
                SettingImport importer = new SettingImport();
                ObservableCollection<USBinfo> usbInfos = importer.LoadFromDeviceFile(ImportDirTextBox.Text);
                MessageBox.Show($"Data imported successfully! Total {usbInfos.Count} device(s) information imported.", "Import Successful");

                // 예를 들어, 데이터를 콘솔에 출력하는 등의 추가 작업을 수행할 수 있습니다.
                foreach (var info in usbInfos)
                {
                    Console.WriteLine($"Device Name: {info.DeviceName}, Status: {info.Status}");
                }
            }
            else
            {
                MessageBox.Show("Please select a file path.", "Import Error");
            }
        }
    }
}
