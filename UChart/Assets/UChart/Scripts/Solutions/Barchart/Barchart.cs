
using UnityEngine;

namespace UChart
{
    public class Barchart : UChartObject
    {
        public float[,] datas = null;
        public Color[,] colors = null;

        protected int xCount = 10;
		protected int yCount = 10;

        public virtual void DataCheck()
        {
            if( null == datas )
            {
                Debug.LogWarning("two-dimensional array data can null be null.");
                return;
            }
            xCount = datas.GetLength(0);
            if( xCount == 0 )
            {
                Debug.LogWarning("two-dimensional array data row is <color=red>zero</color>.");
                return;
            }
            yCount = datas.GetLength(1);
            if( yCount == 0 )
            {
                Debug.LogWarning("two-dimensional array data column is <color=red>zero</color>.");
                return;
            }
            Debug.Log(string.Format("two-dimensional array <color=yellow>[{0},{1}]</color>",xCount,yCount));
        }

        public virtual void Draw()
        {
            this.DataCheck();
        }
    }
}