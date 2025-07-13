using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicRecyclerVisual : MonoBehaviour
{
    private const string RECYCLING = "Recycling";
    [SerializeField] private OrganicRecycler organicRecycler;
    [SerializeField] private GameObject selectedOrganicVisual;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        organicRecycler.RecyclingStart += OrganicRecycler_OnRecyclingStart;

        organicRecycler.RecyclingOver += OrganicRecycler_OnRecyclingOver;
    }

    private void OrganicRecycler_OnRecyclingOver(object sender, EventArgs e)
    {
        animator.SetBool(RECYCLING, false);
        selectedOrganicVisual.SetActive(true);
    }

    private void OrganicRecycler_OnRecyclingStart(object sender, EventArgs e)
    {
        animator.SetBool(RECYCLING, true);
        selectedOrganicVisual.SetActive(false);
    }
}
