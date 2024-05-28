using System;

namespace USBprotect.PermitRequest
{
    public class PermitRequestEnt
    {
        // USB 장치 이름
        public string DeviceName { get; set; }

        // 요청자
        public string Requester { get; set; }

        // 요청 사유
        public string Reason { get; set; }

        // 요청 일시
        public DateTime RequestTime { get; set; }

        // 기본 생성자
        public PermitRequestEnt()
        {
        }

        // 매개변수를 받는 생성자
        public PermitRequestEnt(string deviceName, string requester, string reason, DateTime requestTime)
        {
            DeviceName = deviceName; // 장치 이름 설정
            Requester = requester; // 요청자 설정
            Reason = reason; // 사유 설정
            RequestTime = requestTime; // 요청 일시 설정
        }
    }
}