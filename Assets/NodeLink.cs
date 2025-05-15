// NodeLink.cs �ڵ��������
using UnityEngine;
using System.Collections.Generic;

public class NodeLink : MonoBehaviour
{
    [Header("�ڵ��ʶ")]
    public string nodeID = "Grano";

    [Header("��������")]
    public Color lineColor = Color.cyan; // ����ͼԪ������������ɫ
    [Range(0.1f, 2f)]
    public float widthMultiplier = 1f;   // �ڵ�ר���߿�ϵ��

    public List<Transform> connectedNodes = new List<Transform>();
}
