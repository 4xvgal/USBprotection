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


namespace USBprotect
{
    public partial class Form2 : Form
    {
        private string message; // 클래스 레벨 변수로 메시지 저장
        private static Form2 instance;

        private Form2(string message) // 기본 생성자 대신 메시지를 받는 생성자 정의
        {
            InitializeComponent();
            this.message = message; // 생성자에서 전달된 메시지를 저장
            this.Load += Form2_Load; // Load 이벤트에 핸들러 연결

            //public List<string> getSaveDeviceID() 에서 foreach로 하나씩 꺼내서 리스트 박스에 넣는 작업
            foreach (var id in ParsingUsbDevice.saveDeviceID)
            {
                listBox1.Items.Add(id);
            }
                
       
        }

        // 여러개의 폼이 생성되는 것을 방지하기 위해 싱글톤 패턴을 적용한 GetInstance 메서드 정의
        public static Form2 GetInstance(string message) // 싱글톤 패턴을 적용한 GetInstance 메서드 정의
        {
            
            if (instance != null && !instance.IsDisposed)
            {
                instance.Close(); // 기존 인스턴스 닫기
            }

            instance = new Form2(message); // 새로운 인스턴스 생성

            return instance; // 생성된 인스턴스 반환

        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            MessageBox.Show(message);
        }
    }
 }


            MessageBox.Show("Usb 장치가 감지되었습니다.");
        }
    }
}

