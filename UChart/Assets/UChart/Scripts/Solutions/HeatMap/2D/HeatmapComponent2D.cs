
using UnityEngine;
using System.Collections.Generic;
using UChart.Core;

namespace UChart.HeatMap
{
    public class HeatmapComponent2D : MonoBehaviour
    {
        public new Material material
        {
            get { return this.GetComponent<Widget2D>().material; }
        }

        public HeatMapMode heatMapMode = HeatMapMode.RefreshEachFrame;

        public float interval = 0.02f;

        public float influenceRadius = 3.0f;

        public float intensity = 3.0f;

        private float m_timer = 0.0f;

        public List<GameObject> impactFactors = new List<GameObject>();

        private void Update()
        {
            if (heatMapMode == HeatMapMode.RefreshEachFrame)
            {
                RefreshHeatmap();
                return;
            }
            m_timer += Time.deltaTime;
            if (m_timer > interval)
            {
                RefreshHeatmap();
                m_timer -= interval;
            }
        }

        private void RefreshHeatmap()
        {
            // set impact factor count
            material.SetInt("_FactorCount", impactFactors.Count);

            // set impact factors
            var ifPosition = new Vector4[impactFactors.Count];
            for (int i = 0; i < impactFactors.Count; i++)
                ifPosition[i] = impactFactors[i].transform.position;
            material.SetVectorArray("_Factors", ifPosition);

            // set factor properties
            var properties = new Vector4[impactFactors.Count];
            for (int i = 0; i < impactFactors.Count; i++)
                properties[i] = new Vector2(influenceRadius, intensity);
            material.SetVectorArray("_FactorsProperties", properties);

            // set factor values
            // var values = new float[impactFactors.Count];
            // for( int i = 0 ; i < impactFactors.Count;i++ )
            //     values[i] = Random.Range(0,5);
            // material.SetFloatArray("_FactorsValues",values);
        }
    }
}