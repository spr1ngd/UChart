using UnityEngine;
using UnityEngine.UI;

namespace UChart
{
    public enum Pie2DSytle
    {
        Normal,
        Texture,
        Outline
    }

    public class Pie2D : Pie
    {
        private Image m_pie = null;

        public Image pie 
        {
            get
            {
                if( null == m_pie )
                     m_pie = this.GetComponent<Image>();
                return m_pie;
            }
        }

        public Pie2DSytle pieStyle = Pie2DSytle.Outline;
        public Material pieMaterial = null;

        public override void Draw()
        {
            base.Draw();

            if( null == pieMaterial )
            {
                switch(pieStyle)
                {
                    case Pie2DSytle.Normal:
                        pieMaterial = new Material(Shader.Find("UChart/Pie/2D(Basic)"));
                    break;
                    case Pie2DSytle.Texture:
                        pieMaterial = new Material(Shader.Find("UChart/Pie/2D(Texture)"));
                    break;
                    case Pie2DSytle.Outline:
                        pieMaterial = new Material(Shader.Find("UChart/Pie/2D(Outline)"));
                    break;
                }
            }

            // TODO: 依据饼图数据自动拆分多子级(pie item)进行绘制
            float totalValue = 0;
            for( int i = 0 ;i < pieValues.Length;i++ )
            {
                float value = pieValues[i];
                GameObject pieGO = new GameObject("__PIEITEM__");
                // pieGO.hideFlags = HideFlags.HideInHierarchy;
                pieGO.transform.SetParent(this.myTransform);
                pieGO.transform.localPosition = Vector3.zero;
                pieGO.transform.localScale = Vector3.one;
                var pie = pieGO.AddComponent<Pie2DItem>();
                pie.DrawPieItem(i,totalValue * 360,pieMaterial,pieColors[i]);
                pieGO.GetComponent<RectTransform>().sizeDelta = new Vector2(512,512);
                pie.value = value;
                totalValue += value;
            }
        }
    }

    public class Pie2DItem : PieItem
    {
        public override void DrawPieItem( int index ,float angle ,Material material ,Color color )
        {
            var mat =  GameObject.Instantiate(material) as Material;
            this.myGameobject.AddComponent<Image>().material = mat;
            pieMaterial = this.myGameobject.GetComponent<Image>().material;
            this.color = color;
            transform.localEulerAngles += new Vector3(0,0,angle);
        }

        protected override void SetColor(Color color)
        {
            pieMaterial.SetColor(PieConfig.PIE_COLOR,color);
        }

        protected override void SetValue()
        {
            pieMaterial.SetFloat(PieConfig.PIE_VALUE,value);
        }
    }   
}