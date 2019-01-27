using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour 
{

    #region Fields

    public EnviromentElement enviromentElement;

    public bool canBePickedUp = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canBePickedUp = true;
        }
    }

    #endregion

    #region Methods

    public void PickUp()
    {

    }

    #endregion

}
