using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.Initialization;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Queue_system : MonoBehaviour
{
    private List<object> keys = new List<object>() { "vehicles", "trees" };
    //Define the number of gameobject to be generated for object pooling
    [System.Serializable]
    private struct NumberOfVehiclesGenerated
    {
        public string AssetReferenceName;
        public int numberOfVehicle;
    }
    [SerializeField]
    private List<NumberOfVehiclesGenerated> numOfVehiclesGenerated = new List<NumberOfVehiclesGenerated>();

    //Dictionary of a group catergory of vehicle
    private SortedDictionary<string, VehicleGrp> vehiclesGrp = new SortedDictionary<string, VehicleGrp>();
    //Add all addressable vehicle into dictionary for create new vehicle when the group is empty
    private SortedDictionary<string, GameObject> AddressableVehiclesDictionary = new SortedDictionary<string, GameObject>();
    //Add new name of the character from the data and added gameobject to refer
    private SortedDictionary<string, GameObject> vehicleNewNameList = new SortedDictionary<string, GameObject>();
    //Store new name of the character from the data
    private List<string> compareVehicle = new List<string>();
    //List of pending action inside the queue function
    private Queue<ActionData> pendingActions = new Queue<ActionData>();

    //Queue to check whether it is new subject or update subject
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

    [Obsolete]
    void Start()
    {
        //Fake date to be receive from server
        FakeJsonData.fakeDataSendDelegate += ReceiveData;
        foreach(object key in keys)
        {
            ClearDependencyCacheForAddressable(key.ToString());
        }

        StartCoroutine(AsyncLoadPrefab());
    }

    private void OnDestroy()=>FakeJsonData.fakeDataSendDelegate -= ReceiveData;

    public void ClearDependencyCacheForAddressable(string key)
    {
        Addressables.ClearDependencyCacheAsync(key);
    }

    /// <summary>
    /// Addressable to read data from the server
    /// </summary>
    /// <returns></returns>
    [Obsolete]
    private IEnumerator AsyncLoadPrefab()
    {
        AsyncOperationHandle<IList<GameObject>> intersectionWithMultipleKeys =
            Addressables.LoadAssetsAsync<GameObject>(keys,
                obj =>
                {
                    //Gets called for every loaded asset
                    Debug.Log(obj.name);
                }, Addressables.MergeMode.Intersection);
        yield return intersectionWithMultipleKeys;
        IList<GameObject> multipleKeyResult = intersectionWithMultipleKeys.Result;

        foreach (GameObject keyResult in multipleKeyResult)
        {
            AddressableVehiclesDictionary.Add(keyResult.name, keyResult);
            CreateVehiclePooling(keyResult, keyResult.name);
        }

        //Addressables.Release(intersectionWithMultipleKeys);
    }


    /// <summary>
    /// Event that receive the data from the server
    /// </summary>
    /// <param name="vehicle"></param>
    private void ReceiveData(VehiclesData vehicle)
    {
        if(!compareVehicle.Contains(vehicle.name))
        {
            compareVehicle.Add(vehicle.name);
            lock (pendingActions)
            {
                Debug.Log($"ReceiveData queue to Add:  {vehicle.name}");
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, vehicle));
            }
        }
        else if (compareVehicle.Contains(vehicle.name))
        { 
            if(vehicle.alive)
            {
                lock (pendingActions)
                {
                    Debug.Log($"ReceiveData queue to Updated:  {vehicle.name}");
                    pendingActions.Enqueue(new ActionData(ActionData.Type.Updated, vehicle));
                }
            }
            else
            {
                lock (pendingActions)
                {
                    Debug.Log($"ReceiveData queue to Removed:  {vehicle.name}");
                    pendingActions.Enqueue(new ActionData(ActionData.Type.Removed, vehicle));
                }
            }
        }
    }
    /// <summary>
    /// Update check the condition
    /// </summary>
    private void HandleEvents()
    {
        lock (pendingActions)
        {
            while (pendingActions.Count > 0)
            {
                var action = pendingActions.Dequeue();
                if (action.type == ActionData.Type.Added)
                {
                    Debug.Log($"HandleEvents Added action: {action.vehiclesData.name}");
                    if(vehiclesGrp.ContainsKey(action.vehiclesData.vehicle))
                    {
                        int childCount = vehiclesGrp[action.vehiclesData.vehicle].vehicleGrp.transform.childCount;

                        //if the vehicle group has less than one vehicle inside the group, it will auto generate extra vehicle inside the group
                        if(childCount < 2)
                        {
                            if (AddressableVehiclesDictionary.ContainsKey(action.vehiclesData.vehicle))
                            {
                                ReloadAsset(AddressableVehiclesDictionary[action.vehiclesData.vehicle]);
                            }
                        }
                        
                        GetFromVehicleParent(action.vehiclesData);
                    }

                }
                else if (action.type == ActionData.Type.Updated)
                {
                    Debug.Log($"HandleEventsUpdated Action: {action.vehiclesData.name}");
                    UpdateVehicle(action.vehiclesData);

                }
                else if (action.type == ActionData.Type.Removed)
                {
                    if (vehicleNewNameList.ContainsKey(action.vehiclesData.name))
                    {
                        Debug.Log($"HandleEventsUpdated Removed Action: {action.vehiclesData.name}");
                        RemoveSubject(action.vehiclesData);
                    }
                }
            }
        }
    }

    private void ReloadAsset(GameObject gameObject)
    {
        GameObject newAsset = Instantiate(gameObject);
        newAsset.transform.SetParent(vehiclesGrp[gameObject.name].vehicleGrp.transform);
        newAsset.SetActive(false);
    }

    /// <summary>
    /// Get vehicle from the selected group
    /// </summary>
    /// <param name="vehiclesData"></param>
    private void GetFromVehicleParent(VehiclesData vehiclesData)
    {
        Debug.Log($"GetFromVehicleParent: {vehiclesData.name}");
        GameObject vehicleParent = vehiclesGrp[vehiclesData.vehicle].vehicleGrp;
        GameObject childVehicle = vehicleParent.transform.GetChild(0).gameObject;
        childVehicle.transform.SetParent(null);
        childVehicle.name = vehiclesData.name;
        childVehicle.SetActive(true);
        vehicleNewNameList.Add(vehiclesData.name, childVehicle);

    }
    /// <summary>
    /// Get the vehicle movement script to move to target location
    /// </summary>
    /// <param name="vehiclesData"></param>
    private void UpdateVehicle(VehiclesData vehiclesData)
    {
        if (vehicleNewNameList.ContainsKey(vehiclesData.name))
        {
            Vehicle_Movement vehicle_Movement = vehicleNewNameList[vehiclesData.name].gameObject.GetComponent<Vehicle_Movement>();
            Vector3 vector3 = new Vector3(vehiclesData.positionX, vehiclesData.positionY, vehiclesData.positionZ);
            Debug.Log($"UpdateVehicle: {vehiclesData.name}");
            vehicle_Movement.EnqueuePosition(vector3, vehiclesData.speed);
        }
    }
    /// <summary>
    /// Put the vehicle back to the selected group when the vehicle's alive is false
    /// </summary>
    /// <param name="vehiclesData"></param>
    private void RemoveSubject(VehiclesData vehiclesData)
    {
        vehicleNewNameList[vehiclesData.name].transform.SetParent(vehiclesGrp[vehiclesData.vehicle].vehicleGrp.transform);
        vehicleNewNameList[vehiclesData.name].name = vehiclesGrp[vehiclesData.vehicle].name;
        vehicleNewNameList[vehiclesData.name].SetActive(false);
        compareVehicle.Remove(vehiclesData.name);
        vehicleNewNameList.Remove(vehiclesData.name);
    }

    void Update()=> HandleEvents();

    /// <summary>
    /// Create a pool of vehicle inside the group
    /// </summary>
    /// <param name="prefabInstance"></param>
    /// <param name="name"></param>
    private void CreateVehiclePooling(GameObject prefabInstance, string name)
    {
        GameObject parentVehiclesGrp = new GameObject(name);
        VehicleGrp newVehicleGrp = new VehicleGrp(name, parentVehiclesGrp);
        vehiclesGrp.Add(name, newVehicleGrp);
        for (int i = 0; i < numOfVehiclesGenerated.Count; i++)
        {
            if (numOfVehiclesGenerated[i].AssetReferenceName == name)
            {
                for (int j = 0; j < numOfVehiclesGenerated[i].numberOfVehicle; j++)
                {
                    GameObject newGameObj = Instantiate(prefabInstance);
                    newGameObj.transform.SetParent(parentVehiclesGrp.transform);
                    newGameObj.AddComponent<Vehicle_Movement>();
                    newGameObj.SetActive(false);
                }
            }
        }
    }

}
