using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Trashcan : MonoBehaviour
{
    Holder holder;

    private void Start()
    {
        holder = GetComponent<Holder>();
        holder.OnHold += DestroyObject;
    }

    void DestroyObject(GameObject go)
    {
        Destroy(go);
    }
}