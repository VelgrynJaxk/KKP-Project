using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private const string saveLevel = "SAVELEVEL";
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI trashPointText;
    [SerializeField] private GameObject stageClearTextGameObject;
    [SerializeField] private GameObject gameoverTextGameObject;
    [SerializeField] private GameObject labelRecipeDeliveredTextGameObject;
    [SerializeField] private GameObject recipesDeliveredTextGameObject;
    [SerializeField] private GameObject labelTrashPointTextGameObject;
    [SerializeField] private GameObject trashPointTextGameObject;
    [SerializeField] private GameObject nextLevelButtonGameObject;
    [SerializeField] private GameObject loseTextGameObject;
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    public static int point;
    public float idLevel;

     private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        
        Hide();
    }

    private void Awake() 
    {
        retryButton.onClick.AddListener(() =>{
            Loader.Reload();
        });
        nextLevelButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level2);
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e) 
    {
        if(KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            if (KitchenGameManager.Instance.IsPlayerLose())
            {
                stageClearTextGameObject.SetActive(false);
                labelRecipeDeliveredTextGameObject.SetActive(false);
                recipesDeliveredTextGameObject.SetActive(false);
                labelTrashPointTextGameObject.SetActive(false);
                trashPointTextGameObject.SetActive(false);
                nextLevelButtonGameObject.SetActive(false);
                loseText.text = "TRASH OVER LIMIT!";
            }
            else
            {
                gameoverTextGameObject.SetActive(false);
                loseTextGameObject.SetActive(false);
                trashPointText.text = GameOverUI.point.ToString();
                recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
                var lastLevel = PlayerPrefs.GetInt(saveLevel);
                if (lastLevel <= idLevel)
                {
                    lastLevel += 1;
                    PlayerPrefs.SetInt(saveLevel, lastLevel);
                }
            }          
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}


