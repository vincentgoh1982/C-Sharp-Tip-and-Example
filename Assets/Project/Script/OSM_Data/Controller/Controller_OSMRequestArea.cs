using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TreeGenerateInNodeWayRelation.I_OSMTree;


public class Controller_OSMRequestArea : TreeElement
{
    [Button]
    public void StopLoadingTree() //button for  stop loading the trees
    {

        if (treeExterior != null)
        {
            StopCoroutine(treeExterior);
        }
        if (generateTreeInsideColor != null)
        {
            StopCoroutine(generateTreeInsideColor);
        }

        completedGeneratedTree.Invoke();
        DestroyAllTree();
        OffRequestTree.Invoke();
    }

    [Button]
    public void LoadingTree() //button for loading the trees
    {
        if (app.view.map.floatZoom > app.view.onlineMapsBuildings.zoomRange.min) //Ignore the compare of previous area and just load the tree
        {
            OnRequestTree.Invoke();
            RequestNewTrees();
        }
    }

    #region Event
    public UnityEvent OffRequestTree; //Off the request from OSM
    public UnityEvent OnRequestTree; //On the request from OSM
    public UnityEvent startGeneratingTree; //Start generating interior and exterior trees
    public UnityEvent completedGeneratedTree; //Completed generated interior and exterior trees or Stop generating tree by the user

    private bool CompletedGeneratedExteriorTree = false; //Check the completion of generating the exterior tree
    private bool CompletedGeneratedInteriorTree = false; //Check the completion of generating the interior tree

    //Get PauseAreaEvent
    /*public PauseAreaEvent _pauseAreaEvent;
    private PauseAreaEvent pauseAreaEvent
    {
        get
        {
            if (_pauseAreaEvent == null)
            {
                _pauseAreaEvent = GetComponent<PauseAreaEvent>();

            }
            return _pauseAreaEvent;
        }
    }*/
    #endregion Event

    #region Interface C#
    private readonly IList<I_OSMInterface> _iTrees;

    public Controller_OSMRequestArea()
    {
        _iTrees = new List<I_OSMInterface>();
    }

    //Add all the interface
    public void RegisterITreeChannel(I_OSMInterface channel)
    {
        _iTrees.Add(channel);
    }
    #endregion Interface C#

    private IEnumerator treeExterior; //Coroutine for exterior tree
    private IEnumerator generateTreeInsideColor; // Coroutine for interior tree

    //private void OnDestroy() => pauseAreaEvent.PausePositionEvent -= OnChangePosition;

    private void Start()
    {
        //pauseAreaEvent.PausePositionEvent += OnChangePosition;
        app.view.getContainer();
        UpdateAreas();

    }

    private void OnChangePosition()//Stop all Coroutine and check the map zoom level
    {

        if (treeExterior != null)
        {
            StopCoroutine(treeExterior);
        }
        if (generateTreeInsideColor != null)
        {
            StopCoroutine(generateTreeInsideColor);
        }
        completedGeneratedTree.Invoke();

        if (app.view.map.floatZoom > app.view.onlineMapsBuildings.zoomRange.min)
        {
            UpdateAreas();
        }
    }

    private void CompletedGeneratedAllTrees() //Complete generate interior and exterior trees
    {
        if (CompletedGeneratedExteriorTree == true && CompletedGeneratedInteriorTree == true)
        {
            CompletedGeneratedInteriorTree = false;
            CompletedGeneratedExteriorTree = false;
            completedGeneratedTree.Invoke();
        }
    }

    #region Function to read the Map from OSM data
    private void UpdateAreas() //Get map's location and compare it with the previous map's location
    {

        double tlx, tly, brx, bry;

        app.view.map.GetTileCorners(out tlx, out tly, out brx, out bry);
        OnlineMapsVector2i newTopLeft = new OnlineMapsVector2i((int)Math.Round(tlx - 2), (int)Math.Round(tly - 2));
        OnlineMapsVector2i newBottomRight = new OnlineMapsVector2i((int)Math.Round(brx + 2), (int)Math.Round(bry + 2));

        if (newTopLeft != app.model.prevTopLeft || newBottomRight != app.model.prevBottomRight)
        {
            if (app.view.map.floatZoom > app.view.onlineMapsBuildings.zoomRange.min)
            {
                //Debug.Log("UpdateAreas");
                app.model.prevTopLeft = newTopLeft;
                app.model.prevBottomRight = newBottomRight;
                RequestNewTrees();
            }
        }

    }

