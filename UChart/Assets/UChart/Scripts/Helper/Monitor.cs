
using UnityEngine;
using System.Collections.Generic;

namespace UChart
{
    public class Monitor : MonoBehaviour
    {
        private Dictionary<string,string> m_keyvalues = new Dictionary<string, string>();

        private void Update()
        {
            if ( Input.GetKeyDown(KeyCode.A) )
            {
                m_keyvalues.Add(Random.value.ToString(),Random.Range(0,100).ToString());
            }
        }

        private void OnGUI()
        {
            foreach( var pair in m_keyvalues)
            {
                GUILayout.Label(pair.Key + "____" + pair.Value);
            }
        }

        public void AddMonitor( string title , string value )
        {
            if (m_keyvalues.ContainsKey(title))
            {
                m_keyvalues[title] = value;
                return;
            }
            m_keyvalues.Add(title,value);
        }

        public void RemoveMonitor( string title  )
        {
            if(!m_keyvalues.ContainsKey(title))
                return;
            m_keyvalues.Remove(title);
        }

        public void Clear()
        {
            m_keyvalues.Clear();
        }
    }
}