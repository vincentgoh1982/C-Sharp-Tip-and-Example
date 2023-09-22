using NaughtyAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateJsonFile : MonoBehaviour
{

    public List<Dog> Dogs = new List<Dog>();
    public List<Bird> Birds = new List<Bird>();

    /// <summary>
    /// Generate Dog's information and Bird's information
    /// </summary>
    [Button]
    public void GenerateJsonIntoString()
    {
        Dog[] allDogssArray = new Dog[Dogs.Count];
        Bird[] allBirdsArray = new Bird[Birds.Count];
        
        //Convert all the dog's information into an array
        for(int i = 0; i < Dogs.Count; i++)
        {
            string name = Dogs[i].name;
            string movementType = Dogs[i].movementType;
            float speed = Dogs[i].speed;
            Dog NewDog = new Dog(name, speed, movementType);
            allDogssArray[i] = NewDog;
        }

        //Convert all the bird's information into an array
        for (int i = 0; i < Birds.Count; i++)
        {
            string name = Birds[i].name;
            string movementType = Birds[i].movementType;
            float speed = Birds[i].speed;
            float altitude = Birds[i].altitude;
            Bird NewBird = new Bird(name, speed, movementType, altitude);
            allBirdsArray[i] = NewBird;
        }

        string dog_json = JsonConvert.SerializeObject(allDogssArray);
        string bird_json = JsonConvert.SerializeObject(allBirdsArray);

        ConvertTypeJsonString(dog_json, "Dog");
        ConvertTypeJsonString(bird_json, "Bird");
    }
    /// <summary>
    /// Combine the dog's information and bird's information into animal's information
    /// </summary>
    /// <param name="dog_json"></param>
    /// <param name="animalType"></param>
    private void ConvertTypeJsonString(string dog_json, string animalType)
    {
        Model_Animal model_Animal = new Model_Animal(animalType, dog_json);
        string animal_json = JsonConvert.SerializeObject(model_Animal);
        string jsonString = animalType + "_json";
        SaveIntoFile(animal_json, jsonString);
    }
    /// <summary>
    /// Save the information into file
    /// </summary>
    /// <param name="string_json"></param>
    /// <param name="animal"></param>
    private void SaveIntoFile(string string_json, string animal)
    {
        string path = $"Assets/Resources/{animal}.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(string_json);
        writer.Close();
        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
    }
}
