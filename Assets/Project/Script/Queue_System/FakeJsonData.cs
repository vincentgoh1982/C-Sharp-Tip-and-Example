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

    public delegate void FakeDataSendDelegate(Vehicles vehicles);
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
        List<Vehicles> vehicles = JsonConvert.DeserializeObject<List<Vehicles>>(jsonString);
        
        foreach(Vehicles vehicle in vehicles)
        {
            ShowListOfVehicles(vehicle);
        }
    }

    private void ShowListOfVehicles(Vehicles vehicle)
    {
        Vehicles newVehicle = new Vehicles(vehicle.name, vehicle.vehicle, vehicle.positionX, vehicle.positionY,
                vehicle.positionZ, vehicle.rotationX, vehicle.rotationY, vehicle.rotationZ, vehicle.alive);
        //Debug.Log(newVehicle.name);
        fakeDataSendDelegate?.Invoke(newVehicle);
    }
}


