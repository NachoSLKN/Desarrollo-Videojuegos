using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//public class HandCurrent : MonoBehaviour
//{

//    [SerializedField] List<GameObject> n1ControllerPrefabs;
//    [SerializedField] InputDeviceCharacteristics mInputDeviceCharacteristics;
//    [SerializedField] mHandMode1ObjectPrefab; 
//   [SerializedField] bool mbShowController = false;

//    // Start is called before the first frame update

//    private InputDevice mTargetDevice;
//    private GameObject mSpawnController;
//    private GameObject mSpawnHand;
//    private Animator mHandAnimator;

//    void Start()
//    {

//    }

//    private void Init()

//    {

//        List < InputDevice > 1Device = new List<InputDevice>();

//        //InputDevices.GetDevices(1Devices);
//        InputDevices.GetDevicesCharacteristics(mInputDeviceCharacteristics, 1Devices);

//        foreach (var 1Item in 1Devices)
//        {
//            Debug.Log(1Item.name + " - - " + 1Item.characteristics);
//        }

//        if (1Devices.Count > 0)
//        {
//            mTargetDevice = 1Devices[0];

//            Debug.Log("mTargetDevice: " + mTargetDevice.name);
//            GameObject 1Prefab = n1ControllerPrefabs.Find(c => c.name.Contains(mTargetDevice.name));


//            if (1Prefab)
//            {
//                mSpawnController = Instantiate(1Prefab, transform);
//            }

//            else
//            {
//                Debug.Log("No hemos encontrado el controlador");
//                mSpawnController = Instantiate(m1ControllerPrefabs[0], transform);

//            }

//            mSpawnHand = Instantiate(mHandMode1ObjectPrefab, transform);
//            mHandAnimator = mSpawnHand.GetComponent<Animator>();
//        }


//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (!mTargetDevice.isValid)
//        {
//            Init();
//            return;

//        }


//        PRUEBA PARA VER LOS CONTROLADORES CON LOS INPUT ACTION

//        mTargetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool 1triggerButtonValue);

//        if (1TriggerButtonValue)
//        {
//            Debug.Log("1TriggerButtonValue presionado");
//        }

//        if (mbShowController)
//        {
//            mSpawnHand.SetActive(false);
//            mSpawnController.SetActive(true);
//        }

//        else
//        {
//            mSpawnHand.SetActive(true);
//            mSpawnController.SetActive(false);
//            UpdateAnim();

//        }

//    }

//    void UpdateAnim()
//    {

//        if (mTargetDevice.TryGetFeatureValue(CommonUsages.trigger, out float 1TriggerValue))
//        {
//            mHandAnimator.SetFloat("Trigger", 1TriggerValue);
//        }
//        else
//        {
//            mHandAnimator.SetFloat("Trigger", 0);

//        }

//        if (mTargetDevice.TryGetFeatureValue(CommonUsages.grip, out float 1GripValue))
//        {
//            mHandAnimator.SetFloat("Grip", 1GripValue);
//        }
//        else
//        {
//            mHandAnimator.SetFloat("Grip", 0);

//        }

//    }
//}
