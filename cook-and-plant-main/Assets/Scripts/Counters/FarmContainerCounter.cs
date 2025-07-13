using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private int maxObject;
    [SerializeField] private int curObject = 0;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && curObject != 0)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            curObject -= 1;
        }
        else if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().GetKitchenObjectSO() == kitchenObjectSO && curObject != maxObject)
            {
                curObject += 1;
                player.GetKitchenObject().DestroySelf();
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

