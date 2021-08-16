using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_CombineMeshes : CombineMeshesElements
{
    private void Start()
    {
        app.model.onlineMapsBuildings.OnAllBuildingsCreated += ResetWait;
        app.view.map.OnChangeZoom += ScaleBuildings;
        app.view.map.OnChangePosition += StopAllActions;
    }

    [Button]
    public void ButtonCombineMesh()//Button to combine all building's mesh together
    {
        UpdateMarker();
    }

    private void OnDestroy()
    {
        app.model.onlineMapsBuildings.OnAllBuildingsCreated -= ResetWait;
        app.view.map.OnChangeZoom -= ScaleBuildings;
        app.view.map.OnChangePosition -= StopAllActions;
    }

    // Stop all operations while panning
    private void StopAllActions()
    {
        StopAllCoroutines();
        app.model.isPanning = true;

        // Ensure there are buildings when panning
        app.view.buildingsParent.gameObject.SetActive(true);
    }


    // Create a marker3D for panning and zooming
    private void UpdateMarker()
    {
        if (!app.model.isToggled) return;
        if (app.view.map.floatZoom < app.view.onlineMapsBuildings.zoomRange.min + 0.005f) return;

        // Reset
        StopAllCoroutines();
        app.model.isComplete = false;
        app.model.isPanning = false;
        app.view.buildingsParent.gameObject.SetActive(true);
        app.model.timeBeforeUpdateBuildings = app.model.minWaitTime;

        // If the current marker3D exists, get its scale then remove it
        if (app.model.currentBuildingsMarker != null)
        {
            app.model.previousScale = app.model.currentBuildingsMarker.scale;
            OnlineMapsMarker3DManager.RemoveItem(app.model.currentBuildingsMarker);
        }

        app.model.previousZoom = app.view.map.floatZoom;

        // Create a new marker3D from the new map position
        double lng, lat;
        app.view.map.GetPosition(out lng, out lat);
        OnlineMapsMarker3D newMarker3D = OnlineMapsMarker3DManager.CreateItem(lng, lat, default);
        newMarker3D.range = new OnlineMapsRange(16.0f, app.view.onlineMapsBuildings.zoomRange.max);//Hard code min value to 16.0f 
        newMarker3D.label = app.model.markerName;
        newMarker3D.transform.gameObject.name = app.model.markerName;
        Collider markerCombinedCollider = newMarker3D.transform.GetComponent<Collider>();
        DestroyImmediate(markerCombinedCollider);
        MeshRenderer markerMeshRenderer = newMarker3D.transform.GetComponent<MeshRenderer>();
        DestroyImmediate(markerMeshRenderer);
        app.model.currentBuildingsMarker = newMarker3D;

        StartCoroutine(WaitUpdateBuildings());
    }

    // Called by OnAllBuildingsCreated event to reset wait time
    private void ResetWait()
    {
        if (!app.model.isToggled) return;
        if (!app.model.isPanning)
        {
            Debug.Log($"[{GetType().Name}] Found uncombined buildings");
            UpdateMarker();
        }

        app.model.timeBeforeUpdateBuildings = app.model.minWaitTime;
    }

    // Wait for all uncombined buildings to be generated before updating buildings
    private IEnumerator WaitUpdateBuildings()
    {
        // Time before update buildings ticks constantly downwards till 0
        while (app.model.timeBeforeUpdateBuildings >= 0)
        {
            app.model.timeBeforeUpdateBuildings -= Time.deltaTime;
            yield return null;
        }

        UpdateBuildings();
    }

    // Find the active uncombined buildings
    private void UpdateBuildings()
    {
        int buildingCount = 0;

        // New dictionary of key material name, value list of meshFilters
        Dictionary<string, List<MeshFilter>> listOfBuildings = new Dictionary<string, List<MeshFilter>>();

        // Get meshFilters, materials, and the number of uncombined buildings from the Buildings gameObject
        if (app.view.buildingsParent.transform.childCount > 0)
        {
            foreach (Transform building in app.view.buildingsParent.transform)
            {
                if (building.gameObject.activeInHierarchy)
                {
                    MeshFilter childMeshFilter = building.GetComponent<MeshFilter>();
                    Material childMaterial = building.GetComponent<MeshRenderer>().material;

                    if (!listOfBuildings.ContainsKey(childMaterial.name)) listOfBuildings.Add(childMaterial.name, new List<MeshFilter>());

                    if (listOfBuildings.ContainsKey(childMaterial.name)) listOfBuildings[childMaterial.name].Add(childMeshFilter);

                    buildingCount++;
                }
            }
        }

        app.view.combinedBuildingObjects.Clear();
        CombineMesh(listOfBuildings, buildingCount);
    }

    // Combine all active building mesh filters with the same material into one mesh
    private void CombineMesh(Dictionary<string, List<MeshFilter>> listOfBuildings, int buildingCount)
    {
        foreach (var uniqueMaterial in listOfBuildings)
        {
            GameObject newObject = null;

            // Create a container object for each unique material
            for (int i = 0; i < app.model.buildingMaterials.Length; i++)
            {
                if (uniqueMaterial.Key.Contains(app.model.buildingMaterials[i].name))
                    newObject = app.view.CombinedBuildingsObj(app.model.buildingMaterials[i]);
            }

            // Create a combine instance for each unique material
            CombineInstance[] combine = new CombineInstance[uniqueMaterial.Value.Count];

            // Start combining
            for (int i = uniqueMaterial.Value.Count - 1; i >= 0; i--)
            {
                combine[i].mesh = uniqueMaterial.Value[i].sharedMesh;
                combine[i].transform = uniqueMaterial.Value[i].transform.localToWorldMatrix;
                combine[i].mesh.SetTriangles(uniqueMaterial.Value[i].mesh.triangles, 0);
                combine[i].mesh.subMeshCount = 1;
            }

            newObject.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            app.view.combinedBuildingObjects.Add(newObject);
        }

        if (app.model.meshToMarker != true)
        {
            StopCoroutine(MeshToMarker(buildingCount));
        }
        StartCoroutine(MeshToMarker(buildingCount));
    }

    // Set the new combinedBuildingsObject as child of current marker3D
    private IEnumerator MeshToMarker(int buildingCount)
    {
        while (app.model.currentBuildingsMarker == null) yield return null;

        app.model.meshToMarker = true;

        // Count the number of active uncombined buildings again
        int currentBuildingCount = 0;

        if (app.view.buildingsParent.transform.childCount > 0)
        {
            foreach (Transform building in app.view.buildingsParent.transform)
            {
                if (building.gameObject.activeInHierarchy)
                {
                    currentBuildingCount++;
                }
            }
        }

        // Move the combined building objects from the list into the current marker3D
        for (int i = app.view.combinedBuildingObjects.Count - 1; i >= 0; i--)
        {
            app.view.combinedBuildingObjects[i].transform.SetParent(app.model.currentBuildingsMarker.transform);
            app.view.combinedBuildingObjects.RemoveAt(i);
        }

        // If there are uncombined buildings, restart the process
        if (currentBuildingCount - buildingCount > 0 && !app.model.isPanning)
        {
            Debug.Log($"[{GetType().Name}] Found uncombined buildings");
            UpdateMarker();
            yield break;
        }

        // Finished combine buildings mesh process
        Debug.Log($"[{GetType().Name}] {app.model.currentBuildingsMarker}, Parent: {app.model.currentBuildingsMarker.transform.parent} Child count: {app.model.currentBuildingsMarker.transform.childCount}, Child scale: {app.model.currentBuildingsMarker.transform.GetChild(0).localScale}.");

        app.view.buildingsParent.gameObject.SetActive(false);
        app.model.currentBuildingsMarker.enabled = true;
        app.model.isComplete = true;
        app.model.meshToMarker = false;
    }

    // Only one ScaleBuildings coroutine running at one time
    private void ScaleBuildings()
    {
        //Remove marker if the at the min of the zoom level
        if (app.view.map.floatZoom < app.model.onlineMapsBuildings.zoomRange.min + 0.005f)
        {
            if (app.model.currentBuildingsMarker != null)
            {
                app.model.previousScale = app.model.currentBuildingsMarker.scale;
                OnlineMapsMarker3DManager.RemoveItem(app.model.currentBuildingsMarker);
            }
            return;
        }

        if (app.model.currentBuildingsMarker == null) return;

        if (app.view.map.floatZoom >= 7.0f && app.view.map.floatZoom < 20.0f)
        {
            if (app.model.previousZoom > app.view.map.floatZoom)
            {
                float diffZoom = app.model.previousZoom - app.view.map.floatZoom;
                if (diffZoom <= 1.0f)
                {
                    float divideValue = Mathf.Lerp(app.model.previousScale, app.model.previousScale * 0.45f, diffZoom);
                    app.model.currentBuildingsMarker.scale = divideValue;

                }
                else
                {
                    app.view.map.floatZoom = app.model.previousZoom - 1.0f;
                    app.model.currentBuildingsMarker.scale = app.model.previousScale * 0.45f;
                    app.model.previousZoom = app.view.map.floatZoom;
                    app.model.previousScale = app.model.currentBuildingsMarker.scale;
                }
            }

            else if (app.model.previousZoom < app.view.map.floatZoom)
            {
                float diffZoom = app.view.map.floatZoom - app.model.previousZoom;
                if (diffZoom <= 1.0f)
                {
                    float divideValue = Mathf.Lerp(app.model.previousScale, app.model.previousScale * 1.95f, diffZoom);
                    app.model.currentBuildingsMarker.scale = divideValue;

                }
                else
                {
                    app.view.map.floatZoom = app.model.previousZoom + 1.0f;
                    app.model.currentBuildingsMarker.scale = app.model.previousScale * 1.95f;
                    app.model.previousZoom = app.view.map.floatZoom;
                    app.model.previousScale = app.model.currentBuildingsMarker.scale;
                }
            }
        }
    }
}
