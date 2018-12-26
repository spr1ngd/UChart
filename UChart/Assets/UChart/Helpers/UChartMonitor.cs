
using UnityEngine;
using System.Collections.Generic;

namespace UChart
{
    public class UChartMonitor : MonoBehaviour
    {
        

        private void OnGUI()
        {
            if( GUILayout.Button("Start _UCHART_") )
            {
                DrawUChart();
            }
        }

        private void DrawUChart()
        {
            
        }
    }
}