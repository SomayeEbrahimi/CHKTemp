using Android.Content;
using Android.OS;
using Com.Vivalnk.Model;
using Com.Vivalnk.Vdireader;
using Com.Vivalnk.Vdireaderimpl;
using System;

namespace HCSampleApp.Droid.Temprueture
{
    public class BleManager : IVDICommonBleListener
    {
        private static readonly string TAG = "BluetoothManager";

        private static volatile BleManager mInstance = null;

        private IVDICommonBleReader mBleReader = null;
        private Context mContext;

        private WeakReference<Handler> mHandlerRef;

        public void Destroy()
        {
            if (mHandlerRef != null)
                mHandlerRef.SetTarget(null); // ??
            if (mBleReader != null)
                mBleReader.Destroy();

            mHandlerRef = null;
            mBleReader = null;
            mInstance = null;
        }

        public static BleManager GetInstance(Context context)
        {
            if (mInstance == null)
            {
                lock (typeof(BleManager))
                {
                    if (mInstance == null)
                    {
                        mInstance = new BleManager(context);
                    }
                }
            }

            return mInstance;
        }

        private BleManager(Context context)
        {
            mContext = context;

            if (Convert.ToInt32(Build.VERSION.Sdk) >= 21)
                mBleReader = new VDIBleThermometerL(mContext);
            else
                mBleReader = new VDIBleThermometer(mContext);
            mBleReader.SetListener(this);
        }

        public IVDICommonBleReader GetBleReader()
        {
            return mBleReader;
        }

        public void SetHandler(Handler handler)
        {
            if (mHandlerRef == null)
            {
                mHandlerRef = new WeakReference<Handler>(handler);
            }
            else
            {
                mHandlerRef.SetTarget(null);
                mHandlerRef = null;
                mHandlerRef = new WeakReference<Handler>(handler);
            }
        }

        public void OnNewDeviceDiscovered(DeviceInfo info)
        {
            // TODO Auto-generated method stub
            if (mHandlerRef == null)
            {
                //viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            BleDevice bleDevice = new BleDevice();

            bleDevice.SetDeviceId(info.SN);
            bleDevice.SetMac(info.MacAddress);
            bleDevice.SetRSSI(info.RSSI);
            bleDevice.SetDeviceType(info.DeviceType);

            handler.ObtainMessage((int)Enums.MESSAGE_DEVICE_FOUND, bleDevice)
            .SendToTarget();
        }

        public void OnTemperatureUpdated(TemperatureInfo info)
        {
            if (mHandlerRef == null)
            {
                //viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            BleData bleData = new BleData();

            bleData.SetDeviceId(info.SN);
            bleData.SetBatteryPercent(info.PatchBatteryLevel);
            bleData.SetRSSI(info.RSSI);
            bleData.SetTemperatureValue(info.RawTemperature);
            bleData.SetreadonlyTemperatureValue(info.FinalTemperature);
            bleData.SetTemperatureStatus(info.TemperatureStatus);
            bleData.SetFW(info.PatchFW);
            bleData.SetMac(info.MacAddress);

            handler.ObtainMessage((int)Enums.MESSAGE_TEMPERATURE_UPDATE, bleData)
            .SendToTarget();
        }

        public void OnChargerInfoUpdate(ChargerInfo info)
        {
            if (mHandlerRef == null)
            {
                //viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            handler.ObtainMessage((int)Enums.MESSAGE_CHARGER_INFO_UPDATED, info.ToString())
                    .SendToTarget();
        }

        public void OnDeviceLost(string deviceId)
        {
            if (mHandlerRef == null)
            {
                //viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            handler.ObtainMessage((int)Enums.MESSAGE_DEVIE_LOST, deviceId)
            .SendToTarget();
        }

        public void OnTemperatureAbnormalStatusUpdate(string deviceId, VDIType.ABNORMAL_TEMPERATURE_STATUS status)
        {
            if (mHandlerRef == null)
            {
               // viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            string notification = "";

            if (status == VDIType.ABNORMAL_TEMPERATURE_STATUS.LowTemperature)
            {
                notification = deviceId + " low temperature notification,temperature lower than 34.5 Celsius!";
            }

            handler.ObtainMessage((int)Enums.MESSAGE_TEMPERATURE_ABNORAML, notification)
                    .SendToTarget();
        }

        public void OnTemperatureMissed(string deviceId)
        {
            if (mHandlerRef == null)
            {
                //viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            handler.ObtainMessage((int)Enums.MESSAGE_TEMPERATURE_MISSED, deviceId)
                    .SendToTarget();
        }

        public void PhoneBluetoothOff()
        {
            if (mHandlerRef == null)
            {
                //viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            handler.ObtainMessage((int)Enums.MESSAGE_PHONE_BLUETOOTH_OFF)
                    .SendToTarget();
        }

        public void PhoneLocationOff()
        {
            if (mHandlerRef == null)
            {
                //viLog.e(TAG, "mHandlerRef is null.");
                return;
            }

            mHandlerRef.TryGetTarget(out Handler handler);

            if (handler == null)
            {
                //viLog.e(TAG, "handler is null.");
                return;
            }

            handler.ObtainMessage((int)Enums.MESSAGE_PHONE_LOCATION_OFF)
                    .SendToTarget();
        }
    }
}