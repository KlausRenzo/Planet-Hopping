using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour 
{

    #region Fields

    public EnviromentElement enviromentElement;
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enviromentElement.enviromentSprite != null)
        {
            spriteRenderer.sprite = enviromentElement.enviromentSprite;
        }
    }

    #endregion

}
