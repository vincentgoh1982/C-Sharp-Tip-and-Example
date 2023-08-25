using UnityEngine;

public class VehiclesData
{
    public string name;
    public string vehicle;
    public float positionX;
    public float positionY;
    public float positionZ;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float speed;
    public bool alive;

    public VehiclesData(string _name, string _vehicle, float _positionX, float _positionY, float _positionZ, float _rotationX, float _rotationY, float _rotationZ, float _speed, bool _alive)
    {
        name = _name;
        vehicle = _vehicle;
        positionX = _positionX;
        positionY = _positionY;
        positionZ = _positionZ;
        rotationX = _rotationX;
        rotationY = _rotationY;
        rotationZ = _rotationZ;
        speed = _speed;
        alive = _alive;
    }
}

public class VehicleGrp
{
    public string name;
    public GameObject vehicleGrp;
    public int numberOfVehicles;

    public VehicleGrp(string _name, GameObject _vehicleGrp, int _numberOfVehicles)
    {
        name = _name;
        vehicleGrp = _vehicleGrp;
        numberOfVehicles = _numberOfVehicles;
    }
}




