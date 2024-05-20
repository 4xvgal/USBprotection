// IUSBManagementSystem.cs

using System.Collections.Generic;
using UsbSecurity;

namespace USBprotect.USBmanagement
{
    public interface IUsbManagementSystem
    {
        void InitSystem(); //시스템을 초기화 하고 xml 파일을 불러온다.
        //허용 차단 리스트의 추가, 삭제, 읽기 
        //허용 차단 관리의 통합
        // 화이트 리스트추가 : 클래스 내부 화이트 리스트 필드에 추가, 내부 블랙 리스트 필드에서 삭제
        // 추가할 객체는 IsWhiteListed = true
        // 블랙리스트 추가 : 클래스 내부 화이트 리스트 필드에 삭제, 블랙 리스트 필드에 추가.
        // 추가할 객체는 IsWhiteListed = false
        // XML로 시리얼라이징 -> 파일로 저장  
        bool AddWhiteListedUsb(USBinfo usb);  //화이트 리스트 추가
        bool AddBlackListedUsb(USBinfo usb); //블랙 리스트 추가
        bool RemoveWhiteListedUsb(USBinfo usb); //화이트 리스트 삭제
        bool RemoveBlackListedUsb(USBinfo usb); //블랙 리스트 삭제
        List<USBinfo> GetWhiteListedUsb(); //화이트 리스트 읽기
        List<USBinfo> GetBlackListedUsb(); //블랙 리스트 읽기
        
    
        //허용 차단 리스트를 위한 Xml 파일 입출력
        // 불러오기, 저장하기, 삭제하기
        void LoadAllUsb(); 
        void SaveAllUsb();
        void DeleteAllUsb();
        
        // 꽃힌 장치 감지하여 처리 
        // 허용 리스트에 있으면 통과, 블랙 리스트에 있으면 차단
        // 둘 다 없으면 선 차단
        void HandleUsbDeviceInserted(USBinfo usb);
        void HandleUsbDeviceRemoved(USBinfo usb);
    }
}

