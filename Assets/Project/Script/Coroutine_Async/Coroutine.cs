using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coroutine : MonoBehaviour
{
    public Text myTextField;
    private IEnumerator coroutine;

    [Button]
    public void Press()
    {
        coroutine = WriteCoroutine(" StartCoroutine: ");
        StartCoroutine(coroutine);
        WriteForLoop(" For_Loop ");
    }

    [Button]
    public void Stop_Coroutine()
    {
        StopCoroutine(coroutine);
        myTextField.text = myTextField.text + " StopCoroutine: " + Time.realtimeSinceStartup;
    }

    IEnumerator WriteCoroutine(string word)
    {
        for(int i = 0; i < 5; i++)
        {
            myTextField.text = myTextField.text + (string)word + i + " , time: " + Time.realtimeSinceStartup;
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void  WriteForLoop(string word)
    {
        for (int i = 0; i < 5; i++)
        {
            myTextField.text = myTextField.text + (string)word + i + " , time: " + Time.realtimeSinceStartup;
        }
    }
}
