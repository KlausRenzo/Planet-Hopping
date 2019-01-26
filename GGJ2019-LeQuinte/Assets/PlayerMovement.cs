using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

    #region Fields

    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;

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

    #endregion

    #region Unity Callback

    private void Update()
    {
        if (canMove)
        {
            CheckInputs();
        }

        int cap = 0;
        int targetIndex = currentMovementIndex + 1;

        //player rotation perpendicular
        switch (currentShapeType)
        { 
            case ShapeType.circle:
                cap = currentPlanet.movementCircle.Count;
                if(targetIndex == cap)
                {
                    targetIndex = 0;
                }
                transform.up = NormalVectorBetweenTwo(currentPlanet.movementCircle[currentMovementIndex].transform.position, currentPlanet.movementCircle[targetIndex].transform.position);
                break;
            case ShapeType.tri:
                cap = currentPlanet.movementTri.Count;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }
                transform.up = NormalVectorBetweenTwo(currentPlanet.movementTri[currentMovementIndex].transform.position, currentPlanet.movementTri[targetIndex].transform.position);
                break;
            case ShapeType.quad:
                cap = currentPlanet.movementQuad.Count;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }
                transform.up = NormalVectorBetweenTwo(currentPlanet.movementQuad[currentMovementIndex].transform.position, currentPlanet.movementQuad[targetIndex].transform.position);
                break;
            case ShapeType.esa:
                cap = currentPlanet.movementEsa.Count;
                if (targetIndex == cap)
                {
                    targetIndex = 0;
                }
                transform.up = NormalVectorBetweenTwo(currentPlanet.movementEsa[currentMovementIndex].transform.position, currentPlanet.movementEsa[targetIndex].transform.position);
                break;
        }

        FollowLerp();
    }

    #endregion

    #region Methods

    public void RefreshMovementInfo()
    {
        currentMovementIndex = 0;
        currentShapeType = currentPlanet.planetInfos.planetAppearanceType.shapeType;
        switch(currentShapeType)
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
            //robe
        }
    }

    private bool CheckOverMoved(Directions dir)
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

        if (dir == Directions.Right)
        {
            switch (currentShapeType)
            {
                case ShapeType.circle:
                    if(Vector2.Distance(transform.position, currentPlanet.movementCircle[targetIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
                case ShapeType.tri:
                    if (Vector2.Distance(transform.position, currentPlanet.movementTri[targetIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
                case ShapeType.quad:
                    if (Vector2.Distance(transform.position, currentPlanet.movementQuad[targetIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
                case ShapeType.esa:
                    if (Vector2.Distance(transform.position, currentPlanet.movementEsa[targetIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
        else if (dir == Directions.Left)
        {
            switch (currentShapeType)
            {
                case ShapeType.circle:
                    if (Vector2.Distance(transform.position, currentPlanet.movementCircle[currentMovementIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
                case ShapeType.tri:
                    if (Vector2.Distance(transform.position, currentPlanet.movementTri[currentMovementIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
                case ShapeType.quad:
                    if (Vector2.Distance(transform.position, currentPlanet.movementQuad[currentMovementIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
                case ShapeType.esa:
                    if (Vector2.Distance(transform.position, currentPlanet.movementEsa[currentMovementIndex].transform.position) > vectorsDistance)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        return false;
    }

    private void Move(Directions dir)
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

        if (dir == Directions.Right)
        {
            transform.position += transform.right * playerSpeed;
            if(CheckOverMoved(dir))
            {
                currentMovementIndex++;
                if(currentMovementIndex >= cap)
                {
                    currentMovementIndex = 0;
                }
                switch (currentShapeType)
                {
                    case ShapeType.circle:
                        transform.position = currentPlanet.movementCircle[currentMovementIndex].transform.position;
                        break;
                    case ShapeType.tri:
                        transform.position = currentPlanet.movementTri[currentMovementIndex].transform.position;
                        break;
                    case ShapeType.quad:
                        transform.position = currentPlanet.movementQuad[currentMovementIndex].transform.position;
                        break;
                    case ShapeType.esa:
                        transform.position = currentPlanet.movementEsa[currentMovementIndex].transform.position;
                        break;
                }
            }
        }
        else if (dir == Directions.Left)
        {
            transform.position -= transform.right * playerSpeed;
            if(CheckOverMoved(dir))
            {
                Debug.Log("overwoopat a destra");
                currentMovementIndex--;
                if (currentMovementIndex < 0)
                {
                    currentMovementIndex = cap - 1;
                }
                switch (currentShapeType)
                {
                    case ShapeType.circle:
                        transform.position = currentPlanet.movementCircle[currentMovementIndex].transform.position;
                        break;
                    case ShapeType.tri:
                        transform.position = currentPlanet.movementTri[currentMovementIndex].transform.position;
                        break;
                    case ShapeType.quad:
                        transform.position = currentPlanet.movementQuad[currentMovementIndex].transform.position;
                        break;
                    case ShapeType.esa:
                        transform.position = currentPlanet.movementEsa[currentMovementIndex].transform.position;
                        break;
                }
            }
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