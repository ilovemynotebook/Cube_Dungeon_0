using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 5.0f;
    public bool moveOnXAxis = true;
    public bool moveOnYAxis = true;

    private Transform platformTransform;
    private Vector3 initialPosition;

    private void Start()
    {
        platformTransform = transform;
        initialPosition = platformTransform.position;
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
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 플레이어인 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // 발판의 자식으로 플레이어를 만듭니다.
            collision.transform.parent = platformTransform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌이 끝난 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어를 발판의 자식에서 해제합니다.
            collision.transform.parent = null;
        }
    }
}
