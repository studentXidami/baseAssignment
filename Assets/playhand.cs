using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class playhand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("��ק����")]
    public GameObject TargetArea1; // Ŀ������1
    public GameObject TargetArea2; // Ŀ������1
    public GameObject TargetArea3; // Ŀ������1
    public GameObject TargetArea4; // Ŀ������1
    public MonsterCard card;
    public GameObject showedcard;
    public GameObject controller;
    public GameObject player;
    public float snapDistance = 50f; // ��������(����)
    public bool isPlaced = false;

    private RectTransform targetArea1;
    private RectTransform targetArea2;
    private RectTransform targetArea3;
    private RectTransform targetArea4;
    private RectTransform rectTransform;
    private Image image;
    private Vector2 originalPosition;
    private bool isInTargetArea = false;
    public int idoftarget;
    private bool allowDragging = true;
    private gamecontroller con;

    [Header("��ק����")]
    public bool limitDragArea = true;
    public RectTransform dragBoundary;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        originalPosition = rectTransform.anchoredPosition;
    }
    void Start ()
    {
        targetArea1 = TargetArea1.GetComponent<RectTransform>();
        targetArea2 = TargetArea2.GetComponent<RectTransform>();
        targetArea3 = TargetArea3.GetComponent<RectTransform>();
        targetArea4 = TargetArea4.GetComponent<RectTransform>();
        con = controller.GetComponent<gamecontroller>();
    }

    // ��ʼ��ק
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!allowDragging|| !player.GetComponent<playerbout>().selected) return;
        // ���������������ק��ʼʱ���߼�
        originalPosition = rectTransform.anchoredPosition;
    }

    // ��ק������
    public void OnDrag(PointerEventData eventData)
    {
        if (!allowDragging || !player.GetComponent<playerbout>().selected) return;
        // ����Ļ����ת��ΪCanvas�ֲ�����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        rectTransform.anchoredPosition = localPoint;

        if (limitDragArea && dragBoundary != null)
        {
            // �����ڱ߽������
            Vector2 min = dragBoundary.rect.min + dragBoundary.anchoredPosition;
            Vector2 max = dragBoundary.rect.max + dragBoundary.anchoredPosition;

            rectTransform.anchoredPosition = new Vector2(
                Mathf.Clamp(localPoint.x, min.x, max.x),
                Mathf.Clamp(localPoint.y, min.y, max.y));
        }
        else
        {
            rectTransform.anchoredPosition = localPoint;
        }

    }
    private void SetPlayedcards()
    {
        playerbout pla = player.GetComponent<playerbout>();
        Attack a = showedcard.GetComponent<Attack>();
        a.isFur = false;
        a.id = idoftarget;
        a.ispla = true;
        a.targetPosition1 = con.firstv[idoftarget];
        pla.handcards.Remove(card);
        con.playercards[idoftarget] = card;
        con.showedpla[idoftarget] = showedcard;
        pla.showedCards.Remove(showedcard);
        pla.CardsMove();
        if (con.idofThi != -1 && con.firstlinecards[idoftarget] == null)
        {
            con.ThiMove(idoftarget);
        }
    }

    private bool MeetCon()
    {
        int num = 0;
        for (int i = 0; i < 4; i++)
        {
            if (con.isUsed[i] == true)
                num++;
        }
        bool a = (num >= card.sacrifice);
        if (a)
        {
            int n = 0;
            for (int i = 0; n < card.sacrifice; i++)
            {
                if (con.isUsed[i] == true)
                {
                    con.isUsed[i] = false;
                    con.playercards[i] = null;
                    DelEvent d = con.showedpla[i].GetComponent<DelEvent>();
                    d.enabled = true;
                    con.showedpla[i] = null;
                    con.isEmpty[i] = true;
                    n++;
                }
            }
        }
        return a;
    }
    // ������ק
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!allowDragging || !player.GetComponent<playerbout>().selected) return;

        // ����Ƿ���Ŀ��������
        CheckIfInTargetArea();
        if (isInTargetArea && MeetCon())
        {
            // �ɹ��ŵ�Ŀ������
            SnapToTarget();
            SetPlayedcards();
            Debug.Log("�ɹ��ŵ�Ŀ��λ��!");
            allowDragging = false;
            isPlaced = true;
        }
        else
        {
            // δ�ŵ�Ŀ�����򣬷���ԭ��
            ReturnToOriginalPosition();
            Debug.Log("δ�ŵ�Ŀ��λ�ã��ѷ���ԭ��");
        }
    }

    // ����Ƿ���Ŀ��������
    private void CheckIfInTargetArea()
    {
        gamecontroller con = controller.GetComponent<gamecontroller>();
        if (con.isEmpty[0])
        {
            float distance1 = Vector2.Distance(rectTransform.anchoredPosition, targetArea1.anchoredPosition);
            if (distance1 <= snapDistance)
            {
                isInTargetArea = true;
                idoftarget = 0;
                return;
            }
        }
        if (con.isEmpty[1])
        {
            float distance2 = Vector2.Distance(rectTransform.anchoredPosition, targetArea2.anchoredPosition);
            if (distance2 <= snapDistance)
            {
                isInTargetArea = true;
                idoftarget = 1;
                return;
            }
        }
        if (con.isEmpty[2])
        {
            float distance3 = Vector2.Distance(rectTransform.anchoredPosition, targetArea3.anchoredPosition);
            if (distance3 <= snapDistance)
            {
                isInTargetArea = true;
                idoftarget = 2;
                return;
            }
        }
        if (con.isEmpty[3])
        {
            float distance4 = Vector2.Distance(rectTransform.anchoredPosition, targetArea4.anchoredPosition);
            if (distance4 <= snapDistance)
            {
                isInTargetArea = true;
                idoftarget = 3;
                return;
            }
        }
        else
        {
            isInTargetArea = false;
            idoftarget = 0;
        }
    }

    // ������Ŀ��λ��
    private void SnapToTarget()
    {
        switch (idoftarget)
        {
            case 0:
                rectTransform.anchoredPosition = targetArea1.anchoredPosition;
                con.isEmpty[0] = false;
                break;
            case 1:
                rectTransform.anchoredPosition = targetArea2.anchoredPosition;
                con.isEmpty[1] = false;
                break;
            case 2:
                rectTransform.anchoredPosition = targetArea3.anchoredPosition;
                con.isEmpty[2] = false;
                break;
            case 3:
                rectTransform.anchoredPosition = targetArea4.anchoredPosition;
                con.isEmpty[3] = false;
                break;
        }
    }

    // ����ԭʼλ��
    private void ReturnToOriginalPosition()
    {
        // ʹ�ò�ֵ�����÷��ظ�ƽ��
        StartCoroutine(SmoothReturn());
    }

    // ƽ�����ض���
    private System.Collections.IEnumerator SmoothReturn()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;

        while (elapsed < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, originalPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
