
#if UNITY_STANDALONE
#define IMPORT_GLENABLE
#endif
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class ExampleClass : MonoBehaviour
{
    private Mesh mesh;
    int numPoints = 60000;

    // Use this for initialization
    void Start()
    {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
    }

    void CreateMesh()
    {
        Vector3[] points = new Vector3[numPoints];
        int[] indecies = new int[numPoints];
        Color[] colors = new Color[numPoints];
        for(int i = 0; i < points.Length; ++i)
        {
            points[i] = new Vector3(UnityEngine.Random.Range(-10,10),UnityEngine.Random.Range(-10,10),UnityEngine.Random.Range(-10,10));
            indecies[i] = i;
            colors[i] = new Color(UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),1.0f);
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies,MeshTopology.Points,0);

    }
}

public class TestScene : MonoBehaviour
{
    const System.UInt32 GL_VERTEX_PROGRAM_POINT_SIZE = 0x8642;
    const UInt32 GL_POINT_SMOOTH = 0x0B10;

    const string LibGLPath =
#if UNITY_STANDALONE_WIN
        "opengl32.dll";
#elif UNITY_STANDALONE_OSX
    "/System/Library/Frameworks/OpenGL.framework/OpenGL";
#elif UNITY_STANDALONE_LINUX
    "libGL";  // Untested on Linux, this may not be correct
#else
    null;   // OpenGL ES platforms don't require this feature
#endif

#if IMPORT_GLENABLE
    [DllImport(LibGLPath)]
    public static extern void glEnable(UInt32 cap);
     
    private bool mIsOpenGL;
     
    void Start()
    {
        mIsOpenGL = SystemInfo.graphicsDeviceVersion.Contains("OpenGL");
    }
     
    void OnPreRender()
    {
        if (mIsOpenGL)
            glEnable(GL_VERTEX_PROGRAM_POINT_SIZE);
            glEnable(GL_POINT_SMOOTH);
    }
#endif
}