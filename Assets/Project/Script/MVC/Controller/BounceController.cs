
using TMPro;
using UnityEngine;

// Controls the app workflow.
public class BounceController : BounceElement
{
    // Handles the ball hit event
    public void OnBallGroundHit()
    {
        app.model.bounces++;
        Debug.Log("Bounce" +app.model.bounces);
        app.view.tmp_text.GetComponent<TMP_Text>().text = app.model.bounces.ToString();
        if (app.model.bounces >= app.model.winCondition)
        {
            app.view.ball.enabled = false;
            app.view.ball.GetComponent<Rigidbody>().isKinematic = true; // stops the ball
            OnGameComplete();
        }
    }

    // Handles the win condition
    public void OnGameComplete() 
    { 
        Debug.Log("Victory!!");
        app.view.tmp_text.text = "Victory!!";
    }
}
