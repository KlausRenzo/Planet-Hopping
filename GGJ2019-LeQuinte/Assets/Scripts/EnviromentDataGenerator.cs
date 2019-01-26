using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Sirenix.OdinInspector;

public class EnviromentDataGenerator : MonoBehaviour 
{

    #region Fields

    public List<string> coloredEnviromentTypes;
    public List<string> shapedEnviromentTypes;
    public List<string> coloredAndShapedEnviromentTypes;

    public List<Sea> generatedSeas = new List<Sea>();
    public List<Rock> generatedRocks = new List<Rock>();
    public List<Tree> generatedTrees = new List<Tree>();
    public List<Fauna> generatedFauna = new List<Fauna>();
    public List<Atmosphere> generatedAtmospheres = new List<Atmosphere>();
    public List<PlanetAppearance> generatedPlanetAppearances = new List<PlanetAppearance>();

    private const string spritePath = "Assets/Sprites/Enviroment Sprites";
    private const string endingPath = "Assets/Scripts/ScriptableObject/Enviroment Elements";
    private string[] shapeTypes = System.Enum.GetNames(typeof(ShapeType));
    private string[] colorTypes = System.Enum.GetNames(typeof(ColorType));

    #endregion

    #region Methods

#if UNITY_EDITOR

    [Button(Name = "Generate Enviroment Data", ButtonHeight = 50)]
    public void GenerateData()
    {
        DeleteData();
        int maxProgress = coloredEnviromentTypes.Count + shapedEnviromentTypes.Count + coloredAndShapedEnviromentTypes.Count;
        int progress = 0;

        foreach (string envType in coloredEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType + "/";

            if (envType == "atmospheres")
            {
                foreach (string color in colorTypes)
                {
                    Atmosphere newAtmosphere = ScriptableObject.CreateInstance<Atmosphere>();
                    newAtmosphere.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + envType + ".png", typeof(Sprite));
                    newAtmosphere.name = color + "_" + envType;
                    newAtmosphere.colorType = GetColorType(color);
                    AssetDatabase.CreateAsset(newAtmosphere, currentEndingPath + newAtmosphere.name + ".asset");
                    generatedAtmospheres.Add(newAtmosphere);
                    EditorUtility.SetDirty(newAtmosphere);
                }
            }
            else if (envType == "seas")
            {
                foreach (string color in colorTypes)
                {
                    Sea newSea = ScriptableObject.CreateInstance<Sea>();
                    newSea.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + envType + ".png", typeof(Sprite));
                    newSea.name = color + "_" + envType;
                    newSea.colorType = GetColorType(color);
                    AssetDatabase.CreateAsset(newSea, currentEndingPath + newSea.name + ".asset");
                    generatedSeas.Add(newSea);
                    EditorUtility.SetDirty(newSea);
                }
            }

