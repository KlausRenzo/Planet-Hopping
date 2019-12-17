using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetPreview : MonoBehaviour 
{

	#region Fields

    public float planetRotation = 1;
    public float cloudBgRotation = 2;
    public float cloudFgRotation = 1.5f;

    public Image planetImage;
    public Image bgCloudImage;
    public Image fgCloudImage;
    public Image atmosphereImage;

    #endregion

    #region Unity Callbacks

    private void Update()
    {
        PreviewTick();
    }

    #endregion

    #region Methods

    private void PreviewTick()
    {
        planetImage.transform.Rotate(Vector3.forward, planetRotation);
        fgCloudImage.transform.Rotate(Vector3.forward, cloudFgRotation);
        bgCloudImage.transform.Rotate(Vector3.forward, cloudBgRotation);
    }

    #endregion

}
