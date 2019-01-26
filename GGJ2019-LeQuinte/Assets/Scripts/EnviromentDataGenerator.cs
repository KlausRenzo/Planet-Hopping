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

            if (envType == "Atmospheres")
            {
                foreach (string color in colorTypes)
                {
                    Atmosphere newAtmosphere = ScriptableObject.CreateInstance<Atmosphere>();
                    if (File.Exists(spritePath + "/" + color + "_" + envType))
                    {
                        newAtmosphere.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + envType, typeof(Sprite));
                    }
                    newAtmosphere.name = color + "_" + envType;
                    newAtmosphere.colorType = GetColorType(color);
                    AssetDatabase.CreateAsset(newAtmosphere, currentEndingPath + newAtmosphere.name + ".asset");
                    EditorUtility.SetDirty(newAtmosphere);
                }
            }
            else if (envType == "Seas")
            {
                foreach (string color in colorTypes)
                {
                    Sea newSea = ScriptableObject.CreateInstance<Sea>();
                    if (File.Exists(spritePath + "/" + color + "_" + envType))
                    {
                        newSea.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + envType, typeof(Sprite));
                    }
                    newSea.name = color + "_" + envType;
                    newSea.colorType = GetColorType(color);
                    AssetDatabase.CreateAsset(newSea, currentEndingPath + newSea.name + ".asset");
                    EditorUtility.SetDirty(newSea);
                }
            }

            EditorUtility.DisplayProgressBar("Generate Enviroment Data", "Generating " + envType, progress / maxProgress);
            progress++;
        }

        foreach (string envType in shapedEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType + "/";

            if (envType == "Rocks")
            {
                foreach (string shape in shapeTypes)
                {
                    Rock newRock = ScriptableObject.CreateInstance<Rock>();
                    if (File.Exists(spritePath + "/" + shape + "_" + envType))
                    {
                        newRock.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + shape + "_" + envType, typeof(Sprite));
                    }
                    newRock.name = shape + "_" + envType;
                    newRock.shapeType = GetShapeType(shape);
                    AssetDatabase.CreateAsset(newRock, currentEndingPath + newRock.name + ".asset");
                    EditorUtility.SetDirty(newRock);
                }
            }
            else if (envType == "Fauna")
            {
                foreach (string shape in shapeTypes)
                {
                    Fauna newFauna = ScriptableObject.CreateInstance<Fauna>();
                    if (File.Exists(spritePath + "/" + shape + "_" + envType))
                    {
                        newFauna.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + shape + "_" + envType, typeof(Sprite));
                    }
                    newFauna.name = shape + "_" + envType;
                    newFauna.shapeType = GetShapeType(shape);
                    AssetDatabase.CreateAsset(newFauna, currentEndingPath + newFauna.name + ".asset");
                    EditorUtility.SetDirty(newFauna);
                }
            }

            EditorUtility.DisplayProgressBar("Generate Enviroment Data", "Generating " + envType, progress / maxProgress);
            progress++;
        }

        foreach (string envType in coloredAndShapedEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType + "/";

            if (envType == "PlanetAppearances")
            {
                foreach (string color in colorTypes)
                {
                    foreach (string shape in shapeTypes)
                    {
                        PlanetAppearance newPlanetAppearance = ScriptableObject.CreateInstance<PlanetAppearance>();
                        if (File.Exists(spritePath + "/" + color + "_" + shape + "_" + envType))
                        {
                            newPlanetAppearance.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + shape + "_" + envType, typeof(Sprite));
                        }
                        newPlanetAppearance.name = color + "_" + shape + "_" + envType;
                        newPlanetAppearance.shapeType = GetShapeType(shape);
                        newPlanetAppearance.colorType = GetColorType(color);
                        AssetDatabase.CreateAsset(newPlanetAppearance, currentEndingPath + newPlanetAppearance.name + ".asset");
                        EditorUtility.SetDirty(newPlanetAppearance);
                    }
                }
            }
            else if (envType == "Trees")
            {
                foreach (string color in colorTypes)
                {
                    foreach (string shape in shapeTypes)
                    {
                        Tree newTree = ScriptableObject.CreateInstance<Tree>();
                        if (File.Exists(spritePath + "/" + color + "_" + shape + "_" + envType))
                        {
                            newTree.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + shape + "_" + envType, typeof(Sprite));
                        }
                        newTree.name = color + "_" + shape + "_" + envType;
                        newTree.shapeType = GetShapeType(shape);
                        newTree.colorType = GetColorType(color);
                        AssetDatabase.CreateAsset(newTree, currentEndingPath + newTree.name + ".asset");
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
        ColorType type = ColorType.black;

        switch(color)
        {
            case "black":
                type = ColorType.black;
                break;
            case "red":
                type = ColorType.red;
                break;
            case "green":
                type = ColorType.green;
                break;
            case "white":
                type = ColorType.white;
                break;
            case "blue":
                type = ColorType.blue;
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
            case "penta":
                type = ShapeType.penta;
                break;
            case "circle":
                type = ShapeType.circle;
                break;
        }

        return type;
    }

#endregion

}
