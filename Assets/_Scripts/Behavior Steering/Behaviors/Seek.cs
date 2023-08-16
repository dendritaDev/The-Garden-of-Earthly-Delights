using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSteering
{
    public class Seek : AgentBehavior
    {

        public override Steering GetSteering()
        {
            Steering steering = new Steering();
            steering.linear = target.transform.position - this.transform.position;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;

            return steering;
        }
    }

}
