using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Movement : MonoBehaviour
{
    private Queue<MoveRequest> positionQueue = new Queue<MoveRequest>();
    private bool isMoving = false;

    //Enqueues a new target position and starts the movement coroutine if the system is not already moving
    public void EnqueuePosition(Vector3 newPosition, float speed)
    {
        MoveRequest moveRequest = new MoveRequest(newPosition, speed);
        positionQueue.Enqueue(moveRequest);

        if (!isMoving)
        {
            StartCoroutine(MoveNextPosition());
        }
    }
    //Coroutine takes care of moving to the next position in the queue. It processes positions one by one until the queue is empty.
    private IEnumerator MoveNextPosition()
    {
        isMoving = true;

        while (positionQueue.Count > 0)
        {
            MoveRequest moveRequest = positionQueue.Dequeue();
            float startTime = Time.time;
            float journeyLength = Vector3.Distance(transform.position, moveRequest.Direction);
            float speed = moveRequest.Speed;

            while (transform.position != moveRequest.Direction)
            {
                float distanceCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distanceCovered / journeyLength;

                transform.position = Vector3.Lerp(transform.position, moveRequest.Direction, fractionOfJourney);
                yield return null;
            }
        }

        isMoving = false;
    }

    private struct MoveRequest
    {
        public Vector3 Direction;
        public float Speed;

        public MoveRequest(Vector3 direction, float speed)
        {
            Direction = direction;
            Speed = speed;
        }
    }

}
