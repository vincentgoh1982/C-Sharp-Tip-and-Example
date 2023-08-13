using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles
{
    public string name;
    public string vehicle;
    public float positionX;
    public float positionY;
    public float positionZ;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public bool alive;

    public Vehicles(string _name, string _vehicle, float _positionX, float _positionY, float _positionZ, float _rotationX, float _rotationY, float _rotationZ, bool _alive)
    {
        name = _name;
        vehicle = _vehicle;
        positionX = _positionX;
        positionY = _positionY;
        positionZ = _positionZ;
        rotationX = _rotationX;
        rotationY = _rotationY;
        rotationZ = _rotationZ;
        alive = _alive;
    }

}
