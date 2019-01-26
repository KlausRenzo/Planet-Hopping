using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour 
{

    #region Fields

    public string planetName;
    public PlanetInfo planetInfos;
    public GameObject bgClouds;
    public GameObject fgClouds;
    public SpriteRenderer terrain;
    public SpriteRenderer atmosphere;

    public float bgCloudsSpeed;
    public float fgCloudsSpeed;
    public float planetSpeed;

    #endregion

    private void Start()
    {
        GeneratePlanet();
    }

    private void Update()
    {
        PlanetTick();
    }

    #region Methods

    public void PlanetTick()
    {

    }

    public void GeneratePlanet()
    {
        
    }

    #endregion

}
