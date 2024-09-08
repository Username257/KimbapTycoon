using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class InventoryCombiner : MonoBehaviour
{
    Inventory inventory;

    [SerializeField] Food[] foods;

    Dictionary<Food, int> foodIngredients = new Dictionary<Food, int>(); //����, �κ��丮�� �ִ� ��� ��
    Dictionary<Food, bool> makeableFoods = new Dictionary<Food, bool>(); //����, ���� �� �ִ� �� ����

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public void Combine()
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

        SetMakeable();
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
            }
        }
    }
}
