
using UnityEngine;
using DG.Tweening;
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
                    var render = this.GetComponent<Renderer>();
                    if( null == render)
                        throw new UChartNotFoundException("Can not found Renderer component on HeatMapComponent");
                    m_material = render.material;
                }
                return m_material;
            }
        }

        public HeatMapMode heatMapMode = HeatMapMode.RefreshEachFrame;

        // 热力图刷新间隔 TODO: 支持设置为每帧刷新
        public float interval = 0.02f;
        private float m_timer = 0.0f;

        public bool mimic = false;
        private float m_mimicInterval = 1.0f;
        private float m_mimicTiemr = 0.0f;
        public List<HeatMapFactor> impactFactors = new List<HeatMapFactor>();

        private void Update()
        {
            if (mimic)
            {
                m_mimicTiemr += Time.deltaTime;
                if (m_mimicTiemr > m_mimicInterval)
                {
                    foreach (var factor in impactFactors)
                    {
                        float lerp = factor.temperatureFactor;
                        DOTween.To(() => lerp, x => lerp = x, Random.Range(0.1f, 3.0f), m_mimicInterval).OnUpdate(() =>
                        {
                            factor.temperatureFactor = lerp;
                        });
                    }
                    m_mimicTiemr = 0.0f;
                }
            }

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
            for (int i = 0; i < impactFactors.Count; i++)
            {
                var factor = impactFactors[i];
                properties[i] = new Vector4(factor.influenceRadius, factor.intensity,factor.temperatureFactor,0.0f);
            }
            material.SetVectorArray("_FactorsProperties",properties);

            // TODO: 将温度本身数值作为一个影响因子累乘
            // set factor values
            //var values = new float[impactFactors.Count];
            //for( int i = 0 ; i < impactFactors.Count;i++ )
            //    values[i] = Random.Range(0,5);
            //material.SetFloatArray("_FactorsValues",values);
        }
    }
}