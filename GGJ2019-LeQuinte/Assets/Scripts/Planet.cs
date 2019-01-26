using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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

    public List<GameObject> movementTri = new List<GameObject>();
    public List<GameObject> movementQuad = new List<GameObject>();
    public List<GameObject> movementCircle = new List<GameObject>();
    public List<GameObject> movementEsa = new List<GameObject>();

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
        terrain.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed);
        bgClouds.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed/2f);
        fgClouds.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed/2f*3f);
    }

    public void GeneratePlanet()
    {
        
    }

    #endregion

}
