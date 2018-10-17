
using UnityEngine;
using System.Collections.Generic;

namespace UChart
{
    public class Monitor : MonoBehaviour
    {
        public Dictionary<string,string> keyvalues = new Dictionary<string, string>();

        private void OnGUI()
        {
            foreach( var pair in keyvalues )
            {
                GUILayout.Label(pair.Key + "____" + pair.Value);
            }
        }
    }
}