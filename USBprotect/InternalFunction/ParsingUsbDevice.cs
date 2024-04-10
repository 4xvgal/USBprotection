using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

// class information :: 
// 현재 연결된 USB 저장장치 (이동저장장치만)의 인스턴스 ID 를 추출합니다. 
// 차단 또는 승인할 장치의 인스턴스 아이디에 사용됩니다.
// 2024:04:04:14:15 마지막 수정 LGJ

namespace UsbSecurity
{
    using System;
    using System.Collections.Generic;
    using System.Management; // System.Management 네임스페이스 참조 필요

    class ParsingUsbDevice
    {
    
        public static List<string> saveDeviceID = new List<string>(); // 지금 까지 인식된 USB id 리스트 static 형

        public static List<string> getCurrentUsbID() // .. 현재 연결된 USB id 를 추출하여 리스트에 추가하는 함수
        {
            List<string> currentDeviceID = new List<string>(); // 현재 연결된 USB ID 를 임시로 저장하는 내부 제너릭

            string pattern =  @"USB\\VID_[0-9A-F]+&PID_[0-9A-F]+"; // USB 장치 ID 패턴 정규식 
                
            // 'Win32_PnPEntity'에서 USB 저장 장치만 필터링합니다.
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Service = 'USBSTOR'");
            
            foreach (ManagementObject queryObj in searcher.Get())
            {
                string deviceId = queryObj["PNPDeviceID"].ToString(); // PNP 장치 ID를 가져옴

                if (!string.IsNullOrEmpty(deviceId)) // 장치 ID가 비어있지 않다면
                {   
                    Match parsingdata = Regex.Match(deviceId, pattern); // 장치 ID를 정규식으로 추출
                    if (parsingdata.Success)    // 추출 성공시
                    {
                       currentDeviceID.Add(parsingdata.Value); // 현재 연결된 장치 추출된 장치 ID를 리스트에 추가
                    }

                }
            }

            return currentDeviceID;
        }

        /*#*/public void InsertData() // 현재 인식된 USB 를 전체 LIST 에 저장하는 함수 
        {
            var externalDriveIDs = getCurrentUsbID(); // 이동식 드라이브 인스턴스 ID를 가져옴
            foreach (var id in externalDriveIDs)
            {
                if (!saveDeviceID.Contains(id)) // 리스트에 id가 이미 존재하는지 확인
                {
                    saveDeviceID.Add(id); // 리스트에 없는 경우에만 id를 추가
                }
            }
        }

        public void removeData() // 제거된 이동저장장치의 ID 를 전체 리스트에서 삭제하는 함수
        {
            var externalDriveIDs = getCurrentUsbID(); // 이동식 드라이브 인스턴스 ID를 가져옴 자료형은 
            saveDeviceID.RemoveAll(id => !externalDriveIDs.Contains(id)); // 전체 ID 리스트에서 현재 접속된 USB ID 를 비교해서 중복값이 존재하지 않을 경우 전체 리스트에서 삭제한다.
            // #USB 가 제거 되었을때 제거된 USB 의 ID 를 리스트 상에서 지웁니다.

        }

        //private static List<string> saveDeviceID = new List<string>(); 를 반환하는 코드
        public List<string> getSaveDeviceID()
        {
            return saveDeviceID;
        }

    }
}

    


