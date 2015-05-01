using UnityEngine;
using System.Collections;

 public class AILib  {
     GameObject user;
    public AILib(GameObject Yourself)
     {
         this.user = Yourself;
     }
	public Vector2 Seek ( Vector2 targetPosition, float Speed)
    {
        Vector2 steeringforce = targetPosition - (Vector2)user.transform.position ;
        steeringforce = steeringforce.normalized * Speed;
        return steeringforce;
    }
    public Vector2 Arrive(Vector2 targetPosition, float Speed,float slowingArea)
    {
        Vector2 steeringforce = targetPosition - (Vector2)user.transform.position;
        steeringforce = steeringforce.normalized;
        float Dist = Vector2.Distance((Vector2)user.transform.position, targetPosition);
        if (Dist < slowingArea)
        {
            steeringforce = steeringforce * (Dist / slowingArea) * Speed;
        }
        else
        {
            steeringforce *= Speed;
        }
        
        return steeringforce;
    }
    public Vector2 Flee(Vector2 targetPosition, float Speed)
    {
        Vector2 steeringforce = targetPosition - (Vector2)user.transform.position;
        steeringforce = - steeringforce.normalized * Speed;
        return steeringforce;

    }
    public Vector2 Chase(Vector2 targetPosition,Vector2 targetVelocity,float Speed)
    {
        float time = Vector2.Distance((Vector2)user.transform.position, targetPosition) / targetVelocity.magnitude;
        Vector2 futurePosition = targetPosition + targetVelocity * time;
        Vector2 steeringforce = futurePosition - (Vector2)user.transform.position;
        steeringforce = steeringforce.normalized * Speed;
        return steeringforce;
    }
    public Vector2 Evade(Vector2 targetPosition, Vector2 targetVelocity, float Speed)
    {
        float time = Vector2.Distance((Vector2)user.transform.position, targetPosition) / targetVelocity.magnitude;
        Vector2 futurePosition = targetPosition + targetVelocity * time;
        Vector2 steeringforce = futurePosition - (Vector2)user.transform.position;
        steeringforce = new Vector2(-steeringforce.y, steeringforce.x);
        steeringforce = steeringforce.normalized * Speed;
        return steeringforce;
    }
    public Vector2 Fly(Vector2 targetPosition, float force,float gravity,float hight)
    {
        Vector2 steeringforce = Vector2.zero;
        Vector2 direction = (Vector2)user.transform.position - targetPosition;
        if (direction.magnitude < hight)
            steeringforce = direction.normalized * force;
        else
            steeringforce = direction.normalized * gravity;
        return steeringforce;
    }
    public CircularMotion Wander(Vector2 randomPoint,float speed)
    {
        return CartToPolar(Arrive(randomPoint, speed,5));
    }
    private Vector2 _Cohesion(GameObject[] boids, float cohesionForce)
    {
        Vector2 centerOfMass = Vector2.zero;
        int count = 0;
        foreach (var boid in boids)
        {
            if (boid != user)
            {
                centerOfMass += (Vector2)boid.transform.position;
                count++;
            }
        }
        if (count > 0)
        {
            centerOfMass /= count;
        }
        Vector2 steeringForce = centerOfMass - (Vector2)user.transform.position;
        steeringForce = steeringForce.normalized * cohesionForce;
        return steeringForce;
    }
    private Vector2 _Separation(GameObject[] boids, float separationArea)
    {
        Vector2 steeringForce = Vector2.zero;
        float Distance = 0f;
        float Scale = 0f;
        Vector2 Hv = Vector2.zero;
        foreach (var boid in boids)
        {
            if (boid != user)
            {
                Distance = Vector2.Distance(user.transform.position, boid.transform.position);
                if (Distance < separationArea)
                {
                    // Get heading vector
                    Hv = user.transform.position - boid.transform.position;
                    Hv = Hv.normalized;
                    // get scale
                    Scale = Distance / separationArea;
                    steeringForce += Hv / Scale;
                }
            }

        }
        return steeringForce;
    }
    private Vector2 _Allignment(GameObject[] boids, float alignForce)
    {
        Vector2 centerOfMass = Vector2.zero;
        int count = 0;

        foreach (var boid in boids)
        {
            if (boid != user)
            {
                centerOfMass += boid.GetComponent<Rigidbody2D>().velocity;
                count++;
            }
        }

        if (count > 0)
        {
            centerOfMass /= count;
        }

        Vector2 steeringForce = centerOfMass; //- rigidbody2D.velocity;
        steeringForce = steeringForce.normalized * alignForce;
        return steeringForce;
    }
    public Vector2 Flocking(GameObject[] boids,float cohesionForce,float separationArea, float alignForce)
    {
        Vector2 steeringforce = _Cohesion(boids, cohesionForce) + _Separation(boids, separationArea) + _Allignment(boids, alignForce);
        return steeringforce;
    }
    
    public CircularMotion CartToPolar(Vector2 vector)
    {
        Vector2 tangent = user.transform.right;
        float cosTheta = Vector2.Dot(tangent,vector);
        Direction dir = (cosTheta>0f)? Direction.ClockWise:Direction.AntiClockWise;
        CircularMotion polar = new CircularMotion(dir, vector.magnitude);
        return polar;
    }
}
