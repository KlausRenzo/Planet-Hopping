using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : MonoBehaviour 
{

    #region Fields

    public EnviromentElement enviromentElement;
    public GameObject interactableParticle;
    public GameObject pickupParticle;
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

    public void InteractableParticleOn()
    {
        interactableParticle.GetComponent<ParticleSystem>().Play();
    }

    public void InteractableParticleOff()
    {
        interactableParticle.GetComponent<ParticleSystem>().Stop();
    }

    public void PickedUp()
    {
        pickupParticle.GetComponent<ParticleSystem>().Play();
    }

    #endregion

}
