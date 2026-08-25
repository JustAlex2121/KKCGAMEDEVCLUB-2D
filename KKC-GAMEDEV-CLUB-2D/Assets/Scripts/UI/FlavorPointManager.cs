using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlavorPointManager : MonoBehaviour
{

    //For this to work,
    //1. this script preferrably should be attached to something that will manage the flavor points, such as the Canvas or an Empty.
    //2. There has to be a UI element with a Horizontal Layout Group component on it childed to the Canvas. It will serve as an anchor point for the Flavor display, and comes with options to arrange elements in it.
    //3. There has to be a component on the player that this script can reference for the current and max amount of flavor points.
    //4. Likewise, whenever the current or max flavor amount needs to changed, UpdateFlavorAmount() and UpdateMaxFlavor() must be called from that script, meaning they must have a reference to this one.

    //Unsure if this works. If nothing else, it's something to reference

    private static bool FlavorUIExists;

    //Unless we have a separate script to manage flavor points
    //public PlayerFlavor PlayFav;//Replace this and all references with the component that returns current and max flavor point integers, theoretically on the Player
    public int CurFlav;
    public int MaxFlav;

    public GameObject FullFlavor;//Image of unused flavor energy
    public GameObject TemporaryFlavor;//Image of temporary flavor energy
    public GameObject EmptyFlavor;//Image of used flavor energy

    public List<GameObject> FlavorList;//Container of flavor gameobjects
    public List<GameObject> TemporaryFlavorList;//Container for temporary flavor gameobject
    public GameObject FlavorLocation;//UI element that serves as the location of the UI elements. Can customize it to layout objects in it side-by-side.
    //public GameObject FlavorSlot;//The item of the slot itself; archived

    void Start()
    {
        if (!FlavorUIExists)
        {
            FlavorUIExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(gameObject); }

        //PlayFlav = GameObject.Find("Player").GetComponent<PlayerFlavor>();

        UpdateFlavorAmount();

    }

    //For testing purposes only
    /*void Update()
    { 
        UpdateFlavorAmount();
    }*/

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState state)
    {
        if (state == GameState.PlayerTurn)
        {
            ResetCurToMaxFlavor();
        }
    }
    public void UpdateFlavorAmount()//Call to update UI of change in value
    {
        if(CurFlav<0)//Makes sure current flavor doesn't go negative
        {
            CurFlav = 0; 
        }

        foreach (GameObject i in FlavorList)//Clears FlavorList and destroys contents
        {
            Destroy(i.gameObject);
        }
        FlavorList.Clear();

        for (int i = 0; i < MaxFlav; i++)//Creates flavor slots for flavor points within maximum
        {
            if (i < CurFlav)
            {
                GameObject b = Instantiate(FullFlavor, FlavorLocation.transform);
                FlavorList.Add(b);
            }
            else
            {
                GameObject b = Instantiate(EmptyFlavor, FlavorLocation.transform);
                FlavorList.Add(b);
            }
        }

        foreach (GameObject i in TemporaryFlavorList)//Creates temporary slots for flavor points exceeding maximum
        {
            Destroy(i.gameObject);
        }
        TemporaryFlavorList.Clear();
        if (CurFlav > MaxFlav)
        {
            int a = CurFlav - MaxFlav;
            for (int i = 0; i < a; i++)
            {
                GameObject b = Instantiate(TemporaryFlavor, FlavorLocation.transform);
                TemporaryFlavorList.Add(b);
            }
        }
    }

    public void ResetCurToMaxFlavor()
    {
        CurFlav = MaxFlav;
        UpdateFlavorAmount();
    }

    //Depreciated; Just use UpdateFlavorAmount()
    #region
    /*public void UpdateMaxFlavor()//Call to update the max Flavor limit
    {
        foreach (GameObject i in FlavorList)
        {
            Destroy(i.gameObject);
        }
        FlavorList.Clear();

        for (int i = 0; i < MaxFlav; i++)
        {
            GameObject b = Instantiate(FullFlavor, FlavorLocation.transform);
            FlavorList.Add(b);
        }
    }*/
    #endregion
}
