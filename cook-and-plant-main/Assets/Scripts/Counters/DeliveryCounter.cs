using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance { get; private set; }

    [SerializeField] private List<KitchenObjectSO> trashObjectSO;

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {        
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);        
                player.GetKitchenObject().DestroySelf();      
                KitchenObject.SpawnKitchenObject(trashObjectSO[UnityEngine.Random.Range(0, trashObjectSO.Count)], player);     
            }            
        }
    }
}
