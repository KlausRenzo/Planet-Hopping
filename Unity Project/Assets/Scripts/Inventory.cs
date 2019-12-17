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

    public Image ghostTreeImage;
    public Image ghostRockImage;
    public Image ghostSeaImage;
    public Image ghostFaunaImage;
    public Image ghostAtmosphereImage;

    public PlanetInfo startingPlanetInfos;
    public GameObject winText;
    public Text planetName;

    [HideInInspector]
    public GameFlow gameFlow;
    [HideInInspector]
    public PlayerMovement playerMovement;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        FindObjectOfType<PlayerMovement>().PickUpElement += AddInInventory;
        gameFlow = FindObjectOfType<GameFlow>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    #endregion

    #region Methods

    public void AddInInventory(EnviromentElement envElem)
    {
        if(envElem.GetType() == typeof(Fauna))
        {
            currentFauna = (Fauna)envElem;
            faunaImage.sprite = currentFauna.enviromentSprite;
            faunaImage.color = Color.white;
            playerMovement.PlaySound(PlayerMovement.SoundKeys.Fauna);
        }
        else if (envElem.GetType() == typeof(Atmosphere))
        {
            currentAtmosphere = (Atmosphere)envElem;
            atmosphereImage.sprite = currentAtmosphere.enviromentSprite;
            atmosphereImage.color = Color.white;
            playerMovement.PlaySound(PlayerMovement.SoundKeys.Atmos);
        }
        else if (envElem.GetType() == typeof(Sea))
        {
            currentSea = (Sea)envElem;
            seaImage.sprite = currentSea.enviromentSprite;
            seaImage.color = Color.white;
            playerMovement.PlaySound(PlayerMovement.SoundKeys.Sea);
        }
        else if (envElem.GetType() == typeof(Rock))
        {
            currentRock = (Rock)envElem;
            rockImage.sprite = currentRock.enviromentSprite;
            rockImage.color = Color.white;
            playerMovement.PlaySound(PlayerMovement.SoundKeys.Rock);
        }
        else if (envElem.GetType() == typeof(Tree))
        {
            currentTree = (Tree)envElem;
            treeImage.sprite = currentTree.enviromentSprite;
            treeImage.color = Color.white;
            playerMovement.PlaySound(PlayerMovement.SoundKeys.Tree);
        }

        CheckWin();
    }

    private void CheckWin()
    {
        currentPlanetAppearance = gameFlow.currentPlanet.GetComponent<Planet>().planetInfos.planetAppearanceType;

        if (currentTree == startingPlanetInfos.treeType &&
            currentFauna == startingPlanetInfos.faunaType &&
            currentRock == startingPlanetInfos.rockType &&
            currentSea == startingPlanetInfos.seaType &&
            currentPlanetAppearance == startingPlanetInfos.planetAppearanceType &&
            currentAtmosphere == startingPlanetInfos.atmosphereType)
        {
            gameFlow.Win();
            winText.SetActive(true);
        }
    }

    public void ThinkOn()
    {
        ghostFaunaImage.enabled = true;
        ghostAtmosphereImage.enabled = true;
        ghostRockImage.enabled = true;
        ghostTreeImage.enabled = true;
        ghostSeaImage.enabled = true;
        rockImage.enabled = false;
        atmosphereImage.enabled = false;
        treeImage.enabled = false;
        faunaImage.enabled = false;
        seaImage.enabled = false;
    }

    public void ThinkOff()
    {
        ghostFaunaImage.enabled = false;
        ghostAtmosphereImage.enabled = false;
        ghostRockImage.enabled = false;
        ghostTreeImage.enabled = false;
        ghostSeaImage.enabled = false;
        rockImage.enabled = true;
        atmosphereImage.enabled = true;
        treeImage.enabled = true;
        faunaImage.enabled = true;
        seaImage.enabled = true;
    }

    public void LinkGhosts()
    {
        PlanetInfo planetInfos = gameFlow.currentPlanet.GetComponent<Planet>().planetInfos;

        ghostAtmosphereImage.sprite = planetInfos.atmosphereType.enviromentSprite;
        ghostFaunaImage.sprite = planetInfos.faunaType.enviromentSprite;
        ghostRockImage.sprite = planetInfos.rockType.enviromentSprite;
        ghostSeaImage.sprite = planetInfos.seaType.enviromentSprite;
        ghostTreeImage.sprite = planetInfos.treeType.enviromentSprite;
    }

    public void SetName(string name)
    {
        planetName.text = name;
    }

    #endregion

}
