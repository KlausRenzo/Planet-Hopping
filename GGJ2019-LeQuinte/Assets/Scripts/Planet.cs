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
    public GameFlow gameFlow;

    public float bgCloudsSpeed;
    public float fgCloudsSpeed;
    public float planetSpeed;

    public List<GameObject> movementTri = new List<GameObject>();
    public List<GameObject> movementQuad = new List<GameObject>();
    public List<GameObject> movementCircle = new List<GameObject>();
    public List<GameObject> movementEsa = new List<GameObject>();

    #endregion

    private void Update()
    {
        PlanetTick();
    }

    #region Methods

    public void Disintegration()
    {
        StartCoroutine(Disint());
    }

    public void PlanetTick()
    {
        terrain.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed);
        bgClouds.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed/2f);
        fgClouds.transform.rotation *= Quaternion.Euler(0,0, - Time.deltaTime * planetSpeed/2f*3f);
    }

    public void GeneratePlanet(List<GameObject> enviromentAnchors)
    {
        int elementsLeft = enviromentAnchors.Count - 1;
        int i = 0;
        while (elementsLeft >= 0)
        {
            HotSpot hotSpot = enviromentAnchors[elementsLeft].GetComponent<HotSpot>();
            SpriteRenderer spriteRenderer = enviromentAnchors[elementsLeft].GetComponent<SpriteRenderer>();
            switch (i)
            {
                case 0:
                    i++;
                    elementsLeft--;
                    hotSpot.enviromentElement = planetInfos.faunaType;
                    spriteRenderer.sortingLayerName = "Fauna";
                    break;
                case 1:
                    i++;
                    elementsLeft--;
                    hotSpot.enviromentElement = planetInfos.rockType;
                    spriteRenderer.sortingLayerName = "Rocks";
                    break;
                case 2:
                    i++;
                    elementsLeft--;
                    hotSpot.enviromentElement = planetInfos.seaType;
                    spriteRenderer.sortingLayerName = "Seas";
                    break;
                case 3:
                    i = 0;
                    elementsLeft--;
                    hotSpot.enviromentElement = planetInfos.treeType;
                    spriteRenderer.sortingLayerName = "Trees";
                    break;
            }
        }
    }

    IEnumerator Disint()
    {
        yield return new WaitForSeconds(0.5f);

        float t = 0;
        float maxt = 5;
        float l = 0;
        while(t < maxt)
        {
            t += Time.deltaTime;
            l = t / maxt;

            Color distColor = new Color(Mathf.Lerp(1, 0.2f, l), Mathf.Lerp(1, 0.2f, l), Mathf.Lerp(1, 0.2f, l), 1);

            terrain.color = distColor;
            atmosphere.color = distColor;
            bgClouds.GetComponent<SpriteRenderer>().color = distColor;
            fgClouds.GetComponent<SpriteRenderer>().color = distColor;
            foreach (GameObject go in gameFlow.enviromentAnchors)
            {
                SpriteRenderer spriteRend = go.GetComponent<SpriteRenderer>();
                spriteRend.color = distColor;
            }
            
            yield return null;
        }
    }

    #endregion

}
