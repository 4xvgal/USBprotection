using System;

namespace USBprotect.PermitRequest
{
    public class PermitRequestManagement
    {
        public class PermitRequest
        {
            public string DeviceName { get; set; }
            public string Requester { get; set; }
            public string Reason { get; set; }
            public DateTime RequestTime { get; set; }

            public PermitRequest() { }

            public PermitRequest(string deviceName, string requester, string reason, DateTime requestTime)
            {
                DeviceName = deviceName;
                Requester = requester;
                Reason = reason;
                RequestTime = requestTime;
            }
        }
    }
}
