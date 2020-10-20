using System;
using Android.App;
using Android.Content.PM;
using Com.Vivalnk.Vdireader;
using Com.Vivalnk.Vdireaderimpl;
using Android.OS;
using Acr.UserDialogs;
using HCSampleApp.Droid.Temprueture;
using System.IO;
using System.Text;

namespace HCSampleApp.Droid
{
    [Activity(Label = "HCSampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static IVDICommonBleReader BleReader;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            UserDialogs.Init(this);

            // Init temprueture sdk
            var sdk = Convert.ToInt32(Build.VERSION.Sdk);

            if (sdk >= 21)
            {
                BleReader = new VDIBleThermometerL(this);
            }
            else
            {
                BleReader = new VDIBleThermometer(this);
            }

            BleReader.SetListener(new BleManager(this));

            TempruetureHandler tempHandler = new TempruetureHandler();

            tempHandler.ObtainMessage();
        }

        public class TempruetureHandler : Handler
        {
            public override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);

                GetData(msg);
            }

            void GetData(Message msg)
            {
                var userDialogs = UserDialogs.Instance;

                string response = (string)msg.Obj;

                switch (msg.What)
                {
                    case (int)Enums.MESSAGE_DEVICE_FOUND:
                        BleReader.StopDeviceDiscovery();
                        BleReader.AddPDList(response);

                        userDialogs.Toast($"Device({response}) successfully paired :)");
                        break;

                    case (int)Enums.MESSAGE_TEMPERATURE_UPDATE:
                        GetDisplayTemperatureResultString((BleData)msg.Obj);
                        break;

                    case (int)Enums.MESSAGE_CHARGER_INFO_UPDATED:
                        if (response.Contains("low"))
                        {
                            userDialogs.Toast("Charger low battery!");
                        }
                        break;

                    case (int)Enums.MESSAGE_TEMPERATURE_MISSED:
                        userDialogs.Toast("Temperature missed!");
                        break;

                    case (int)Enums.MESSAGE_DEVIE_LOST:
                        userDialogs.Toast("Device lost!");
                        BleReader.PurgePDList(); // Clear the current paired device list, it actually remove all founded devices
                        break;

                    case (int)Enums.MESSAGE_TEMPERATURE_ABNORAML:
                        userDialogs.Toast("Abnormal low temperature, please set the patch in right place");
                        break;

                    case (int)Enums.MESSAGE_PHONE_BLUETOOTH_OFF:
                        BleReader.StopTemperatureUpdate();
                        userDialogs.Toast("Phone bluetooth off");
                        break;

                    case (int)Enums.MESSAGE_PHONE_LOCATION_OFF:
                        BleReader.StopTemperatureUpdate();
                        userDialogs.Toast("Phone location off");
                        break;
                }
            }

            string GetDisplayTemperatureResultString(BleData data)
            {
                string deviceId = data.GetDeviceId();
                int batteryPercent = data.GetBatteryPercent();
                float temperature = data.GetTemperatureValue();
                float finalTemperature = data.GetreadonlyTemperatureValue();

                VDIType.TEMPERATURE_STATUS status = data.GetTemperatureStatus();

                string statusString = "";

                if (status == VDIType.TEMPERATURE_STATUS.Normal)
                {
                    statusString = "temperatureNormal";
                }
                else if (status == VDIType.TEMPERATURE_STATUS.Warmup)
                {
                    statusString = "temperatureWarmUp";
                }

                StringBuilder result = new StringBuilder("deviceId");
                result.Append(" ").Append(deviceId);

                string currentTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                LogData(deviceId + ", " + currentTime +
                        ", raw temperature " + temperature + ", final temperature " + finalTemperature +
                        ", " + statusString + ", battery " + batteryPercent + "%" + ", FW " + data.GetFW() +
                        ", mac " + data.GetMac() + ", rssi " + data.GetRSSI());

                result.Append("  ").Append(finalTemperature).Append("  ").Append(batteryPercent).Append("%").Append("\r\n");

                return result.ToString();
            }

            void LogData(string content)
            {
                string rootPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                string path = Path.Combine(rootPath, "temprueture.txt");

                using var writer = new StreamWriter(path, append: true);

                writer.WriteLine($"Temperature : {content}");
            }
        }
    }
}