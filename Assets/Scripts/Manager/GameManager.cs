using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(0, 1000)]
    [SerializeField] int initCoin = 50;
    [Header("손님 출현 최소, 최대 시간")]
    [Range(1f, 20f)]
    [SerializeField] float minTime = 10f;
    [Range(2f, 40f)]
    [SerializeField] float maxTime = 20f;
    [Header("손님 기분에 따른 인기도 감소, 증가")]
    [Range(-10, 0)]
    [SerializeField] int halfAnger = -2;
    [Range(-10, 0)]
    [SerializeField] int fullAnger = -3;
    [Range(0, 20)]
    [SerializeField] int happy = 2;
    [Header("분노 타이머")]
    [Range(0.5f, 10f)]
    [SerializeField] float halfAngerTime = 15f;
    [Range(0.5f, 20f)]
    [SerializeField] float fullAngerTime = 20f;

    [ContextMenu("값 초기화")]
    public void InitValues()
    {
        initCoin = 50;
        minTime = 10f;
        maxTime = 20f;
        halfAnger = -2;
        fullAnger = -3;
        happy = 2;
        halfAngerTime = 15f;
        fullAngerTime = 20f;
    }


    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    static DataManager data;
    public static DataManager Data { get { return data; } }

    static FlowManager flow;
    public static FlowManager Flow { get { return flow; } }

    static LevelManager level;
    public static LevelManager Level { get { return level; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        InitManagers();
    }

    void InitManagers()
    {
        if (data == null)
        {
            data = CreateGameObject("DataManager").AddComponent<DataManager>();
            data.Init(initCoin);
        }

        if (flow == null)
            flow = CreateGameObject("FlowManager").AddComponent<FlowManager>();

        if (level == null)
        {
            level = CreateGameObject("LevelManager").AddComponent<LevelManager>();
            level.Init(minTime, maxTime, halfAnger, fullAnger, happy, halfAngerTime, fullAngerTime);
        }
    }

    GameObject CreateGameObject(string name)
    {
        GameObject newGO = new GameObject();
        newGO.name = name;
        newGO.transform.parent = transform;

        return newGO;
    }

}
