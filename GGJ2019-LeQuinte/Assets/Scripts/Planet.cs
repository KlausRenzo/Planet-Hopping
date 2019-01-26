using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour 
{

    #region Fields

    public PlanetInfo planetInfos;
    public GameObject bgClouds;
    public GameObject fgClouds;
    public GameObject grass;
    public SpriteRenderer terrain;
    public SpriteRenderer atmosphere;

    public float bgCloudsSpeed;
    public float fgCloudsSpeed;
    public float planetSpeed;

    #endregion

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
