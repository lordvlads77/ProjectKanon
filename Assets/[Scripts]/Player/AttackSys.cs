using System.Collections;
using System.Collections.Generic;
using ProjectSaga;
using UnityEngine;

public class AttackSys : MonoBehaviour
{
    [SerializeField] private GameObject _weaponfbx = default;
    [SerializeField] private GameObject _swordSheathfbx = default;
    private Animator _animator = default;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        
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
            }
        }
    }

    IEnumerator WithdrawingSequence()
    { 
        AnimationController.Instance.WithdrawingWeapon();
        yield return new WaitForSeconds(0.5f);
        _swordSheathfbx.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _weaponfbx.SetActive(true);
        yield break;
    }
}
