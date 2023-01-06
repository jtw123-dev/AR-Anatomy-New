using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Animator _anim;
    [SerializeField] private int _currentAnim = 0;
    [SerializeField] private CalloutLabel _label;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is null");

            return _instance;
        }
    }

    private void Awake()
    {

        _instance = this;
    }

    private void Update()
    {
        if (_currentAnim == 1)
        {
            _label.ActivateLine();
        }
        else
        {
            _label.DeactiveateLine();
        }
    }

    public void NextButton()
    {
        if (_currentAnim==2)
        {
            _currentAnim = 2;
        }
        else
        {
            _currentAnim++;
            _anim.SetInteger("NextAnim", _currentAnim);
        }  
    }
    
    public void PreviousButton()
    {        
        if (_currentAnim==0)
        {
            _currentAnim = 0;
        }

        else
        {
            _currentAnim--;
            _anim.SetInteger("NextAnim", _currentAnim);
        }    
    }
}
