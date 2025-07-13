using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnorganicRecyclerSFX : MonoBehaviour
{
    [SerializeField] private AnorganicRecycler anorganicRecyclerCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anorganicRecyclerCounter.OnStateChanged += AnorganicRecyclerCounter_OnStateChanged;
    }

    private void AnorganicRecyclerCounter_OnStateChanged(object sender, AnorganicRecycler.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == AnorganicRecycler.State.Recycling;
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
