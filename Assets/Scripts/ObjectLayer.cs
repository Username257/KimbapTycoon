using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectDetector �� ��ȣ�ۿ��ϴ� Ŭ������, �÷��̾ ������Ʈ�� ��/�� ����ʿ� �ִ��Ŀ� ���� ������Ʈ�� sortingOrder �� ���� ��
/// </summary>
public class ObjectLayer : MonoBehaviour
{
    [SerializeField] Renderer sprite;
    [SerializeField] bool hasPriority;

    int playerSortingOrder = 5;

    public void SetLayer(bool setTop)
    {
        if (setTop)
            sprite.sortingOrder = hasPriority ? playerSortingOrder + 2 : playerSortingOrder + 1;
        else
            sprite.sortingOrder = hasPriority ? playerSortingOrder - 1 : playerSortingOrder - 2;
    }
}
