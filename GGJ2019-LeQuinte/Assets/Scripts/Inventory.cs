using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{

    #region Fields

    public Tree currentTree;
    public Rock currentRock;
    public Sea currentSea;
    public Fauna currentFauna;
    public Atmosphere currentAtmosphere;
    public PlanetAppearance currentPlanetAppearance;

    public Image treeImage;
    public Image rockImage;
    public Image seaImage;
    public Image faunaImage;
    public Image atmosphereImage;

    public PlanetInfo startingPlanetInfos;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        FindObjectOfType<PlayerMovement>().PickUpElement += AddInInventory;
    }

    #endregion

    #region Methods

    public void AddInInventory(EnviromentElement envElem)
    {
        if(envElem.GetType() == typeof(Fauna))
        {
            currentFauna = (Fauna)envElem;
            faunaImage.sprite = currentFauna.enviromentSprite;
        }
        else if (envElem.GetType() == typeof(Atmosphere))
        {
            currentAtmosphere = (Atmosphere)envElem;
            atmosphereImage.sprite = currentAtmosphere.enviromentSprite;
        }
        else if (envElem.GetType() == typeof(Sea))
        {
            currentSea = (Sea)envElem;
            seaImage.sprite = currentSea.enviromentSprite;
        }
        else if (envElem.GetType() == typeof(Rock))
        {
            currentRock = (Rock)envElem;
            rockImage.sprite = currentRock.enviromentSprite;
        }
        else if (envElem.GetType() == typeof(Tree))
        {
            currentTree = (Tree)envElem;
            treeImage.sprite = currentTree.enviromentSprite;
        }
    }

    #endregion

}
