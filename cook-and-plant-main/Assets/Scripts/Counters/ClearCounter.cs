using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;


    // private void Update() {
    //     if (testing && Input.GetKeyDown(KeyCode.T))
    //     {
    //         if (kitchenObject != null)
    //         {
    //             kitchenObject.SetKitchenObjectParent(secondClearCounter);
    //         }
    //     }
    // }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player holding a plate
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO())){
                    GetKitchenObject().DestroySelf();
                    }
                } else
                {
                    //player is not carrying plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter holding a Plate
                    if (plateKitchenObject.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().DestroySelf();
                    }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
