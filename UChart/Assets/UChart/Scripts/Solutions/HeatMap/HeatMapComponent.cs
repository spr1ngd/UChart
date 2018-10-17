
using UnityEngine;
using System.Collections.Generic;

namespace UChart.HeatMap
{
    public enum HeatMapMode
    {
        RefreshEachFrame,
        RefreshByInterval
    }

    public class HeatMapComponent : MonoBehaviour
    {
        // TODO: set point properties for shader properties.
        // TODO: get mesh.material

        private Material m_material = null;

        public Material material 
        {
            get
            {
                if( null == m_material )
                {
                    var renderer = this.GetComponent<Renderer>();
                    if( null == renderer )
                        throw new UChartNotFoundException("Can not found Renderer component on HeatMapComponent");
                    m_material = renderer.material;
                }
                return m_material;
            }
        }

        public HeatMapMode heatMapMode = HeatMapMode.RefreshEachFrame;

        // 热力图刷新间隔 TODO: 支持设置为每帧刷新
        public float interval = 0.02f;

        // 热力影响半径
        public float influenceRadius = 3.0f;

        // 亮度
        public float intensity = 3.0f;

        private float m_timer = 0.0f;

        public List<GameObject> impactFactors = new List<GameObject>();

        private void Update()
        {
            if( heatMapMode == HeatMapMode.RefreshEachFrame )
            {
                 RefreshHeatmap();
                 return;
            }
            m_timer += Time.deltaTime;
            if( m_timer > interval )
            {
                RefreshHeatmap();
                m_timer -= interval;
            }
        }

        private void RefreshHeatmap()
        {
            // set impact factor count
            material.SetInt("_FactorCount",impactFactors.Count);

            // set impact factors
            var ifPosition = new Vector4[impactFactors.Count];
            for( int i = 0 ; i < impactFactors.Count;i++ )
                ifPosition[i] = impactFactors[i].transform.position;
            material.SetVectorArray("_Factors",ifPosition);

            // set factor properties
            var properties = new Vector4[impactFactors.Count];
            for( int i = 0 ; i < impactFactors.Count;i++ )
                properties[i] = new Vector2(influenceRadius,intensity);
            material.SetVectorArray("_FactorsProperties",properties);

            // TODO: 将温度本身数值作为一个影响因子累乘
            // set factor values
            // var values = new float[impactFactors.Count];
            // for( int i = 0 ; i < impactFactors.Count;i++ )
            //     values[i] = Random.Range(0,5);
            // material.SetFloatArray("_FactorsValues",values);
        }
    }
}