    private void RequestNewTrees() //Enquire information of the map's location and information of the request of data's type(landuse = forest)
    {
        Debug.Log("RequestNewTrees");
        double tlx, tly, brx, bry;
        app.view.map.projection.TileToCoordinates(app.model.prevTopLeft.x, app.model.prevTopLeft.y, app.view.map.zoom, out tlx, out tly);
        app.view.map.projection.TileToCoordinates(app.model.prevBottomRight.x, app.model.prevBottomRight.y, app.view.map.zoom, out brx, out bry);

        app.model.requestTreeData = string.Format(OnlineMapsUtils.numberFormat, "(node[natural=tree]({0},{1},{2},{3});way[landuse = forest]({0},{1},{2},{3});way[landuse = cemetery]({0},{1},{2},{3}) ;way[landuse = orchard]({0},{1},{2},{3}); way[leisure = park]({0},{1},{2},{3}); way[leisure = golf_course]({0},{1},{2},{3}); way[natural = wood]({0},{1},{2},{3}); relation[landuse = forest]({0},{1},{2},{3}); relation[landuse = cemetery]({0},{1},{2},{3}); relation[landuse = orchard]({0},{1},{2},{3}); relation[leisure = park]({0},{1},{2},{3}); relation[leisure = golf_course]({0},{1},{2},{3}); relation[natural = wood]({0},{1},{2},{3}););out;>;out skel qt;",
        bry, tlx, tly, brx);

        SendRequest();
    }

    private void SendRequest() //Enquire OSM data from online or offline server
    {

        app.model.osmRequest = OnlineMapsOSMAPIQuery.Find(app.model.requestTreeData);
        app.model.osmRequest.OnSuccess += OnTreesRequestSuccess;
        app.model.osmRequest.OnFailed += OnTreesRequestFailed;
        app.model.requestTreeData = null;

    }

    private void OnTreesRequestFailed(OnlineMapsTextWebService request) //Tree data request failed
    {
        if (app.model.numberRetryRequestTreeData > 1)
        {
            RequestNewTrees();
            app.model.numberRetryRequestTreeData -= 1;
        }
        else
        {
            Debug.Log("Tree Data request from OnlineMapsTextWebService failed...");
            app.model.numberRetryRequestTreeData = 5;
            app.model.osmRequest = null;
        }

    }

    private void OnTreesRequestSuccess(OnlineMapsTextWebService request) //Tree data request success
    {
        string response = request.response;
        if (response.Length < 1)
        {
            if (app.model.numberRetryRequestTreeData > 1)
            {
                RequestNewTrees();
                app.model.numberRetryRequestTreeData -= 1;
            }
            else
            {
                Debug.Log("Tree Data's length is less than 1");
                app.model.numberRetryRequestTreeData = 5;
            }
        }
        else
        {
            DestroyAllTree();
            CheckNodeWayRelation(response);
            app.model.osmRequest = null;
        }

    }

    private async void CheckNodeWayRelation(string response) //Break down the tree Data into longitude and latitude format
    {

        foreach (var treeChannel in _iTrees)
        {
            if (treeChannel != null)
            {
                treeChannel.GetMapLngLat(app.view.map.bottomRightPosition.x, app.view.map.bottomRightPosition.y, app.view.map.topLeftPosition.x, app.view.map.topLeftPosition.y, app.model.radiusSquared);
                TreeGrp treeGrp = await treeChannel.ReadOSMNodeWayRelationAsync(response);

                if (treeGrp.treeDefineByColor == false) //Generate tree from OSM
                {
                    startGeneratingTree.Invoke();
                    treeExterior = GenerateTreeExterior(treeGrp.lng, treeGrp.lat);
                    StartCoroutine(treeExterior);
                }

                else if (treeGrp.treeDefineByColor == true) //Generate tree from color of the map
                {
                    generateTreeInsideColor = GenerateTreeInsideColor(treeGrp.lng, treeGrp.lat);
                    StartCoroutine(generateTreeInsideColor);
                }
            }
        }
        app.model.osmRequest = null;
    }
    #endregion Function to read the Map from OSM data

