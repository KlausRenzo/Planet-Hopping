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

    [Button(Name = "Generate Enviroment Data", ButtonHeight = 50)]
    public void GenerateData()
    {
        DeleteData();

        foreach (string envType in coloredEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType;

            if (envType == "Atmospheres")
            {
                foreach (string color in colorTypes)
                {
                    Atmosphere newAtmosphere = ScriptableObject.CreateInstance<Atmosphere>();
                    newAtmosphere.enviromentSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath + "/" + color + "_" + envType, typeof(Sprite));
                }
            }
            else if (envType == "Seas")
            {
                foreach (string color in colorTypes)
                {

                }
            }
        }

        foreach (string envType in shapedEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType;

            if (envType == "Rocks")
            {
                foreach (string shape in shapeTypes)
                {

                }
            }
            else if (envType == "Fauna")
            {
                foreach (string shape in shapeTypes)
                {

                }
            }
        }

        foreach (string envType in coloredAndShapedEnviromentTypes)
        {
            string currentEndingPath = endingPath + "/" + envType;

            if (envType == "PlanetAppearances")
            {
                foreach (string color in colorTypes)
                {
                    foreach (string shape in shapeTypes)
                    {

                    }
                }
            }
            else if (envType == "Trees")
            {
                foreach (string color in colorTypes)
                {
                    foreach (string shape in shapeTypes)
                    {

                    }
                }
            }
        }
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

    #endregion

}
