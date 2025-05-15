using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    private bool isInit;
    public Vector2 targetPosition1; // Ŀ��λ�ã�ʹ�� Vector2��
    public Vector2 targetPosition2;
    public float speed = 500.0f; // �ƶ��ٶ�
    public float returnSpeed = 500.0f; // �����ٶ�
    public float collisionDistance = 1.0f; // ײ������
    public bool isFur;//�Ƿ�������Ŀ��
    public bool isDou;
    private Vector2 targetPosition;

    public GameObject contorller;
    public int id;
    public bool ispla;
    private gamecontroller con;

    private Vector2 startPosition; // ��ʼλ�ã�ʹ�� Vector2��
    public bool isReturning = false; // �Ƿ����ڷ���
    private bool sec = false;//�Ƿ��ǵڶ���ײ��
    private bool sec1 = false;

    void Start()
    {
        targetPosition = targetPosition1;
        startPosition = transform.position; // ������ʼλ��
        con = contorller.GetComponent<gamecontroller>();
        isInit = true;
    }

    void Update()
    {
        if(!isInit)
        {
            targetPosition = targetPosition1;
            startPosition = transform.position;
            isInit = true;
        }
        if (!isFur)
        {
            con.DefMove(ispla, id);
        }
        else
        {
            if (!sec)
                con.DefMove(ispla, id - 1);
            else
                con.DefMove(ispla, id + 1);
        }
        if (!isReturning)
        {
            // �ƶ���Ŀ��λ��
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;

            // ����Ƿ񵽴�Ŀ��λ��
            if (Vector2.Distance(transform.position, targetPosition) < collisionDistance)
            {
                isReturning = true; // ��ʼ����
            }
        }
        else
        {
            // ������ʼλ��
            transform.position = Vector2.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);
            if (transform.position.y == startPosition.y && isFur && !sec)
            {
                sec = true;
                targetPosition = targetPosition2;
                isReturning = false;
            }
            else if (transform.position.y == startPosition.y)
            {
                isReturning = false;
                targetPosition = targetPosition1;
                sec = false;
                if (!isDou || sec1)
                {
                    enabled = false;
                    con.enabled = true;
                }
                else sec1 = true;
                isInit = false;
            }
        }
    }
}