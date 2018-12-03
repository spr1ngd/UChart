

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UChart
{
    public class LoomParam
    {
        public Action<Transform> action;
        public Transform param;
    }

    public class Loom : MonoBehaviour
    {
        private static Loom m_instance = null;
        public static Loom Instance { get { return m_instance; } }

        public void Init()
        {
            m_instance = this.gameObject.AddComponent<Loom>();
        }

        public void Release()
        {
            updateDic.Clear();
            updateParamDic.Clear();
            updateDic = null;
            updateParamDic = null;
        }

        private IDictionary<int,Action> updateDic = new Dictionary<int,Action>();
        private IDictionary<int,LoomParam> updateParamDic = new Dictionary<int,LoomParam>();

        private void Update()
        {
            if(null != updateDic && updateDic.Count > 0)
            {
                foreach(Action update in updateDic.Values)
                {
                    update();
                }
            }
            if(null != updateParamDic && updateParamDic.Count > 0)
            {
                foreach(var action in updateParamDic.Values)
                {
                    action.action(action.param);
                }
            }
        }

        public void InvokeUpdate(Action action)
        {
            int hash = action.GetHashCode();
            if(updateDic.ContainsKey(hash))
                return;
            updateDic.Add(hash,action);
        }

        public void InvokeUpdate(Action<Transform> action,Transform param)
        {
            int hash = action.GetHashCode();
            var loomParam = new LoomParam() { action = action,param = param };
            if(updateParamDic.ContainsKey(hash)) updateParamDic[hash] = loomParam;
            else updateParamDic.Add(hash,loomParam);
        }

        public void RemoveUpdate(Action action)
        {
            int hash = action.GetHashCode();
            if(!updateDic.ContainsKey(hash))
                return;
            updateDic.Remove(hash);
        }

        public void RemoveUpdate(Action<Transform> action)
        {
            int hash = action.GetHashCode();
            if(!updateParamDic.ContainsKey(hash))
                return;
            updateParamDic.Remove(hash);
        }
    }
}