using System;
using System.Collections.Generic;

namespace UsbSecurity
{
    internal class PermitRequestApprove
    {
        private List<PermitRequestEnt> requests;
        private ManageAllowList manageAllowList;

        public PermitRequestApprove()
        {
            requests = PermitRequestEnt.LoadRequests();
            manageAllowList = new ManageAllowList();
        }

        public void ApproveRequest(int index)
        {
            if (index >= 0 && index < requests.Count)
            {
                var approvedRequest = requests[index];
                requests.RemoveAt(index);
                PermitRequestEnt.SaveRequests(requests);
                PermitRequestEnt.SaveApprovedRequest(approvedRequest);

                USBinfo usbInfo = new USBinfo
                {
                    DeviceName = approvedRequest.DeviceName,
                    DeviceId = approvedRequest.DeviceId,
                    PnpDeviceId = approvedRequest.DeviceId,
                    Status = "Approved",
                    IsWhiteListed = true
                };

                USBinfo.WhiteListDevices.Add(usbInfo);
                manageAllowList.enableEveryDevice(approvedRequest.DeviceId);
            }
            else
            {
                throw new ArgumentOutOfRangeException("index", "Index is out of range.");
            }
        }
    }
}
