using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorSteering
{
    /// <summary>
    /// Class responsible of controlling the movement behavior of the agent and handling
    /// all the steering behaviors attached for bleending/prioritise them
    /// </summary>
    public class Agent : MonoBehaviour
    {
        [Header("Blending Related attributes")]
        [Tooltip("Blend by weight")]
        public bool blendWeight = false;
        [Tooltip("Blend by priority")]
        public bool blendPriority = false;
        [Tooltip("Priority threshold for taking the value into account")]
        public float priorityThreshold = 0.2f;

        [Space]
        [Header("Steering Behavior")]
        public float maxSpeed;
        public float maxAccel;
        public float maxRotation;
        public float maxAngularAccel;
        [Tooltip("Angular velocity")]
        public float orientation;
        [Tooltip("variable value, just like speed, for changing orientation")]
        public float rotation;
        [Tooltip("Kinematic or Dynamic Movement")]
        public bool isKinematic = true;

        [Space]
        [Header("Debug Info")]
        public Vector2 velocity;
        public float stunned;
        public Vector3 knocnkbackVector;
        [Tooltip("a mas fuerza, mayor distancia de knockback")]
        public float knockbackForce; 
        [Tooltip("a mas tiempo, mas duracion del knockback")]
        public float knockbackTimeWeight;
        public float velocityMagnitude;

        protected Steering steering;

        // Steering groups, grouped by priority value
        private Dictionary<int, List<Steering>> groups;

        private Rigidbody2D rigidbody2D;

        void Start()
        {
            velocity = Vector3.zero;
            steering = new Steering();
            groups = new Dictionary<int, List<Steering>>();
            rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            if (!isKinematic)
                return;

            ProcessStun();

            Vector3 displacement = (velocity + CalculateKnockback()) * Time.deltaTime;

            SetOrientation();

            // The ForceMode will depend on what you want to achieve
            // We are using VelocityChange for illustration purposes
            rigidbody2D.AddForce(displacement, ForceMode2D.Force);
            rigidbody2D.rotation = orientation;

        }

        private void Update()
        {

            if (isKinematic)
                return;

            ProcessStunDeltaTime();

            Vector3 displacement = (velocity + CalculateKnockback()) * Time.deltaTime;
            SetOrientation();

            transform.Translate(displacement/*, Space.World*/);
            
            // restore rotation to initial point
            // before rotating the object to our value
            transform.rotation = new Quaternion();
            transform.Rotate(Vector3.up, orientation);
        }


        private void SetOrientation()
        {
            orientation += rotation * Time.deltaTime;

            //Clamp la orientacion
            if (orientation < 0.0f)
                orientation += 360.0f;
            else if (orientation > 360.0f)
                orientation -= 360.0f;
        }

        private void ProcessStun()
        {
            if (stunned > 0f)
            {
                velocity = Vector2.zero;
                stunned -= Time.fixedDeltaTime; //fixedDeltaTime para el que actua con fisicas
            }
        }

        private void ProcessStunDeltaTime()
        {
            if (stunned > 0f)
            {
                velocity = Vector2.zero;
                stunned -= Time.deltaTime; 
            }
        }

        private Vector2 CalculateKnockback()
        {
            if (knockbackTimeWeight > 0f)
            {
                knockbackTimeWeight -= Time.fixedDeltaTime;
            }
            //mientras knockbackTimeWeight no sea 0, seguimos haciendo knockback, independientemente de si el agente esta stunned o no
            return knocnkbackVector * knockbackForce * (knockbackTimeWeight > 0f ? 1f : 0f); 
        }

        private void LateUpdate()
        {
            if (blendPriority)
            {
                steering = GetPrioritySteering();
                groups.Clear();
            }

            velocity += steering.linear * Time.deltaTime;
            rotation += steering.angular * Time.deltaTime;


            if (velocity.magnitude > maxSpeed)
            {
                velocity.Normalize();
                velocity = velocity * maxSpeed;
            }
            velocityMagnitude = velocity.magnitude;

            if (rotation > maxRotation)
            {
                rotation = maxRotation;
            }

            if (steering.angular == 0.0f)
            {
                rotation = 0.0f;
            }

            if (steering.linear.sqrMagnitude == 0.0f)
            {
                velocity = Vector3.zero;
            }

            steering = new Steering();
        }

        public void SetSteering(Steering steering)
        {
            this.steering = steering;
        }

        public void SetSteeringByWeight(Steering steering, float weight)
        {
            this.steering.linear += (weight * steering.linear);
            this.steering.angular += (weight * steering.angular);
        }

        public void SetSteeringByPriority(Steering steering, int priority)
        {
            if (!groups.ContainsKey(priority))
            {
                groups.Add(priority, new List<Steering>());
            }
            groups[priority].Add(steering);
        }

        private Steering GetPrioritySteering()
        {
            Steering steering = new Steering();
            List<int> gIdList = new List<int>(groups.Keys);
            gIdList.Sort();
            foreach (int gid in gIdList)
            {
                steering = new Steering();
                foreach (Steering singleSteering in groups[gid])
                {
                    steering.linear += singleSteering.linear;
                    steering.angular += singleSteering.angular;
                }
                if (steering.linear.magnitude > priorityThreshold
                     || Mathf.Abs(steering.angular) > priorityThreshold)
                {
                    return steering;
                }
            }
            return steering;
        }

    }
}
