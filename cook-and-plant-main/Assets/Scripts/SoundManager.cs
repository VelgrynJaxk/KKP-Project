using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }


    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private AudioSource audioSource;
    private float volume = 1f;

    private void Awake() 
    {
        
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
        
    }

    public void Start() 
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccessSFX;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailedSFX;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaceHere += BaseCounter_OnAnyObjectPlaceHere;
        AnorganicTrashCounter.OnAnorganicObjectTrashed += AnorganicTrashCounter_OnAnyObjectTrashed;
        OrganicTrashCounter.OnOrganicObjectTrashed += OrganicTrashCounter_OnAnyObjectTrashed;
        AnorganicTrashCounter.OnWrongObjectTrashed += AnorganicTrashCounter_OnWrongObjectTrashed;
        OrganicTrashCounter.OnWrongObjectTrashed += OrganicTrashCounter_OnWrongObjectTrashed;
    }

    private void OrganicTrashCounter_OnWrongObjectTrashed(object sender, System.EventArgs e)
    {
        OrganicTrashCounter anorganicTrashCounter = OrganicTrashCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, anorganicTrashCounter.transform.position, GetVolume());
    }

    private void AnorganicTrashCounter_OnWrongObjectTrashed(object sender, System.EventArgs e)
    {
        AnorganicTrashCounter anorganicTrashCounter = AnorganicTrashCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, anorganicTrashCounter.transform.position, GetVolume());
    }

    private void AnorganicTrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        AnorganicTrashCounter trashCounter = sender as AnorganicTrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position, GetVolume());
    }

    private void OrganicTrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        OrganicTrashCounter trashCounter = sender as OrganicTrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position, GetVolume());
    }

    private void BaseCounter_OnAnyObjectPlaceHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position, GetVolume());
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position, GetVolume());
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position, GetVolume());
    }

    private void DeliveryManager_OnRecipeFailedSFX(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position, GetVolume());
    }

    private void DeliveryManager_OnRecipeSuccessSFX(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position, GetVolume());
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)  
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, GetVolume());
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)  
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * GetVolume());
    }

    public void PlayFootstepsSound(Vector3 position, float volume) 
    {
        PlaySound(audioClipRefsSO.footstep, position, GetVolume());
    }

    public void PlayCountdownSound() 
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero, GetVolume());
    }

    public void PlayWarningSound(Vector3 position) 
    {
        PlaySound(audioClipRefsSO.warning, position, GetVolume());
    }

    public void ChangeVolume(float value)
    {
        volume = value;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
