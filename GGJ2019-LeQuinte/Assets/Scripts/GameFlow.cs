using System.Collections;
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

    private PlayerMovement playerMovement;
    private PlanetGenerator planetGenerator;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        planetGenerator = FindObjectOfType<PlanetGenerator>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.RefreshMovementInfo();
    }

    private void Update()
    {
        if (Input.GetKeyDown(moveToLeftPlanet))
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
        playerMovement.canMove = false;

        //animation

        Destroy(currentPlanet);
        currentPlanet = planetGenerator.GeneratePlanet((int) jumpDirection);

        nextPlanetsToLoad.Clear();
        nextPlanetsToLoad = planetGenerator.GetNextPlanets();

        LoadNextPreviews();

        //animation land
        playerMovement.currentPlanet = currentPlanet.GetComponent<Planet>();
        playerMovement.RefreshMovementInfo();
        LaunchAgent();
        playerMovement.canMove = true;
    }

    private void LoadNextPreviews()
    {
        if (LoadNextPlanets != null)
        {
            LoadNextPlanets(nextPlanetsToLoad);
        }
    }

    private void LaunchAgent()
    {
        Debug.Log("Agent");
        List<GameObject> movementGameObjects = new List<GameObject>();
        switch (currentPlanet.GetComponent<Planet>().planetInfos.planetAppearanceType.shapeType)
        {
            case ShapeType.circle:
                movementGameObjects = currentPlanet.GetComponent<Planet>().movementCircle;
                break;
            case ShapeType.tri:
                movementGameObjects = currentPlanet.GetComponent<Planet>().movementTri;
                break;
            case ShapeType.quad:
                movementGameObjects = currentPlanet.GetComponent<Planet>().movementQuad;
                break;
            case ShapeType.esa:
                movementGameObjects = currentPlanet.GetComponent<Planet>().movementEsa;
                break;
        }
        GameObject agentGameObject = new GameObject();
        Agent agent = agentGameObject.AddComponent<Agent>();
        float progress = 0f;
        Vector3 lastInsert = Vector3.zero;
        List<GameObject> Anchors = new List<GameObject>();
        while (progress < movementGameObjects.Count)
        {
            if (Mathf.FloorToInt(progress) + 1 >= movementGameObjects.Count)
                break;
            Vector3 first = movementGameObjects[Mathf.FloorToInt(progress)].transform.position;
            lastInsert = (lastInsert == Vector3.zero) ? first : lastInsert;

            Vector3 second = movementGameObjects[Mathf.FloorToInt(progress) + 1 ].transform.position;
            agent.transform.position = Vector3.Lerp(first, second, progress % 1);

            if ((agent.transform.position - lastInsert).magnitude > 0.5f)
            {
                GameObject g = new GameObject();
                g.transform.position = agent.transform.position;
                g.transform.parent = currentPlanet.transform.Find("Terrain");
                lastInsert = g.transform.position;
                Anchors.Add(g);
            }
            progress += Time.deltaTime;
        }
    }

    #endregion
}