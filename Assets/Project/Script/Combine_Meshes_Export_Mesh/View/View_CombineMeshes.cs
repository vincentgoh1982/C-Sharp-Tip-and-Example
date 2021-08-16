using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class View_CombineMeshes : CombineMeshesElements
{
    // Initialize the container gameObject for each combined mesh
    public GameObject CombinedBuildingsObj(Material mat)
    {
        GameObject buildingMeshes = new GameObject($"[{mat}] {app.model.combinedBuildingsName}");
        Mesh mesh = new Mesh();
        buildingMeshes.AddComponent<MeshFilter>().mesh = mesh;
        mesh.indexFormat = IndexFormat.UInt32;
        MeshRenderer meshRenderer = buildingMeshes.AddComponent<MeshRenderer>();
        meshRenderer.material = mat;

        return buildingMeshes;
    }

    public List<GameObject> combinedBuildingObjects = new List<GameObject>();

    public OnlineMaps _map; //Get online map
    public OnlineMaps map
    {
        get
        {
            if (_map == null)
                GameObject.FindObjectOfType<OnlineMaps>();
            return _map;
        }
    }

    public OnlineMapsBuildings _onlineMapsBuildings; //Get Onlinemapsbuildings
    public OnlineMapsBuildings onlineMapsBuildings
    {
        get
        {
            if (_onlineMapsBuildings == null)
                GameObject.FindObjectOfType<OnlineMapsBuildings>();
            return _onlineMapsBuildings;
        }
    }

    public GameObject _buildingsParent;
    public GameObject buildingsParent
    {
        get
        {
            if (_buildingsParent == null) _buildingsParent = map.transform.Find("Buildings").gameObject;
            return _buildingsParent;
        }
    }
}
