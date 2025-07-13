using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private FarmContainerCounter farmContainerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        farmContainerCounter.OnPlayerGrabbedObject += FarmContainerCounter_OnPlayerGrabbedObject;
    }

    private void FarmContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
