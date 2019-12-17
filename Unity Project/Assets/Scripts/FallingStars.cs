using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStars : MonoBehaviour 
{

    #region Fields

    public GameObject fallingStar;
    public Vector2 starsAmountMinMax = new Vector2(2,15);
    public Vector2 starsFrequencyMinMax = new Vector2(0, 100);

    private float timer;

    #endregion

    #region Unity Callbacks

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 100/Random.Range(starsFrequencyMinMax.x, starsFrequencyMinMax.y))
        {
            timer = 0;
            SpawnStars();
        }
    }

    #endregion

    #region Methods

    private void SpawnStars()
    {
        int starsToSpawn = (int)Random.Range(starsAmountMinMax.x, starsAmountMinMax.y);
        for (int i = 0; i < starsToSpawn; i++)
        {
            GameObject newStar = Instantiate(fallingStar, transform);
            newStar.transform.position = new Vector2(Random.Range(-9f,9f), Random.Range(-5f,5f));
            newStar.GetComponent<Animator>().speed = Random.Range(0.5f,2f);
            newStar.transform.Rotate(Vector3.forward, Random.Range(30f, 60f));
        }
    }

    #endregion

}
