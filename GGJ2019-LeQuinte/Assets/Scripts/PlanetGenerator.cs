using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class PlanetGenerator : MonoBehaviour 
{

    #region Fields

    public int previewsToGenerateEveryTime = 3;
    public Vector2 minMaxGeneratedPlanetSpeed = new Vector2(1f, 5f);
    public Vector2 minMaxGeneratedBgCloudsSpeed = new Vector2(1f, 5f);
    public Vector2 minMaxGeneratedFgCloudsSpeed = new Vector2(1f, 5f);

    public float mainShapeProbability = 40;
    public float secondaryShapeProbability = 30;
    public float mainColorProbability = 40;
    public float secondaryColorProbability = 30;

    public GameObject planet;
    public GameObject currentPlanet;
    public List<PlanetInfo> nextPlanetInfos = new List<PlanetInfo>();
    [Space(10)]
    public List<string> affixes = new List<string>();
    public List<string> planetNames = new List<string>();

    EnviromentDataGenerator dataGenerator;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        dataGenerator = GetComponent<EnviromentDataGenerator>();
    }

    #endregion

    #region Methods

    public GameObject GeneratePlanet(int index)
    {
        GameObject newPlanetObject = Instantiate(planet, currentPlanet.transform);
        Planet newPlanet = newPlanetObject.GetComponent<Planet>();

        newPlanet.planetInfos = nextPlanetInfos[index];
        newPlanet.bgCloudsSpeed = newPlanet.planetInfos.bgCloudSpeed;
        newPlanet.fgCloudsSpeed = newPlanet.planetInfos.fgCloudSpeed;
        newPlanet.planetSpeed = newPlanet.planetInfos.planetSpeed;
        newPlanet.planetName = GenerateName();

        if (newPlanet.planetInfos.planetAppearanceType.enviromentSprite != null)
        {
            newPlanet.terrain.sprite = newPlanet.planetInfos.planetAppearanceType.enviromentSprite;
        }
        if (newPlanet.planetInfos.atmosphereType.enviromentSprite != null)
        {
            newPlanet.atmosphere.sprite = newPlanet.planetInfos.atmosphereType.enviromentSprite;
        }

        //generate all the little assets

        return newPlanetObject;
    }

    public List<PlanetInfo> GetNextPlanets()
    {
        nextPlanetInfos.Clear();

        for (int i = 0; i < previewsToGenerateEveryTime; i++)
        {
            nextPlanetInfos.Add(GeneratePlanetInfo());
        }

        return nextPlanetInfos;
    }

    private PlanetInfo GeneratePlanetInfo()
    {
        PlanetInfo newPlanetInfo = ScriptableObject.CreateInstance<PlanetInfo>();

        newPlanetInfo.bgCloudSpeed = UnityEngine.Random.Range(minMaxGeneratedBgCloudsSpeed.x, minMaxGeneratedBgCloudsSpeed.y);
        newPlanetInfo.fgCloudSpeed = UnityEngine.Random.Range(minMaxGeneratedFgCloudsSpeed.x, minMaxGeneratedFgCloudsSpeed.y);
        newPlanetInfo.planetSpeed = UnityEngine.Random.Range(minMaxGeneratedPlanetSpeed.x, minMaxGeneratedPlanetSpeed.y);

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

        if (IsMainProbabilityType(ProbabilityType.Color))
        {
            newPlanetInfo.grassType = dataGenerator.generatedGrass[GetFilteredEnviromentListIndex(dataGenerator.generatedGrass, mainColor)];
        }
        else
        {
            newPlanetInfo.grassType = dataGenerator.generatedGrass[GetFilteredEnviromentListIndex(dataGenerator.generatedGrass, dataGenerator.GetComplementaryColorType(mainColor))];
        }

        if (IsMainProbabilityType(ProbabilityType.Shape))
        {
            newPlanetInfo.rockType = dataGenerator.generatedRocks[GetFilteredEnviromentListIndex(dataGenerator.generatedRocks, newPlanetInfo.planetAppearanceType.colorType, mainShape)];
        }
        else
        {
            newPlanetInfo.rockType = dataGenerator.generatedRocks[GetFilteredEnviromentListIndex(dataGenerator.generatedRocks, newPlanetInfo.planetAppearanceType.colorType, dataGenerator.GetComplementaryShapeType(mainShape))];
        }

        if (IsMainProbabilityType(ProbabilityType.Shape))
        {
            newPlanetInfo.faunaType = dataGenerator.generatedFauna[GetFilteredEnviromentListIndex(dataGenerator.generatedFauna, mainColor)];
        }
        else
        {
            newPlanetInfo.faunaType = dataGenerator.generatedFauna[GetFilteredEnviromentListIndex(dataGenerator.generatedFauna, dataGenerator.GetComplementaryColorType(mainColor))];
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

    private string GenerateName()
    {
        string name = affixes[UnityEngine.Random.Range(0, affixes.Count)] + " " + planetNames[UnityEngine.Random.Range(0, planetNames.Count)];

        return name;
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

    private int GetFilteredEnviromentListIndex(List<Grass> list, ColorType colorType)
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

    private int GetFilteredEnviromentListIndex(List<Rock> list, ColorType colorType, ShapeType shapeType)
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

    private int GetFilteredEnviromentListIndex(List<Fauna> list, ColorType colorType)
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
