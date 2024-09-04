using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ObjectLayer �� ��ȣ�ۿ��ϴ� Ŭ������, Object �� �����ϰ� �÷��̾ ������Ʈ�� ��/�ڿ� �ִ� �Ϳ� ���� ������Ʈ�� sortingOrder �� ������
/// </summary>
public class ObjectDetector : MonoBehaviour
{
    float radius = 0.2f;
    [SerializeField]float boundary = 0.4f;
    float distance = 0f;

    int objectLayerMask = (1 << 6);
    
    ObjectLayer objectLayer;


    private void Update()
    {
        headHit();
        BodyHit();
    }

    void headHit()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y + boundary), radius, transform.up, distance, objectLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null)
            {
                objectLayer = hits[i].collider.gameObject.GetComponent<ObjectLayer>();

                if (objectLayer != null)
                    objectLayer.SetLayer(false);
            }
        }
    }

    void BodyHit()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y - boundary), radius, transform.up, distance, objectLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null)
            {
                objectLayer = hits[i].collider.gameObject.GetComponent<ObjectLayer>();

                if (objectLayer != null)
                    objectLayer.SetLayer(true);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + boundary, 0), radius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - boundary, 0), radius);
    }
}
