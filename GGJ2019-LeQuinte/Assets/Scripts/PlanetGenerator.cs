using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class PlanetGenerator : MonoBehaviour 
{

    #region Fields

    public int planetsToGenerateEveryTime = 1;
    public Vector2 minMaxGeneratedPlanetSpeed = new Vector2(1f, 5f);
    public Vector2 minMaxGeneratedBgCloudsSpeed = new Vector2(1f, 5f);
    public Vector2 minMaxGeneratedFgCloudsSpeed = new Vector2(1f, 5f);

    public float mainShapeProbability = 40;
    public float secondaryShapeProbability = 30;
    public float mainColorProbability = 40;
    public float secondaryColorProbability = 30;

    public GameObject planet;
    public GameObject nextPlanets;
    public List<GameObject> generatedPlanets = new List<GameObject>(); 

    EnviromentDataGenerator dataGenerator;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        dataGenerator = GetComponent<EnviromentDataGenerator>();
        GeneratePlanets();
    }

    #endregion

    #region Methods

    public void GeneratePlanets()
    {
        for (int i = 0; i < planetsToGenerateEveryTime; i++)
        {
            GameObject newPlanetObject = Instantiate(planet, nextPlanets.transform);
            Planet newPlanet = newPlanetObject.GetComponent<Planet>();

            newPlanet.planetInfos = GeneratePlanetInfo();
            newPlanet.bgCloudsSpeed = UnityEngine.Random.Range(minMaxGeneratedBgCloudsSpeed.x, minMaxGeneratedBgCloudsSpeed.y);
            newPlanet.fgCloudsSpeed = UnityEngine.Random.Range(minMaxGeneratedFgCloudsSpeed.x, minMaxGeneratedFgCloudsSpeed.y);
            newPlanet.planetSpeed = UnityEngine.Random.Range(minMaxGeneratedPlanetSpeed.x, minMaxGeneratedPlanetSpeed.y);
            newPlanet.planetName = GenerateName(newPlanet.planetInfos.planetAppearanceType);

            if (newPlanet.planetInfos.planetAppearanceType.enviromentSprite != null)
            {
                newPlanet.terrain.sprite = newPlanet.planetInfos.planetAppearanceType.enviromentSprite;
            }

            generatedPlanets.Add(newPlanetObject);
        }
    }

    private PlanetInfo GeneratePlanetInfo()
    {
        PlanetInfo newPlanetInfo = ScriptableObject.CreateInstance<PlanetInfo>();

        newPlanetInfo.planetAppearanceType = dataGenerator.generatedPlanetAppearances[UnityEngine.Random.Range(0, dataGenerator.generatedPlanetAppearances.Count)];

        ColorType mainColor = newPlanetInfo.planetAppearanceType.colorType;
        ShapeType mainShape = newPlanetInfo.planetAppearanceType.shapeType;

        if (IsMainProbabilityType(ProbabilityType.Color))
        {
            newPlanetInfo.atmosphereType = dataGenerator.generatedAtmospheres[GetFilteredEnviromentListIndex(dataGenerator.generatedAtmospheres, mainColor)];
        }
        else
        {
            newPlanetInfo.atmosphereType = dataGenerator.generatedAtmospheres[GetFilteredEnviromentListIndex(dataGenerator.generatedAtmospheres, dataGenerator.GetComplementaryColorType(mainColor))];
        }

        if (IsMainProbabilityType(ProbabilityType.Color))
        {
            newPlanetInfo.seaType = dataGenerator.generatedSeas[GetFilteredEnviromentListIndex(dataGenerator.generatedSeas, mainColor)];
        }
        else
        {
            newPlanetInfo.seaType = dataGenerator.generatedSeas[GetFilteredEnviromentListIndex(dataGenerator.generatedSeas, dataGenerator.GetComplementaryColorType(mainColor))];
        }

        if (IsMainProbabilityType(ProbabilityType.Shape))
        {
            newPlanetInfo.rockType = dataGenerator.generatedRocks[GetFilteredEnviromentListIndex(dataGenerator.generatedRocks, mainShape)];
        }
        else
        {
            newPlanetInfo.rockType = dataGenerator.generatedRocks[GetFilteredEnviromentListIndex(dataGenerator.generatedRocks, dataGenerator.GetComplementaryShapeType(mainShape))];
        }

        if (IsMainProbabilityType(ProbabilityType.Shape))
        {
            newPlanetInfo.faunaType = dataGenerator.generatedFauna[GetFilteredEnviromentListIndex(dataGenerator.generatedFauna, mainShape)];
        }
        else
        {
            newPlanetInfo.faunaType = dataGenerator.generatedFauna[GetFilteredEnviromentListIndex(dataGenerator.generatedFauna, dataGenerator.GetComplementaryShapeType(mainShape))];
        }

        if (IsMainProbabilityType(ProbabilityType.Shape))
        {
            if (IsMainProbabilityType(ProbabilityType.Color))
            {
                newPlanetInfo.treeType = dataGenerator.generatedTrees[GetFilteredEnviromentListIndex(dataGenerator.generatedTrees, mainColor, mainShape)];
            }
            else
            {
                newPlanetInfo.treeType = dataGenerator.generatedTrees[GetFilteredEnviromentListIndex(dataGenerator.generatedTrees, dataGenerator.GetComplementaryColorType(mainColor), mainShape)];
            }
        }
        else
        {
            if (IsMainProbabilityType(ProbabilityType.Color))
            {
                newPlanetInfo.treeType = dataGenerator.generatedTrees[GetFilteredEnviromentListIndex(dataGenerator.generatedTrees, mainColor, dataGenerator.GetComplementaryShapeType(mainShape))];
            }
            else
            {
                newPlanetInfo.treeType = dataGenerator.generatedTrees[GetFilteredEnviromentListIndex(dataGenerator.generatedTrees, dataGenerator.GetComplementaryColorType(mainColor), dataGenerator.GetComplementaryShapeType(mainShape))];
            }
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

    private int GetFilteredEnviromentListIndex(List<Atmosphere> list, ColorType colorType)
    {
        int index;

        for (index = 0; index < list.Count; index++)
        {
            if(list[index].colorType == colorType)
            {
                return index;
            }
        }

        return index;
    }

    private int GetFilteredEnviromentListIndex(List<Sea> list, ColorType colorType)
    {
        int index;

        for (index = 0; index < list.Count; index++)
        {
            if (list[index].colorType == colorType)
            {
                return index;
            }
        }

        return index;
    }

    private int GetFilteredEnviromentListIndex(List<Rock> list, ShapeType shapeType)
    {
        int index;

        for (index = 0; index < list.Count; index++)
        {
            if (list[index].shapeType == shapeType)
            {
                return index;
            }
        }

        return index;
    }

    private int GetFilteredEnviromentListIndex(List<Fauna> list, ShapeType shapeType)
    {
        int index;

        for (index = 0; index < list.Count; index++)
        {
            if (list[index].shapeType == shapeType)
            {
                return index;
            }
        }

        return index;
    }

    private int GetFilteredEnviromentListIndex(List<Tree> list, ColorType colorType, ShapeType shapeType)
    {
        int index;

        for (index = 0; index < list.Count; index++)
        {
            if (list[index].shapeType == shapeType && list[index].colorType == colorType)
            {
                return index;
            }
        }

        return index;
    }

    private bool IsMainProbabilityType(ProbabilityType type)
    {
        bool usesMain = false;
        if (type == ProbabilityType.Color)
        {
            if (UnityEngine.Random.Range(0f, 100f) < mainColorProbability)
            {
                usesMain = true;
            }
        }
        else if (type == ProbabilityType.Shape)
        {
            if (UnityEngine.Random.Range(0f, 100f) < mainShapeProbability)
            {
                usesMain = true;
            }
        }
        return usesMain;
    }

    #endregion

}
