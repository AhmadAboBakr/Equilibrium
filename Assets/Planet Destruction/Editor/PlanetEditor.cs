using UnityEngine;
using System.Collections;
using UnityEditor;
using ClipperLib;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
[CustomEditor(typeof(PlanetClass))]
public class PlanetEditor : Editor {
    public static bool  EditorEnabled=true;
    float timer = 0;
    static Vector3[] ClippingPoly;// = new Vector3[] { new Vector3(-0.25f, -0.25f, 0), new Vector3(0.25f, -0.25f, 0), new Vector3(0, 0.25f, 0), new Vector3(-0.25f, -0.25f, 0) };
    static Vector3[] EditorClippingPoly;
    int Scale = 100;
    //static float OuterLayerThickness = 0.25f;
    Vector2[] PlanetOuterContour;
    Vector2[] PlanetOuterShell;
    //Bitmap uvMap;
    //[MenuItem(string.Format("{0} Planet Editor", (EditorEnabled ? "Disable" : "Enable")))]
    [MenuItem("Planet Editor/Enable\\Disable Planet Editor")]
    public static void TogglePlanetEditor()
    {
        EditorEnabled = !EditorEnabled;

    }
    PlanetClass planet;
    //PolygonCollider2D planetCollider;
    PolygonCollider2D[] planetColliders;
    void OnEnable()
    {
        if (EditorEnabled)
            Tools.current = Tool.None;
        planet = target as PlanetClass;
        planetColliders = planet.gameObject.GetComponents<PolygonCollider2D>();
        PlanetOuterContour = planet.points;
        //vertCount = 5;
        UpdateClippingPoly();
        timer = Time.realtimeSinceStartup;
        //uvMap = new Bitmap("Assets\\uvMap.png");
        
    }
    bool mouseDown = false;
    static float UserDefinedSize = 0.25f;
    static int vertCount = 5;
    Vector2 lastMousPos;
    public void OnSceneGUI()
    {
        
        PlanetClass planet = target as PlanetClass;
        if (EditorEnabled)
        {
            bool NeadRedraw = false;
            if (Event.current.type == EventType.MouseDown)
            {
                //mouseDown = true;
                Selection.activeObject = target;
                int controlID = GUIUtility.GetControlID(FocusType.Passive);
                GUIUtility.hotControl = controlID;
                Event.current.Use();
                if (Event.current.button == 0)
                    Add();
                else if (Event.current.button == 1)
                    Subtract();
                NeadRedraw = true;
                //Debug.Log("Mouse down");
            }
            if (Event.current.type == EventType.MouseMove)
            {
                NeadRedraw = true;
                //Debug.Log("Need redraw");

            }
            if (Event.current.type == EventType.MouseDrag&& Event.current.button==0)
            {
                if (Event.current.mousePosition != lastMousPos)
                {
                    
                    if (Time.realtimeSinceStartup - timer > 0.3f)
                    {
                        Add();
                        lastMousPos = Event.current.mousePosition;
                        UpdateUVMap();
                        timer = Time.realtimeSinceStartup;
                    }
                    
                    
                }
                NeadRedraw = true;
                //Debug.Log("Need redraw");

            }
            if (Event.current.type == EventType.MouseUp && ColliderNeedToUpdate)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //for (int i = 0; i < 1; i++)
                //    SmoothOuterContour();
                UpdatePlanetMesh(PlanetOuterContour);
                UpdateShell((target as PlanetClass).shellThickness);

                UpdateCollider();
                //planetCollider.points = PlanetOuterContour;
                sw.Stop();
                //UnityEngine.Debug.Log("Update collider in " + sw.ElapsedMilliseconds + " ms");
                NeadRedraw = true;
                UpdateUVMap();

                saveTextureChangesToDisk(planet.GetComponent<Renderer>().sharedMaterial.GetTexture("_MaskTex") as Texture2D);
            }
            if (Event.current.type == EventType.MouseDrag && Event.current.button == 1)
            {
                if (Event.current.mousePosition != lastMousPos)
                {
                    Subtract();
                    lastMousPos = Event.current.mousePosition;
                    UpdateUVMap();
                }

                NeadRedraw = true;

            }
            if (Event.current.control && Event.current.type == EventType.ScrollWheel)
            {
                UserDefinedSize -= Event.current.delta.y * 0.01f;
                UpdateClippingPoly();
                Event.current.Use();
                NeadRedraw = true;
                //Debug.Log("increase size");
            }
            if (Event.current.shift && Event.current.type == EventType.ScrollWheel)
            {
                vertCount -= (int)(Event.current.delta.y / 3);
                if (vertCount < 3) vertCount = 3;
                UpdateClippingPoly();
                Event.current.Use();
                NeadRedraw = true;
                //Debug.Log("increase size");
            }
            Vector3 mousePos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin + SceneView.GetAllSceneCameras()[0].transform.forward;
            //mousePos = SceneView.GetAllSceneCameras()[0].ScreenToWorldPoint(new Vector3(Event.current.mousePosition.x, Event.current.mousePosition.y,5) );
            float handleSize = HandleUtility.GetHandleSize(mousePos);
            Handles.color = new UnityEngine.Color(1, 1, 1, 1);
            Handles.DrawPolyLine(PlanetUtils.TranslateArray(EditorClippingPoly, mousePos.x, mousePos.y, planet.transform.position.z));
            if (PlanetOuterShell != null) Handles.DrawPolyLine(PlanetUtils.Vec2ToVec3Arr(PlanetOuterShell));
            Handles.color = new UnityEngine.Color(1, 0, 0, 0.3f);
            //Handles.DrawSolidDisc(mousePos, Vector3.forward, handleSize * UserDefinedSize);
            Handles.DrawSolidDisc(mousePos, Vector3.forward,  UserDefinedSize);
            if (NeadRedraw)
                SceneView.RepaintAll();
        }

    }

    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        #region Generate
        /*
        if (GUILayout.Button("Generate"))
        {
            Planet planet = target as Planet;
            EdgeCollider2D collider = planet.GetComponent<EdgeCollider2D>();
            if (!collider)
                collider = planet.gameObject.AddComponent<EdgeCollider2D>();
            MeshFilter meshFilter = planet.gameObject.GetComponent<MeshFilter>();
            if (!meshFilter)
                meshFilter = planet.gameObject.AddComponent<MeshFilter>();
            Vector2[] points = new Vector2[100];
            Mesh m = new Mesh();

            Vector3[] vertices = new Vector3[100];
            Vector2[] uv = new Vector2[100];
            for (int i = 0; i < points.Length; i++)
            {
                float degree = 360 - (i * 1.0f / (points.Length - 1)) * 360.0f;
                points[i].x = planet.Radius * Mathf.Cos(degree * Mathf.Deg2Rad);
                points[i].y = planet.Radius * Mathf.Sin(degree * Mathf.Deg2Rad);

                    vertices[i] = new Vector3(planet.Radius * Mathf.Cos(degree * Mathf.Deg2Rad),
                        planet.Radius * Mathf.Sin(degree * Mathf.Deg2Rad), 0);

               
            }
            uv = PlanetUtils.UvsFromVertices(vertices, (int)planet.Radius);
            Triangulator tringulator = new Triangulator(PlanetUtils.Vec3ToVec2Arr(vertices));
            int[] triangles = tringulator.Triangulate();
            Array.Reverse(triangles);
            PlanetOuterContour = points;
            collider.points = points;
            m.vertices = vertices;
            m.uv = uv;
            m.triangles = triangles;
            meshFilter.sharedMesh = m;
            UpdateShell();
            //uvMap = new Bitmap(512, 512);
            UpdateUVMap();
            //uvMap.Save("Assets\\uvMap.png", System.Drawing.Imaging.ImageFormat.Png);
            //AssetDatabase.ImportAsset("Assets\\uvMap.png");
        }*/
        #endregion
         
        #region Smooth Map
        if (GUILayout.Button("Smooth UVMap"))
        {

            for (int i = 0; i < 1; i++)
                SmoothOuterContour();
            float kernelvalue = (float)(1.0 / 9);
            float[,] kernel ={{kernelvalue,kernelvalue,kernelvalue},
                            {kernelvalue,kernelvalue,kernelvalue},
                            {kernelvalue,kernelvalue,kernelvalue}};
            Texture2D tex = planet.GetComponent<Renderer>().sharedMaterial.GetTexture("_MaskTex") as Texture2D;
            ApplayKernelWithColors(tex, new Point(1, 1), tex.width - 1, tex.height - 1, kernel);
            saveTextureChangesToDisk(tex);
            //ApplayKernelWithColors(uvMap, new Point(1, 1), uvMap.Width - 1, uvMap.Height - 1, kernel);
            //uvMap.Save("Assets\\uvMap.png", System.Drawing.Imaging.ImageFormat.Png);
            //AssetDatabase.ImportAsset("Assets\\uvMap.png");
        }
        #endregion
        //(target as Planet).renderer.sortingLayerName = GUILayout.TextField((target as Planet).renderer.sortingLayerName);

        //(target as Planet).renderer.sortingOrder = int.Parse(GUILayout.TextField((target as Planet).renderer.sortingOrder.ToString()));

    }
    private void UpdateUVMap()
    {
        Texture2D uvMapTex = planet.GetComponent<Renderer>().sharedMaterial.GetTexture("_MaskTex") as Texture2D;
        //byte[] uvMapData = uvMapTex.EncodeToPNG();

        //Bitmap uvMap = new Bitmap(512, 512);

        //using (MemoryStream ms = new MemoryStream(uvMapData))
        //{
        //    uvMap = new Bitmap(ms, false);
        //}
        Bitmap uvMap = new Bitmap(uvMapTex.width, uvMapTex.height);
        System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(uvMap);
        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        gfx.FillRectangle(Brushes.Blue, new Rectangle(0, 0, uvMap.Width, uvMap.Height));
        gfx.TranslateTransform(uvMap.Width / 2, uvMap.Height / 2);
        //gfx.ScaleTransform(uvMap.Width / (planet.Radius*2), -uvMap.Height  / (planet.Radius*2));
        PointF[] outerContour = Vec2ArrToPoint(PlanetOuterContour);
        PointF[] innerContour = Vec2ArrToPoint(PlanetOuterShell);
        float scale = GetScale(outerContour);
        //UnityEngine.Debug.Log("Scale:"+scale);
        gfx.ScaleTransform(uvMap.Width/ scale, - uvMap.Height/scale);
        planet.GetComponent<Renderer>().sharedMaterial.SetFloat("Radius", scale/2);
        //PathGradientBrush brush = new PathGradientBrush(outerContour);
        //brush.CenterColor = System.Drawing.Color.Wheat;
        //brush.SurroundColors = new System.Drawing.Color[] { System.Drawing.Color.Red, System.Drawing.Color.Magenta, System.Drawing.Color.Blue};
        //Blend b = new Blend();
        //b.Factors = new float[] { 0, 0.1f, 1 };
        //b.Positions = new float[] { 0, 0.5f, 1 };
        //brush.Blend = b;
        //brush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
        //gfx.FillPolygon(brush, outerContour);
        gfx.FillPolygon(Brushes.Red, outerContour);
        gfx.FillPolygon(Brushes.Green, innerContour);
        Texture2D tex = planet.GetComponent<Renderer>().sharedMaterial.GetTexture("_MaskTex") as Texture2D;
        tex.LoadImage(ImageToByte2(uvMap));
        uvMap.Dispose();
        //tex.Apply();
    }
    void saveTextureChangesToDisk(Texture2D texture)
    {

        string imagePath = AssetDatabase.GetAssetPath(texture.GetInstanceID());
        //UnityEngine.Debug.Log("imagePath:" + imagePath);
        using (MemoryStream ms = new MemoryStream(texture.EncodeToPNG()))
        {
            Bitmap bmp = new Bitmap(ms);
            bmp.Save(imagePath);
            bmp.Dispose();
        }
        AssetDatabase.ImportAsset(imagePath);
    }
    public static float GetScale(PointF[] outerContour)
    {
        PointF min = outerContour[0], max=outerContour[0];
        for(int i=0;i<outerContour.Length;i++){
            if (outerContour[i].X > max.X) max.X = outerContour[i].X;
            if (outerContour[i].Y > max.Y) max.Y = outerContour[i].Y;
            if (outerContour[i].X < min.X) min.X = outerContour[i].X;
            if (outerContour[i].Y < min.Y) min.Y = outerContour[i].Y;
        }
        //UnityEngine.Debug.Log("Bounds:"+min.ToString()+" -- "+max.ToString());
        //float boundsWidth = max.X - min.X;
        //float boundstHeight = max.Y - min.Y;
        //if (boundsWidth > boundstHeight)
        //{
        //    return  boundsWidth;
        //}
        //else
        //    return  boundstHeight;
        return 2* Mathf.Max(Mathf.Abs(min.X), Mathf.Abs(min.Y), Mathf.Abs(max.X), Mathf.Abs(max.Y))*1.01f;
    }
    public static byte[] ImageToByte2(Image img)
    {
        byte[] byteArray = new byte[0];
        using (MemoryStream stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Close();

            byteArray = stream.ToArray();
        }
        //Array.Reverse(byteArray);
        return byteArray;
    }
    private void UpdateClippingPoly()
    {
        //UnityEngine.Debug.Log("Updating clipping area with " + vertCount + " verts");
        ClippingPoly = new Vector3[vertCount];
        EditorClippingPoly = new Vector3[vertCount+1];
        for (int i = 0; i < vertCount; i++)
        {
            float degree =360- (i*1.0f /(vertCount))* 360.0f;
            ClippingPoly[i] = new Vector3(
                UserDefinedSize * Mathf.Cos(degree * Mathf.Deg2Rad),
                UserDefinedSize * Mathf.Sin(degree * Mathf.Deg2Rad),
                0
                );
            EditorClippingPoly[i] = new Vector3(
                UserDefinedSize * Mathf.Cos(degree * Mathf.Deg2Rad),
                UserDefinedSize * Mathf.Sin(degree * Mathf.Deg2Rad),
                0
                );
        }
        EditorClippingPoly[vertCount] = EditorClippingPoly[0];
    }
    bool ColliderNeedToUpdate = false;
    private void Add()
    {
        //UnityEngine.Debug.Log("Add");
        Stopwatch sw = new Stopwatch();
        sw.Start();
        GameObject planet = (target as PlanetClass).gameObject;
        Vector2[] points = planet.GetComponent<PolygonCollider2D>().points;
        //clipping point should be translated to mouse position relative to planet space
        //assume the planet in (0,0,0)
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin + SceneView.GetAllSceneCameras()[0].transform.forward;
        Vector3[] clippingPoints = PlanetUtils.TranslateArray(ClippingPoly, mousePos.x-planet.transform.position.x, mousePos.y-planet.transform.position.y, 0);
        List<IntPoint> orignalPoly = PlanetUtils.Vec2ArrtoPolygon(PlanetOuterContour, Scale);
        List<IntPoint> clipPoly = PlanetUtils.Vec3ArrtoPolygon(clippingPoints, Scale);
        Clipper c = new Clipper();
        
        c.AddPath(orignalPoly, PolyType.ptSubject, true);
        c.AddPath(clipPoly, PolyType.ptClip, true);
        List<List<IntPoint>> result = new List<List<IntPoint>>();
        c.Execute(ClipType.ctUnion, result);
        long clippingTime = sw.ElapsedMilliseconds;
        UpdatePlanetMesh( result);
        long timeafterTriangluation = sw.ElapsedMilliseconds;
        PlanetOuterContour = PlanetUtils.PolygonTovec2Arr(result[0], Scale);
        ColliderNeedToUpdate = true;
        UpdateShell((target as PlanetClass).shellThickness);
    }

    private void UpdatePlanetMesh(List<List<IntPoint>> result)
    {
        Mesh planetMesh = planet.GetComponent<MeshFilter>().sharedMesh;
        planetMesh.triangles = null;
        UnityEngine.Debug.Log(result.Count);

        if (result.Count >1)
        {
            
            if (result[0].Count > result[1].Count)
            {
                planetMesh.vertices = PlanetUtils.PolyToVector3Arr(result[0], Scale);
            
            }
            else
            {
                planetMesh.vertices = PlanetUtils.PolyToVector3Arr(result[1], Scale);
            }
        }
        else {
            planetMesh.vertices = PlanetUtils.PolyToVector3Arr(result[0], Scale);
        }
        planetMesh.uv = PlanetUtils.UvsFromVertices(planetMesh.vertices, 10);
        Triangulator3 tri = new Triangulator3(planetMesh.vertices);
        planetMesh.triangles = tri.Triangulate();
        planetMesh.Optimize();
    }
    private void UpdatePlanetMesh(Vector2[] vertices)
    {
        Mesh planetMesh = planet.GetComponent<MeshFilter>().sharedMesh;
        planetMesh.triangles = null;
        planetMesh.vertices = PlanetUtils.Vec2ToVec3Arr(vertices);
        planetMesh.uv = PlanetUtils.UvsFromVertices(planetMesh.vertices, 10);
        Triangulator3 tri = new Triangulator3(planetMesh.vertices);
        planetMesh.triangles = tri.Triangulate();
    }
    private void UpdateShell(float thickness)
    {
        ClipperOffset clipOffset = new ClipperOffset();
        clipOffset.AddPath(PlanetUtils.Vec2ArrtoPolygon(PlanetOuterContour, Scale), JoinType.jtRound, EndType.etClosedPolygon);
        List<List<IntPoint>> solution = new List<List<IntPoint>>();
        //UnityEngine.Debug.Log("Thickness:" + OuterLayerThickness);
        clipOffset.Execute(ref solution, -Scale * thickness);
        PlanetOuterShell = PlanetUtils.PolygonTovec2Arr(solution[0], Scale);
    }
    private void SmoothOuterContour()
    {
        for (int i = 1; i < PlanetOuterContour.Length - 1; i++)
        {
            Vector2 leftMidPoint = (PlanetOuterContour[i - 1] + PlanetOuterContour[i]) / 2;
            Vector2 rightMidPoint = (PlanetOuterContour[i] + PlanetOuterContour[i+1]) / 2;
            PlanetOuterContour[i] = (leftMidPoint + rightMidPoint) / 2;
        }
    }
    private void Subtract()
    {
        //UnityEngine.Debug.Log("Sub");
        GameObject planet = (target as PlanetClass).gameObject;
        Vector2[] points = planet.GetComponent<PolygonCollider2D>().points;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin + SceneView.GetAllSceneCameras()[0].transform.forward;
        //clipping point should be translated to mouse position relative to planet space
        //assume the planet in (0,0,0)
        Vector3[] clippingPoints = PlanetUtils.TranslateArray(ClippingPoly, mousePos.x, mousePos.y, 0);
        List<IntPoint> orignalPoly = PlanetUtils.Vec2ArrtoPolygon(PlanetOuterContour, Scale);
        List<IntPoint> clipPoly = PlanetUtils.Vec3ArrtoPolygon(clippingPoints, Scale);
        Clipper c = new Clipper();
        c.AddPath(orignalPoly, PolyType.ptSubject, true);
        c.AddPath(clipPoly, PolyType.ptClip, true);
        List<List<IntPoint>> result = new List<List<IntPoint>>();
        c.Execute(ClipType.ctDifference, result);
        Mesh planetMesh = planet.GetComponent<MeshFilter>().sharedMesh;
        planetMesh.triangles = null;
        planetMesh.vertices = PlanetUtils.PolyToVector3Arr(result[0], Scale);
        planetMesh.uv = PlanetUtils.UvsFromVertices(planetMesh.vertices, 10);
        Triangulator3 tri = new Triangulator3(planetMesh.vertices);
        planetMesh.triangles = tri.Triangulate();
        PlanetOuterContour = PlanetUtils.PolygonTovec2Arr(result[0], Scale);
        UpdateShell((target as PlanetClass).shellThickness);
        ColliderNeedToUpdate = true;
        //PolygonToEdgeCollider(planet.GetComponent<EdgeCollider2D>(), result[0], Scale);
    }

    public void ApplayKernelWithColors(Bitmap image, Point upperLeftCorner, int width, int height, float[,] kernel)
    {
        if (upperLeftCorner.X + width > image.Width || upperLeftCorner.Y + height > image.Height)
            throw new Exception("the width or height out of the image range");
        if (kernel.GetUpperBound(0) != kernel.GetUpperBound(1) || kernel.GetUpperBound(0) / 2 != 1)
            throw new Exception("Invalid kernel");
        int kernelSize = kernel.GetUpperBound(0) + 1;
        int NumberOfPixels = kernelSize / 2;//for kernelsize(3)=1,for Kernelsize(5)=2,....etc
        int colIndex = 0;
        for (int x = upperLeftCorner.X; x < width; x++)
        {
            colIndex++;
            for (int y = upperLeftCorner.Y; y < height; y++)
            {
                int newPixelRValue = 0;
                int newPixelGValue = 0;
                int newPixelBValue = 0;
                for (int i = 0; i < kernelSize; i++)
                {
                    for (int j = 0; j < kernelSize; j++)
                    {
                        newPixelRValue += (int)Math.Round(image.GetPixel(x + i - NumberOfPixels, y + j - NumberOfPixels).R * kernel[j, i]);
                        newPixelGValue += (int)Math.Round(image.GetPixel(x + i - NumberOfPixels, y + j - NumberOfPixels).G * kernel[j, i]);
                        newPixelBValue += (int)Math.Round(image.GetPixel(x + i - NumberOfPixels, y + j - NumberOfPixels).B * kernel[j, i]);
                    }
                }
                NormalizeColorComponent(ref newPixelRValue);
                NormalizeColorComponent(ref newPixelGValue);
                NormalizeColorComponent(ref newPixelBValue);
                image.SetPixel(x, y, System.Drawing.Color.FromArgb(newPixelRValue, newPixelGValue, newPixelBValue));
            }
            
            EditorUtility.DisplayProgressBar("Applying mean filter on UVMap image", "Please Wait...", colIndex * 1.0f / width);
        }
        EditorUtility.ClearProgressBar();
    }
    public void ApplayKernelWithColors(Texture2D image, Point upperLeftCorner, int width, int height, float[,] kernel)
    {
        int counter = 0;
        if (upperLeftCorner.X + width > image.width || upperLeftCorner.Y + height > image.height)
            throw new Exception("the width or height out of the image range");
        if (kernel.GetUpperBound(0) != kernel.GetUpperBound(1) || kernel.GetUpperBound(0) / 2 != 1)
            throw new Exception("Invalid kernel");
        int kernelSize = kernel.GetUpperBound(0) + 1;
        int NumberOfPixels = kernelSize / 2;//for kernelsize(3)=1,for Kernelsize(5)=2,....etc
        int colIndex = 0;
        for (int x = upperLeftCorner.X; x < width; x++)
        {
            colIndex++;
            for (int y = upperLeftCorner.Y; y < height; y++)
            {
                float newPixelRValue = 0;
                float newPixelGValue = 0;
                float newPixelBValue = 0;
                 counter++;
                for (int i = 0; i < kernelSize; i++)
                {
                    for (int j = 0; j < kernelSize; j++)
                    {
                        newPixelRValue += image.GetPixel(x + i - NumberOfPixels, y + j - NumberOfPixels).r * kernel[j, i];
                        newPixelGValue += image.GetPixel(x + i - NumberOfPixels, y + j - NumberOfPixels).g * kernel[j, i];
                        newPixelBValue += image.GetPixel(x + i - NumberOfPixels, y + j - NumberOfPixels).b * kernel[j, i];
                    }
                }

                NormalizeColorComponent(ref newPixelRValue);
                NormalizeColorComponent(ref newPixelGValue);
                NormalizeColorComponent(ref newPixelBValue);
                
                image.SetPixel(x, y,new UnityEngine.Color(newPixelRValue, newPixelGValue, newPixelBValue));
            }
            float progress = colIndex * 1.0f / width;
            EditorUtility.DisplayProgressBar("Applying mean filter on UVMap image ", "Please Wait..." +Mathf.Round(progress*100)+"%", progress);
        }
        image.Apply();
        EditorUtility.ClearProgressBar();
    }
    void NormalizeColorComponent(ref int newPixelValue)
    {
        if (newPixelValue < 0) newPixelValue = 0;
        if (newPixelValue > 255) newPixelValue = 255;
    }
    void NormalizeColorComponent(ref float newPixelValue)
    {
        if (newPixelValue < 0) newPixelValue = 0;
        if (newPixelValue > 1) newPixelValue = 1;
    }
    //utility functions
    public static PointF[] Vec2ArrToPoint(Vector2[] data)
    {
        PointF[] result = new PointF[data.Length];
        for (int i = 0; i < data.Length; i++)
            result[i] = new PointF(data[i].x, data[i].y);
        return result;
    }
    void UpdateCollider()
    {
        
        var points = planet.GetComponent<MeshFilter>().sharedMesh.vertices;
        List<List<Vector2>> pointsForCollider = new List<List<Vector2>>();
        for (int i = 0; i < planetColliders.Length; i++)
        {
            var collisionVertices = new List<Vector2>();
            collisionVertices.Add(Vector2.zero);
            pointsForCollider.Add(collisionVertices);
        }
        int segment = 0;
        int segmentDivider = PlanetOuterContour.Length / planetColliders.Length;
        for (int i = 0; i < PlanetOuterContour.Length; ++i)
        {
            segment = (int)(i / ((float)segmentDivider));
            if (segment == planetColliders.Length) segment--;
            if (segment != 0 && pointsForCollider[segment].Count == 1)
            {
                pointsForCollider[segment - 1].Add(PlanetOuterContour[i]);
            }
            pointsForCollider[segment].Add(PlanetOuterContour[i]);
        }
        pointsForCollider[segment].Add(points[0]);

        for (int i = 0; i < planetColliders.Length; ++i)
        {
            planetColliders[i].points = pointsForCollider[i].ToArray();
            //UnityEngine.Debug.Log(planetColliders[i].points.Length);
        }
        planet.points = PlanetOuterContour;
    }
}
