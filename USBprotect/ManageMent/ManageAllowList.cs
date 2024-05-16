using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbSecurity;

namespace USBsecurity
{

    class ManageAllowList
    {
        DevconCMD devconCMD = new DevconCMD(); // devconCMD 클래스 인스턴스 생성 



        public ManageAllowList()
        {
           // 화이트 리스트 컬렉션 변경 이벤트 헨들러 등록
           USBinfo.WhiteListDevices.CollectionChanged += WhitekList_CollectionChanged;
        }

        // blackList 컬렉션에 변경이 발생할 때 호출되는 메서드

        private void WhitekList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // 변경 유형이 추가일 경우 실행 (ㄹㅇ 이게 뭐냐면 새로운 항목이 추가되었을 때 실행)
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

                foreach (var Item in e.NewItems) // 새로 추가된 항목들을 가져옴
                {
                    USBinfo newItem = Item as USBinfo; // USBinfo 클래스로 캐스팅
 
                    enableEveryDevice(newItem.DeviceId); // 장치 활성화 해버리기
                }
            }
        }
        public void enableEveryDevice(string DeviceId) // 장치 활성화 메서드
        {
            devconCMD.DevconCommand($"enable \"@{DeviceId}\""); //명령어 실행   
        }

        public void BlackToWhite(string deviceid)
        {
            var device = USBinfo.BlackListDevices.FirstOrDefault(x => x.PnpDeviceId.Trim().Equals(deviceid.Trim(), StringComparison.OrdinalIgnoreCase));

            if (device != null)
            {   
                // 비동기 처리: 장치를 화이트리스트로 이동
                // 임시 변수를 사용하여 컬렉션 변경 이벤트 핸들러가 완료된 후 컬렉션 수정
                var toAdd = device;
                var toRemove = device;

                // 실제 컬렉션 수정은 이벤트 핸들러 외부에서 수행
                Task.Run(() => {
                    USBinfo.WhiteListDevices.Add(toAdd);
                    USBinfo.BlackListDevices.Remove(toRemove);
                });
            }
            else
            {
                // 예외 처리: 객체가 존재하지 않음
                MessageBox.Show($"Device with ID {deviceid} not found in whitelist.");
            }
        }

    }
}
