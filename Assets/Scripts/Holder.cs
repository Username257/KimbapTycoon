using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class Holder : MonoBehaviour
{
    [SerializeField] Transform holdPlace;
    public GameObject holdingObj;
    public UnityAction<GameObject> OnHold;

    public void Hold(GameObject go)
    {
        go.transform.parent = holdPlace;

        //z ���� ���� �� ��� �ִ� ������Ʈ�� ���� ���õ� �� �ְ�
        go.transform.localPosition = new Vector3(0f, 0f, -0.1f);

        holdingObj = go;
        OnHold?.Invoke(go);
    }    
}
