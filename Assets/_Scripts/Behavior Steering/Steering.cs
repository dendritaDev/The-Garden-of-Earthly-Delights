using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSteering
{
    /// <summary>
    /// Base classs that works as blueprint for all the different type of steering Behaviors to return 
    /// the angular and linear velocity of the agent. Seek, Arrive, Wander...
    /// </summary>
    public class Steering
    {
        public float angular;
        public Vector2 linear;
        public Steering()
        {
            angular = 0.0f;
            linear = new Vector2();
        }
    }
}
