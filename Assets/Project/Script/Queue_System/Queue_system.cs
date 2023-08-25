using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Queue_system : MonoBehaviour
{
    [SerializeField] 
    private List<AssetReference> prefabReferences = new List<AssetReference>();

    //Dictionary of a group catergory of vehicle
    private SortedDictionary<string, VehicleGrp> vehiclesGrp = new SortedDictionary<string, VehicleGrp>();
    //Add all addressable vehicle into dictionary for create new vehicle when the group is empty
    private SortedDictionary<string, AssetReference> AddressableVehiclesDictionary = new SortedDictionary<string, AssetReference>();
    //Add new name of the character from the data
    private SortedDictionary<string, GameObject> ExistingVehiclesList = new SortedDictionary<string, GameObject>();

    private List<string> compareVehicle = new List<string>();

    private Queue<ActionData> pendingActions = new Queue<ActionData>();

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
        
        LoadPrefabsAsync();
    }

    private void OnDestroy()=>FakeJsonData.fakeDataSendDelegate -= ReceiveData;

    private void LoadPrefabsAsync()
    {
        foreach (AssetReference prefabReference in prefabReferences)
        {
            AsyncOperationHandle<GameObject> handle = prefabReference.LoadAssetAsync<GameObject>();
            handle.Completed += (completedHandle) => Handle_LoadCompleted(completedHandle, prefabReference);
        }
    }

    private void Handle_LoadCompleted(AsyncOperationHandle<GameObject> handle, AssetReference prefabReference)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefabInstance = handle.Result;
            AddressableVehiclesDictionary.Add(prefabReference.Asset.name, prefabReference);
            // Now you can use the loaded prefabInstance in your scene.
            Debug.Log($"Loaded prefab:  {prefabReference.RuntimeKey} , {prefabReference.Asset.name}");
            CreateVehiclePooling(prefabInstance, prefabReference.Asset.name);
        }
        else
        {
            Debug.LogError("Failed to load prefab: " + prefabReference.RuntimeKey + ". Error: " + handle.OperationException);
        }
    }

    private void ReceiveData(VehiclesData vehicle)
    {
        if(!compareVehicle.Contains(vehicle.name))
        {
            compareVehicle.Add(vehicle.name);
            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, vehicle));
            }
        }
        else if (compareVehicle.Contains(vehicle.name))
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
                    if(vehiclesGrp.ContainsKey(action.vehiclesData.vehicle))
                    {
                        int childCount = vehiclesGrp[action.vehiclesData.vehicle].vehicleGrp.transform.childCount;

                        if(childCount < 2)
                        {
                            if (AddressableVehiclesDictionary.ContainsKey(action.vehiclesData.vehicle))
                            {
                                ReloadAsset(AddressableVehiclesDictionary[action.vehiclesData.vehicle]);
                            }
                        }

                        GetFromVehicleParent(action.vehiclesData);

                    }
                    
                    //GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);

                }
                else if (action.type == ActionData.Type.Updated)
                {
                    Debug.Log($"Updated: {action.vehiclesData.name}");
                    UpdateVehicle(action.vehiclesData);

                }
                else if (action.type == ActionData.Type.Removed)
                {
                    if (ExistingVehiclesList.ContainsKey(action.vehiclesData.name))
                    {
                        Debug.Log($"Removed: {action.vehiclesData.name}");
                        RemoveSubject(action.vehiclesData.name);
                    }
                }
            }
        }
    }

    private void GetFromVehicleParent(VehiclesData vehiclesData)
    {
        Debug.Log($"GetFromVehicleParent: {vehiclesData.name}");
        GameObject vehicleParent = vehiclesGrp[vehiclesData.vehicle].vehicleGrp;
        GameObject childVehicle = vehicleParent.transform.GetChild(0).gameObject;
        childVehicle.transform.SetParent(null);
        childVehicle.name = vehiclesData.name;
        childVehicle.SetActive(true);
        ExistingVehiclesList.Add(vehiclesData.name, childVehicle);

    }

    private void UpdateVehicle(VehiclesData vehiclesData)
    {
        if (ExistingVehiclesList.ContainsKey(vehiclesData.name))
        {
            Vehicle_Movement vehicle_Movement = ExistingVehiclesList[vehiclesData.name].gameObject.GetComponent<Vehicle_Movement>();
            Vector3 vector3 = new Vector3(vehiclesData.positionX, vehiclesData.positionY, vehiclesData.positionZ);
            Debug.Log($"UpdateVehicle: {vehiclesData.name}");
            vehicle_Movement.AddNewPosition(vector3, vehiclesData.speed);
        }
    }

    private void RemoveSubject(string name)
    {
        compareVehicle.Remove(name);
        ExistingVehiclesList.Remove(name);
    }

    // Update is called once per frame
    void Update()
    {
        HandleEvents();
    }

    private void CreateVehiclePooling(GameObject prefabInstance, string name)
    {
        GameObject parentVehiclesGrp = new GameObject(name);
        VehicleGrp newVehicleGrp = new VehicleGrp(name, parentVehiclesGrp, 2);
        vehiclesGrp.Add(name, newVehicleGrp);

        for (int j = 0; j < newVehicleGrp.numberOfVehicles; j++)
        {
            GameObject newGameObj = Instantiate(prefabInstance);
            newGameObj.transform.SetParent(parentVehiclesGrp.transform);
            newGameObj.AddComponent<Vehicle_Movement>();
            newGameObj.SetActive(false);
        }
    }

    private void ReloadAsset(AssetReference assetReference)
    {
        AsyncOperationHandle<GameObject> handle = assetReference.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_ReloadCompleted;
    }

    private void Handle_ReloadCompleted(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject reloadedAsset = handle.Result;
            Debug.Log($"Handle_ReloadCompleted: {reloadedAsset.name}");
            GameObject newAsset = Instantiate(reloadedAsset);
            newAsset.transform.SetParent(vehiclesGrp[reloadedAsset.name].vehicleGrp.transform);

            // Do something with the reloaded asset.
        }
    }

}
