using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Planet : MonoBehaviour 
{

    #region Fields

    public PlanetInfo planetInfos;
    public GameObject bgClouds;
    public GameObject fgClouds;
    public SpriteRenderer terrain;
    public SpriteRenderer grass;
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
        terrain.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed);
        grass.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed);
        bgClouds.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed/2f);
        fgClouds.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed/2f*3f);
    }

    public void GeneratePlanet()
    {

    }

    #endregion

}
