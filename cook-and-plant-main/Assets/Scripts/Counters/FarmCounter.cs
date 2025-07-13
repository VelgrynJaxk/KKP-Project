using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        idle,
        Grow,
        Ripe,
        Rotten,
    }

    [SerializeField] private PlantRecipeSO[] plantRecipeSOArray;
    [SerializeField] private RottenRecipeSO[] rottenRecipeSOArray;

    [SerializeField] private State state;
    [SerializeField] private int maxPlantToHarvestCount;
    [SerializeField] private int TomatoCount;
    private float growTimer;
    private PlantRecipeSO plantRecipeSO;
    private float rottenTimer;
    private RottenRecipeSO rottenRecipeSO;

    void Start()
    {
        state = State.idle;
    }

    void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.idle:
                    break;
                case State.Grow:
                    

                    growTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = growTimer / plantRecipeSO.plantTimerMax
                    });

                    if (growTimer > plantRecipeSO.plantTimerMax)
                    {
                        //Grow
                        GetKitchenObject().DestroySelf();                   
                        KitchenObject.SpawnKitchenObject(plantRecipeSO.plant, this);


                        state = State.Ripe;
                        rottenTimer = 0f;
                        rottenRecipeSO = GetRottenRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }

                    break;
                case State.Ripe:
                    rottenTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = rottenTimer / rottenRecipeSO.rottenTimerMax
                    });

                    if (rottenTimer > rottenRecipeSO.rottenTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(rottenRecipeSO.output, this);

                        state = State.Rotten;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                    break;
                case State.Rotten:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);   
                            
                    plantRecipeSO = GetGrowRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Grow;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(plantRecipeSO.initialPlant, this);     
                    growTimer = 0f;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = growTimer / plantRecipeSO.plantTimerMax
                    });
                }
            }
            else
            {
                //player not carying anything
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                
            }
            else
            {
                if (state == State.Ripe)
                {
                    if (TomatoCount != maxPlantToHarvestCount)
                    {
                       KitchenObject.SpawnKitchenObject(plantRecipeSO.output, player);
                       TomatoCount += 1;
                       Debug.Log(TomatoCount);
                    }else if (TomatoCount >= maxPlantToHarvestCount - 1)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(plantRecipeSO.output, player);
                        state = State.idle;
                        TomatoCount = 0;
                                                                                                           
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        { 
                           state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    
                    
                }else if (state == State.Rotten)
                {
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(rottenRecipeSO.output, player);

                    state = State.idle;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    { 
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        PlantRecipeSO growRecipeSO = GetGrowRecipeSOWithInput(inputKitchenObjectSO);
        Debug.Log(GetGrowRecipeSOWithInput(inputKitchenObjectSO));
        return growRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        PlantRecipeSO fryingRecipeSO = GetGrowRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private PlantRecipeSO GetGrowRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (PlantRecipeSO plantRecipeSo in plantRecipeSOArray)
        {
            Debug.Log(plantRecipeSo.input);
            if (plantRecipeSo.input == inputKitchenObjectSO)
            {
                return plantRecipeSo;
            }
        }
        return null;
    }

    private RottenRecipeSO GetRottenRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (RottenRecipeSO rottenRecipeSo in rottenRecipeSOArray)
        {
            if (rottenRecipeSo.input == inputKitchenObjectSO)
            {
                return rottenRecipeSo;
            }
        }

        return null;
    }

    public bool IsGrowth()
    {
        return state == State.Ripe;
    }
}
