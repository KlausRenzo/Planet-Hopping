using UnityEngine;

public class GrassHotspot : MonoBehaviour
{
    public EnviromentElement enviromentElement;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enviromentElement.enviromentSprite != null)
        {
            spriteRenderer.sprite = enviromentElement.enviromentSprite;
        }
    }
}