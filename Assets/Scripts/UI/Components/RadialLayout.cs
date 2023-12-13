using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RadialLayout : LayoutGroup
    {
        [SerializeField] private float fDistance; 
        [Range(0f,360f)] private float MinAngle, MaxAngle, StartAngle;
        protected override void OnEnable() { base.OnEnable(); CalculateRadial(); }
        public override void SetLayoutHorizontal() { }
        public override void SetLayoutVertical() { }
        
        public override void CalculateLayoutInputVertical()
        {
            CalculateRadial();
        }
        
        public override void CalculateLayoutInputHorizontal()
        { 
            CalculateRadial();
        }
        
    #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            CalculateRadial();
        }
    #endif
        
        private void CalculateRadial()
        {
            m_Tracker.Clear();
            if (transform.childCount == 0)
                return;
            var fOffsetAngle = ((MaxAngle - MinAngle)) / (transform.childCount -1);
            var fAngle = StartAngle;
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = (RectTransform)transform.GetChild(i);
                if (child != null)
                {
                    m_Tracker.Add(this, child, 
                        DrivenTransformProperties.Anchors |
                                    DrivenTransformProperties.AnchoredPosition |
                                    DrivenTransformProperties.Pivot);
                    var vPos = new Vector3(Mathf.Cos(fAngle * Mathf.Deg2Rad), Mathf.Sin(fAngle * Mathf.Deg2Rad), 0);
                    child.localPosition = vPos * fDistance;
                    
                    var rotationAngle = fAngle - 90f;
                    child.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
                    
                    child.anchorMin = child.anchorMax = child.pivot = new Vector2(0.5f, 0.5f);
                    fAngle += fOffsetAngle;
                }
            }
        }
    }
}
