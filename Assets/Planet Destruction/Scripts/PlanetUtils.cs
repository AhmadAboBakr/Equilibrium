using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ClipperLib;
using System;
public class PlanetUtils {

    public static Vector3[] TranslateArray(Vector3[] data, float dx, float dy, float dz)
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
    public static void TranslateArray(Vector2[] data, float dx, float dy)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i].x += dx;
            data[i].y += dy;
        }
    }
    public static Vector2[] Vec3ToVec2Arr(Vector3[] data)
    {
        Vector2[] vec2Data = new Vector2[data.Length];
        for (int i = 0; i < data.Length; i++)
            vec2Data[i] = data[i];
        return vec2Data;
    }
    public static Vector3[] Vec2ToVec3Arr(Vector2[] data)
    {
        Vector3[] vec2Data = new Vector3[data.Length];
        for (int i = 0; i < data.Length; i++)
            vec2Data[i] = data[i];
        return vec2Data;
    }
    
    public static Vector2[] UvsFromVertices(Vector3[] arr, int radius)
    {
        Vector2[] result = new Vector2[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            result[i] = (arr[i] / radius) / 2 + new Vector3(0.5f, 0.5f);
            float angle = Mathf.Atan2(arr[i].y, arr[i].x);
            float u = arr[i].x * Mathf.Sin(angle) + arr[i].y * Mathf.Cos(angle);
            float v = arr[i].x * Mathf.Cos(angle) + arr[i].y * Mathf.Sin(angle);
            //result[i] = new Vector3(arr[i].y, arr[i].x, 0) + new Vector3(0.5f, 0.5f);
            v = Mathf.Sqrt(arr[i].x * arr[i].x + arr[i].y * arr[i].y);
            u = NormalizeAngle(angle) / 360;
            //result[i] = new Vector3(u,v, 0) + new Vector3(0.5f, 0.5f);
            //result[i] = new Vector3(arr[i].y/radius , arr[i].x/radius , 0) + new Vector3(0.5f, 0.5f);
            //result[i] =new Vector2( Mathf.Abs((arr[i]).x),Mathf.Abs(arr[i].y))/radius;// +new Vector3(0.5f, 0.5f);
            result[i] = new Vector3(arr[i].x * 6 / radius, arr[i].y * 6 / radius, 0) + new Vector3(0.5f, 0.5f);
        }
        return result;
    }
    public static float NormalizeAngle(float angleRadian)
    {
        float result = angleRadian * Mathf.Rad2Deg;
        if (result < 0)
            result += 360;
        if (result > 360)
            result -= 360;
        return result;
    }
    public static List<IntPoint> Vec2ArrtoPolygon(Vector2[] points, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale + tx * scale, points[i].y * scale + ty * scale));
        return poly;
    }
    public static List<IntPoint> Vec2ArrtoPolygon(Vector2[] points, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale, points[i].y * scale));
        return poly;
    }
    public static List<IntPoint> Vec3ArrtoPolygon(Vector3[] points, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale + tx * scale, points[i].y * scale + ty * scale));
        return poly;
    }
    public static List<IntPoint> Vec3ArrtoPolygon(Vector3[] points, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < points.Length; i++)
            poly.Add(new IntPoint(points[i].x * scale, points[i].y * scale));
        return poly;
    }
    public static Vector3[] PolyToVector3Arr(List<IntPoint> poly, int scale)
    {
        Vector3[] result = new Vector3[poly.Count];
        for (int i = 0; i < poly.Count; i++)
            result[i] = new Vector3(poly[i].X * 1.0f / scale, poly[i].Y * 1.0f / scale, 0);
        return result;
    }
    public static List<IntPoint> EdgeColliderToPolygon(EdgeCollider2D collider, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < collider.pointCount; i++)
            poly.Add(new IntPoint(collider.points[i].x * scale + tx * scale, collider.points[i].y * scale + ty * scale));
        return poly;
    }
    public static List<IntPoint> PolygonColliderToPolygon(PolygonCollider2D collider, float tx, float ty, int scale)
    {
        List<IntPoint> poly = new List<IntPoint>();
        for (int i = 0; i < collider.points.Length; i++)
            poly.Add(new IntPoint(collider.points[i].x * scale + tx * scale, collider.points[i].y * scale + ty * scale));
        return poly;
    }
    public static void PolygonToEdgeCollider(EdgeCollider2D collider, List<IntPoint> polygon, int scale)
    {
        Vector2[] points = new Vector2[polygon.Count];
        for (int i = 0; i < polygon.Count; i++)
        {
            points[i] = new Vector2(polygon[i].X * 1.0f / scale, polygon[i].Y * 1.0f / scale);
        }
        collider.points = points;

    }
    public static Vector2[] PolygonTovec2Arr(List<IntPoint> polygon, int scale)
    {
        Vector2[] points = new Vector2[polygon.Count];
        for (int i = 0; i < polygon.Count; i++)
        {
            points[i] = new Vector2(polygon[i].X * 1.0f / scale, polygon[i].Y * 1.0f / scale);
        }
        return points;

    }
    public static void PrintArray(Vector2[] arr)
    {
        string str = "";
        for (int i = 0; i < arr.Length; i++)
        {
            str += arr[i] + "\n";
        }
    }
    public static void PrintArray(Vector3[] arr)
    {
        string str = "";
        for (int i = 0; i < arr.Length; i++)
        {
            str += arr[i] + "\n";
        }
        UnityEngine.Debug.Log(str);
    }
}
