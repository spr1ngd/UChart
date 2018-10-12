
using UnityEngine;

namespace UChart
{
    public class UChartObject : MonoBehaviour
    {
        protected Transform myTransform = null;
        protected string uchartId = string.Empty;

        public virtual void Init()
        {
            this.myTransform = this.transform;
        }

        public virtual void GenerateId()
        {
            uchartId = string.Format("uchart_{0}",NewGuid());
        }

        public string NewGuid()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}