using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [HideInInspector]
    public Vector3 movementVector;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;

    public float speed = 3f;
    public int speedBonus = 0;

    Animate animate;
    private PauseManager pauseManager;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animate>();

        movementVector = new Vector3();
    }

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    void Update()
    {

            

        //Movement();
        
    }

    private void FixedUpdate()
    {

        if (pauseManager.isGamePaused)
        {

            rigidbody2d.velocity = Vector2.zero;
            animate.horizontal = 0f;
            animate.vertical = 0f;
            return;
        }

        Movement();
    }


    private void Movement()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        movementVector.Normalize();

        if (movementVector.x != 0)
        {
            lastHorizontalVector = movementVector.x;
        }

        if (movementVector.y != 0)
        {
            lastVerticalVector = movementVector.y;
        }

        animate.horizontal = movementVector.x;
        animate.vertical = movementVector.y;

        //"flip sprite renderer"
        if(movementVector.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
        else if(movementVector.x > 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            
        }

        movementVector *= (speed + (float)speedBonus);
        rigidbody2d.velocity = movementVector;
    }

}
