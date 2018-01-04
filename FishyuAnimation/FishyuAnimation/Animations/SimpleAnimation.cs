using FishyuAnimation.Interpolation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FishyuAnimation.Animations
{
    public class SimpleAnimation : Animation, IInterpolation
    {
        public SimpleAnimation()
        {
            iInterpolation = this;
        }

        public InterpolationValue GetInterpolationValue()
        {
            return new InterpolationValue()
            {
                V1 = 1.0f
            };
        }

        public float GetPerInterpolation(float input)
        {
            throw new NotImplementedException();
        }
    }
}
