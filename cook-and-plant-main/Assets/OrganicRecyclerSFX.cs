using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicRecyclerSFX : MonoBehaviour
{
    [SerializeField] private OrganicRecycler organicRecyclerCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        organicRecyclerCounter.OnStateChanged += AnorganicRecyclerCounter_OnStateChanged;
    }

    private void AnorganicRecyclerCounter_OnStateChanged(object sender, OrganicRecycler.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == OrganicRecycler.State.Recycling;
        if(playSound)
        {
            audioSource.Play();
        } 
        else 
        {
            audioSource.Pause();
        }
    }

    private void Update() 
        {
            audioSource.volume = SoundManager.Instance.GetVolume();
        }
}
