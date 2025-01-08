using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public void Init(float minTime, float maxTime, int halfAnger, int fullAnger, int happy, float halfAngerTime, float fullAngerTime)
    {
        visitTime_min = minTime;
        visitTime_max = maxTime;

        popularity_halfAnger = halfAnger;
        popularity_fullAnger = fullAnger;
        popularity_happy = happy;

        this.halfAngerTime = halfAngerTime;
        this.fullAngerTime = fullAngerTime;
    }

    //popularity
    int popularity = 0;
    public int Popularity { get { return popularity; } }

    int popularity_halfAnger = -2;
    int popularity_fullAnger = -3;
    int popularity_happy = 2;

    public UnityAction OnPopularityChanged;

    public void SetPopularity(Customer.Emotions customersEmotion)
    {
        switch (customersEmotion)
        {
            case Customer.Emotions.None:
                break;
            case Customer.Emotions.HalfAnger:
                popularity += popularity_halfAnger;
                break;
            case Customer.Emotions.FullAnger:
                popularity += popularity_fullAnger;
                break;
            case Customer.Emotions.Happy:
                popularity += popularity_happy;
                break;
        }

        if (popularity < 0)
            popularity = 0;

        OnPopularityChanged?.Invoke();
        SetVisitTime();
    }

    //customerVisitTime;
    [SerializeField] float visitTime_min;
    public float VisitTime_min { get { return visitTime_min; } }

    [SerializeField] float visitTime_max;
    public float VisitTime_max { get { return visitTime_max; } }

    void SetVisitTime()
    {
        int temp = -popularity;
        
        //�ּڰ��� �α⵵ 10�� ���� ������ 1�ʾ� �پ���
        visitTime_min += temp * 0.1f;
        //�ִ��� �ּڰ��� �� ���� ������ ���̰�, �ּڰ��� Ŭ ���� ������ �ø���
        visitTime_max = visitTime_min * 2f;
    }

    //customer's patient
    float halfAngerTime = 15f;
    public float HalfAngerTime { get { return halfAngerTime; } }
    float fullAngerTime = 20f;
    public float FullAngerTime { get { return fullAngerTime; } }

}
