using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    public enum SoundKeys
    {
        Walk,
        Hop,
        Jump,
        Land,
        Atmos,
        Tree,
        Rock,
        Sea,
    }

    #region Fields

    public Action<EnviromentElement> PickUpElement;

    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode pickUp;

    public float timeForPlanetHop = 2;
    public float playerSpeed;

    public Planet currentPlanet;
    public bool canMove = false;

    public List<AudioClip> playerWalking;
    public AudioClip planetHopSound;
    public AudioClip jumpSound;
    public AudioClip landingSound;
    public AudioClip pickupAtmosphere;
    public AudioClip pickupTree;
    public AudioClip pickupRock;
    public AudioClip pickupSea;
    public AudioClip pickupFauna;

    [HideInInspector]
    public Directions currentDirection;

    public List<HotSpot> pickableHotSpots;

    private float currentLerpIndex;
    private int currentMovementIndex;
    private float currentLerp;
    private AudioSource audioSource;
    private ShapeType currentShapeType;
    private int horizontalMovement;
    private Animator anim;
    private bool isCharging = false;
    public bool isJumping = false;

    #endregion

    #region Unity Callback

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canMove)
        {
            CheckInputs();
        }

        int cap = 0;
        int targetIndex = currentMovementIndex + 1;
        Vector3 up = new Vector3();
        //player rotation perpendicular
        switch (currentShapeType)
        {
            case ShapeType.circle:
                cap = currentPlanet.movementCircle.Count;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                up = NormalVectorBetweenTwo(currentPlanet.movementCircle[currentMovementIndex].transform.position, currentPlanet.movementCircle[targetIndex].transform.position);
                break;
            case ShapeType.tri:
                cap = currentPlanet.movementTri.Count;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                up = NormalVectorBetweenTwo(currentPlanet.movementTri[currentMovementIndex].transform.position, currentPlanet.movementTri[targetIndex].transform.position);
                break;
            case ShapeType.quad:
                cap = currentPlanet.movementQuad.Count;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                up = NormalVectorBetweenTwo(currentPlanet.movementQuad[currentMovementIndex].transform.position, currentPlanet.movementQuad[targetIndex].transform.position);
                break;
            case ShapeType.esa:
                cap = currentPlanet.movementEsa.Count;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                up = NormalVectorBetweenTwo(currentPlanet.movementEsa[currentMovementIndex].transform.position, currentPlanet.movementEsa[targetIndex].transform.position);
                break;
        }

        transform.up = Vector3.Lerp(transform.up, up, 0.2f);

        if (horizontalMovement == 1)
        {
            Look(Directions.Right);
        }
        else if (horizontalMovement == -1)
        {
            Look(Directions.Left);
        }

        FollowLerp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("HotSpot"))
        {
            HotSpot currentHotSpot = collision.gameObject.GetComponent<HotSpot>();

            pickableHotSpots.Add(currentHotSpot);
        }

        ClearListFromNone(pickableHotSpots);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HotSpot"))
        {
            HotSpot currentHotSpot = collision.gameObject.GetComponent<HotSpot>();

            pickableHotSpots.Remove(currentHotSpot);
        }

        ClearListFromNone(pickableHotSpots);
    }

    #endregion

    #region Methods

    private void CheckInputs()
    {
        horizontalMovement = 0;

        if (Input.GetKey(moveLeft))
        {
            horizontalMovement--;
        }

        if (Input.GetKey(moveRight))
        {
            horizontalMovement++;
        }

        if (Input.GetKeyDown(jump) && !isJumping)
        {
            anim.SetInteger("Status", 1);
        }
        if (Input.GetKeyUp(jump) && !isJumping)
        {
            isJumping = true;

            if (isCharging)
            {
                ReleaseCharge();
                anim.SetInteger("Status", 3);
            }
            else
            {
                anim.SetInteger("Status", 2);
            }
        }

        if(Input.GetKeyDown(pickUp))
        {
            if(isCharging || anim.GetInteger("Status") == 1)
            {
                ReleaseCharge();
                anim.SetInteger("Status", 0);
            }
            else
            {
                PickUp();
            }
        }
    }

    private void PickUp()
    {
        if (pickableHotSpots.Count == 0)
        {
            PickUpElement(currentPlanet.planetInfos.atmosphereType);
        }
        else
        {
            PickUpElement(pickableHotSpots[0].enviromentElement);
        }
    }

    private void ClearListFromNone(List<HotSpot> list)
    {
        for(int i = list.Count - 1; i >= 0; i--)
        {
            if(list[i] == null)
            {
                list.RemoveAt(i);
            }
        }
    }

    public void SetStatus(int integer)
    {
        anim.SetInteger("Status", integer);
    }

    public void SetLanded()
    {
        isJumping = false;
        anim.SetInteger("Status", 0);
    }

    public void EndlessCharge()
    {
        isCharging = true;
        anim.speed = 0;
    }

    private void ReleaseCharge()
    {
        anim.speed = 1;
        isCharging = false;
    }

    public void RefreshMovementInfo()
    {
        currentMovementIndex = 0;
        currentShapeType = currentPlanet.planetInfos.planetAppearanceType.shapeType;
        switch (currentShapeType)
        {
            case ShapeType.circle:
                transform.position = currentPlanet.movementCircle[0].transform.position;
                break;
            case ShapeType.tri:
                transform.position = currentPlanet.movementTri[0].transform.position;
                break;
            case ShapeType.quad:
                transform.position = currentPlanet.movementQuad[0].transform.position;
                break;
            case ShapeType.esa:
                transform.position = currentPlanet.movementEsa[0].transform.position;
                break;
        }
    }

    private void Look(Directions dir)
    {
        if (dir == Directions.Right)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir == Directions.Left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FollowLerp()
    {
        int cap = GetCurrentShapeCap();

        currentLerpIndex += (horizontalMovement * Time.deltaTime) / CalculateVectorDistace();
        currentLerpIndex = (currentLerpIndex + cap) % cap;

        currentMovementIndex = Mathf.FloorToInt(currentLerpIndex);

        switch (currentShapeType)
        {
            case ShapeType.circle:
                transform.position = Vector3.Lerp(currentPlanet.movementCircle[currentMovementIndex].transform.position, currentPlanet.movementCircle[(currentMovementIndex + 1) % cap].transform.position,
                    currentLerpIndex % 1);
                break;
            case ShapeType.tri:
                transform.position = Vector3.Lerp(currentPlanet.movementTri[currentMovementIndex].transform.position, currentPlanet.movementTri[(currentMovementIndex + 1) % cap].transform.position, currentLerpIndex % 1);
                break;
            case ShapeType.quad:
                transform.position = Vector3.Lerp(currentPlanet.movementQuad[currentMovementIndex].transform.position, currentPlanet.movementQuad[(currentMovementIndex + 1) % cap].transform.position,
                    currentLerpIndex % 1);
                break;
            case ShapeType.esa:
                transform.position = Vector3.Lerp(currentPlanet.movementEsa[currentMovementIndex].transform.position, currentPlanet.movementEsa[(currentMovementIndex + 1) % cap].transform.position, currentLerpIndex % 1);
                break;
        }
    }

    public void AgentLerp()
    {
        RefreshMovementInfo();
        bool stop = false;
        while (!stop)
        {
            int cap = GetCurrentShapeCap();
            Debug.Log($"agent lerping: {currentLerpIndex}");
            currentLerpIndex += (Time.deltaTime) / CalculateVectorDistace();
            if (currentLerpIndex >= cap)
                stop = true;
            currentLerpIndex = (currentLerpIndex + cap) % cap;

            currentMovementIndex = Mathf.FloorToInt(currentLerpIndex);

            switch (currentShapeType)
            {
                case ShapeType.circle:
                    transform.position = Vector3.Lerp(currentPlanet.movementCircle[currentMovementIndex].transform.position, currentPlanet.movementCircle[(currentMovementIndex + 1) % cap].transform.position,
                        currentLerpIndex % 1);
                    break;
                case ShapeType.tri:
                    transform.position = Vector3.Lerp(currentPlanet.movementTri[currentMovementIndex].transform.position, currentPlanet.movementTri[(currentMovementIndex + 1) % cap].transform.position,
                        currentLerpIndex % 1);
                    break;
                case ShapeType.quad:
                    transform.position = Vector3.Lerp(currentPlanet.movementQuad[currentMovementIndex].transform.position, currentPlanet.movementQuad[(currentMovementIndex + 1) % cap].transform.position,
                        currentLerpIndex % 1);
                    break;
                case ShapeType.esa:
                    transform.position = Vector3.Lerp(currentPlanet.movementEsa[currentMovementIndex].transform.position, currentPlanet.movementEsa[(currentMovementIndex + 1) % cap].transform.position,
                        currentLerpIndex % 1);
                    break;
            }
        }
    }

    private float CalculateVectorDistace()
    {
        int cap = 0;
        int targetIndex = 0;
        float vectorsDistance = 0;

        switch (currentShapeType)
        {
            case ShapeType.circle:
                cap = currentPlanet.movementCircle.Count;
                targetIndex = currentMovementIndex + 1;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                vectorsDistance = Vector2.Distance(currentPlanet.movementCircle[currentMovementIndex].transform.position, currentPlanet.movementCircle[targetIndex].transform.position);
                break;
            case ShapeType.tri:
                cap = currentPlanet.movementTri.Count;
                targetIndex = currentMovementIndex + 1;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                vectorsDistance = Vector2.Distance(currentPlanet.movementTri[currentMovementIndex].transform.position, currentPlanet.movementTri[targetIndex].transform.position);
                break;
            case ShapeType.quad:
                cap = currentPlanet.movementQuad.Count;
                targetIndex = currentMovementIndex + 1;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                vectorsDistance = Vector2.Distance(currentPlanet.movementQuad[currentMovementIndex].transform.position, currentPlanet.movementQuad[targetIndex].transform.position);
                break;
            case ShapeType.esa:
                cap = currentPlanet.movementEsa.Count;
                targetIndex = currentMovementIndex + 1;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }

                vectorsDistance = Vector2.Distance(currentPlanet.movementEsa[currentMovementIndex].transform.position, currentPlanet.movementEsa[targetIndex].transform.position);
                break;
        }

        return vectorsDistance;
    }

    private int GetCurrentShapeCap()
    {
        int cap = 0;

        switch (currentShapeType)
        {
            case ShapeType.circle:
                cap = currentPlanet.movementCircle.Count;
                break;
            case ShapeType.tri:
                cap = currentPlanet.movementTri.Count;
                break;
            case ShapeType.quad:
                cap = currentPlanet.movementQuad.Count;
                break;
            case ShapeType.esa:
                cap = currentPlanet.movementEsa.Count;
                break;
        }

        return cap;
    }

    private Vector3 NormalVectorBetweenTwo(Vector3 firstVector, Vector3 secondVector)
    {
        return Vector2.Perpendicular(secondVector - firstVector);
    }

    public void PlaySound(SoundKeys soundKey)
    {
        switch(soundKey)
        {
            case SoundKeys.Walk:
                audioSource.PlayOneShot(playerWalking[UnityEngine.Random.Range(0, playerWalking.Count)]);
                break;
            case SoundKeys.Hop:
                audioSource.PlayOneShot(planetHopSound);
                break;
            case SoundKeys.Jump:
                audioSource.PlayOneShot(jumpSound);
                break;
            case SoundKeys.Land:
                audioSource.PlayOneShot(landingSound);
                break;
            case SoundKeys.Tree:
                audioSource.PlayOneShot(pickupTree);
                break;
            case SoundKeys.Rock:
                audioSource.PlayOneShot(pickupRock);
                break;
            case SoundKeys.Sea:
                audioSource.PlayOneShot(pickupSea);
                break;
            case SoundKeys.Atmos:
                audioSource.PlayOneShot(pickupAtmosphere);
                break;
        }
    }

	#endregion

}
