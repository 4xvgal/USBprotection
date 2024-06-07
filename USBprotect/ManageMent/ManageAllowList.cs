using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UsbSecurity
{

    class ManageAllowList
    {

        string devconPath = @"C:\Program Files (x86)\Windows Kits\10\Tools\10.0.22621.0\x64\devcon.exe"; // !! devcon 모듈의 경로에 대한 수정 요구됨 
        public ManageAllowList()
        {
            // 화이트 리스트 컬렉션 변경 이벤트 헨들러 등록
            USBinfo.WhiteListDevices.CollectionChanged += WhitekList_CollectionChanged;
        }

        public string AllowDevconCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = devconPath,
                Arguments = command,
                UseShellExecute = true, // 셸 실행 사용
                Verb = "runas", // 관리자 권한 요청
                CreateNoWindow = true
            };

            try
            {
                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                        return output;
                    else
                        return $"Failed with error: {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Error executing Devcon command: {ex.Message}";
            }
        }

        internal bool SetDevconPath(string path) // devcon 경로 설정
        {
            devconPath = path; // 경로 설정
            if (checkDevconExist() == true)
            { // devcon 존재 여부 확인
                return true;
            }

            return false;
        }
        private bool checkDevconExist() // devcon 존재 여부 확인
        {
            //devon help 를 실행해 결과가 나오면 devcon 이 존재하는 것으로 판단
            //Device Console Help 을 포함해야함
            string result = AllowDevconCommand("help");
            if (result.Contains("Device Console Help")) // 결과에 Device Console Help 가 포함되어 있으면
            {
                return true; // 존재함
            }
            else
            {
                return false; // 존재하지 않음
            }

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
            AllowDevconCommand($"enable \"@{DeviceId}\""); //명령어 실행   
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
                    MessageBox.Show(deviceid + "가 승인되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
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