using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryCombiner : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] Food[] foods;

    Dictionary<Food, int> foodIngredients = new Dictionary<Food, int>(); //����, �κ��丮�� �ִ� ��� ��
    Dictionary<Food, bool> makeableFoods = new Dictionary<Food, bool>(); //����, ���� �� �ִ� �� ����

    //UI
    Slot[] slots;
    int slotSize = 10;
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform slotParent;

    [SerializeField] InteractableObject interactable;
    [SerializeField] GameObject body;

    int slotIndex;

    [SerializeField] Maker maker;


    private void Awake()
    {
        interactable.OnInteract += Show;

        Init();
    }

    void Init()
    {
        slots = new Slot[slotSize];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab);
            slots[i].transform.parent = slotParent;
            slots[i].gameObject.SetActive(false);
        }

        body.SetActive(false);
    }

    void Show(bool isInterated)
    {
        body.SetActive(isInterated);

        slotIndex = 0; //���� �ʱ�ȭ

        Combine();
        SetMakeable();
    }

    void Combine()
    {
        for (int i = 0; i < foods.Length; i++)
            foodIngredients[foods[i]] = 0; //�ʱ�ȭ

        foreach (Ingredient element in inventory.Slot.Keys) //�κ��丮�� �ִ� ��ᰡ
        {
            for (int i = 0; i < foods.Length; i++) //�� ������
            {
                for (int j = 0; j < foods[i].Ingredients.Count; j++) //��� �� �ϳ�����
                {
                    if (foods[i].Ingredients[j] == element)
                    {
                        if (!foodIngredients.ContainsKey(foods[i]))
                            foodIngredients.Add(foods[i], 0);

                        foodIngredients[foods[i]]++;
                    }
                }
            }
        }
    }

    void SetMakeable()
    {
        for (int i = 0; i < foods.Length; i++)
            makeableFoods[foods[i]] = false; //�ʱ�ȭ

        foreach (Food element in foodIngredients.Keys)
        {
            if (element.Ingredients.Count == foodIngredients[element]) //��ᰡ ��� ������
            {
                if (!makeableFoods.ContainsKey(element))
                    makeableFoods.Add(element, false);

                makeableFoods[element] = true; //���� �� �ִ� ���� true

                ShowMakeable(element);
            }
        }
    }

    Food _makeableFood;
    Food makeableFood { get { return _makeableFood; } 
        set 
        {
            _makeableFood = value;
            StartMake();
        }
    }

    void ShowMakeable(Food makeableFood)
    {
        slots[slotIndex].sprite.sprite = makeableFood.FoodModel;
        slots[slotIndex].nameText.text = makeableFood.FoodName;
        slots[slotIndex].gameObject.SetActive(true);

        Button button = slots[slotIndex].GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { this.makeableFood = makeableFood; }) ;

        slotIndex++;
    }

    void StartMake()
    {
        maker.StartMake(makeableFood);
    }
}