using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnviromentDataGenerator : MonoBehaviour 
{

    #region Fields

    public List<string> enviromentTypes;

    private const string spritePath = "";
    private const string endingPath = "";
    private string[] shapeTypes = System.Enum.GetNames(typeof(ShapeType));
    private string[] colorTypes = System.Enum.GetNames(typeof(ColorType));

    #endregion

    #region Methods

    public void GenerateData()
    {
        DeleteData();

    }

    private void DeleteData()
    {
        Directory.GetFiles(endingPath);
    }

    #endregion

}
