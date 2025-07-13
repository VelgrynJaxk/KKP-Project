using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicTrashCounter : BaseCounter
{
    public int PointMin;
    public static OrganicTrashCounter Instance { get; private set; }

    public static event EventHandler OnOrganicObjectTrashed;
    public static event EventHandler OnWrongObjectTrashed;
    public event EventHandler OnPlayerTrashed;

    [SerializeField] private TrashRecipeSO trashRecipeSOArray;

    private int currentOrganicTrash = 0;
    private int maxTrash = 0;

    private void Start() 
    {
        maxTrash =  KitchenGameManager.Instance.GetMaxOrganicTrash();
    }

    private void Awake() 
    {
        Instance = this;   
    }

    new public static void ResetStaticData()
    {
        OnOrganicObjectTrashed = null;
        OnWrongObjectTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (currentOrganicTrash < maxTrash)
        {
         if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    TrashRecipeSO trashRecipeSO = GetTrashRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    AddOrganicTrash();

                    OnOrganicObjectTrashed?.Invoke(this, EventArgs.Empty);
                    
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
        if (currentOrganicTrash  > 0)
        {
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                TrashRecipeSO trashRecipeSO = GetTrashRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                KitchenObject.SpawnKitchenObject(trashRecipeSO.output, player);

                currentOrganicTrash = 0;
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

    public int GetCurrentOrganicTrash()
    {
        return currentOrganicTrash;
    }

    public void AddOrganicTrash()
    {
        currentOrganicTrash += 1;
    }
}
