using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using ProjectSaga;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    public float velocidad;
    public float velocidadRotacion;
    public float fuerzaSalto;

    Vector3 movimiento;
    
    [Header("Referencia")]
    public Rigidbody rigi;
    
    public ProjectSaga.AnimationController animController;
    
    
    [Header("CheckGround")]
    public Vector3 checkgroundPosition;
    public bool isGround;
    public float checkGroundRatio;
    public LayerMask checkGroundMask;


    public override void OnStartNetwork()
    {
        if (Owner.IsLocalClient) // The Owner
        {
            name += " (Local)";
        }
    }
    private void FixedUpdate()
    {
        if (IsOwner == false)
        {
            return;
        }
        movimiento.x = Input.GetAxisRaw("Horizontal") * velocidad;
        movimiento.z = Input.GetAxisRaw("Vertical") * velocidad;
        movimiento = transform.TransformDirection(movimiento); // Transforma una direccion local en direccion del mundo.

        isGround = Physics.CheckSphere(transform.position + checkgroundPosition, checkGroundRatio, checkGroundMask);
        
        movimiento.y = rigi.velocity.y; 
        rigi.velocity = movimiento;
    }

    private void Update()
    {
        if (IsOwner == false)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -velocidadRotacion * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
        }
        if (AttackSys.Instance._isWithdrawn == false) // KeyDown y KeyUp no funcionan correctamente en el FixedUpdate
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                StartCoroutine(JumpingCoRutine());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                StartCoroutine(SwordJumpingCoRutine());
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            animController.Moving();
        }
        
        if (AttackSys.Instance._isWithdrawn == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animController.notMoving();
                animController.SwordRun();
            }
            else
            {
                animController.notSwordRun();
            }
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            animController.notMoving();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + checkgroundPosition, checkGroundRatio);
    }

    IEnumerator JumpingCoRutine()
    {
        animController.jumping();
        yield return new WaitForSeconds(1.2f);
        rigi.AddForce(Vector3.up * fuerzaSalto);
        yield break;
    }
    
    IEnumerator SwordJumpingCoRutine()
    {
        animController.SwordJumping();
        yield return new WaitForSeconds(0.5f);
        rigi.AddForce(Vector3.up * fuerzaSalto);
        yield break;
    }
    
}
