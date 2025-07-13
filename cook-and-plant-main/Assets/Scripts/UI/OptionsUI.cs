using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set;}

    [SerializeField] private Slider soundSlider;
    [SerializeField] TMP_Text soundVolText;
    [SerializeField] private Slider musicSlider;
    [SerializeField] TMP_Text musicVolText;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAltButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI InteractAltText;
    [SerializeField] private TextMeshProUGUI PauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake() {
        Instance = this;
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        closeButton.onClick.AddListener(() => {
            Hide();
        });

        soundSlider.onValueChanged.AddListener(SetSoundVolText);
        musicSlider.onValueChanged.AddListener(SetMusicVolText);
        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right);});
        InteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact);});
        InteractAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate);});
        PauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause);});
    }

    private void Start() 
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
        musicSlider.value = MusicManager.Instance.GetVolume();
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

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }
    private void UpdateVisual()
    {
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        InteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindingBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
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