            EditorUtility.DisplayProgressBar("Generate Enviroment Data", "Generating " + envType, progress / maxProgress);
            progress++;
        }

        foreach (string envType in shapedEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType + "/";

            if (envType == "rocks")
            {
                foreach (string shape in shapeTypes)
                {
                    foreach (string color in colorTypes)
                    {
                        Rock newRock = ScriptableObject.CreateInstance<Rock>();
                        newRock.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + shape + "_" + envType + ".png", typeof(Sprite));
                        newRock.name = color + "_" + shape + "_" + envType;
                        newRock.shapeType = GetShapeType(shape);
                        newRock.colorType = GetColorType(color);
                        AssetDatabase.CreateAsset(newRock, currentEndingPath + newRock.name + ".asset");
                        generatedRocks.Add(newRock);
                        EditorUtility.SetDirty(newRock);
                    }
                }
            }
            else if (envType == "fauna")
            {
                foreach (string shape in shapeTypes)
                {
                    Fauna newFauna = ScriptableObject.CreateInstance<Fauna>();
                    newFauna.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + shape + "_" + envType + ".png", typeof(Sprite));
                    newFauna.name = shape + "_" + envType;
                    newFauna.shapeType = GetShapeType(shape);
                    AssetDatabase.CreateAsset(newFauna, currentEndingPath + newFauna.name + ".asset");
                    generatedFauna.Add(newFauna);
                    EditorUtility.SetDirty(newFauna);
                }
            }

            EditorUtility.DisplayProgressBar("Generate Enviroment Data", "Generating " + envType, progress / maxProgress);
            progress++;
        }

        foreach (string envType in coloredAndShapedEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType + "/";

            if (envType == "planetappearances")
            {
                foreach (string color in colorTypes)
                {
                    foreach (string shape in shapeTypes)
                    {
                        PlanetAppearance newPlanetAppearance = ScriptableObject.CreateInstance<PlanetAppearance>();
                        newPlanetAppearance.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + shape + "_" + envType + ".png", typeof(Sprite));
                        newPlanetAppearance.name = color + "_" + shape + "_" + envType;
                        newPlanetAppearance.shapeType = GetShapeType(shape);
                        newPlanetAppearance.colorType = GetColorType(color);
                        AssetDatabase.CreateAsset(newPlanetAppearance, currentEndingPath + newPlanetAppearance.name + ".asset");
                        generatedPlanetAppearances.Add(newPlanetAppearance);
                        EditorUtility.SetDirty(newPlanetAppearance);
                    }
                }
            }
            else if (envType == "trees")
            {
                foreach (string color in colorTypes)
                {
                    foreach (string shape in shapeTypes)
                    {
                        Tree newTree = ScriptableObject.CreateInstance<Tree>();
                        newTree.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + shape + "_" + envType + ".png", typeof(Sprite));
                        newTree.name = color + "_" + shape + "_" + envType;
                        newTree.shapeType = GetShapeType(shape);
                        newTree.colorType = GetColorType(color);
                        AssetDatabase.CreateAsset(newTree, currentEndingPath + newTree.name + ".asset");
                        generatedTrees.Add(newTree);
                        EditorUtility.SetDirty(newTree);
                    }
                }
            }

            EditorUtility.DisplayProgressBar("Generate Enviroment Data", "Generating " + envType, progress / maxProgress);
            progress++;
        }

        AssetDatabase.SaveAssets();
        EditorUtility.ClearProgressBar();
    }

    private void DeleteData()
    {
        generatedAtmospheres.Clear();
        generatedSeas.Clear();
        generatedRocks.Clear();
        generatedFauna.Clear();
        generatedPlanetAppearances.Clear();
        generatedTrees.Clear();

        foreach (string envType in coloredEnviromentTypes)
        {
            var files = Directory.GetFiles(endingPath + "/" + envType);
            for (int i = files.Length - 1; i >= 0; i--)
            {
                File.Delete(files[i]);
            }
        }

        foreach (string envType in shapedEnviromentTypes)
        {
            var files = Directory.GetFiles(endingPath + "/" + envType);
            for (int i = files.Length - 1; i >= 0; i--)
            {
                File.Delete(files[i]);
            }
        }

        foreach (string envType in coloredAndShapedEnviromentTypes)
        {
            var files = Directory.GetFiles(endingPath + "/" + envType);
            for (int i = files.Length - 1; i >= 0; i--)
            {
                File.Delete(files[i]);
            }
        }
    }

#endif

    private ColorType GetColorType(string color)
    {
        ColorType type = ColorType.purple;

        switch(color)
        {
            case "black":
                type = ColorType.purple;
                break;
            case "red":
                type = ColorType.red;
                break;
            case "green":
                type = ColorType.green;
                break;
            case "blue":
                type = ColorType.blue;
                break;
        }

        return type;
    }

    public ColorType GetComplementaryColorType(ColorType colorType)
    {
        ColorType type = ColorType.purple;

        int rand = Random.Range(0, 2);

        switch (colorType)
        {
            case ColorType.purple:
                if (rand == 0)
                {
                    type = ColorType.blue;
                }
                else
                {
                    type = ColorType.red;
                }
                break;
            case ColorType.red:
                if (rand == 0)
                {
                    type = ColorType.purple;
                }
                else
                {
                    type = ColorType.green;
                }
                break;
            case ColorType.green:
                if (rand == 0)
                {
                    type = ColorType.red;
                }
                else
                {
                    type = ColorType.blue;
                }
                break;
            case ColorType.blue:
                if (rand == 0)
                {
                    type = ColorType.green;
                }
                else
                {
                    type = ColorType.purple;
                }
                break;
        }

        return type;
    }

    private ShapeType GetShapeType(string shape)
    {
        ShapeType type = ShapeType.circle;

        switch (shape)
        {
            case "tri":
                type = ShapeType.tri;
                break;
            case "quad":
                type = ShapeType.quad;
                break;
            case "esa":
                type = ShapeType.esa;
                break;
            case "circle":
                type = ShapeType.circle;
                break;
        }

        return type;
    }

    public ShapeType GetComplementaryShapeType(ShapeType shapeType)
    {
        ShapeType type = ShapeType.circle;

        int rand = Random.Range(0,2);

        switch (shapeType)
        {
            case ShapeType.tri:
                if (rand == 0)
                {
                    type = ShapeType.quad;
                }
                else
                {
                    type = ShapeType.circle;
                }
                break;
            case ShapeType.quad:
                if (rand == 0)
                {
                    type = ShapeType.tri;
                }
                else
                {
                    type = ShapeType.esa;
                }
                break;
            case ShapeType.esa:
                if (rand == 0)
                {
                    type = ShapeType.quad;
                }
                else
                {
                    type = ShapeType.circle;
                }
                break;
            case ShapeType.circle:
                if (rand == 0)
                {
                    type = ShapeType.esa;
                }
                else
                {
                    type = ShapeType.tri;
                }
                break;
        }

        return type;
    }

    #endregion

}
