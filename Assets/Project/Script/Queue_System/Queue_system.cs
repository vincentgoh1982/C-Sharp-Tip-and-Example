using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Queue_system : MonoBehaviour
{
    private SortedDictionary<string, GameObject> existingVehicleList = new SortedDictionary<string, GameObject>();
    private SortedDictionary<string, VehiclesData> addedVehicleList = new SortedDictionary<string, VehiclesData>();

    private GameObject parentVehiclesGrp;
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
        parentVehiclesGrp = new GameObject("VehiclesGrp");
        for (int i = 0; i < assetReferences.Count; i++ )
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
            Debug.Log($"HandleVehicleLoadComplete: {newGameobj.name}");
            existingVehicleList.Add(newGameobj.name, newGameobj);
        }
        else
        {
            Debug.LogError("Failed to load gameobject: " + handle.OperationException);
        }
    }

    private void ReceiveData(VehiclesData vehicle)
    {
        if(!addedVehicleList.ContainsKey(vehicle.name))
        {
            lock (pendingActions)
            {
                addedVehicleList.Add(vehicle.name, vehicle);
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, vehicle));
            }
        }
        else if (addedVehicleList.ContainsKey(vehicle.name))
        { 
            if(vehicle.alive)
            {
                lock (pendingActions)
                {
                    pendingActions.Enqueue(new ActionData(ActionData.Type.Updated, vehicle));
                }
            }
            else
            {
                lock (pendingActions)
                {
                    pendingActions.Enqueue(new ActionData(ActionData.Type.Removed, vehicle));
                }
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
            Debug.Log($"UpdateVehicle: {vehiclesData.name}");
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
            GameObject newVehicle = Instantiate(existingVehicleList[vehicle.vehicle], new Vector3(vehicle.positionX, vehicle.positionY, vehicle.positionZ), Quaternion.Euler(vehicle.rotationX, 
                vehicle.rotationY, vehicle.rotationZ));
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
