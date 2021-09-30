using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_OtherToMainThread : Element_MainThread
{
    private bool coroutineRunning = false;

    private float prevLon, prevLat, prevZoom;
    private void Awake()
    {
        UnityThread.initUnityThread(); //initizlize the function
    }

    void Start()
    {
        app.controller.LonLatZoomEvent += ReceiveLonLatZoom;
    }

    private void ReceiveLonLatZoom(string lonLatZoom)
    {

        float lon = 0, lat = 0, zoom = 0;

        if (coroutineRunning)
        {
            UnityThread.executeStopCoroutine(ChangeLonLatZoom(lon, lat, zoom));//To stop a coroutine function in the main Thread from another Thread
        }

        string[] textSplit = lonLatZoom.Split(char.Parse("_"));
        for (int i = 0; i < textSplit.Length; i++)
        {

            string[] LonLatZoomValve = textSplit[i].Split(char.Parse(":"));

            string removeSpace = LonLatZoomValve[0].Replace(" ", "");
            if (removeSpace.ToLower() == "lon")
            {
                try
                {
                    lon = float.Parse(LonLatZoomValve[1]);
                    prevLon = lon;
                }
                catch (System.FormatException)
                {
                    lon = prevLon; // or other default value as appropriate in context.
                }

            }
            else if (removeSpace.ToLower() == "lat")
            {
                try
                {
                    lat = float.Parse(LonLatZoomValve[1]);
                    prevLon = lon;
                }
                catch (System.FormatException)
                {
                    lat = prevLat; // or other default value as appropriate in context.
                }
            }
            else if (removeSpace.ToLower() == "zoom")
            {
                try
                {
                    zoom = float.Parse(LonLatZoomValve[1]);
                    prevZoom = zoom;
                }
                catch (System.FormatException)
                {
                    zoom = prevZoom; // or other default value as appropriate in context.
                }
            }
            else
            {
                Debug.Log("There is not longititude, latitude and zoom value");
            }

        }
        UnityThread.executeCoroutine(ChangeLonLatZoom(lon, lat, zoom));//To start a coroutine function in the main Thread from another Thread

    }

    IEnumerator ChangeLonLatZoom(float endLng, float endLat, float endZoom)
    {
        coroutineRunning = true;

        app.view.map.GetPosition(out double lng, out double lat);
        double startLng = lng;
        double startLat = lat;
        float startZoom = app.view.map.floatZoom;
        float timeElapsed = 0;


        while (timeElapsed < app.model.lerpDuration)
        {
            float newLon = Mathf.Lerp((float)startLng, (float)endLng, timeElapsed / app.model.lerpDuration);
            float newLat = Mathf.Lerp((float)startLat, (float)endLat, timeElapsed / app.model.lerpDuration);
            float newZoom = Mathf.Lerp((float)startZoom, (float)endZoom, timeElapsed / app.model.lerpDuration);
            timeElapsed += Time.deltaTime;
            app.view.map.SetPositionAndZoom(newLon, newLat, newZoom);
            yield return null;
        }
        app.view.map.SetPositionAndZoom(endLng, endLat, (float)endZoom);
        coroutineRunning = false;
    }
}
