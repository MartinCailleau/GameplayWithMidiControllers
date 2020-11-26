using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Reaktion
{
    public class TestGear : MonoBehaviour
    {

        public ReaktorLink reaktor;
        public Modifier intensity;
        public bool enableColor;
        public Gradient colorGradient;

        void Awake()
        {
            reaktor.Initialize(this);
            UpdateColor(0);
        }

        void Update()
        {
            UpdateColor(reaktor.Output);
        }

        void UpdateColor(float param)
        {
            if (intensity.enabled)
                GetComponent<Light>().intensity = intensity.Evaluate(param);
            if (enableColor)
            {
                LineRenderer[] lines = GetComponentsInChildren<LineRenderer>();
                foreach(LineRenderer line in lines)
                {
                    line.startColor = colorGradient.Evaluate(param);
                    line.endColor = colorGradient.Evaluate(param);
                }
            }

                
        }
    }
}