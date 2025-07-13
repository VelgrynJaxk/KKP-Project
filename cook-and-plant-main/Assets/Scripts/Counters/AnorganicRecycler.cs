using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnorganicRecycler : BaseCounter, IHasProgress
{
    public int PointPlus;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler <OnStateChangedEventArgs> OnStateChanged;

    public event EventHandler RecyclingStart;
    public event EventHandler RecyclingOver;
    public class OnStateChangedEventArgs : EventArgs{
        public State state;
    }
    public enum State 
    {
        idle,
        Recycling,
        Recycled,
    }

    [SerializeField] private RecyclingRecipeSO [] recyclingRecipeSOArray;

    private State state;
    private float recyclingTimer;
    private RecyclingRecipeSO recyclingRecipeSO;

    private void Start() 
    {
        state = State.idle;
    }

    private void Update()
    {
        if (HasKitchenObject()){
            switch (state) {
                case State.idle :                      
                    break;
                case State.Recycling :
                    RecyclingStart?.Invoke(this, EventArgs.Empty);
                    recyclingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = recyclingTimer/ recyclingRecipeSO.recyclingTimerMax
                    });

                    if ((recyclingTimer > recyclingRecipeSO.recyclingTimerMax))
                    {
                        //Recycled
                        GetKitchenObject().DestroySelf();

                        state = State.Recycled;
                        GameOverUI.point += PointPlus;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });

                        RecyclingOver?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case State.Recycled :
                    break;
            }
        } 
    }

    public override void Interact(Player player){
        if (!HasKitchenObject())
        {
         if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    recyclingRecipeSO = GetRecyclingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Recycling;
                    recyclingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = recyclingTimer/ recyclingRecipeSO.recyclingTimerMax
                });
                }
            } else {
            }
        }
    } 
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        RecyclingRecipeSO recyclingRecipeSO = GetRecyclingRecipeSOWithInput(inputKitchenObjectSO);
        return recyclingRecipeSO != null;
    }

    private RecyclingRecipeSO GetRecyclingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (RecyclingRecipeSO recyclingRecipeSO in recyclingRecipeSOArray)
        {
            if (recyclingRecipeSO.input == inputKitchenObjectSO)
            {
                return recyclingRecipeSO;
            }
        }

        return null;
    }
}
