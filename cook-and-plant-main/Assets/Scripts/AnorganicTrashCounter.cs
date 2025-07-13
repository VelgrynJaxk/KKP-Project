using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnorganicTrashCounter : BaseCounter
{
    public int PointMin;
    public static AnorganicTrashCounter Instance { get; private set; }

    public static event EventHandler OnAnorganicObjectTrashed;
    public static event EventHandler OnWrongObjectTrashed;
    public event EventHandler OnPlayerTrashed;

    [SerializeField] private TrashRecipeSO trashRecipeSOArray;

    private int currentAnorganicTrash = 0;
    private int maxTrash = 0;

    private void Start() 
    {
        maxTrash =  KitchenGameManager.Instance.GetMaxAnorganicTrash();
    }

    private void Awake() 
    {
        Instance = this;  
    }

    new public static void ResetStaticData()
    {
        OnAnorganicObjectTrashed = null;
        OnWrongObjectTrashed = null;
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

                    OnAnorganicObjectTrashed?.Invoke(this, EventArgs.Empty);
                    
                    OnPlayerTrashed?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnWrongObjectTrashed?.Invoke(this, EventArgs.Empty);
                    player.GetKitchenObject().DestroySelf();
                    if (GameOverUI.point >= PointMin)
                    {
                        GameOverUI.point -= PointMin;
                    }
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

    public void AddAnorganicTrash()
    {
        currentAnorganicTrash += 1;
    }
}
