using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Pooling : Element_Pooling
{
    private void Update()
    {
        // Ctrl was pressed, launch a projectile
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            IEnumerator coroutine;

            if (app.model.ballPoolingID <= app.view.PoolingBall.Length-1)
            {
                clone = app.view.PoolingBall[app.model.ballPoolingID].GetComponent<Rigidbody>();
                clone.gameObject.SetActive(true);
                clone.velocity = transform.TransformDirection(Vector3.up * 10);
                coroutine = WaitAndHide(clone);
                StartCoroutine(coroutine);
                app.model.ballPoolingID += 1;
            }
            else
            {
                app.model.ballPoolingID = 0;
            }

        }
    }

    private IEnumerator WaitAndHide(Rigidbody clone)
    {

        yield return new WaitForSeconds(1.0f);
        clone.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        clone.gameObject.transform.rotation = new Quaternion(0, 0, 0,0);
        clone.gameObject.SetActive(false);

    }
}
