
using UnityEngine;
using UnityEngine.UI;

namespace UChart.Core
{
    public class Widget2D : MaskableGraphic
    {
        // TODO: 根据自定义的长宽节点绘制指定网格数量的widget面板
        // TODO: 默认大小1x1

        private Vector2 m_origin = Vector2.zero;

        protected Vector2 size
        {
            get { return this.GetComponent<RectTransform>().sizeDelta; }
        }

        protected Vector2 pivot
        {
            get { return this.GetComponent<RectTransform>().pivot; }
        }

        [Range(2,100)]
        public int horizontalCount = 2;
        [Range(2,100)]
        public int verticalCount = 2;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            int quadCount = 0;
            int vertexCount = 0;

            float horizontalCell = size.x / (horizontalCount - 1);
            float verticalCell = size.y / (verticalCount - 1);
            //Debug.Log(string.Format("[Horizontal, Veritical]:[{0},{1}]",horizontalCell,verticalCell));

            float uvXPer = 1.0f / (horizontalCount - 1);
            float uvYPer = 1.0f / (verticalCount - 1);

            Vector2 leftTop = m_origin + new Vector2(-size.x * pivot.x,size.y * (1 - pivot.y));

            for(int y = 0; y < verticalCount - 1; y++)
            {
                for(int x = 0; x < horizontalCount - 1; x++)
                {
                    Vector2 origin = leftTop + new Vector2(x * horizontalCell,-y * verticalCell);
                    vh.AddUIVertexQuad(new UIVertex[]
                    {
                        new UIVertex()
                        {
                            position = origin + new Vector2(0,-verticalCell),
                            color = color,
                            uv0 = new Vector2(x * uvXPer, 1-(y+1) * uvYPer),
                            uv1 = new Vector2(x * uvXPer, 1-(y+1) * uvYPer)
                        },
                        new UIVertex()
                        {
                            position = origin ,
                            color = color,
                            uv0 = new Vector2(x * uvXPer, 1-y * uvYPer),
                            uv1 = new Vector2(x * uvXPer, 1-y * uvYPer) 
                        },
                        new UIVertex()
                        {
                            position = origin + new Vector2(horizontalCell,0) ,
                            color = color,
                            uv0 = new Vector2((x+1) * uvXPer, 1-y * uvYPer),
                            uv1 = new Vector2((x+1) * uvXPer, 1-y * uvYPer)
                        },
                        new UIVertex()
                        {
                            position = origin + new Vector2(horizontalCell,-verticalCell),
                            color = color,
                            uv0 = new Vector2((x+1) * uvXPer, 1-(y+1) * uvYPer),
                            uv1 = new Vector2((x+1) * uvXPer, 1-(y+1) * uvYPer)
                        },
                    });
                    vertexCount += 4;
                    quadCount++;
                }
            }

            //Debug.Log(string.Format("当前绘制[ {0} ]个Quad",quadCount));
        }
    }
}
