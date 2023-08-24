using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Movement : MonoBehaviour
{
    private Queue<Vector3> targetQueue = new Queue<Vector3>();
    public float speed = 1.0f;
    private float timeElapsed = 0;
    public float lerpDuration = 10;
    private Vector3 previousPosition;

    public void AddNewPosition(Vector3 newPosition, float speed)
    {
        this.speed = speed;

        if (newPosition != previousPosition)
        {
            previousPosition = newPosition;
            targetQueue.Enqueue(newPosition);
            //StartMoving();
            StartCoroutine(MoveRotateVehicle());
        }
        else
        {
            Debug.Log($"Gameobject's position is similar to the new position: {newPosition}");
        }
    }

    private IEnumerator MoveRotateVehicle()
    {
        while (targetQueue.Count > 0)
        {
            Debug.Log($"MoveRotateVehicle: {targetQueue.Count}");
            var routine = StartCoroutine(MoveVehicle());
            yield return routine;
            if(targetQueue.Count > 0)
            {
                timeElapsed = 0;
                targetQueue.Dequeue();
            }
        }
    }

    private IEnumerator MoveVehicle()
    {
        Vector3 targetPosition = targetQueue.Peek();

        while (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            Debug.Log($"MoveVehicle: {transform.position} timer: {timeElapsed}");

            if (transform.position == targetPosition)
            {
                break;
            }

        }
    }
}
