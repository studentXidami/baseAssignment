using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeDuration = 0.5f; // �𶯳���ʱ��
    public float shakeMagnitude = 0.1f; // ��ǿ��
    private Vector3 initialPosition; // ��ʼλ��
    private float elapsedTime; // �ѹ�ȥ��ʱ��

    void Start()
    {
        initialPosition = transform.position; // �����ʼλ��
    }

    void Update()
    {
        if (elapsedTime < shakeDuration)
        {
            // �����µ�λ��
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = initialPosition + new Vector3(x, y, 0);
            elapsedTime += Time.deltaTime; // �ۼ�ʱ��
        }
        else
        {
            // �ָ���ʼλ��
            transform.position = initialPosition;
            this.enabled = false;
        }
    }
}
