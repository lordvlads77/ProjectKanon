using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using ProjectSaga;
using UnityEngine;

public class AttackSys : NetworkBehaviour
{
    [SerializeField] private GameObject _weaponfbx = default;
    [SerializeField] private GameObject  _swordSheathfbx = default;
    private Animator _animator = default;
    public bool _isWithdrawn = false;
    public ProjectSaga.AnimationController animController;
    public ProjectSaga.SFXController sfxController;


    /*public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        //_weaponfbx = GetComponent<MeshRenderer>();
        if (IsServerStarted == true && _isWithdrawn == true)
        {
            
        }
    }*/

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (IsOwner == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_isWithdrawn == false)
            {
                StartCoroutine(WithdrawingSequence());
                _isWithdrawn = true;
            }
            else
            {
                Debug.Log("Your already withdrawn your weapon");
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StartCoroutine(NotWithdrawingSequence());
        }

        if (Input.GetMouseButton(0))
        {
            if (_isWithdrawn == false)
            {
                Debug.Log("You need to have your weapon withdrawn first");
            }
            else
            {
                StartCoroutine(WeaponSlashSequence());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_isWithdrawn == false)
            {
                Debug.Log("You need to have your weapon withdrawn first");
            }
            else
            {
                StartCoroutine(NotSlashing());
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_isWithdrawn == false)
            {
                Debug.Log("You need to have your weapon withdrawn first");
            }
            else
            {
                StartCoroutine(SheetingSequence());
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (_isWithdrawn == true)
            {
                StartCoroutine(NotSheathing());
                _isWithdrawn = false;
            }
            else
            {
                Debug.Log("You cannot sheath your weapon if it is already sheathed");
            }
        }
    }

    IEnumerator WithdrawingSequence()
    { 
        animController.WithdrawingWeapon();
        yield return new WaitForSeconds(0.2f);
        sfxController.SwordSheath();
        yield return new WaitForSeconds(0.5f);
        _swordSheathfbx.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _weaponfbx.SetActive(true);
        yield break;
    }
    
    IEnumerator SheetingSequence()
    {
        animController.Sheating();
        yield return new WaitForSeconds(0.46f);
        sfxController.SwordSheath();
        yield return new WaitForSeconds(0.8f);
        _weaponfbx.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _swordSheathfbx.SetActive(true);
        yield break;
    }
    
    IEnumerator WeaponSlashSequence()
    {
        animController.WeaponSlash();
        yield return new WaitForSeconds(0.8f);
        sfxController.SwordSwing();
        yield break;
    }

    IEnumerator NotWithdrawingSequence()
    {
        yield return new WaitForSeconds(1.28f);
        animController.NotWithdrawing();
    }

    IEnumerator NotSlashing()
    {
        yield return new WaitForSeconds(2.01f);
        animController.FinishedSlashing();
    }
    
    IEnumerator NotSheathing()
    {
        yield return new WaitForSeconds(1.38f);
        animController.FinishedSheathing();
    }
}
