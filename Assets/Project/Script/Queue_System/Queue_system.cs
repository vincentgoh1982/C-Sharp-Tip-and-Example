using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue_system : MonoBehaviour
{
    private SortedDictionary<string, Vehicles> vehiclesObjectsList;
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
        public Vehicles vehiclesData;

        public ActionData(Type type, Vehicles vehiclesData) : this()
        {
            this.type = type;
            this.vehiclesData = vehiclesData;
        }
    }

    void Start()
    {
        vehiclesObjectsList = new SortedDictionary<string, Vehicles>();
        FakeJsonData.fakeDataSendDelegate += ReceiveData;
    }
    private void OnDestroy()=>FakeJsonData.fakeDataSendDelegate -= ReceiveData;


    private void ReceiveData(Vehicles vehicle)
    {
        if(!vehiclesObjectsList.ContainsKey(vehicle.name))
        {
            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, vehicle));
            }
        }
        else if (vehiclesObjectsList.ContainsKey(vehicle.name))
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
                    vehiclesObjectsList.Add(action.vehiclesData.name, action.vehiclesData);
                    //GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    //qrCodeObject.GetComponent<SpatialGraphNodeTracker>().Id = action.qrCode.SpatialGraphNodeId;
                    //qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                    //qrCodesObjectsList.Add(action.qrCode.Id, qrCodeObject);
                }
                else if (action.type == ActionData.Type.Updated)
                {
                    Debug.Log($"Updated: {action.vehiclesData.name}");
                    //if (!qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                    //{
                    //    GameObject qrCodeObject = Instantiate(qrCodePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    //    qrCodeObject.GetComponent<SpatialGraphNodeTracker>().Id = action.qrCode.SpatialGraphNodeId;
                    //    qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
                    //    qrCodesObjectsList.Add(action.qrCode.Id, qrCodeObject);
                    //}
                }
                else if (action.type == ActionData.Type.Removed)
                {
                    if (vehiclesObjectsList.ContainsKey(action.vehiclesData.name))
                    {
                        Debug.Log($"Removed: {action.vehiclesData.name}");
                        vehiclesObjectsList.Remove(action.vehiclesData.name);
                        //Destroy(qrCodesObjectsList[action.qrCode.Id]);
                        //qrCodesObjectsList.Remove(action.qrCode.Id);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleEvents();
    }
}
