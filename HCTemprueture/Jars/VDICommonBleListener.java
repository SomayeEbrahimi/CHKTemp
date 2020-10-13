package com.vivalnk.vdireader;


import com.vivalnk.model.ChargerInfo;
import com.vivalnk.model.DeviceInfo;
import com.vivalnk.model.TemperatureInfo;

/**
 * Interface for reading task listener.
 *
 * @author vivalnk
 */
public interface VDICommonBleListener {


  /**
   * Found new device
   * @param deviceInfo, device's info object, contains device's sn, mac, deviceType(encryted or not),
   * rssi
   */
  void onNewDeviceDiscovered(DeviceInfo deviceInfo);


  /**
   * New temperature signal detected
   * @param tempInfo, temperature info object, contains device's sn, mac, patch firmware,rssi,patch
   * battery, raw temperature, final temperature, temperature status
   */
  void onTemperatureUpdated(TemperatureInfo tempInfo);


  /**
   *
   * @param chargerInfo, charger's info object, contains device's sn, charger mac, charger fw, charger
   * battery status, rssi
   */
  void onChargerInfoUpdate(ChargerInfo chargerInfo);

  /**
   * Once the paired device's continuous temperature loss reach the lost threshold ,refer to {@link
   * VDICommonBleReader {setLostThreshold}}
   *
   * @param deviceId, device's unique serial number
   */
  public void onDeviceLost(String deviceId);


  /**
   * The SDK triggers below function every time when a temperature update is expected but fails to
   * receive.
   *
   * @param deviceId, device's unique serial number
   */
  public void onTemperatureMissed(String deviceId);


  /**
   * The SDK triggers below function every time user turn off the phone's bluetooth
   */
  public void phoneBluetoothOff();

  /**
   * The SDK triggers below function every time user turn off the phone's location(GPS)
   */
  public void phoneLocationOff();


  /**
   * The SDK triggers below function when meets abnormal temperature status
   *
   * @param deviceId, device's unique serial number
   * @param status, device's abnormal temperature satatus,refer to {$link
   * VDIType.ABNORMAL_TEMPERATURE_STATUS}
   */
  public void onTemperatureAbnormalStatusUpdate(String deviceId,
      VDIType.ABNORMAL_TEMPERATURE_STATUS status);


}
