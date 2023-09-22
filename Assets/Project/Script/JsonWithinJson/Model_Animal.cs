using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Animal
{
    public string typeOfAnimal;
    public string jsonString;

    public Model_Animal(string _typeOfAnimal, string _jsonString)
    {
        typeOfAnimal = _typeOfAnimal;
        jsonString = _jsonString;
    }
}
[Serializable]
public class Dog
{
    public string name;
    public float speed;
    public string movementType;

    public Dog(string _name, float _speed, string _movementType)
    {
        name = _name;
        speed = _speed;
        movementType = _movementType;
    }
}
[Serializable]
public class Bird
{
    public string name;
    public float speed;
    public string movementType;
    public float altitude;

    public Bird(string _name, float _speed, string _movementType, float _altitude)
    {
        name = _name;
        speed = _speed;
        movementType = _movementType;
        altitude = _altitude;
    }
}
