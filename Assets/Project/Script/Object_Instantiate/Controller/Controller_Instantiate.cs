
using UnityEngine;

public class Missile : Element_Instantiate
{

}

public class Controller_Instantiate : Element_Instantiate
{
    private void Update()
    {
        // Ctrl was pressed, launch a projectile
        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(app.view.ball, app.model.InstantiateParent.localPosition, transform.rotation);
            
            // Give the cloned object an initial velocity along the current
            // object's Z axis
            clone.velocity = transform.TransformDirection(Vector3.up * 10);
            Destroy(clone.gameObject, 1.0f);
        }
    }

}
