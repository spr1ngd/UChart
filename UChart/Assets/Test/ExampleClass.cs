using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleClass : MonoBehaviour
{
    public RenderTexture RenderTexture;

    void Start()
    {
        RenderTexture.active = RenderTexture;
        //RenderTexture.
        //Camera.main.enabled = false;
        //print(RenderTexture.active);

        //var scene = SceneManager.CreateScene("UCHART");
        //var roots = scene.GetRootGameObjects();
        //print(roots.Length);

        //var uchart = new GameObject("__UCHART__");
        //SceneManager.MoveGameObjectToScene(uchart,scene );
        //uchart.AddComponent<TestScene>();
    }

    void Update()
    {
        RenderTexture.active = RenderTexture;
    }
}

public class TestScene : MonoBehaviour
{
    private void Awake()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SceneManager.MoveGameObjectToScene(cube,SceneManager.GetSceneByName("UCHART"));
    }
}