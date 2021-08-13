using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Tree : TreeElement
{

    public int numberRetryRequestTreeData = 5; //Time checking of request and stopping area

    public float radiusSquared = 0.25f; //Map's radius

    public int numberTreeAppearPerSecond = 30; //Number of tree appear per second

    //Store previous map value to compare the current map value
    public OnlineMapsVector2i prevBottomRight;
    public OnlineMapsVector2i prevTopLeft;

    public OnlineMapsOSMAPIQuery osmRequest; //OSM Data

    public string requestTreeData; //information of the map's location and request of data's type(landuse = forest)

}
