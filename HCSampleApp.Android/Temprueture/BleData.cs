using Com.Vivalnk.Vdireader;
using System;

namespace HCSampleApp.Droid.Temprueture
{
    [Serializable]
    public class BleData : Java.Lang.Object
    {
        private static readonly long serialVersionUID = -1151382250238024534L;

        private string mDeviceId;
        private int mBatteryPercent;
        private float mTemperature;
        private float mreadonlyTemperature;
        private string mFW;
        private string mMac;
        private int mRssi;
        private VDIType.TEMPERATURE_STATUS mTemperatureStatus;

        public void SetDeviceId(string deviceId)
        {
            mDeviceId = deviceId;
        }

        public string GetDeviceId()
        {
            return mDeviceId;
        }

        public void SetBatteryPercent(int batteryPercent)
        {
            mBatteryPercent = batteryPercent;
        }

        public int GetBatteryPercent()
        {
            return mBatteryPercent;
        }

        public void SetTemperatureValue(float temperature)
        {
            mTemperature = temperature;
        }

        public float GetTemperatureValue()
        {
            return mTemperature;
        }

        public void SetreadonlyTemperatureValue(float temperature)
        {
            mreadonlyTemperature = temperature;
        }

        public float GetreadonlyTemperatureValue()
        {
            return mreadonlyTemperature;
        }

        public void SetFW(string fw)
        {
            mFW = fw;
        }

        public string GetFW()
        {
            return mFW;
        }

        public void SetMac(string mac)
        {
            mMac = mac;
        }

        public string GetMac()
        {
            return mMac;
        }

        public void SetRSSI(int rssi)
        {
            mRssi = rssi;
        }

        public int GetRSSI()
        {
            return mRssi;
        }

        public void SetTemperatureStatus(VDIType.TEMPERATURE_STATUS status)
        {
            mTemperatureStatus = status;
        }

        public VDIType.TEMPERATURE_STATUS GetTemperatureStatus()
        {
            return mTemperatureStatus;
        }
    }
}