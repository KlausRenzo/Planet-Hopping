using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    #region Fields

    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;

    public float timeForPlanetHop = 2;
    public float playerSpeed;

    public Planet currentPlanet;
    public bool canMove = false;

    [HideInInspector]
    public Directions currentDirection;

    private float currentLerpIndex;
    public int currentMovementIndex;
    private float currentLerp;
    private ShapeType currentShapeType;
    private int horizontalMovement;
    private Animator anim;
    private float startJumpTime;

    #endregion

    #region Unity Callback

    private void Start()
    {
        anim = GetComponent<Animator>();
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
                if(targetIndex == cap)
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

        if (Input.GetKeyDown(jump))
        {
            startJumpTime = Time.time;
        }
        if (Input.GetKeyUp(jump))
        {
            if(Time.time - startJumpTime <= 0)
            {
                //planetHop
            }
            else
            {
                anim.SetInteger("Status", 2);
            }
        }
    }

    public void SetLanded()
    {
        anim.SetBool("isJumping", false);
    }

    public void EndlessCharge()
    {
        anim.speed = 0;
    }

    private void ReleaseCharge()
    {
        anim.speed = 1;
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
                transform.position = Vector3.Lerp(currentPlanet.movementCircle[currentMovementIndex].transform.position, currentPlanet.movementCircle[(currentMovementIndex + 1) % cap].transform.position, currentLerpIndex % 1);
                break;
            case ShapeType.tri:
                transform.position = Vector3.Lerp(currentPlanet.movementTri[currentMovementIndex].transform.position, currentPlanet.movementTri[(currentMovementIndex + 1) % cap].transform.position, currentLerpIndex % 1);
                break;
            case ShapeType.quad:
                transform.position = Vector3.Lerp(currentPlanet.movementQuad[currentMovementIndex].transform.position, currentPlanet.movementQuad[(currentMovementIndex + 1) % cap].transform.position, currentLerpIndex % 1);
                break;
            case ShapeType.esa:
                transform.position = Vector3.Lerp(currentPlanet.movementEsa[currentMovementIndex].transform.position, currentPlanet.movementEsa[(currentMovementIndex + 1) % cap].transform.position, currentLerpIndex % 1);
                break;
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

    
	#endregion

}