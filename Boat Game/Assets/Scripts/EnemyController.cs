using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float time;
    bool endReached = false;
    [SerializeField] float timeScale;
    [SerializeField] List<GameObject> checkpoints = new List<GameObject>(); //points to generate the bezier path
    [SerializeField] float totalRotation = 0;
    [SerializeField] float degreePerSecond = 10f;
    [SerializeField] float rotationAmount = 180f;
    [SerializeField] bool drawBezier = false;
    [SerializeField] bool startPathReversed = false;
    private void Start()
    {
        if (startPathReversed) checkpoints.Reverse(); //if the bool is turned on in the inspector have the starting path reversed
    }
    Vector3 GetBezier(List<GameObject> points, float t)
    {
        //this function is adapted from a public repo linked from this video tutorial https://www.youtube.com/watch?v=wtoPrJadjz4
        Vector3 resultingPos = BezierEquation.GetPointOnCurve(points[0].transform.position, points[1].transform.position, points[2].transform.position, points[3].transform.position, t);
        BezierEquation.DrawLines(points[0].transform.position, points[1].transform.position, points[2].transform.position, points[3].transform.position, t, drawBezier);
        return resultingPos;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        time += Time.fixedDeltaTime * timeScale; //time value for Bezier
        if (time >= 1f)
        {
            endReached = true;
            time = 0;
        }
        if (endReached == true)
        {
            if (Mathf.Abs(totalRotation) < Mathf.Abs(rotationAmount))
            {
                RotateShip(); //start rotating the ship if the total rotation has not yet reached the desired rotation to traverse back
            }
            else
            {
                endReached = false;
                time = 0;
                totalRotation = 0; //reset values
                checkpoints.Reverse(); //reverse bezier curve points so the ship starts to travel back

            }
        }
        else if (endReached == false) //if the ship has not reached the end of the bezier path
        {
            Vector3 newPosition = GetBezier(checkpoints, time); //get position it needs to travel to
            transform.LookAt(newPosition, Vector3.up); //ship needs to be turning towards the position on the bezier curve, so use lookAt
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z); //set the position
        }

    }
    void RotateShip()
    {
        transform.RotateAround(transform.position, new Vector3(0, 1, 0), Time.fixedDeltaTime * degreePerSecond); //rotate ship in y axis by set amount of degrees per frame
        totalRotation += Time.fixedDeltaTime * degreePerSecond;
    }
}
