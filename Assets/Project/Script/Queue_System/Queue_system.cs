using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Queue_system : MonoBehaviour
{
    private SortedDictionary<string, GameObject> existingVehicleList = new SortedDictionary<string, GameObject>();
    
    private Queue<ActionData> pendingActions = new Queue<ActionData>();

    public List<AssetReference> assetReferences = new List<AssetReference>();

    struct ActionData
    {
        public enum Type
        {
            Added,
            Updated,
            Removed
        };
        public Type type;
        public VehiclesData vehiclesData;

        public ActionData(Type type, VehiclesData vehiclesData) : this()
        {
            this.type = type;
            this.vehiclesData = vehiclesData;
        }
    }

    void Start()
    {
        FakeJsonData.fakeDataSendDelegate += ReceiveData;
        for(int i = 0; i < assetReferences.Count; i++ )
        {
            SetAssetInsideScene(i);
        }
    }
    private void OnDestroy()=>FakeJsonData.fakeDataSendDelegate -= ReceiveData;

    public void SetAssetInsideScene(int vehiclesIndex)
    {
        AsyncOperationHandle<GameObject> vehicleHandle = assetReferences[vehiclesIndex].LoadAssetAsync<GameObject>();
        vehicleHandle.Completed += HandleVehicleLoadComplete;
    }

    private void HandleVehicleLoadComplete(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject newGameobj = handle.Result;
            existingVehicleList.Add(newGameobj.name, newGameobj);
        }
        else
        {
            Debug.LogError("Failed to load gameobject: " + handle.OperationException);
        }
    }

    private void ReceiveData(VehiclesData vehicle)
    {
        if(!existingVehicleList.ContainsKey(vehicle.name))
        {
            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, vehicle));
            }
        }
        else if (existingVehicleList.ContainsKey(vehicle.name))
        { 
            if(vehicle.alive)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Updated, vehicle));
            }
            else
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Removed, vehicle));
            }
        }
    }

    private void HandleEvents()
    {
        lock (pendingActions)
        {
            while (pendingActions.Count > 0)
            {
                var action = pendingActions.Dequeue();
                if (action.type == ActionData.Type.Added)
                {
                    Debug.Log($"Added: {action.vehiclesData.name}");
                    AddNewSubject(action.vehiclesData);
                    //GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);

                }
                else if (action.type == ActionData.Type.Updated)
                {
                    Debug.Log($"Updated: {action.vehiclesData.name}");
                    UpdateVehicle(action.vehiclesData);

                }
                else if (action.type == ActionData.Type.Removed)
                {
                    if (existingVehicleList.ContainsKey(action.vehiclesData.name))
                    {
                        Debug.Log($"Removed: {action.vehiclesData.name}");
                        RemoveSubject(action.vehiclesData.name);
                        //Destroy(qrCodesObjectsList[action.qrCode.Id]);
                        //qrCodesObjectsList.Remove(action.qrCode.Id);
                    }
                }
            }
        }
    }

    private void UpdateVehicle(VehiclesData vehiclesData)
    {
        if (existingVehicleList.ContainsKey(vehiclesData.name))
        {
            Vehicle_Movement vehicle_Movement = existingVehicleList[vehiclesData.name].GetComponent<Vehicle_Movement>();
            Vector3 vector3 = new Vector3(vehiclesData.positionX, vehiclesData.positionY, vehiclesData.positionZ);
            vehicle_Movement.AddNewPosition(vector3, vehiclesData.speed);
        }
    }

    private void RemoveSubject(string name)
    {
        existingVehicleList.Remove(name);
    }

    private void AddNewSubject(VehiclesData vehicle)
    {
        if(existingVehicleList.ContainsKey(vehicle.vehicle))
        {
            GameObject newVehicle = Instantiate(existingVehicleList[vehicle.vehicle], new Vector3(0, 0, 0), Quaternion.identity);
            existingVehicleList.Add(vehicle.name, newVehicle);
            newVehicle.AddComponent<Vehicle_Movement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleEvents();
    }
}
