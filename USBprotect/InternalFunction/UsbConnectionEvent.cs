using System;
using System.Windows.Forms;
using USBprotect;

namespace UsbSecurity
{
    public class UsbConnectionEvent
    {
        public void OnUsbConnected()    // USB 연결 이벤트 발생 시 실행되는 메서드
        {
            CloseForm1(); // Form1 닫기
            ShowForm2(); // USB가 연결되면 Form2를 표시
        }

        private void ShowForm2()    // Form2 표시
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void CloseForm1()   // Form1 닫기
        {                           // Form1이 안닫혀서 임시로 만듦.
            Form form1 = Application.OpenForms["Form1"];
            if (form1 != null)
            {
                form1.Close();
            }
        }
    }
}
