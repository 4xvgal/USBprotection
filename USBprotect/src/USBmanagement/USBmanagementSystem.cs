// USBManagementSystem.cs

using System.Collections.Generic;
using System.IO;
using UsbSecurity;


namespace USBprotect.USBmanagement
{
    public class UsbManagementSystem : IUsbManagementSystem
    {
        //필드
        private List<USBinfo> WhiteListedUSB;
        private List<USBinfo> BlackListedUSB;

        public readonly string Filepath;


        // 생성자
        public UsbManagementSystem(string whitePath, string blackPath, string filepath)
        {
            this.Filepath = filepath;
            WhiteListedUSB = new List<USBinfo>();
            BlackListedUSB = new List<USBinfo>();
        }
    
        // 초기화 메소드
        public void InitSystem() //시스템을 초기화 하고 xml 파일을 불러온다.
        {
            WhiteListedUSB = new List<USBinfo>();
            BlackListedUSB = new List<USBinfo>();
            LoadAllUsb();
        } 
        //허용 차단 리스트의 추가,삭제, 읽기
        
        //허용 차단 관리의 통합
        // 화이트 리스트추가 : 클래스 내부 화이트 리스트 필드에 추가, 내부 블랙 리스트 필드에서 삭제
        // 추가할 객체는 IsWhiteListed = true
        // 블랙리스트 추가 : 클래스 내부 화이트 리스트 필드에 삭제, 블랙 리스트 필드에 추가.
        // 추가할 객체는 IsWhiteListed = false
        // XML로 시리얼라이징 -> 파일로 저장
        public bool AddWhiteListedUsb(USBinfo usb) //매개변수 usb를 화이트 리스트에 추가한다.
        {
            usb.IsWhiteListed = true;
            WhiteListedUSB.Add(usb);
            BlackListedUSB.Remove(usb);
            SaveAllUsb();
            return true;
        }

        public bool AddBlackListedUsb(USBinfo usb)
        {
            //매개변수 usb를 블랙 리스트에 추가한다.
            usb.IsWhiteListed = false;
            BlackListedUSB.Add(usb);
            WhiteListedUSB.Remove(usb);
            SaveAllUsb();
            return true;
        }

        public bool RemoveWhiteListedUsb(USBinfo usb) //매개변수 usb에 해당하는 usb를 찾아서 삭제한다. 용도는 리스트에서 완전히 삭제하는것
        {
            //매개변수 usb에 해당하는 usb를 찾아서 삭제한다.
            WhiteListedUSB.Remove(usb);
            SaveAllUsb();
        }

        public bool RemoveBlackListedUsb(USBinfo usb)
        {
            //매개변수 usb에 해당하는 usb를 찾아서 삭제한다.
            BlackListedUSB.Remove(usb);
            SaveAllUsb();
        }

        public List<USBinfo> GetWhiteListedUsb()
        {
            //화이트 리스트를 반환한다.
            return WhiteListedUSB;
        }

        public List<USBinfo> GetBlackListedUsb()
        {
            //블랙 리스트를 반환한다.
            return BlackListedUSB;
        }
        //xml 에서 파일을 불러온다.
        //객체들을 리스트로 저장한다.
        //저장된 객체들을 두 개의 리스트로 나눈다.
        public void LoadAllUsb()
        {
            //load all usb from xml
            List<USBinfo> temp = new List<USBinfo>();
            UsBxmlSerializer.DeserializeFromDeviceXml(Filepath);
            //divide the list into two lists
            foreach (USBinfo usb in temp)
            {
                if (usb.IsWhiteListed)
                {
                    WhiteListedUSB.Add(usb);
                }
                else
                {
                    BlackListedUSB.Add(usb);
                }
            }
        }

        public void SaveAllUsb()  //두 리스트를 병합해 xml 파일로 시리얼라이징 한다.
        { 
       
            List<USBinfo> temp = new List<USBinfo>();
            foreach (USBinfo usb in WhiteListedUSB)
            {
                temp.Add(usb);
            }
            foreach (USBinfo usb in BlackListedUSB)
            {
                temp.Add(usb);
            }
            UsBxmlSerializer.SerializeToDeviceXml(temp, Filepath);
        }

        public void DeleteAllUsb() //리스트를 초기화 하고 xml 파일도 삭제한다.
        {
            WhiteListedUSB = new List<USBinfo>();
            BlackListedUSB = new List<USBinfo>();
            File.Delete(Filepath);
        }
    }
}

