using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_planetinfo", menuName = "PlanetInfo")]
public class PlanetInfo : ScriptableObject 
{

    #region Fields

    public Sea seaType;
    public Tree treeType;
    public Rock rockType;
    public Fauna faunaType;
    public Atmosphere atmosphereType;   
    public PlanetAppearance planetAppearanceType;
    public float bgCloudSpeed;
    public float fgCloudSpeed;
    public float planetSpeed;

    #endregion

}
