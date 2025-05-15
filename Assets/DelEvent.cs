using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DelEvent : MonoBehaviour
{
    public GameObject controller;
    public float duration = 0.5f; // ��ʧ����ʱ��
    private SpriteRenderer spriteRenderer; // SpriteRenderer ����
    private gamecontroller con;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��ȡ SpriteRenderer ���
        if(spriteRenderer==null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        StartCoroutine(FadeOut()); // ����Э��
        con = controller.GetComponent<gamecontroller>();
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            // ����͸����
            float alpha = 1.0f - (elapsedTime / duration);
            color.a = alpha;
            spriteRenderer.color = color;
            elapsedTime += Time.deltaTime; // �ۼ�ʱ��
            yield return null; // �ȴ���һ֡
        }
        // ������ɺ����ٶ���
        Destroy(gameObject);
        con.enabled = true;
    }
}
