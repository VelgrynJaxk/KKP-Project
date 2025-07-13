using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnorganicTrashUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    private string currentTrash;
    private string maxTrash;

    private void Start() 
    {
        maxTrash = KitchenGameManager.Instance.GetMaxAnorganicTrash().ToString();
    }
    
    private void Update() 
    {
        currentTrash = AnorganicTrashCounter.Instance.GetCurrentAnorganicTrash().ToString();

        countText.text = currentTrash + "/" + maxTrash;
    }
}
