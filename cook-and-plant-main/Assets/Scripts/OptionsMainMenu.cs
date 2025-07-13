using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMainMenu : MonoBehaviour
{
    public static OptionsMainMenu Instance { get; private set;}

    [SerializeField] private Slider soundSlider;
    [SerializeField] TMP_Text soundVolText;
    [SerializeField] private Slider musicSlider;
    [SerializeField] TMP_Text musicVolText;
    [SerializeField] private Button closeButton;


    private void Awake() {
        Instance = this;
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        closeButton.onClick.AddListener(() => {
            Hide();
        });

        soundSlider.onValueChanged.AddListener(SetSoundVolText);
        musicSlider.onValueChanged.AddListener(SetMusicVolText);
    }

    private void Start() 
    {
    //     musicSlider.value = MusicManager.Instance.GetVolume();
         soundSlider.value = SoundManager.Instance.GetVolume();
    }

    public void OnSoundSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeVolume(value);
        SetSoundVolText(value);
    }

    public void OnMusicSliderValueChanged(float value)
    {
        MusicManager.Instance.SetVolume(value);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

     private void OnEnable()
    {
        SetSoundVolText(soundSlider.value);
        SetMusicVolText(musicSlider.value);
    }

    public void SetSoundVolText(float value)
    {
        soundVolText.text = Mathf.RoundToInt(value * 100f).ToString();
    }

    public void SetMusicVolText(float value)
    {
        musicVolText.text = Mathf.RoundToInt(value * 100f).ToString();
    }
}
