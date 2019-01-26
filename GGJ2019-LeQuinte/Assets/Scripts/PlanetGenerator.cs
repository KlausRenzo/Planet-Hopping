using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlanetGenerator : MonoBehaviour 
{

    #region Fields

    public int planetsToGenerateEveryTime = 3;
    public Vector2 minMaxGeneratedPlanetSpeed = new Vector2(1f, 5f);
    public Vector2 minMaxGeneratedBgCloudsSpeed = new Vector2(1f, 5f);
    public Vector2 minMaxGeneratedFgCloudsSpeed = new Vector2(1f, 5f);

    public float mainShapeProbability = 40;
    public float secondaryShapeProbability = 30;
    public float mainColorProbability = 40;
    public float secondaryColorProbability = 30;

    EnviromentDataGenerator dataGenerator;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        dataGenerator = GetComponent<EnviromentDataGenerator>();
    }

    #endregion

    #region Methods

    public List<Planet> GeneratePlanets()
    {
        List<Planet> generatedPlanets = new List<Planet>();

        for(int i = 0; i < planetsToGenerateEveryTime; i++)
        {
            var newPlanet = new Planet
            {
                planetInfos = GeneratePlanetInfo(),
                bgCloudsSpeed = Random.Range(minMaxGeneratedBgCloudsSpeed.x, minMaxGeneratedBgCloudsSpeed.y),
                fgCloudsSpeed = Random.Range(minMaxGeneratedFgCloudsSpeed.x, minMaxGeneratedFgCloudsSpeed.y),
                planetSpeed = Random.Range(minMaxGeneratedPlanetSpeed.x, minMaxGeneratedPlanetSpeed.y)
            };
            newPlanet.planetName = GenerateName(newPlanet.planetInfos.planetAppearanceType);
            newPlanet.terrain.sprite = newPlanet.planetInfos.planetAppearanceType.enviromentSprite;

            generatedPlanets.Add(newPlanet);
        }

        return generatedPlanets;
    }

    private PlanetInfo GeneratePlanetInfo()
    {
        bool usesMainColor = false;
        bool usesMainShape = false;

        var newPlanetInfo = new PlanetInfo();

        newPlanetInfo.planetAppearanceType = dataGenerator.generatedPlanetAppearances[Random.Range(0, dataGenerator.generatedPlanetAppearances.Count)];
        if(Random.Range(0f,100f) < mainColorProbability)
        {
            usesMainColor = true;
        }
        if (usesMainColor)
        {
            newPlanetInfo.atmosphereType
        }
        else
        {

        }


        return newPlanetInfo;
    }

    private string GenerateName(PlanetAppearance planetAppearanceType)
    {
        //based on planetAppearanceType

        return "Nome Pianeta";
    }

    [Button(Name = "Flat Main and Secondary Probabilities", ButtonHeight = 50)]
    public void FlatProbabilities()
    {
        mainColorProbability = 100 - (secondaryColorProbability * 2);
        mainShapeProbability = 100 - (secondaryShapeProbability * 2);
    }

    private CheckProbability(string probabilityType)
    {

    }

    #endregion

}
