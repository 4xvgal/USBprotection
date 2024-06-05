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

        // 장치 ID
        public string DeviceId { get; set; }

        // 파일 경로
        public static string FilePath { get; } = "PermitRequests.xml";

        // 기본 생성자
        public PermitRequestEnt() { }

        // 매개변수를 받는 생성자
        public PermitRequestEnt(string deviceName, string requester, string reason, DateTime requestTime, string deviceId)
        {
            DeviceName = deviceName;
            Requester = requester;
            Reason = reason;
            RequestTime = requestTime;
            DeviceId = deviceId;
        }
    }
}
