using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

[RequireComponent(typeof(Interactor))]
[RequireComponent(typeof(Holder))]
public class Server: MonoBehaviour
{
    Interactor interactor;

    public InteractableObject serveTarget;
    public InteractableObject holdTarget;

    Holder myholder;

    public UnityAction OnServe;

    public void Awake()
    {
        interactor = GetComponent<Interactor>();
        myholder = GetComponent<Holder>();

        interactor.OnTryInteract += DoHoldOrServe;
    }

    void DoHoldOrServe(InteractableObject obj)
    {
        Holder holder = obj.GetComponent<Holder>();

        if (holder == null)
        {
            holdTarget = null;
            serveTarget = null;
            return;
        }

        if (holder.alreadyHold && myholder.holdingObj == null)
        {
            holdTarget = obj;
            TryHold();
        }
        else if (!holder.alreadyHold && myholder.holdingObj != null)
        {
            serveTarget = obj;
            TryServe();
        }
    }

    void TryHold()
    {
        if (myholder.holdingObj != null)
            return;

        /*
        if (!obj.canBeHolded)
            return;
        */

        Holder holder = holdTarget.GetComponent<Holder>();

        if (holder == null)
            return;

        holder.alreadyHold = false;

        holdTarget.OnInteract += Hold;
    }

    void Hold(bool isInteracted)
    {
        if (!isInteracted)
            return;

        if (holdTarget == null)
            return;

        Holder targatHolder = holdTarget.GetComponent<Holder>();

        if (targatHolder == null)
            return;

        myholder.Hold(holdTarget.GetComponent<Holder>().Give());
        myholder.alreadyHold = true;

        holdTarget.OnInteract -= Hold;
        GetComponent<Collider2D>().enabled = false; //Hold �ϸ� �ٽ� �ٷ� Serve �� �� �ְ�
        GetComponent<Collider2D>().enabled = true;

        /*
        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //������� �� �ٸ� �繰�� Interact �� �ǰ�
        if (holdTargetCol != null)
            holdTargetCol.enabled = false;
        */
    }

    void TryServe()
    {
        /*
        if (obj.canBeHolded)
            return;
        */

        if (serveTarget == null)
            return;

        if (myholder.holdingObj == null)
            return;

        serveTarget.OnInteract += Serve;
    }

    void Serve(bool isInteracted)
    {
        if (!isInteracted)
            return;

        if (serveTarget == null)
            return;

        serveTarget.GetComponent<Holder>().Hold(myholder.Give());
        OnServe?.Invoke();

        serveTarget.OnInteract -= Serve;

        GetComponent<Collider2D>().enabled = false; //Serve �ϸ� �ٽ� �ٷ� Hold �� �� �ְ�
        GetComponent<Collider2D>().enabled = true;

        /*
        Collider2D holdTargetCol = holdTarget.GetComponent<Collider2D>(); //�ٸ� �繰 ���� �������� �� �ٽ� �� �� �ֵ��� �ݶ��̴� ���ֱ�
        if (holdTargetCol != null)
            holdTargetCol.enabled = true;
        */
    }
}
