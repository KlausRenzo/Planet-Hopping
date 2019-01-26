﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameFlow : MonoBehaviour 
{

	#region Fields

    public Action<List<PlanetInfo>> LoadNextPlanets;

    public List<PlanetInfo> nextPlanetsToLoad;
    public GameObject currentPlanet;

    public KeyCode moveToLeftPlanet;
    public KeyCode moveToTopPlanet;
    public KeyCode moveToRightPlanet;

    private PlanetGenerator planetGenerator;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        planetGenerator = FindObjectOfType<PlanetGenerator>();
        playerVertexController = FindObjectOfType<VertexController>();
        SetNewMovementVertexes();
    }

    private void Update()
    {
        if(Input.GetKeyDown(moveToLeftPlanet))
        {
            LeavePlanet(JumpDirections.Left);
        }
        if (Input.GetKeyDown(moveToTopPlanet))
        {
            LeavePlanet(JumpDirections.Top);
        }
        if (Input.GetKeyDown(moveToRightPlanet))
        {
            LeavePlanet(JumpDirections.Right);
        }
    }

    #endregion

    #region Methods

    private void LeavePlanet(JumpDirections jumpDirection)
    {
        //jump -> stop play

        //animation

        Destroy(currentPlanet);
        currentPlanet = planetGenerator.GeneratePlanet((int)jumpDirection);

        nextPlanetsToLoad.Clear();
        nextPlanetsToLoad = planetGenerator.GetNextPlanets();

        LoadNextPreviews();

        //animation land
    }

    private void LoadNextPreviews()
    {
        if (LoadNextPlanets != null)
        {
            LoadNextPlanets(nextPlanetsToLoad);
        }
    }

    #endregion

}
