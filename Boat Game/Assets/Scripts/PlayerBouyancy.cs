using System.Collections;
using System.Collections.Generic;
using UnityEngine; //https://www.youtube.com/watch?v=iasDPyC0QOg used this tutorial to create the code for the bouyancy for the speedboat.
//This allows for bouyancy to be calculated at different pivot points, rather than the midpoint of the boat, allowing it to keep afloat while moving under torque, rather than submerging in the ocean

public class PlayerBuoyancy : MonoBehaviour
{
    [SerializeField] Transform[] floatingTransforms; //get Transforms of all positions that allow object to be bouyant
    [SerializeField] float underWaterDrag = 3f;
    [SerializeField] float underWaterAngularDrag = 1f;
    [SerializeField] float airDrag = 0f;
    [SerializeField] float airAngularDrag = 0.05f;
    [SerializeField] float power = 15f; //upswards force to keep object bouyant
    [SerializeField] float depth = 0f; //height of the water
    Rigidbody playerRb;
    bool underwater;
    int pointsUnderWater; //number of points currently submerged

    // Start is called before the first frame update

    void Start()

    {
        playerRb = this.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()

    {
        pointsUnderWater = 0; //if the pointsUnderWater is at any point not 0, the rigidbody is treated as being under water
        for (int i = 0; i < floatingTransforms.Length; i++)
        {
            float diff = floatingTransforms[i].position.y - depth; //check if point is underwater
            if (diff < 0)
            {
                playerRb.AddForceAtPosition(Vector3.up * power * Mathf.Abs(diff), floatingTransforms[i].position, ForceMode.Force); //apply upwards force to point so that it is no longer underwater
                //lower the point the stronger the force
                pointsUnderWater += 1;
                if (!underwater) //if the object was previously not underwater
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }

        if (underwater && pointsUnderWater == 0) //if the rigidbody was underwater it is now no longer underwater
        {
            underwater = false;
            SwitchState(false);
        }
    }
    void SwitchState(bool submerged)
    {
        if (submerged) //apply relevant drag forces to objects above or below the water
        {
            playerRb.drag = underWaterDrag; //required drag forces for a submerged object
            playerRb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            playerRb.drag = airDrag; //required drag forces for an object in air
            playerRb.angularDrag = airAngularDrag;
        }

    }
}
