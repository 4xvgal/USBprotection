// USBManagementSystem.cs
using System.Collections.Generic;
using UsbSecurity;
using static UsbSecurity.USBinfo;
using static USBprotect.src.USBmanagement.USBxmlSerializer;
using System.Windows.Forms;
using USBprotect.src.USBmanagement;


public class USBManagementSystem : IUSBManagementSystem
{
    private List<USBinfo> WhiteListedUSB;
    private List<USBinfo> BlackListedUSB;

    public string filepath;


    /// </summary>
    /// <param name="whitePath"></param>
    /// <param name="blackPath"></param>
    public USBManagementSystem(string whitePath, string blackPath)
    {
        WhiteListedUSB = new List<USBinfo>();
        BlackListedUSB = new List<USBinfo>();
    }

    //xml 에서 파일을 불러온다.
    //객체들을 리스트로 저장한다.
    //저장된 객체들을 두 개의 리스트로 나눈다.

   
    public void LoadAllUSB()
    {
        //load all usb from xml
        List<USBinfo> temp = new List<USBinfo>();
        USBxmlSerializer.DeserializeFromDeviceXml(temp, filepath);
        //divide the list into two lists
        foreach (USBinfo usb in temp)
        {
            if (usb.IsWhiteListed)
            {
                WhiteListedUSB.Add(usb);
            }
            else
            {
                BlackListedUSB.Add(usb);
            }
        }
    }

    public void InitSystem()
    {
        throw new System.NotImplementedException();
    }

    public void AddWhiteListedUSB(USBinfo usb)
    {
        throw new System.NotImplementedException();
    }

    public void AddBlackListedUSB(USBinfo usb)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveWhiteListedUSB(USBinfo usb)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveBlackListedUSB(USBinfo usb)
    {
        throw new System.NotImplementedException();
    }

    public List<USBinfo> GetWhiteListedUSB()
    {
        throw new System.NotImplementedException();
    }

    public List<USBinfo> GetBlackListedUSB()
    {
        throw new System.NotImplementedException();
    }

    public void LoadBlackListedUSB()
    {
        throw new System.NotImplementedException();
    }

    public void LoadWhiteListedUSB()
    {
        throw new System.NotImplementedException();
    }

    public void SaveListedUSB()
    {
        throw new System.NotImplementedException();
    }

    public void SaveBlackListedUSB()
    {
        throw new System.NotImplementedException();
    }
}

