using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(FoodStacker))]
public class KimbapMaker : Maker
{
    FoodStacker foodStacker;
    InteractableObject interactableObject;

    private void Awake()
    {
        foodStacker = GetComponent<FoodStacker>();
        interactableObject = GetComponent<InteractableObject>();
        interactableObject.OnInteract += Make;
    }

    public override void Make()
    {
        foodStacker.InitFood(Food.FoodType.OriginalKimbap);

        OnKeyDown += foodStacker.StackIngredients;
        OnClear += foodStacker.Complete; 

        Minigame_Keyboard(foodStacker.originalKimbap.Ingredients.Count);
    }

    
}
