using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TrashRecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> input;
    public KitchenObjectSO output;

}
