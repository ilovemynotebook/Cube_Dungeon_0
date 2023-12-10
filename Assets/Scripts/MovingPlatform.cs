using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 5.0f;
    public bool moveOnXAxis = true;
    public bool moveOnYAxis = true;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float newPosition = Mathf.PingPong(Time.time * speed, distance);

        // 사용자가 선택한 축으로만 이동
        Vector3 newPositionVector = new Vector3(
            moveOnXAxis ? initialPosition.x + newPosition : transform.position.x,
            moveOnYAxis ? initialPosition.y + newPosition : transform.position.y,
            initialPosition.z
        );

        transform.position = newPositionVector;
    }
}
