using System;
using System.Collections.Generic;

namespace UsbSecurity
{
    internal class PermitRequestDelete
    {
        private List<PermitRequestEnt> requests;
        private PermitRequestEnt permitRequestEnt;

        public PermitRequestDelete()
        {
            permitRequestEnt = new PermitRequestEnt();
            requests = permitRequestEnt.LoadRequests();
        }
        public void RemoveRequest(int index)
        {
            permitRequestEnt.RemoveRequest(index);
        }
    }
}
