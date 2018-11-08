
using UnityEngine;
using System.Collections;

namespace UChart.Scatter
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Scatter3DModel : MonoBehaviour
    {
        private Mesh mesh;
        int numPoints = 60000;
    
        private void Start () 
        {
            mesh = new Mesh();    
            GetComponent<MeshFilter>().mesh = mesh;
            CreateMesh();
        }
    
        private void CreateMesh() 
        {
            Vector3[] points = new Vector3[numPoints];
            int[] indecies = new int[numPoints];
            Color[] colors = new Color[numPoints];
            for(int i = 0; i < points.Length; ++i)
            {
                points[i] = new Vector3(Random.Range(-10,10),Random.Range(-10,10),Random.Range(-10,10));
                indecies[i] = i;
                //colors[i] = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),1.0f);
            } 
            mesh.vertices = points;
            //mesh.colors = colors;
            mesh.SetIndices(indecies, MeshTopology.Points,0);    
        }

        private void Update()
        {
            
        }
    }
}