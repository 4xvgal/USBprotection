using System;
using System.Diagnostics;
using System.Management;
using USBprotect;
using USBprotect.InternalFunction;

// class information :: 
// USB 장치의 접속을 감지하는 클래스 입니다. 
// System.Management 는 솔루션 탐색기 종속성에서 우클릭 -> NUGET 패키지 관리자를 통해 검색하셔서 다운받으시면 바로 작동합니다.
// 2024:04:04:14:15 마지막 수정 LGJ

namespace UsbSecurity
{

    //아래 코드의 용도를 각각 설명한다
    public class DeviceMonitor // USB 장치를 감시하는 클래스
    {
        //정보::ManagementEventWatcher 클래스는 WMI 이벤트를 감시하는 클래스 ::
        //라이브러리는 System.Management 네임스페이스에 있음

        private ManagementEventWatcher insertWatcher; // USB 장치 삽입 감시 객체
        private ManagementEventWatcher removeWatcher; // USB 장치 제거 감시 객체
        private DevconCMD devcon; // devcon 명령줄 객체 
        private ParsingUsbDevice parsingUsbDevice; // 인스턴스 id 추철 객체
        //private UsbConnectionEvent usbConnectionEvent;



        public DeviceMonitor()   // 생성자
        {

           // usbConnectionEvent = new UsbConnectionEvent(); // UsbConnectionEvent 인스턴스 생성
            this.devcon = new DevconCMD(); // DevconClass 인스턴스 생성

            // WMI query for USB device insertion events
            var insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity' AND TargetInstance.DeviceID LIKE 'USB%'");  // USB 장치 삽입 감시 쿼리
            insertWatcher = new ManagementEventWatcher(insertQuery);                                                                        // USB 장치 삽입 감시 인스턴스
            insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);                                                // USB 장치 삽입 이벤트 핸들러

            // WMI query for USB device removal events
            var removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity' AND TargetInstance.DeviceID LIKE 'USB%'"); // USB 장치 삽입 감시 쿼리
            removeWatcher = new ManagementEventWatcher(removeQuery);                                                                       // USB 장치 제거 감시 인스턴스
            removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);                                                // USB 장치 제거 이벤트 핸들러

        }


        public void Start() // USB 장치 감시 시작 인스턴스
        {
            insertWatcher.Start();
            removeWatcher.Start();
            parsingUsbDevice = new ParsingUsbDevice(); // USB 장치 인스턴스 추출 객체 생성
        }

        public void Stop() // USB 장치 감시 중지 인스턴스
        {
            insertWatcher.Stop();
            removeWatcher.Stop();
        }

        private void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            parsingUsbDevice.GetUsbDevices(); // USB 장치 목록 추출
            Console.WriteLine("USB 장치가 감지됨");
            parsingUsbDevice.showUSBinfo();
            //Console.WriteLine(" ");
            //usbConnectionEvent.OnUsbConnected();    // USB가 연결되면 UsbConnectionEvent의 OnUsbConnected() 메서드 호출


            FormEventBase formEvent = new UnauthorizedUsbFormEvent();
            formEvent.PopUpForm();
       
        }

        private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e) // USB 장치 제거 이벤트
        {
            FormEventBase formEvent = new RemoveUsbFormEvent();
            formEvent.PopUpForm();
            //parsingUsbDevice.removeData(); // 해당 usb를 리스트 에서 삭제
        }


    }

}

