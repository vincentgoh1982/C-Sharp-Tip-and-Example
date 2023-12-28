using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using static System.Net.WebRequestMethods;
using Newtonsoft;
using Newtonsoft.Json;
using NaughtyAttributes;
namespace AddressableAmendment
{
    public class ChangeAddressablesIPAddress : MonoBehaviour
    {
        public static string BASE_URL = "http://172.16.1.81:8088/Vehicles";
        private string filePath;

        static void ChangeStaticStringValue(string newValue)
        {
            BASE_URL = newValue;
        }

        private void Awake()
        {
            string folderName = "ipAddressForAddressables"; // Replace with the folder name you want to check

            // Combine the persistent data path with the folder name
            string folderPath = Path.Combine(Application.persistentDataPath, folderName);

            CheckFolderExist(folderPath);

            filePath = folderPath +"/" + folderName + "addressableAmend.json";

            CheckFileExist(filePath);
        }

        private void CheckFileExist(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                // Read the contents of the file
                string jsonContent = System.IO.File.ReadAllText(filePath);

                // Now you have the JSON content in a string
                Debug.Log("File content:\n" + jsonContent);

                // If you want to parse the JSON, you can use a library like JsonUtility or Newtonsoft.Json
                // Example using Unity's JsonUtility
                IpAddressAddressables dataObject = JsonConvert.DeserializeObject<IpAddressAddressables>(jsonContent);

                // Now you can use the dataObject in your script
                Debug.Log("Parsed JSON data:\n" + dataObject.ToString());
                ChangeStaticStringValue(dataObject.ipAddressForAddressables);
            }
            else
            {
                IpAddressAddressables dataObject = new IpAddressAddressables(BASE_URL);
                string stringObject = JsonConvert.SerializeObject(dataObject);
                System.IO.File.WriteAllText(filePath, stringObject);
            }
        }

        private void CheckFolderExist(string folderPath)
        {

            // Check if the folder exists
            if (Directory.Exists(folderPath))
            {
                Debug.Log("Folder exists: " + folderPath);

                // If the folder exists, you can perform additional actions here
            }
            else
            {
                Debug.Log("Folder does not exist: " + folderPath);

                // If the folder does not exist, you can create it
                Directory.CreateDirectory(folderPath);
                Debug.Log("Folder created: " + folderPath);
            }
        }
    }
}

[Serializable]
public class IpAddressAddressables
{
    public string ipAddressForAddressables;

    public IpAddressAddressables(string _ipAddressForAddressables)
    {
        ipAddressForAddressables = _ipAddressForAddressables;
    }
}