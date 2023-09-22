using NaughtyAttributes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadJsonFile : MonoBehaviour
{
    public TextAsset DogInformation;
    public TextAsset BirdInformation;
    /// <summary>
    /// Read all the information from json and get the correct type of animal's json information
    /// </summary>
    [Button]
    public void ReadJsonFiles()
    {
        
        Model_Animal model_Animal_Dog = JsonConvert.DeserializeObject<Model_Animal>(DogInformation.ToString());
        if (model_Animal_Dog.typeOfAnimal == "Dog")
        {
            List<Dog> dogs = JsonConvert.DeserializeObject<List<Dog>>(model_Animal_Dog.jsonString);
            foreach(Dog dog in dogs)
            {
                Debug.Log($" name: {dog.name}, speed: {dog.speed}, movementType: {dog.movementType}");
            }
        }

        Model_Animal model_Animal_Bird = JsonConvert.DeserializeObject<Model_Animal>(BirdInformation.ToString());
        if (model_Animal_Bird.typeOfAnimal == "Bird")
        {
            List<Bird> birds = JsonConvert.DeserializeObject<List<Bird>>(model_Animal_Bird.jsonString);
            foreach (Bird bird in birds)
            {
                Debug.Log($" name: {bird.name}, speed: {bird.speed}, movementType: {bird.movementType}, altitude: {bird.altitude}");
            }
        }
    }
}
