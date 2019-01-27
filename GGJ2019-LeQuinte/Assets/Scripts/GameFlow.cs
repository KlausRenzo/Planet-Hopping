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

    public int maxNumberOfElements;
    public int minNumberOfElements;

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

        int numberOfElementsToPlace = UnityEngine.Random.Range(minNumberOfElements, maxNumberOfElements);
        List<GameObject> anchors = new List<GameObject>();

        while (anchors.Count < numberOfElementsToPlace)
        {
            int firstIndex = UnityEngine.Random.Range(0, movementGameObjects.Count);
            int secondIndex = (firstIndex + 1) % movementGameObjects.Count; 

            Vector3 first = movementGameObjects[firstIndex].transform.position;
            Vector3 second = movementGameObjects[secondIndex].transform.position;

            Vector3 direction = second - first;
            float distance = direction.magnitude;
            Vector3 unit = direction.normalized;

            Vector3 newHotSpotVector = first + (unit * UnityEngine.Random.Range(0f, distance));

            GameObject newHotSpot = Instantiate(hotspotPrefab, currentPlanet.transform.Find("Terrain"));
            newHotSpot.transform.position = newHotSpotVector;
            newHotSpot.transform.up = Vector2.Perpendicular(second - first);
            float scaler = UnityEngine.Random.Range(minScaler, maxScaler);
            newHotSpot.transform.localScale = Vector3.one * scaler;
            anchors.Add(newHotSpot);
        }

        enviromentAnchors = anchors;
    }

    public void Win()
    {

    }

    #endregion
}