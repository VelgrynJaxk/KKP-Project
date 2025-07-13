using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private TMP_Text pointText;

    private void Awake()
        {
            recipeTemplate.gameObject.SetActive(false);
        }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnREcipeSpawned;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnREcipeCompleted;

        UpdateVisual();
    }

    private void Update()
    {
        pointText.text = GameOverUI.point.ToString();
    }

    private void DeliveryManager_OnREcipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnREcipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
       foreach ( RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
       {
            Transform recipeTransform =  Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
       }
    }        
}