    #region Function to check Longitiude and latitude within the green color
    private bool ColorIsForest(double longititude, double latitude) //Check the green color of the individual tile in order to populate the longtitude and latitude value for the tree
    {
        bool isGreen = false;

        double tx, ty;
        OnlineMaps.instance.projection.CoordinatesToTile(longititude, latitude, OnlineMaps.instance.zoom, out tx, out ty); // Convert coordinates to tile position

        // Get tile index
        int itx = (int)tx;
        int ity = (int)ty;

        OnlineMapsTile tile = OnlineMaps.instance.tileManager.GetTile(OnlineMaps.instance.zoom, itx, ity); // Get tile

        while (tile != null && tile.status != OnlineMapsTileStatus.loaded) // If the tile exists, but is not yet loaded, take the parent
        {
            tile = tile.parent;
            tx /= 2;
            ty /= 2;
        }

        if (tile == null) // If the tile does not exist
        {
            Debug.Log("No loaded tiles under longtitude and latitude");
            return false;
        }

        // Calculate the relative position
        double rx = tx - (int)tx;
        double ry = ty - (int)ty;

        if (!OnlineMapsControlBase.instance.resultIsTexture) // For Target - Tileset
        {
            //GetPixelBilinear can only use on the main threads
            Color color = tile.texture.GetPixelBilinear((float)rx, 1 - (float)ry);
            if (color.g > color.r)
            {
                if (color.g > color.b)
                {
                    isGreen = true;
                }
            }
        }
        else
        {
            isGreen = false;
        }

        return isGreen;
    }
    #endregion  Function to check Longitiude and latitude within the circle and green color

    #region Function to create and destroy tree
    IEnumerator GenerateTreeInsideColor(List<double> lngTreeGroup, List<double> latTreeGroup) //Filter the interior tree's location by colors
    {
        int numberTree = 0;

        if (latTreeGroup.Count > 0)
        {
            for (int i = 0; i < latTreeGroup.Count; i++)
            {
                if (ColorIsForest(lngTreeGroup[i], latTreeGroup[i])) //Check whether the map's tile is green
                {
                    if (numberTree > app.model.numberTreeAppearPerSecond)
                    {
                        numberTree = 0;
                        yield return new WaitForSeconds(3.0f);
                    }
                    else
                    {
                        TreeGeneration(lngTreeGroup[i], latTreeGroup[i]);
                        numberTree++;
                    }
                }
            }
            CompletedGeneratedInteriorTree = true;
            CompletedGeneratedAllTrees();

        }
    }

    IEnumerator GenerateTreeExterior(List<double> lngTreeGroup, List<double> latTreeGroup) //Get the tree's location from OSM data
    {
        int numberTree = 0;

        if (lngTreeGroup.Count != 0)
        {
            for (int i = 0; i < lngTreeGroup.Count; i++)
            {
                if (numberTree > app.model.numberTreeAppearPerSecond)
                {
                    numberTree = 0;
                    yield return new WaitForSeconds(1.5f);
                }
                else
                {
                    TreeGeneration(lngTreeGroup[i], latTreeGroup[i]);
                    numberTree++;
                }
            }

            CompletedGeneratedExteriorTree = true;
            CompletedGeneratedAllTrees();
        }
    }

    private void TreeGeneration(double longitiude, double latitude) //Generate tree using online 3D markers
    {
        OnlineMapsMarker3D marker3D;
        string MakerTag = "forest";
        marker3D = OnlineMapsMarker3DManager.CreateItem(longitiude, latitude, app.view.markerTreePrefab);
        marker3D.label = MakerTag;
        var treeCollider = marker3D.transform.GetComponent<Collider>();
        DestroyImmediate(treeCollider);

        marker3D.transform.parent = app.view.getContainer().transform;
        marker3D.range = new OnlineMapsRange(16.8f, app.view.onlineMapsBuildings.zoomRange.max);
    }

    private void DestroyAllTree() //Delete all tree when the map change location
    {
        //Get the list of tree from the empty gameobject TreeGrps
        var listTree = app.view.getContainer().GetComponentInChildren<Transform>();

        if (listTree.childCount > 0)
        {
            foreach (Transform t in listTree)
            {
                var instanceTree = t.GetComponent<OnlineMapsMarker3DInstance>().marker;
                OnlineMapsMarker3DManager.RemoveItem((OnlineMapsMarker3D)instanceTree);
            };
        }
    }
    #endregion Function to create and destroy tree
}


