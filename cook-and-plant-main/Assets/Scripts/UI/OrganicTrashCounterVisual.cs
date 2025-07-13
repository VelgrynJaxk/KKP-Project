using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicTrashCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private OrganicTrashCounter trashOrganicCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        trashOrganicCounter.OnPlayerTrashed += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
