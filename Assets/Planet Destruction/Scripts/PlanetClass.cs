using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ClipperLib;

public class PlanetClass : MonoBehaviour {
    public float Radius;
    public float shellThickness = 0.25f;
    public int DiggingResolutionScale = 100;
    public Vector2[] points;
    public PolygonCollider2D []planetColliders;
	// Use this for initialization
	void Start () {
	    planetColliders= this.GetComponents<PolygonCollider2D>();
	}
	

    public void Dig(PolygonCollider2D collider, Vector3 position)
    {
        PlanetUtils.PrintArray(collider.points);


        //PolygonCollider2D planetCollider = GetComponent<PolygonCollider2D>();
        //Vector2[] points = collider.points;
        //clipping point should be translated to mouse position relative to planet space
        //assume the planet in (0,0,0)
        Vector2[] clippingPoints = TranslateArray(collider.points, position.x - transform.position.x, position.y-transform.position.y);
        List<IntPoint> orignalPoly = Vec2ArrtoPolygon(points, DiggingResolutionScale);
        List<IntPoint> clipPoly = Vec2ArrtoPolygon(clippingPoints, DiggingResolutionScale);
        Clipper c = new Clipper();
        c.AddPath(orignalPoly, PolyType.ptSubject, true);
        c.AddPath(clipPoly, PolyType.ptClip, true);
        List<List<IntPoint>> result = new List<List<IntPoint>>();
        c.Execute(ClipType.ctDifference, result);
        Mesh planetMesh = GetComponent<MeshFilter>().mesh;
        planetMesh.triangles = null;
        planetMesh.vertices = PolyToVector3Arr(result[0], DiggingResolutionScale);
        Triangulator3 tri = new Triangulator3(planetMesh.vertices);
        planetMesh.triangles = tri.Triangulate();
        points = PolygonTovec2Arr(result[0], DiggingResolutionScale);
        for (int i = 0; i < 3; i++)
            SmoothOuterContour();
        UpdateCollider();

    }
    private Vector3[] TranslateArray(Vector3[] data, float dx, float dy, float dz)
    {
        Vector3[] data2 = new Vector3[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            data2[i].x = data[i].x + dx;
            data2[i].y = data[i].y + dy;
            data2[i].z = data[i].z + dz;
        }
        return data2;
    }
    private Vector2[] TranslateArray(Vector2[] data, float dx, float dy)
    {
        Vector2[] data2 = new Vector2[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            data2[i].x = data[i].x + dx;
            data2[i].y = data[i].y + dy;
        }
        return data2;
    }
    //private void TranslateArray(Vector2[] data, float dx, float dy)
    //{
    //    for (int i = 0; i < data.Length; i++)
    //    {
    //        data[i].x += dx;
    //        data[i].y += dy;
    //    }
    //}
    Vector2[] Vec3ToVec2Arr(Vector3[] data)
    {
        Vector2[] vec2Data = new Vector2[data.Length];
        for (int i = 0; i < data.Length; i++)
            vec2Data[i] = data[i];
        return vec2Data;
    }
    Vector3[] Vec2ToVec3Arr(Vector2[] data)
    {
        Vector3[] vec2Data = new Vector3[data.Length];
        for (int i = 0; i < data.Length; i++)
            vec2Data[i] = data[i];
        return vec2Data;
    }
    List<IntPoint> Vec2ArrtoPolygon(Vector2[] points, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale + tx * scale, points[i].y * scale + ty * scale));
        return poly;
    }
    List<IntPoint> Vec2ArrtoPolygon(Vector2[] points, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale, points[i].y * scale));
        return poly;
    }
    List<IntPoint> Vec3ArrtoPolygon(Vector3[] points, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale + tx * scale, points[i].y * scale + ty * scale));
        return poly;
    }
    List<IntPoint> Vec3ArrtoPolygon(Vector3[] points, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale, points[i].y * scale));
        return poly;
    }
    Vector3[] PolyToVector3Arr(List<IntPoint> poly, int scale)
    {
        Vector3[] result = new Vector3[poly.Count];
        for (int i = 0; i < poly.Count; i++)
            result[i] = new Vector3(poly[i].X * 1.0f / scale, poly[i].Y * 1.0f / scale, 0);
        return result;
    }
    List<IntPoint> EdgeColliderToPolygon(EdgeCollider2D collider, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < collider.pointCount; i++)
            poly.Add(new IntPoint(collider.points[i].x * scale + tx * scale, collider.points[i].y * scale + ty * scale));
        return poly;
    }
    List<IntPoint> PolygonColliderToPolygon(PolygonCollider2D collider, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < collider.points.Length; i++)
            poly.Add(new IntPoint(collider.points[i].x * scale + tx * scale, collider.points[i].y * scale + ty * scale));
        return poly;
    }
    void PolygonToEdgeCollider(EdgeCollider2D collider, List<IntPoint> polygon, int scale)
    {
        Vector2[] points = new Vector2[polygon.Count];
        for (int i = 0; i < polygon.Count; i++)
        {
            points[i] = new Vector2(polygon[i].X * 1.0f / scale, polygon[i].Y * 1.0f / scale);
        }
        collider.points = points;

    }
    Vector2[] PolygonTovec2Arr(List<IntPoint> polygon, int scale)
    {
        Vector2[] points = new Vector2[polygon.Count];
        for (int i = 0; i < polygon.Count; i++)
        {
            points[i] = new Vector2(polygon[i].X * 1.0f / scale, polygon[i].Y * 1.0f / scale);
        }
        return points;

    }
    void UpdateCollider()
    {

        List<List<Vector2>> pointsForCollider = new List<List<Vector2>>();
        for (int i = 0; i < planetColliders.Length; i++)
        {
            var collisionVertices = new List<Vector2>();
            collisionVertices.Add(Vector2.zero);
            pointsForCollider.Add(collisionVertices);
        }
        float segmentDivider = Mathf.Round((float)points.Length / (float)(planetColliders.Length));
        int segment = 0;
        for (int i = 0; i < points.Length; ++i)
        {
            segment = (int)(i / segmentDivider);
            if (segment == planetColliders.Length) segment--;
            if (segment != 0 && pointsForCollider[segment].Count == 1)
            {
                pointsForCollider[segment - 1].Add(points[i]);
            }
            pointsForCollider[segment].Add(points[i]);
        }
        pointsForCollider[segment].Add(points[0]);
        for (int i = 0; i < planetColliders.Length; ++i)
        {
            planetColliders[i].points = pointsForCollider[i].ToArray();
            //UnityEngine.Debug.Log(planetColliders[i].points.Length);
        }
    }



    private void SmoothOuterContour()
    {
        for (int i = 1; i < points.Length - 1; i++)
        {
            Vector2 leftMidPoint = (points[i - 1] + points[i]) / 2;
            Vector2 rightMidPoint = (points[i] + points[i + 1]) / 2;
            points[i] = (leftMidPoint + rightMidPoint) / 2;
        }
    }

}
