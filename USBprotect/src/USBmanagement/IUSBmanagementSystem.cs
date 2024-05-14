// IUSBManagementSystem.cs
using System.Collections.Generic;
using UsbSecurity;

using System;
using System.Xml;

public interface IUSBManagementSystem
{
    void InitSystem();
    void AddWhiteListedUSB(USBinfo usb);
    void AddBlackListedUSB(USBinfo usb);
    void RemoveWhiteListedUSB(USBinfo usb);
    void RemoveBlackListedUSB(USBinfo usb);
    List<USBinfo> GetWhiteListedUSB();
    List<USBinfo> GetBlackListedUSB();
    
    void LoadBlackListedUSB();
    void LoadWhiteListedUSB();
    void SaveListedUSB();
    void SaveBlackListedUSB();

    void LoadAllUSB();
}

