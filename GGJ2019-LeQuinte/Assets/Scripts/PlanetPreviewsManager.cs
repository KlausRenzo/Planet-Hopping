using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPreviewsManager : MonoBehaviour 
{

    #region Fields

    public GameObject planetPreview;
    public List<GameObject> anchors;

    #endregion

    #region Unity Callbacks

    public void Awake()
    {
        FindObjectOfType<GameFlow>().LoadNextPlanets += SpawnPreviews;
    }

    #endregion

    #region Methods

    public void SpawnPreviews(List<PlanetInfo> planetToSpawn)
    {
        foreach(GameObject go in anchors)
        {
            foreach(Transform tr in go.GetComponentInChildren<Transform>())
            {
                Destroy(tr.gameObject);
            }
        }

        for(int i = 0; i < anchors.Count; i++)
        {
            GameObject newPreview = Instantiate(planetPreview, anchors[i].transform);
            PlanetPreview newPlanetPreview = newPreview.GetComponent<PlanetPreview>();

            newPlanetPreview.atmosphereImage.sprite = planetToSpawn[i].atmosphereType.enviromentSprite;
            newPlanetPreview.planetImage.sprite = planetToSpawn[i].planetAppearanceType.enviromentSprite;
            newPlanetPreview.cloudBgRotation = planetToSpawn[i].bgCloudSpeed;
            newPlanetPreview.cloudFgRotation = planetToSpawn[i].fgCloudSpeed;
            newPlanetPreview.planetRotation = planetToSpawn[i].planetSpeed;
        }
    }

    #endregion

}
