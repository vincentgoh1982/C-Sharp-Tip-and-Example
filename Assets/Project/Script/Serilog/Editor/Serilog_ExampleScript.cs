using System;
using UnityEngine;
using Serilog;
using NaughtyAttributes;
using UnityEditor;
using System.IO;

public class Serilog_ExampleScript : MonoBehaviour
{
    public void Start()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    [Button]
    public void DebugTesting()
    {
        Log.Information("Hello, world!");

        int a = 10, b = 0;
        try
        {
            Log.Debug("Dividing {A} by {B}", a, b);
            Console.WriteLine(a / b);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Something went wrong");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    [Button]
    public void OpenFile()
    {
        //string argument = "/select, \"" + commonMaterialPath + "\"";
        //System.Diagnostics.Process.Start("explorer.exe", argument);
        string projectFolder = Path.Combine(Application.dataPath, "../Logs/");
        Debug.Log(projectFolder);
        EditorUtility.RevealInFinder(projectFolder);
    }
}
