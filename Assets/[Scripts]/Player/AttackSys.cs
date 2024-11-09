using System.Collections;
using System.Collections.Generic;
using ProjectSaga;
using UnityEngine;

public class AttackSys : MonoBehaviour
{
    public static AttackSys Instance { get; private set; }
    [SerializeField] private GameObject _weaponfbx = default;
    [SerializeField] private GameObject _swordSheathfbx = default;
    private Animator _animator = default;
    public bool _isWithdrawn = false;
    

    
    
    void Awake()
    {
        Instance = this;
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (PickUpSys.Instance._isPicked == false)
            {
                Debug.Log("No weapon picked up");
            }
            else
            {
                StartCoroutine(WithdrawingSequence());
                _isWithdrawn = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_isWithdrawn == false)
            {
                Debug.Log("You need to have your weapon withdrawn first");
            }
            else
            {
                AnimationController.Instance.WeaponSlash();
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
                _isWithdrawn = false;
            }
        }
    }

    IEnumerator WithdrawingSequence()
    { 
        AnimationController.Instance.WithdrawingWeapon();
        yield return new WaitForSeconds(0.5f);
        _swordSheathfbx.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _weaponfbx.SetActive(true);
        yield break;
    }
    
    IEnumerator SheetingSequence()
    {
        AnimationController.Instance.Sheating();
        yield return new WaitForSeconds(0.8f);
        _weaponfbx.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _swordSheathfbx.SetActive(true);
        yield break;
    }
}
