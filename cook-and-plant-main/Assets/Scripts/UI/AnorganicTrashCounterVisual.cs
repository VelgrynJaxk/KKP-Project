using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnorganicTrashCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private AnorganicTrashCounter trashAnorganicCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        trashAnorganicCounter.OnPlayerTrashed += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
