using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    [Header("�ڵ�����")]
    public List<Transform> connectedNodes = new List<Transform>();  // �����ӵĽڵ�

    [Header("�Ӿ�����")]
    public Color highlightColor = Color.yellow;    // ��ͣ������ɫ
    public float glowIntensity = 1.2f;             // ����ǿ��

    private Material originalMaterial;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        AutoDetectConnections();
    }

    void AutoDetectConnections()
    {
        // �Զ����2�׷�Χ�ڵ����ڽڵ�
        Collider[] nodes = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider col in nodes)
        {
            if (col.CompareTag("Node") && col.transform != transform)
            {
                connectedNodes.Add(col.transform);
            }
        }
    }

    void OnMouseEnter()
    {
        if (!BackgroundMovement.Instance.isMoving)
        {
            GetComponent<Renderer>().material.color = highlightColor * glowIntensity;
        }
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalMaterial.color;
    }

    void OnDrawGizmosSelected()
    {
        // ����������
        Gizmos.color = Color.cyan;
        foreach (Transform node in connectedNodes)
        {
            if (node != null)
                Gizmos.DrawLine(transform.position, node.position);
        }
    }
}