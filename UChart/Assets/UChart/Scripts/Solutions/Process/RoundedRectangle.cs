
using UnityEngine;
using UnityEngine.UI;

namespace UChart
{
    [ExecuteInEditMode]
    public class RoundedRectangle : Image
    {
        public float percent = 1.0f;
        public Color startColor = Color.white;
        public Color endColor = new Color32(255,147,0,255);

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

        protected override void OnEnable()
        {
            base.OnEnable();
            this.Init();
        }

        private void Init()
        {
            this.sprite = emptySprite;
            if(null == instanceMaterial)
                instanceMaterial = GameObject.Instantiate<Material>(new Material(Shader.Find("UChart/Process/Process2D")));
            this.material = instanceMaterial;
        }

        private void Update()
        {
            this.UpdateMaterial();
        }

        public override Material GetModifiedMaterial(Material baseMaterial)
        {
            Material mat = baseMaterial;
            mat.SetColor("_StartColor",startColor);
            mat.SetColor("_EndColor",endColor);
            var size = this.GetComponent<RectTransform>().sizeDelta;
            mat.SetFloat("_Width",size.x);
            mat.SetFloat("_Height",size.y);
            return base.GetModifiedMaterial(baseMaterial);
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