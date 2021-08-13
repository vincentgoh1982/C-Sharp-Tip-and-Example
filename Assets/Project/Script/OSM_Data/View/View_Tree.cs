using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_Tree : TreeElement
{
    public GameObject markerTreePrefab; //Tree marker game object
    
    private GameObject objContainer = null; //Create Tree empty gameobject to contain all trees
    public GameObject getContainer()
    {
        if (objContainer == null)
            createContainer();
        return objContainer;
    }
    public GameObject createContainer()
    {
        string containerName = "TreeGrps";

        if (objContainer == null)
        {
            // Create an empty container in the partent to store entities
            objContainer = new GameObject();
            objContainer.name = containerName;
            objContainer.transform.parent = transform;

            // Reset the position, rotation and scale of the container
            objContainer.transform.localPosition = Vector3.zero;
            objContainer.transform.localEulerAngles = Vector3.zero;
            objContainer.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        return objContainer;
    }

    public OnlineMaps _map; //Get online map
    public OnlineMaps map
    {
        get
        {
            if (_map == null)
                _map = GameObject.FindObjectOfType<OnlineMaps>();//replace with config setting 
            return _map;
        }
    }

    public OnlineMapsBuildings _onlineMapsBuildings; //Get Onlinemapsbuildings
    public OnlineMapsBuildings onlineMapsBuildings
    {
        get
        {
            if (_onlineMapsBuildings == null)
                _onlineMapsBuildings = GameObject.FindObjectOfType<OnlineMapsBuildings>();//replace with config setting 
            return _onlineMapsBuildings;
        }
    }
}
