package com.vivalnk.vdireader;

import com.vivalnk.vdireader.VDIType.CHECKBLE_STATUS_TYPE;
import java.util.ArrayList;


public interface VDICommonBleReader {

  /**
   * Check the phone's Bluetooth setting
   */
  public CHECKBLE_STATUS_TYPE checkBle();

  /**
   * Set the ble listener for ble information update.
   *
   * @param listener, refer to {@link VDICommonBleListener}.
   */
  public void setListener(VDICommonBleListener listener);

  /**
   * Set the pairing rssi value for device discovery
   */
  public void setPairingRssi(int rssi);

  /**
   * @return the current sdk pairing rssi value
   */
  public int getPairingRssi();

  /**
   * Start to search nearby ble devices
   */
  public void startDeviceDiscovery();

  /**
   * Stop tp find new nle devices
   */
  public void stopDeviceDiscovery();

  /**
   * Found device must be added to paired device list, then this device's data would be able to be
   * collected
   *
   * @return true if adding operation successfully
   */
  public boolean addPDList(String deviceId);

  /**
   * Found device must be added to paired device list, then this device's data would be able to be collected
   * @param deviceId
   * @password the device's password
   * @return true if adding operation successfully
   */
  public boolean addPDList(String deviceId, String password);

  /**
   * Remove the device from paired device list,sdk wouldn't collect the device's data any more
   */
  public boolean removePDList(String deviceId);

  /**
   * Clear the current paired device list
   */
  public void purgePDList();

  /**
   * @return current paired devcie list length
   */
  public int getPDListLength();

  /**
   * @return the current paired device list
   */
  public ArrayList<String> iteratePDList();

  /**
   * Start to collect the paired device data
   */
  public void startTemperatureUpdate();

  /**
   * Stop to collect the paired device data
   */
  public void stopTemperatureUpdate();

  /**
   * Once the paired device lost continues data count more than the sdk lost threshold, sdk would
   * notify {@link VDICommonBleListener  onDeiceLost(String deviceId)} to tell the app layer. SDK
   * will remove the lost device from paired device list and stop temperature update. App need to add
   * the patch to paired device list again if it wants to receive the patch info again. Every time's
   * lost means lost 16 seconds,so the lost threshold time = 16s * count. This feature only works
   * in foreground (screen on). The default value is 0  which means this feature disabled.
   */
  public void setLostThreshold(int count);

  /**
   * @return the current sdk lost threshold
   */
  public int getLostThreshold();

  /**
   * Call this function when activity onPause
   */

  public void suspend();

  /**
   * Call this function when activity onResume
   */
  public void resume();

  /**
   * Release the sdk resources
   */
  public void destroy();

 // public void updateEncryptedDeviceType(ENCRYPTED_DEVICE_TYPE type);

}