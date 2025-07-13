using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameobjectArray;
    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e){
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    
    private void Show()
    {
        foreach (var visualGameobject in visualGameobjectArray)
        {
            visualGameobject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualGameobject in visualGameobjectArray)
        {
            visualGameobject.SetActive(false);
        }
    }
}
