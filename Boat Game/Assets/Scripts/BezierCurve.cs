using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierEquation
{
    public static Vector3 GetPointOnCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        //formula taken from this website https://denisrizov.com/2016/06/02/bezier-curves-unity-package-included/
        //bezier curve is essentially a series of lerps between points p0 and p1, p1 and p2 and p2 and p3
        //these lerps return Vector3 values of a, b and c respectively. Lerp between a and b and b and c to give d and e
        //final point is a lerp between d and e
        //using a series of lerps is not optimal so use this simplified formula instead
        float u = 1f - t; //part of lerp formula: (1f-t) * a + t * b
        float t2 = t * t; //represents time value squared
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t; //represents time value cubed
        Vector3 result = (u3) * p0 + (3f * u2 * t) * p1 + (3f * u * t2) * p2 + (t3) * p3; //finalised formula derived from website
        return result;
    }
    public static void DrawLines(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float time, bool on)
    {
        if (on) //using lerps is not optimal so a bool is passed from the enemy ships that can be turned on or off in the inspector
        { //leaving it off bypasses this code
            Debug.DrawLine(p0, p1, Color.green); //drawing gizmo code was taken from the public repo linked by this tutorial
            Debug.DrawLine(p1, p2, Color.green); //https://www.youtube.com/watch?v=wtoPrJadjz4
            Debug.DrawLine(p2, p3, Color.green);

            Vector3 a = Vector3.Lerp(p0, p1, time);
            Vector3 b = Vector3.Lerp(p1, p2, time);
            Vector3 c = Vector3.Lerp(p2, p3, time);

            Debug.DrawLine(a, b, Color.yellow);
            Debug.DrawLine(b, c, Color.yellow);

            Vector3 d = Vector3.Lerp(a, b, time);
            Vector3 e = Vector3.Lerp(b, c, time);

            Debug.DrawLine(d, e, Color.red);
        }
    }
}
