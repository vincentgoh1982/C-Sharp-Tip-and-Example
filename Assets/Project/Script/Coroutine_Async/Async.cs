using NaughtyAttributes;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Async : MonoBehaviour
{
    public Text myTextField;
    private CancellationTokenSource source;

    [Button]
    public void Press()
    {
        source = new CancellationTokenSource();
        WriteAsync(source.Token);
        WriteForLoop(" For_Loop ");
    }
    [Button]
    public void Stop()
    {
        source.Cancel();
        myTextField.text = myTextField.text + " StopAsync " + " , time: " + Time.realtimeSinceStartup;
    }

    private async void WriteAsync(CancellationToken token)
    {
        for (int i = 0; i < 5; i++)
        {
            myTextField.text = myTextField.text + " StartAsync " + i + " , time: " + Time.realtimeSinceStartup;
            await Task.Delay(TimeSpan.FromSeconds(3), token);
        }
    }

    private void WriteForLoop(string word)
    {
        for (int i = 0; i < 5; i++)
        {
            myTextField.text = myTextField.text + (string)word + i + " , time: " + Time.realtimeSinceStartup;
        }
    }
}
