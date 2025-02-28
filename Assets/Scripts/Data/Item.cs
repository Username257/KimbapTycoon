using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] string itemName_eng;
    public string ItemName_eng { get { return itemName_eng; } }

    [SerializeField] Sprite model;
    public Sprite Model { get { return model; } }

    [SerializeField] int price;
    public int Price { get { return price; } }
}
