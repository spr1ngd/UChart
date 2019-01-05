
using UnityEngine;

namespace UChart
{
    public class Pie : UChartObject
    {
        public const string PIE_COUNT = "_PieCount";
        public const string PIE_VALUES = "_PieValues";
        public const string PIE_COLORS = "_PieColors";
        // 默认饼图上限数量10个

        public float[] pieValues;
        public Color[] pieColors;

        public virtual void DataCheck()
        {
            if( null == pieValues || null == pieColors || pieValues.Length != pieColors.Length )
                throw new UChartException("count of VALUES not equal to count of COLORS.");
        }

        public virtual void Draw()
        {
            DataCheck();
        }
    }
}