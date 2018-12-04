
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UChart
{ 
    public enum UpdateType
    {
        UPDATE,
        FIXEDUPDATE,
        LATEUPDATE
    }

    public class Loom : MonoBehaviour
    {
        private static Loom m_instance = null;
        public static Loom Instance
        {
            get
            {
                if (null == m_instance)
                {
                    var loom = new GameObject("__Loom__");
#if UCHART_DEBUG
                    loom.hideFlags = HideFlags.HideInHierarchy;
#endif
                    m_instance = loom.AddComponent<Loom>();
                }
                return m_instance;
            }
        }

        public void OnApplicationQuit()
        {
            m_updateDic.Clear();
            m_updateDic = null;
        }

        private IDictionary<UpdateType, IDictionary<string, Action>> m_updateDic = new Dictionary<UpdateType, IDictionary<string, Action>>();
 
        private void Update()
        {
            IDictionary<string,Action> updateDic = null;
            if(m_updateDic.TryGetValue(UpdateType.UPDATE,out updateDic))
            {
                foreach(KeyValuePair<string,Action> pair in updateDic)
                {
                    pair.Value();
                }
            }
        }

        private void FixedUpdate()
        {
            IDictionary<string,Action> updateDic = null;
            if(m_updateDic.TryGetValue(UpdateType.FIXEDUPDATE,out updateDic))
            {
                foreach(KeyValuePair<string,Action> pair in updateDic)
                {
                    pair.Value();
                }
            }
        }

        private void LateUpdate()
        {
            IDictionary<string,Action> updateDic = null;
            if(m_updateDic.TryGetValue(UpdateType.LATEUPDATE,out updateDic))
            {
                foreach(KeyValuePair<string,Action> pair in updateDic)
                {
                    pair.Value();
                }
            }
        }

        public void InvokeUdpate( string updateId,Action updateAction,UpdateType updateType = UpdateType.UPDATE)
        {
            if (string.IsNullOrEmpty(updateId))
            {
                Debug.LogError(string.Format("can't add null udpate id"));
                return;
            }
            if ( null == updateAction )
            {
                Debug.LogError(string.Format("can't add null udpate action"));
                return;
            }
            IDictionary<string, Action> updateDic = null;
            if (!m_updateDic.TryGetValue(updateType, out updateDic))
            {
                updateDic = new Dictionary<string, Action>();
                m_updateDic[updateType] = updateDic;
            }
            if (updateDic.ContainsKey(updateId))
            {
                Debug.LogWarning(string.Format("can't add same id [{0}] update event.",updateId));
                return;
            }
            m_updateDic[updateType].Add(updateId,updateAction);
        }

        public void RemoveUpdate(string updateId,UpdateType updateType = UpdateType.UPDATE)
        {
            if(string.IsNullOrEmpty(updateId))
            {
                Debug.LogError(string.Format("can't add null udpate id"));
                return;
            }
            if (m_updateDic.ContainsKey(updateType))
            {
                var updateDic = m_updateDic[updateType];
                if (updateDic.ContainsKey(updateId))
                    m_updateDic[updateType].Remove(updateId);
                else
                {
                    Debug.LogWarning(string.Format("don't exist update ud [{0}]",updateId));
                }
            }
            else
            {
                Debug.LogWarning(string.Format("don't exist [{0}] type update action.",updateType));
            }
        }
    }
}