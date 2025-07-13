using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class PlantRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO initialPlant;
    public KitchenObjectSO plant;
    public KitchenObjectSO output;
    public float plantTimerMax;
}
