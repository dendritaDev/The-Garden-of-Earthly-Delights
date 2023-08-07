using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPersonaje : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del personaje

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0f) * velocidad * Time.deltaTime;
        transform.position += movimiento;
    }
}