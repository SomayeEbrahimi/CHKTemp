using Acr.UserDialogs;
using Com.Vivalnk.Vdireader;
using HCSampleApp.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(HCSampleApp.Droid.Temprueture.TempruetureManger))]

namespace HCSampleApp.Droid.Temprueture
{
    public class TempruetureManger : ITempruetureManger
    {
        public void StartPairing()
        {
            var userDialogs = UserDialogs.Instance;

            var bluetoothStatus = MainActivity.BleReader.CheckBle();

            if (bluetoothStatus == VDIType.CHECKBLE_STATUS_TYPE.SystemBleNotEnabled)
            {
                userDialogs.Toast("Bluetooth not enabled!");
            }
            else if (bluetoothStatus == VDIType.CHECKBLE_STATUS_TYPE.SystemLocationNotEnabled)
            {
                userDialogs.Toast("Location not enabled!");
            }
            else if (bluetoothStatus == VDIType.CHECKBLE_STATUS_TYPE.SystemNotSupportBle)
            {
                userDialogs.Toast("Bluetooth not supported!");
            }
            else if (bluetoothStatus == VDIType.CHECKBLE_STATUS_TYPE.ResultOk)
            {
                MainActivity.BleReader.StartDeviceDiscovery();
            }
        }

        public void StopPairing()
        {
            MainActivity.BleReader.StopDeviceDiscovery();
        }

        public void StartMeasuring()
        {
            MainActivity.BleReader.StartTemperatureUpdate();
        }

        public void StopMeasuring()
        {
            MainActivity.BleReader.StopTemperatureUpdate();
        }
    }
}