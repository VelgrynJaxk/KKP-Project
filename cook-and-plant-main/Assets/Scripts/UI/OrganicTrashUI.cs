using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrganicTrashUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    private string currentTrash;
    private string maxTrash;

    private void Start() 
    {
        maxTrash = KitchenGameManager.Instance.GetMaxOrganicTrash().ToString();
    }
    
    private void Update() 
    {
        currentTrash = OrganicTrashCounter.Instance.GetCurrentOrganicTrash().ToString();

        countText.text = currentTrash + "/" + maxTrash;
    }
}
