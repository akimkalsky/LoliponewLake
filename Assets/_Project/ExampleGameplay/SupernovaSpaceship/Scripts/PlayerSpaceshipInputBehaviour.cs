using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceshipInputBehaviour : MonoBehaviour
{
    
    [Header("Input Smoothing")]
    //Steering
    public float steeringSmoothing;
    private Vector3 rawInputSteering;
    private Vector3 smoothInputSteering;

    //Thrust
    public float thrustSmoothing;
    private float rawInputThrust;
    private float smoothInputThrust;





    
    //AccShip
    public float myAccelaration;
    public float myAccelarationDecay;

    public float myCurrentSpeed;


    //Shooting
    private bool shootHeld;

    [Header("Data Output")]
    public SpaceshipData spaceshipData;

    public void OnSteering(InputAction.CallbackContext value)
    {
        Vector2 rawInput = value.ReadValue<Vector2>();
        rawInputSteering = new Vector3(rawInput.y, 0, -rawInput.x);

         
        
    }

    public void OnThrust(InputAction.CallbackContext value)
    {
        rawInputThrust = value.ReadValue<float>();
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            shootHeld = true;
        } else if(value.canceled)
        {
            shootHeld = false;
        }
    } 

    void Update()
    {
        InputSmoothing();
        SetInputData();

         if(Input.GetKey(KeyCode.H))
        { 
            if(spaceshipData.thrustAmount<120)
            {spaceshipData.thrustAmount+=myAccelaration;}
           
        }
         if(!Input.GetKeyDown(KeyCode.H))
        { 
            if(spaceshipData.thrustAmount>51)
            {spaceshipData.thrustAmount-=myAccelarationDecay;}



        }



    }

     void InputSmoothing()
    {
        //Steering
        smoothInputSteering = Vector3.Lerp(smoothInputSteering, rawInputSteering, Time.deltaTime * steeringSmoothing);

        //Thrust
        smoothInputThrust = Mathf.Lerp(smoothInputThrust, rawInputThrust, Time.deltaTime * thrustSmoothing);
    }

    void SetInputData()
    {
        spaceshipData.UpdateInputData(smoothInputSteering, smoothInputThrust, shootHeld);
    }

}
