
using UnityEngine;
using UnityEngine.UI;

namespace UChart.HeatMap
{
    [ExecuteInEditMode]
    public class Heatmap2D : Image
    {
        private Material instanceMaterial;
        private static Sprite m_emptySprite;
        private Sprite emptySprite
        {
            get
            {
                if(null == m_emptySprite)
                    m_emptySprite = OnePixelWhiteSprite();
                return m_emptySprite;
            }
        }

        [Header("SETTING")]
        [SerializeField] public Color lineColor = Color.white;
        [SerializeField] [Range(2,200)] public int width = 20;
        [SerializeField] [Range(2,200)] public int height = 20;

        protected override void OnEnable()
        {
            base.OnEnable();
            this.Init();
        }

        private void Init()
        {
            this.sprite = emptySprite;
            if(null == instanceMaterial)
                instanceMaterial = GameObject.Instantiate<Material>(new Material(Shader.Find("UChart/HeatMap/HeatMap2D")));
            this.material = instanceMaterial;
        }

        private void Update()
        {
            this.UpdateMaterial();
        }

        public override Material GetModifiedMaterial(Material baseMaterial)
        {
            Material mat = baseMaterial;
            mat.SetColor("_LineColor",lineColor);
            mat.SetInt("_Width",width);
            mat.SetInt("_Height",height);
            var size = this.GetComponent<RectTransform>().sizeDelta;
            mat.SetInt("_TextureWidth",(int)size.x);
            mat.SetInt("_TextureHeight",(int)size.y);
            return base.GetModifiedMaterial(mat);
        }

        Sprite OnePixelWhiteSprite()
        {
            Texture2D tex = new Texture2D(1,1);
            tex.SetPixel(0,0,Color.white);
            tex.Apply();
            return Sprite.Create(tex,new Rect(0,0,1,1),Vector2.zero);
        }
    }
}