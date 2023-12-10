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

        // ����ڰ� ������ �����θ� �̵�
        Vector3 newPositionVector = new Vector3(
            moveOnXAxis ? initialPosition.x + newPosition : transform.position.x,
            moveOnYAxis ? initialPosition.y + newPosition : transform.position.y,
            initialPosition.z
        );

        transform.position = newPositionVector;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �÷��̾��� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������ �ڽ����� �÷��̾ ����ϴ�.
            collision.transform.parent = platformTransform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �浹�� ���� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾ ������ �ڽĿ��� �����մϴ�.
            collision.transform.parent = null;
        }
    }
}
