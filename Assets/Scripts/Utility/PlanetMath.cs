using UnityEngine;
using System.Collections;

public static class PlanetMath  {
    //Such WOW much Amaze a class with comments

    /*
     * This Functions returns the desired force direction from the movemnt vector
     * Params : 
     *  * Vector2 force: this is the non plantery projected vector
     *  * Vector2 desiredDirection: this is the desired direction the object want to move to  should be transform.right on most cases
     *  -------------------------------------------------------------------------------------------
     *                                        __ 1  if the desired force is in the right direction
     * returns : float that can either be  --|__ 0  if the desired force is zero
     *                                       |__ -1 if the desired force is in the negative right direction
     * --------------------------------------------------------------------------------------------
     * Revision History:
     *      * 29/3 : created                   /Bakr
     *      * 
     * 
     */
    public static float  PlanteryDirictionFromVector(Vector2 force,Vector2 currentLocation){
        
        float cosTheta = Vector2.Dot(currentLocation, force);
        return (cosTheta > 0f) ? 1: -1;
    }
    /*
     * This function returns the optimal radial distance between two objects on the surface of the planet 
     * it assumes that the planet is placed at (0,0)
     *  * Params : 
     *  * Vector2 pos1      : this is the position of the first object
     *  * Vector2 pos2      : this is the position of the second object
     *  -------------------------------------------------------------------------------------------
     * returns : a float representing the distance
     * Discalmer   : The results are far from accurate
     * Disclamer 2 : It will not work correctly with multi dug pathes
     * --------------------------------------------------------------------------------------
     * LOG
     * ---
     * 30/3/2015 Created                                                    |       Bakr
     */
    public static float PlanteryDistance(Vector2 pos1,Vector2 pos2)
    {
        float radius = Mathf.Max(pos1.magnitude, pos2.magnitude);
        float theta = Vector3.Angle(pos1, pos2);
        theta*=Mathf.Deg2Rad;
        //this is added to make compnstate for the diffrence due to digging
        float elevationDiffence = Mathf.Abs(pos2.magnitude-pos1.magnitude);
        return Mathf.Abs(theta * radius)+elevationDiffence;
    }
    /*
 * This function returns the shortest direction between two vectors
 * it assumes that the planet is placed at (0,0)
 *  * Params : 
 *  * Vector2 pos1      : this is the position of the first object
 *  * Vector2 pos2      : this is the position of the second object
 *  -------------------------------------------------------------------------------------------
 * returns 
     * an int representing the direction
 *  -------------------------------------------------------------------------------------------
 * LOG
 * ---
 * 31/3/2015 Created                                                    |       Bakr
 */
    public static float ShortestDirection(Vector2 pos1, Vector2 pos2)
    {
        float temp = pos2.y;
        pos2.y = -pos2.x;
        pos2.x = temp;
        return -Mathf.Sign( Vector3.Dot(pos1,pos2));
    }
}
