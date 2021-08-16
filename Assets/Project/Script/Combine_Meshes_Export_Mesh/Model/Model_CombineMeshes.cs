using System.Collections.Generic;
using UnityEngine;

public class Model_CombineMeshes : CombineMeshesElements
{
    public Material[] buildingMaterials; //Building's material

    public float previousZoom;
    public float previousScale;

    public string markerName = "CombinedBuildingsMarker";

    public string combinedBuildingsName = "CombinedBuildings";//Combined Building's name
    
    public float timeBeforeUpdateBuildings = 0;
    
    public float minWaitTime = 5.0f;
    
    public bool isPanning = false;
    
    public bool isToggled = true;
    
    public bool isComplete = false;

    public OnlineMapsBuildings onlineMapsBuildings;

    public OnlineMapsMarker3D currentBuildingsMarker;

    public bool IsComplete => isComplete;

    public bool meshToMarker = true;
}
