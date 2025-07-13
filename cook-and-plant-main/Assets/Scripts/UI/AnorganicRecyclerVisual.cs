using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnorganicRecyclerVisual : MonoBehaviour
{
    private const string RECYCLING = "Recycling";
    [SerializeField] private AnorganicRecycler anorganicRecycler;
    [SerializeField] private GameObject selectedVisual;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        anorganicRecycler.RecyclingStart += AnorganicRecycler_OnRecyclingStart;

        anorganicRecycler.RecyclingOver += AnorganicRecycler_OnRecyclingOver;
    }

    private void AnorganicRecycler_OnRecyclingOver(object sender, EventArgs e)
    {
        animator.SetBool(RECYCLING, false);
        selectedVisual.SetActive(true);
    }

    private void AnorganicRecycler_OnRecyclingStart(object sender, EventArgs e)
    {
        animator.SetBool(RECYCLING, true);
        selectedVisual.SetActive(false);
    }
}
