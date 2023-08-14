using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using NaughtyAttributes;

public class FakeJsonData : MonoBehaviour
{
    //string jsonFile ="/FakeData.json";

    public delegate void FakeDataSendDelegate(VehiclesData vehicles);
    public static FakeDataSendDelegate fakeDataSendDelegate;

    [Button]
    public void SendData()
    {
        //string pathJson = Application.persistentDataPath + jsonFile;
        //string lines = System.IO.File.ReadAllText(pathJson);
        //List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(lines);
        TextAsset FakeDataText = Resources.Load("FakeData") as TextAsset;
        string jsonString = FakeDataText.text;
        //Debug.Log(jsonString);
        List<VehiclesData> vehicles = JsonConvert.DeserializeObject<List<VehiclesData>>(jsonString);
        
        foreach(VehiclesData vehicle in vehicles)
        {
            ShowListOfVehicles(vehicle);
        }
    }

    private void ShowListOfVehicles(VehiclesData vehicle)
    {
        VehiclesData newVehicle = new VehiclesData(vehicle.name, vehicle.vehicle, vehicle.positionX, vehicle.positionY,
                vehicle.positionZ, vehicle.rotationX, vehicle.rotationY, vehicle.rotationZ, vehicle.speed, vehicle.alive);
        //Debug.Log(newVehicle.name);
        fakeDataSendDelegate?.Invoke(newVehicle);
    }
}


