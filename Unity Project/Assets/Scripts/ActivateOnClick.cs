using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnClick : MonoBehaviour 
{

    #region Fields

    public GameObject objectToActivate;

	#endregion

	#region Methods

    public void Activate()
    {
        objectToActivate.SetActive(true);
    }

    public void Deactivate()
    {
        objectToActivate.SetActive(false);
    }

    #endregion

}
