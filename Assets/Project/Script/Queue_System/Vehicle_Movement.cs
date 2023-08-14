using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Movement : MonoBehaviour
{
    private Queue<Vector3> targetQueue = new Queue<Vector3>();
    public float speed = 1.0f;
    private bool isMoving = false;

    public void AddNewPosition(Vector3 newPosition, float speed)
    {
        Debug.Log($"AddNewPosition: {newPosition.x},{newPosition.y},{newPosition.z}");
        Vector3 vector3D = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        this.speed = speed;
        targetQueue.Enqueue(vector3D);
        StartMoving();
    }

    private void Update()
    {
        // Check for input to start moving
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            StartMoving();
        }

        // Check if the GameObject is moving
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void StartMoving()
    {
        isMoving = true;
    }

    private void MoveToTarget()
    {
        if (targetQueue.Count == 0)
        {
            isMoving = false;
            return;
        }

        Vector3 targetPosition = targetQueue.Peek();

        // Move the GameObject towards the current target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the GameObject has reached the target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Remove the current target from the queue
            targetQueue.Dequeue();
        }
    }
}
