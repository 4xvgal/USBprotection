using System;
using System.Collections.Generic;

namespace UsbSecurity
{
    internal class PermitRequestDelete
    {
        private List<PermitRequestEnt> requests;

        public PermitRequestDelete()
        {
            requests = PermitRequestEnt.LoadRequests();
        }
        public void RemoveRequest(int index)
        {
            PermitRequestEnt.RemoveRequest(index);
        }
    }
}
