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
    float boundary = 0.3f;
    float distance = 0f;

    int objectLayerMask = (1 << 6);

    private void Update()
    {
        headHit();
        BodyHit();
    }

    void headHit()
    {
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y + boundary), radius, transform.up, distance, objectLayerMask);

        if (hit.collider != null)
            hit.collider.gameObject.GetComponent<ObjectLayer>().SetLayer(false);
    }

    void BodyHit()
    {
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y - boundary), radius, transform.up, distance, objectLayerMask);

        if (hit.collider != null)
            hit.collider.gameObject.GetComponent<ObjectLayer>().SetLayer(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + boundary, 0), radius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - boundary, 0), radius);
    }
}
