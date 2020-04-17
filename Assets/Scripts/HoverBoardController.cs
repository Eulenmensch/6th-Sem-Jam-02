using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class HoverBoardController : MonoBehaviour
{
    public float ForwardAccelleration;
    public float BackwardAccelleration;
    public float TurnForce;
    public float HoverForce;
    public float HoverHeight;
    public GameObject[] HoverPoints;
    public LayerMask RayLayerMask;

    private Rigidbody RigidbodyReference;
    private float CurrentThrust;
    private float CurrentTurnForce;

    private Vector2 InputVector;


    // Start is called before the first frame update
    void Start()
    {
        RigidbodyReference = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveInput();
    }

    private void FixedUpdate()
    {
        Hover();
        ApplyMoveInput();
    }

    void Hover()
    {
        RaycastHit hit;
        foreach (var hoverPoint in HoverPoints)
        {
            if (Physics.Raycast(hoverPoint.transform.position, Vector3.down, out hit, HoverHeight, RayLayerMask))
            {
                //RigidbodyReference.AddForceAtPosition(Vector3.up * HoverForce * (1.0f - (hit.distance / HoverHeight)), hoverPoint.transform.position);
                RigidbodyReference.AddForceAtPosition(Vector3.up * (HoverForce / Mathf.Pow(hit.distance, HoverHeight)), hoverPoint.transform.position);
            }
            else
            {
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    RigidbodyReference.AddForceAtPosition(hoverPoint.transform.up * HoverForce, hoverPoint.transform.position);
                }
                else
                {
                    RigidbodyReference.AddForceAtPosition(hoverPoint.transform.up * -HoverForce, hoverPoint.transform.position);
                }
            }
        }
    }

    void HandleMoveInput()
    {
        CurrentThrust = 0.0f;
        float thrustInput = InputVector.y;
        if (thrustInput > 0)
        {
            CurrentThrust = thrustInput * ForwardAccelleration;
        }
        else if (thrustInput < 0)
        {
            CurrentThrust = thrustInput * BackwardAccelleration;
        }

        CurrentTurnForce = 0.0f;
        float turnInput = InputVector.x;
        CurrentTurnForce = turnInput * TurnForce;
    }

    void ApplyMoveInput()
    {
        if (CurrentThrust != 0)
        {
            RigidbodyReference.AddForce(transform.forward * CurrentThrust);
        }

        if (CurrentTurnForce != 0)
        {
            RigidbodyReference.AddRelativeTorque(Vector3.up * CurrentTurnForce);
        }
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
    }
}
