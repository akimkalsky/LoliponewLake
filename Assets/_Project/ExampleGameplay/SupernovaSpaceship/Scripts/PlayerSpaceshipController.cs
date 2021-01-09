using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceshipController : MonoBehaviour
{
    [Header("Spaceship Settings")]
    public SpaceshipData data;

    [Header("Physics")]
    public Rigidbody spaceshipRigidbody;

    [Header("Shooting")]
    public ObjectPoolBehaviour projectileObjectPool;
    public Transform projectileSpawnTransform;
    private float nextShot = 0.1f;
    public Transform shipModel;


    //mynewcode Sututff
    public float myRotateSpeed;

      public Vector3 myCurrentRotation;





    void Update()
    {
        MoveSpaceship();
        TurnSpaceship();
        


        CalculateShootingLogic();
        
           myCurrentRotation=shipModel.localEulerAngles;



    }

     void MoveSpaceship()
    {
        spaceshipRigidbody.velocity = transform.forward * data.thrustAmount * (Mathf.Max(data.thrustInput,.2f));
    }

    void TurnSpaceship()
    {


        //roate here?
        Vector3 newTorque = new Vector3(data.steeringInput.x * data.pitchSpeed, -data.steeringInput.z * data.yawSpeed, 0);
        spaceshipRigidbody.AddRelativeTorque(newTorque);

        spaceshipRigidbody.rotation =
            Quaternion.Slerp(spaceshipRigidbody.rotation, Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0)), .5f);

        VisualSpaceshipTurn();


       





    }

    void VisualSpaceshipTurn()
    {

        {shipModel.localEulerAngles = new Vector3(data.steeringInput.x * data.leanAmount_Y
            , shipModel.localEulerAngles.y, data.steeringInput.z * data.leanAmount_X);}

           



 
       

        //SOME STUFF I Thought could be intereting
       if(myCurrentRotation.z>=78&& myCurrentRotation.z<=91){Debug.Log("90Degrees");}


        if(myCurrentRotation.z>=268&& myCurrentRotation.z<=271){Debug.Log("270Degrees");}







        

            




    }

    void CalculateShootingLogic()
    {
        if(data.shootInput == true)
        {
            if(Time.time > nextShot)
            {
                ShootProjectile();
                nextShot = Time.time + data.shootRate;
            }
        }
    }

    void ShootProjectile()
    {
        
        GameObject newProjectile = projectileObjectPool.GetPooledObject();
        newProjectile.transform.position = projectileSpawnTransform.position;
        newProjectile.transform.rotation = projectileSpawnTransform.rotation;
        newProjectile.SetActive(true);
        
    }


}
