
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class Material_to_material : EditorWindow
{
    public List<GameObject> gameObjList = new List<GameObject>();
    public string root_path = "";
    public List<Material> materialList = new List<Material>();
    public string stringToEdit = "";
    private static readonly byte[] byteData;

    [MenuItem("Window/MaterialDetector")]

    public static void ShowWindow()
    {
        GetWindow<Material_to_material>("MaterialDetector");
    }

    void OnGUI()
    {
        string path = "";
        string material_path = "";
        string texture_path = "";
        //new line 2019
        string commonMaterialPath = (Application.dataPath) + "/Resources/common_material.txt";

        //new amendment
        stringToEdit = GUI.TextArea(new Rect(25, 350, 350, 250), stringToEdit, 200);
        //window Code
        GUILayout.Label("1. This is a creating or deleting folder tool.", EditorStyles.boldLabel);
        GUILayout.Label("(Remember to import the fbx into the folder.)", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();

        see_list_material();

        if (GUILayout.Button("Creating material and texture folder"))
        {

            var obj = Selection.activeObject;

            if (obj == null) path = "Assets";
            else path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

            path = finding_folder(path);

            material_path = path + "/material";
            texture_path = path + "/texture";
            root_path = path;

            create_folder(material_path); //create material folder 
            extract_fbx(root_path, texture_path, material_path); // extract texture and material into folder

        }

        if (GUILayout.Button("Deleting material, texture folder and fbx"))
        {
            var obj = Selection.activeObject;

            if (obj == null) path = "Assets";
            else path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

            path = finding_folder(path);
            material_path = path + "/material";
            texture_path = path + "/texture";
            delete_folder(material_path, path);
            delete_folder(texture_path, path);
        }

        GUILayout.EndHorizontal();

        GUILayout.Label("2. Drag the game object to here.");
        GUILayout.BeginHorizontal();

        Rect rect = GUILayoutUtility.GetRect(100f, 50f);
        GUI.Box(rect, "Drag Objects here", EditorStyles.boldLabel);
        if (rect.Contains(Event.current.mousePosition))
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            if (Event.current.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();

                foreach (var obj in DragAndDrop.objectReferences)
                {
                    Debug.Log("obj" + obj);
                    root_path = obj.name;
                    Finding_child_item((GameObject)obj);
                }
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        root_path = EditorGUILayout.TextField("Game object's root", root_path);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Number of game object", gameObjList.Count.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Linking individual material to common material"))
        {
            if (root_path != "Assets")
            {
                CheckMaterial();
            }
            else
            {
                Debug.LogWarning("A warning! The path is wrong. Click Reconnect fbx path button.");
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("3. Click button(Open list in window) to add new common material's name", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        commonMaterialPath = EditorGUILayout.TextField("Resource Dir", commonMaterialPath);
        if (GUILayout.Button("Open list in window"))
        {
            window_list_material();
        }
        GUILayout.EndHorizontal();

    }


    //New line 2019
    void window_list_material()
    {
        //string argument = "/select, \"" + commonMaterialPath + "\"";
        //System.Diagnostics.Process.Start("explorer.exe", argument);
        Application.OpenURL((Application.dataPath) + "/Resources/common_material.txt");
    }

    private void CheckMaterial()//cannot reassign a single element of sharedMaterials, have to reassign the entire array.
    {
        if(materialList.Count != 0)
        {
            materialList.Clear();
        }

        for (int i = 0; i < gameObjList.Count; i++)
        {
            GameObject item = gameObjList[i];

            if (item.GetComponent<Renderer>())
            {
                item.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }

            Material[] sharedMaterialsCopy = item.GetComponent<Renderer>().sharedMaterials;
            for (int j = 0; j< sharedMaterialsCopy.Length; j++)
            {
                TextAsset common_materials_info = Resources.Load("common_material") as TextAsset;
                stringToEdit = common_materials_info.text;
                string[] material_list = Regex.Split(stringToEdit, "\n|\r|\r\n");
                
                foreach (string a_material_list in material_list)
                {
                    string[] listMaterials = Regex.Split(a_material_list, "_");
                    string[] gameobjectMaterial = Regex.Split(sharedMaterialsCopy[j].ToString(), "_");

                    foreach (string listMaterial in listMaterials)
                    {
                        if (gameobjectMaterial[0].ToLower() == listMaterial.ToLower())
                        {
                            Material newMat = Resources.Load(a_material_list) as Material;
                            sharedMaterialsCopy[j] = newMat;
                            materialList.Add(sharedMaterialsCopy[j]);
                            Debug.Log(sharedMaterialsCopy[j]);
                        }
                        if (gameobjectMaterial[0].ToLower() == "shadow")
                        {
                            materialList.Add(sharedMaterialsCopy[j]);
                            item.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                        }
                    }
                }
                item.GetComponent<Renderer>().sharedMaterials = sharedMaterialsCopy;
            }

        }
    }

    private void Finding_child_item(GameObject game_object_item)
    {
        gameObjList.Clear();

        Transform[] GameObject_children;
        GameObject_children = game_object_item.GetComponentsInChildren<Transform>(true);

        foreach (var GameObject_child in GameObject_children)
        {
            if (GameObject_child.GetComponent<Renderer>())
            {
                gameObjList.Add(GameObject_child.gameObject);
            }
        }
    }

    void see_list_material()
    {

        TextAsset common_materials_info = Resources.Load("common_material") as TextAsset;
        stringToEdit = common_materials_info.text;

    }

    string finding_folder(string path)
    {

        if (path.Length > 0)
        {
            if (Directory.Exists(path))
            {
                //path = path;
                return (path);
                //Debug.Log("It is a folder!");

            }
            else
            {
                path = "File";
                return (path);
                //Debug.Log("It is a file!");
            }
        }
        else
        {
            path = "Not in assets folder";
            // Debug.Log("Nothing found!");
            return (path);
        }
    }

    void create_folder(string material_path)
    {
        bool exists = AssetDatabase.IsValidFolder(material_path);
        if (!exists)
        {
            //Debug.Log(material_path);
            Directory.CreateDirectory(material_path);
            AssetDatabase.Refresh();
            //Debug.Log("Created");
        }
        else
        {
            Debug.Log("The folders have created.");

        }
    }

    void extract_fbx(string path, string texture_path, string material_path)
    {
        var fileInfo = Directory.GetFiles(path, "*.fbx");

        //Debug.Log("extract");
        foreach (var file in fileInfo)
        {
            string file_info = file.Replace(@"\", "/");
            //Debug.Log("file");
            //Texture extractor
            var modelImporter = AssetImporter.GetAtPath(file_info) as ModelImporter;
            modelImporter.ExtractTextures(texture_path);
            AssetDatabase.Refresh();

            //Material extractor
            object[] materials = AssetDatabase.LoadAllAssetsAtPath(file_info);

            foreach (Object material in materials)
            {
                //Debug.Log(material);
                if (material.GetType() == typeof(Material))
                {
                    //Debug.Log("material");
                    var newAssetPath = string.Join(Path.DirectorySeparatorChar.ToString(), new[] { material_path, material.name }) + ".mat";
                    var AmendAssetPath = newAssetPath.Replace(@"\", "/");
                    //Debug.Log(AmendAssetPath);

                    //Debug.Log(file_info);
                    AssetDatabase.ExtractAsset(material, AmendAssetPath);
                    AssetDatabase.WriteImportSettingsIfDirty(file_info);
                    AssetDatabase.ImportAsset(file_info, ImportAssetOptions.ForceUpdate);
                    //AssetDatabase.Refresh();
                }

            }

        }
        AssetDatabase.Refresh();
    }

    void delete_folder(string material_path, string path)
    {
        bool exists = AssetDatabase.IsValidFolder(path);
        //Debug.Log(path);
        //Debug.Log(exists);

        if (exists)
        {
            //Debug.Log(material_path);
            FileUtil.DeleteFileOrDirectory(material_path);
            AssetDatabase.Refresh();
            var fileInfo = Directory.GetFiles(path, "*.fbx");
            foreach (var file in fileInfo)
            {
                string file_info = file.Replace(@"\", "/");
                FileUtil.DeleteFileOrDirectory(file_info);
                //Debug.Log(file_info);
            }
            //Debug.Log("Deleted");
        }
        else
        {
            //Debug.Log(material_path);
            Debug.Log("Nothing to delete.");
        }
    }
}
