using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static TrashCounter Instance { get; private set; }
    [SerializeField] private TrashRecipeSO trashRecipeSOArray;
    [SerializeField] private int maxTrash = 10;

    public event EventHandler OnPlayerTrashedObject;
    private int currentAnorganicTrash = 0;

    private void Awake() 
    {
        Instance = this;  
    }


    public override void Interact(Player player)
    {
        if (currentAnorganicTrash <= maxTrash)
        {
         if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    TrashRecipeSO trashRecipeSO = GetTrashRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    AddAnorganicTrash();

                    OnPlayerTrashedObject?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

     public override void InteractAlternate(Player player)
    {
        if (currentAnorganicTrash  > 0)
        {
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                TrashRecipeSO trashRecipeSO = GetTrashRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                KitchenObject.SpawnKitchenObject(trashRecipeSO.output, player);

                currentAnorganicTrash = 0;
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        TrashRecipeSO trashRecipeSO = GetTrashRecipeSOWithInput(inputKitchenObjectSO);
        return trashRecipeSO != null;
    }

    private TrashRecipeSO GetTrashRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        for (int i = 0; i < trashRecipeSOArray.input.Count; i++)
            {
                if (trashRecipeSOArray.input[i] == inputKitchenObjectSO)
                {
                    return trashRecipeSOArray;
                }
            }

        return null;
    }

    public int GetCurrentAnorganicTrash()
    {
        return currentAnorganicTrash;
    }

    public int GetCurrentMaxAnorganicTrash()
    {
        return maxTrash;
    }

    public void AddAnorganicTrash()
    {
        currentAnorganicTrash += 1;
    }
}