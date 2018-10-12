
using UnityEngine;

namespace UChart
{
    public class Point : UChartObject,IPoint
    {
        public override void Init()
        {
            base.Init();

        }

        public virtual void SetColor(Color color)
        {

        }

        public virtual void SetColor(Color32 color32)
        {

        }

        public virtual void SetSize(int size)
        {

        }

        public virtual void SetTexture(Texture2D texture)
        {

        }

        public override void GenerateId()
        {
            uchartId = string.Format("point_{0}",NewGuid());
        }
    }
}