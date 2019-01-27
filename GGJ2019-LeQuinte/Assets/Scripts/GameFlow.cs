using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameFlow : MonoBehaviour
{
    #region Fields

    public Action<List<PlanetInfo>> LoadNextPlanets;

    public List<GameObject> enviromentAnchors;
    public List<PlanetInfo> nextPlanetsToLoad;
    public GameObject currentPlanet;
    public GameObject hotspotPrefab;

    public KeyCode moveToLeftPlanet;
    public KeyCode moveToTopPlanet;
    public KeyCode moveToRightPlanet;

    public float minScaler = 0.6f;
    public float maxScaler = 1.2f;

    public float distanceTraveled = 2;
    public int maxNumberOfElements = 10;
    public int minNumberOfElements = 4;

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

        LaunchAgent();

        currentPlanet.GetComponent<Planet>().GeneratePlanet(enviromentAnchors);

        nextPlanetsToLoad.Clear();
        nextPlanetsToLoad = planetGenerator.GetNextPlanets();

        LoadNextPreviews();

        //animation land
        playerMovement.currentPlanet = currentPlanet.GetComponent<Planet>();
        playerMovement.RefreshMovementInfo();
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
        int numberOfElementsToPlace = UnityEngine.Random.Range(minNumberOfElements, maxNumberOfElements);
        Vector3 lastInsert = Vector3.zero;
        List<GameObject> anchors = new List<GameObject>();
        int i = 0;
        while (anchors.Count < numberOfElementsToPlace)
        {
            i++;

            if (Mathf.FloorToInt(progress) + 1 >= movementGameObjects.Count)
                break;
            Vector3 first = movementGameObjects[Mathf.FloorToInt(progress)].transform.position;
            lastInsert = (lastInsert == Vector3.zero) ? first : lastInsert;

            Vector3 second = movementGameObjects[Mathf.FloorToInt(progress) + 1 ].transform.position;
            agent.transform.position = Vector3.Lerp(first, second, progress % 1);

            if (i >= UnityEngine.Random.Range(5,10))
            {
                GameObject g = Instantiate(hotspotPrefab, currentPlanet.transform.Find("Terrain"));
                g.transform.position = agent.transform.position;
                lastInsert = g.transform.position;
                g.transform.up = Vector2.Perpendicular(second - first);
                float scaler = UnityEngine.Random.Range(minScaler, maxScaler);
                g.transform.localScale = Vector3.one * scaler;
                anchors.Add(g);
                i = 0;
            }
            progress += Time.deltaTime * 20;
            progress %= movementGameObjects.Count;
        }
        enviromentAnchors = anchors;

        Destroy(agentGameObject);
    }

    #endregion
}