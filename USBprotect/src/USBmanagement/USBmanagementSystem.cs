using System;
using System.Collections.Generic;
using System.IO;
using UsbSecurity;

namespace USBprotect.USBmanagement
{
    public class UsbManagementSystem : IUsbManagementSystem
    {
        private List<USBinfo> _whiteListedUsb;
        private List<USBinfo> _blackListedUsb;
        public readonly string Filepath;

        public UsbManagementSystem(string filepath)
        {
            Filepath = filepath;
            _whiteListedUsb = new List<USBinfo>();
            _blackListedUsb = new List<USBinfo>();
        }
        //시스템 초기화
        public void InitSystem()
        {
            try
            {
                LoadAllUsb(); // XML 파일 불러오기
                Console.WriteLine("System initialized and USB data loaded."); //시스템 초기화 완료
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to initialize system: {ex.Message}"); //시스템 초기화 실패
                throw;
            }
        }
        //화이트 리스트 추가
        public bool AddWhiteListedUsb(USBinfo usb)
        {
            try
            {
                usb.IsWhiteListed = true;
                if (!_whiteListedUsb.Contains(usb))
                {
                    _whiteListedUsb.Add(usb);
                    Console.WriteLine($"USB {usb.DeviceId} added to white-list.");
                }
                _blackListedUsb.Remove(usb);
                SaveAllUsb();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding USB {usb.DeviceId} to white-list: {ex.Message}");
                return false;
            }
        }
        //블랙 리스트 추가
        public bool AddBlackListedUsb(USBinfo usb)
        {
            try
            {
                usb.IsWhiteListed = false;
                if (!_blackListedUsb.Contains(usb))
                {
                    _blackListedUsb.Add(usb);
                    Console.WriteLine($"USB {usb.DeviceId} added to black-list.");
                }
                _whiteListedUsb.Remove(usb);
                SaveAllUsb();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding USB {usb.DeviceId} to black-list: {ex.Message}");
                return false;
            }
        }
        //화이트 리스트 삭제
        public bool RemoveWhiteListedUsb(USBinfo usb)
        {
            try
            {
                if (_whiteListedUsb.Remove(usb))
                {
                    SaveAllUsb();
                    Console.WriteLine($"USB {usb.DeviceId} removed from white-list.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error removing USB {usb.DeviceId} from white-list: {ex.Message}");
                return false;
            }
        }
        //블랙 리스트 삭제
        public bool RemoveBlackListedUsb(USBinfo usb)
        {
            try
            {
                if (_blackListedUsb.Remove(usb))
                {
                    SaveAllUsb();
                    Console.WriteLine($"USB {usb.DeviceId} removed from black-list.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error removing USB {usb.DeviceId} from black-list: {ex.Message}");
                return false;
            }
        }
        //화이트 리스트 반환
        public List<USBinfo> GetWhiteListedUsb()
        {
            return _whiteListedUsb;
        }
        //블랙 리스트 반환
        public List<USBinfo> GetBlackListedUsb()
        {
            return _blackListedUsb;
        }
        //XML 파일 불러오기
        public void LoadAllUsb()
        {
            try
            {
                if (File.Exists(Filepath))
                {
                    var temp = UsBxmlSerializer.DeserializeFromDeviceXml(Filepath);
                    foreach (var usb in temp)
                    {
                        if (usb.IsWhiteListed)
                            _whiteListedUsb.Add(usb);
                        else
                            _blackListedUsb.Add(usb);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load USB data from XML: {ex.Message}");
                throw;
            }
        } 
        //XML 파일 저장
        public void SaveAllUsb()
        {
            try
            {
                var allUsbList = new List<USBinfo>(_whiteListedUsb.Count + _blackListedUsb.Count);
                allUsbList.AddRange(_whiteListedUsb);
                allUsbList.AddRange(_blackListedUsb);
                UsBxmlSerializer.SerializeToDeviceXml(allUsbList, Filepath);
                Console.WriteLine("All USB data has been successfully serialized to XML.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to serialize USB data to XML: " + ex.Message);
                throw;
            }
        }
        //XML 파일 삭제
        public void DeleteAllUsb()
        {
            try
            {
                _whiteListedUsb.Clear();
                _blackListedUsb.Clear();
                if (File.Exists(Filepath))
                {
                    File.Delete(Filepath);
                    Console.WriteLine($"The file '{Filepath}' has been successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete USB data file: {ex.Message}");
                throw;
            }
        }
    }
}
