using System.Collections;
using System.Collections.Generic;
using ProjectSaga;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
    public float velocidadRotacion;
    public float fuerzaSalto;

    Vector3 movimiento;
    
    [Header("Referencia")]
    public Rigidbody rigi;
    
    
    [Header("CheckGround")]
    public Vector3 checkgroundPosition;
    public bool isGround;
    public float checkGroundRatio;
    public LayerMask checkGroundMask;
    

    private void FixedUpdate()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal") * velocidad;
        movimiento.z = Input.GetAxisRaw("Vertical") * velocidad;
        movimiento = transform.TransformDirection(movimiento); // Transforma una direccion local en direccion del mundo.

        isGround = Physics.CheckSphere(transform.position + checkgroundPosition, checkGroundRatio, checkGroundMask);
        
        movimiento.y = rigi.velocity.y; 
        rigi.velocity = movimiento;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -velocidadRotacion * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space) && isGround) // KeyDown y KeyUp no funcionan correctamente en el FixedUpdate
        {
            rigi.AddForce(Vector3.up * fuerzaSalto);
        }

        if (Input.GetKey(KeyCode.W))
        {
            AnimationController.Instance.Moving();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            AnimationController.Instance.notMoving();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + checkgroundPosition, checkGroundRatio);
    }
    
}
