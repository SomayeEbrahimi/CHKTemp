using Com.Vivalnk.Vdireader;
using System;

namespace HCSampleApp.Droid.Temprueture
{
    [Serializable]
    public class BleDevice : Java.Lang.Object
    {
        private static readonly long serialVersionUID = -5404305906224876702L;

        private string mDeviceId;
        private string mMac;
        private int mRssi;
        private VDIType.DEVICE_TYPE type;

        public string GetDeviceId()
        {
            return mDeviceId;
        }

        public void SetDeviceId(string deviceId)
        {
            mDeviceId = deviceId;
        }

        public string GetMac()
        {
            return mMac;
        }

        public void SetMac(string mac)
        {
            mMac = mac;
        }

        public void SetRSSI(int rssi)
        {
            mRssi = rssi;
        }

        public int GetRSSI()
        {
            return mRssi;
        }

        public void SetDeviceType(VDIType.DEVICE_TYPE type)
        {
            this.type = type;
        }

        public VDIType.DEVICE_TYPE GetDeviceType()
        {
            return type;
        }
    }
}