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
            PermitRequestEnt.ApproveRequest(index);
        }
    }
}
