using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class KimbapMaker : Maker
{
    InteractableObject interactableObject;

    private new void Awake()
    {
        base.Awake();

        interactableObject = GetComponent<InteractableObject>();
        interactableObject.OnInteract += Make;
    }
    
    public void Make(bool isInteracted)
    {
        if (!isInteracted || FoodStacker.curFood == null)
        {
            StopMake();
            FoodStacker.Stop();

            Done();

            return;
        }

        OnKeyDown += FoodStacker.StackIngredients;
        OnClear += FoodStacker.Complete; 

        OnClear += Done;

        Minigame_Keyboard(FoodStacker.curFood.Ingredients.Count + 1); //재료 수 + 1 해야 완성되게
    }

    public void Done()
    {
        OnKeyDown -= FoodStacker.StackIngredients;
        OnClear -= FoodStacker.Complete;
    }
    
}
