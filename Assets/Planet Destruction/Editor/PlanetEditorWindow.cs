using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ClipperLib;
using System.Collections.Generic;
using System;
using System.IO;
public class PlanetEditorWindow : EditorWindow
{


    float PlanetRadius = 182;
    float shellThickness = 6;
    int numberOfSegments = 20;
    string PlanetName = "Planet_1";
    int resolutionScale = 10000;
    Material PlanetMat;
    Texture planetMask;
    Vector4 skyColor = new Vector4(1, 1, 1, 1);
    Vector4 outerLayerColor = new Vector4(0, 1, 0, 1);
    Texture GroundTexture;
    Texture coreTexture;
    int maskSize = 512;
    Vector2[] PlanetOuterContour;
    Vector2[] PlanetOuterShell;
    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Planet Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        PlanetEditorWindow window = (PlanetEditorWindow)EditorWindow.GetWindow(typeof(PlanetEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        PlanetName = EditorGUILayout.TextField("Planet Name:", PlanetName);
        PlanetRadius = EditorGUILayout.FloatField("Planet Radius", PlanetRadius);
        if (PlanetRadius < 0.1) PlanetRadius = 0.1f;
        shellThickness = EditorGUILayout.FloatField("Shell Thickness", shellThickness);
        if (shellThickness < 0) shellThickness = 0;
        if (shellThickness > PlanetRadius) shellThickness = PlanetRadius;

        numberOfSegments = EditorGUILayout.IntField("NumberOfSegments", numberOfSegments);
        if (numberOfSegments <= 0) numberOfSegments = 1;

        GroundTexture = EditorGUILayout.ObjectField("core Texture:", GroundTexture, typeof(Texture2D)) as Texture;
        coreTexture = EditorGUILayout.ObjectField("inner Texture:", coreTexture, typeof(Texture2D)) as Texture;

        outerLayerColor = EditorGUILayout.ColorField("Outer Layer Color", outerLayerColor);
        skyColor = EditorGUILayout.ColorField("Sky Color", skyColor);
        EditorGUILayout.BeginHorizontal();
        maskSize = EditorGUILayout.IntField("Mask Image Size", maskSize);
        EditorGUILayout.LabelField("x", GUILayout.Width(10));
        EditorGUILayout.LabelField(maskSize.ToString());
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Generate"))
        {
            ///////////////TODO///////////////
            //Generate the UV Image and set its import settings
            //Generate Material using shaderPlanet3 
            //Generate empty object
            //Adding edge\collider to created object
            //Adding Mesh renderer pt created object
            //Generate Mesh 
            //Assign the mesh to the mesh renderer component


            GameObject planetObject = new GameObject(PlanetName);
            PlanetClass planet = planetObject.AddComponent<PlanetClass>();
            //PolygonCollider2D collider = planetObject.AddComponent<PolygonCollider2D>();
            PolygonCollider2D[] colliders = new PolygonCollider2D[numberOfSegments];
            List<List<Vector2>> pointsForCollider = new List<List<Vector2>>();
            for (int i = 0; i < colliders.Length; ++i)
            {
                colliders[i] = planetObject.AddComponent<PolygonCollider2D>();
                var collisionVertices = new List<Vector2>();
                collisionVertices.Add(Vector2.zero);
                pointsForCollider.Add(collisionVertices);
            }
            MeshFilter meshFilter = planetObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = planetObject.AddComponent<MeshRenderer>();
            Vector2[] points = new Vector2[100];
            Mesh m = new Mesh();
            Vector3[] vertices = new Vector3[100];
            Vector2[] uv = new Vector2[100];
            planet.Radius = PlanetRadius;
            planet.shellThickness = shellThickness;
            for (int i = 0; i < points.Length; i++)
            {
                float degree = 360 - (i * 1.0f / (points.Length - 1)) * 360.0f;
                points[i].x = planet.Radius * Mathf.Cos(degree * Mathf.Deg2Rad);
                points[i].y = planet.Radius * Mathf.Sin(degree * Mathf.Deg2Rad);

                vertices[i] = new Vector3(planet.Radius * Mathf.Cos(degree * Mathf.Deg2Rad),
                    planet.Radius * Mathf.Sin(degree * Mathf.Deg2Rad), 0);
            }
            uv = PlanetUtils.UvsFromVertices(vertices, (int)planet.Radius);
            TriangulatorScript tringulator = new TriangulatorScript(PlanetUtils.Vec3ToVec2Arr(vertices));
            int[] triangles = tringulator.Triangulate();
            Array.Reverse(triangles);
            PlanetOuterContour = points;
            PlanetUtils.PrintArray(PlanetOuterContour);
            UpdateShell();
            int segmentDivider = PlanetOuterContour.Length / numberOfSegments;
            for (int i = 0; i < points.Length; ++i)
            {
                int segment = (int)(i/((float)segmentDivider));
                if (segment != 0 && pointsForCollider[segment].Count == 1)
                {
                    pointsForCollider[segment-1].Add(points[i]);
                }
                pointsForCollider[segment].Add(points[i]);
            }
            for (int i = 0; i < numberOfSegments; ++i)
            {

                colliders[i].points = pointsForCollider[i].ToArray();
            }
            planet.points = points;
           // collider.points = points;
            m.vertices = vertices;
            m.uv = uv;
            m.triangles = triangles;
            meshFilter.sharedMesh = m;

            Bitmap uvMap = new Bitmap(maskSize, maskSize);
            UpdateUVMap(uvMap);
            DirectoryInfo dir = new DirectoryInfo("Assets\\" + PlanetName);
            if (!dir.Exists)
            {
                dir.Create();
            }
            string uvMapFileName = "Assets\\" + PlanetName + "\\" + PlanetName + "_uvMap.png";
            uvMap.Save(uvMapFileName, System.Drawing.Imaging.ImageFormat.Png);
            AssetDatabase.ImportAsset(uvMapFileName);

            //Creating material
            var material = new Material(Shader.Find("PlanetShader3"));
            material.SetTexture("_MainTex", GroundTexture);
            material.SetTexture("_CoreTex", coreTexture);
            material.SetColor("_ShellColor", outerLayerColor);
            material.SetColor("_SkyColor", skyColor);
            PointF[] outerContour = PlanetEditor.Vec2ArrToPoint(PlanetOuterContour);
            material.SetFloat("Radius", PlanetEditor.GetScale(outerContour) / 2);
            Texture2D uvMapTex = AssetDatabase.LoadAssetAtPath(uvMapFileName, typeof(Texture2D)) as Texture2D;
            material.SetTexture("_MaskTex", uvMapTex);
            string materiaFileName = "Assets\\" + PlanetName + "\\" + PlanetName + "_mat.mat";
            AssetDatabase.CreateAsset(material, materiaFileName);
            meshRenderer.material = material;
        }

    }
    private void UpdateShell()
    {
        ClipperOffset clipOffset = new ClipperOffset();
        //UnityEngine.Debug.Log("Updating shell outerContCount:" + PlanetOuterContour.Length + " Resolution Scale:" + resolutionScale + " ShellThickness:" + shellThickness);
        PlanetUtils.PrintArray(PlanetOuterContour);
        clipOffset.AddPath(PlanetUtils.Vec2ArrtoPolygon(PlanetOuterContour, resolutionScale), JoinType.jtRound, EndType.etClosedPolygon);
        List<List<IntPoint>> solution = new List<List<IntPoint>>();
        clipOffset.Execute(ref solution, -resolutionScale * shellThickness);
        PlanetOuterShell = PlanetUtils.PolygonTovec2Arr(solution[0], resolutionScale);
    }
    private void UpdateUVMap(Bitmap uvMap)
    {

        System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(uvMap);
        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        gfx.FillRectangle(Brushes.Blue, new Rectangle(0, 0, uvMap.Width, uvMap.Height));
        gfx.TranslateTransform(uvMap.Width / 2, uvMap.Height / 2);
        //gfx.ScaleTransform(uvMap.Width / (planet.Radius*2), -uvMap.Height  / (planet.Radius*2));
        PointF[] outerContour = PlanetEditor.Vec2ArrToPoint(PlanetOuterContour);
        PointF[] innerContour = PlanetEditor.Vec2ArrToPoint(PlanetOuterShell);
        float scale = PlanetEditor.GetScale(outerContour);
        //Debug.Log("Scale" + PlanetEditor.GetScale(outerContour) / 2);
        gfx.ScaleTransform(uvMap.Width / scale, -uvMap.Height / scale);
        gfx.FillPolygon(Brushes.Red, outerContour);
        gfx.FillPolygon(Brushes.Green, innerContour);
        //Texture2D tex = planet.renderer.sharedMaterial.GetTexture("_MaskTex") as Texture2D;
        //tex.LoadImage(ImageToByte2(uvMap));
        //uvMap.Dispose();
        //tex.Apply();
    }
}
class PlanetMaskTextureImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (assetPath.Contains("uvMap"))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.isReadable = true;
            textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
            textureImporter.maxTextureSize = 1024;
        }
    }
}
