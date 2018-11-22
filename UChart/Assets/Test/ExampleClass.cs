using System.Collections;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    public Mesh mainMesh;
    public Mesh miniMapMesh;

    void OnRenderObject()
    {
        // Render different meshes for the object depending on whether
        // the main camera or minimap camera is viewing.
        if(Camera.current.name == "MiniMapcam")
        {
            Graphics.DrawMeshNow(miniMapMesh,transform.position,transform.rotation);
        }
        else
        {
            Graphics.DrawMeshNow(mainMesh,transform.position,transform.rotation);
        }
    }
}