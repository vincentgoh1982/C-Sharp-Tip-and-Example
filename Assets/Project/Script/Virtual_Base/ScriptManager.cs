using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public List<ScriptDescription> scriptsToExecute = new List<ScriptDescription>();
    [Button]
    public void PressToStart()
    {
        // Loop through and start each script
        foreach (ScriptDescription script in scriptsToExecute)
        {
            if (script.enableScript == true)
            {
                script.monoBehaviourBaseScript.MonoBehaviourVirtualStart();
            }
        }
    }
}

[System.SerializableAttribute]
public class ScriptDescription
{
    public BaseScript monoBehaviourBaseScript; //Starting of the script behaviour
    public string description; //Describle the script behaviour
    public bool enableScript; //Enable the script to run or disable the script

    public ScriptDescription(BaseScript _monoBehaviourBaseScript, string _description, bool _enableScript)
    {
        monoBehaviourBaseScript = _monoBehaviourBaseScript;
        description = _description;
        enableScript = _enableScript;
    }
}

