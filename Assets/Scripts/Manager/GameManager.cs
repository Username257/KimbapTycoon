using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("â�� ���")]
    [Range(0, 1000)]
    [SerializeField] int initCoin = 50;
    [Header("�մ� ���� �ּ�, �ִ� �ð�")]
    [Range(1f, 20f)]
    [SerializeField] float minTime = 10f;
    [Range(2f, 40f)]
    [SerializeField] float maxTime = 20f;
    [Header("�մ� ��п� ���� �α⵵ ����, ����")]
    [Range(-10, 0)]
    [SerializeField] int halfAnger = -2;
    [Range(-10, 0)]
    [SerializeField] int fullAnger = -3;
    [Range(0, 20)]
    [SerializeField] int happy = 2;
    [Header("�г� Ÿ�̸�")]
    [Range(0.5f, 10f)]
    [SerializeField] float halfAngerTime = 15f;
    [Range(0.5f, 20f)]
    [SerializeField] float fullAngerTime = 20f;

    [ContextMenu("�� �ʱ�ȭ")]
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

        SceneManager.sceneLoaded += DetectSceneChange;
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

    void DestroyManagers()
    {
        if (data != null)
            Destroy(data);
        
        if (flow != null)
            Destroy(flow);

        if (level != null)
            Destroy(level);
    }

    GameObject CreateGameObject(string name)
    {
        GameObject newGO = new GameObject();
        newGO.name = name;
        newGO.transform.parent = transform;

        return newGO;
    }

    private void DetectSceneChange(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene")
            DestroyManagers();
        else if (scene.name == "GameScene")
            InitManagers();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= DetectSceneChange;
    }
}
