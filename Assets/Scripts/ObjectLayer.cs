using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectDetector �� ��ȣ�ۿ��ϴ� Ŭ������, �÷��̾ ������Ʈ�� ��/�� ����ʿ� �ִ��Ŀ� ���� ������Ʈ�� sortingOrder �� ���� ��
/// </summary>
public class ObjectLayer : MonoBehaviour
{
    [SerializeField] Renderer sprite;

    int playerSortingOrder = 5;

    /// <summary>
    /// �÷��̾��� �Ӹ��� �浹���� ��� false, �÷��̾��� ���� �浹���� ��� true
    /// </summary>
    /// <param name="setTop"></param>
    public void SetLayer(bool setTop)
    {
        if (setTop)
        {
            sprite.sortingOrder = playerSortingOrder + 1;
        }
        else
        {
            sprite.sortingOrder = playerSortingOrder - 1;
        }
    }
}
