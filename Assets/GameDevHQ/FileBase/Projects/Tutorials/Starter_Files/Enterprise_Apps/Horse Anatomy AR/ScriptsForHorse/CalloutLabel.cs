using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalloutLabel : MonoBehaviour {

    [SerializeField] private LineRenderer _line;
    [SerializeField] private GameObject _targetObj;
    [SerializeField] private GameObject _label;
    
    private void Start()
    {
        _line.startWidth = 0.01f;
        _line.endWidth = 0.01f;
    }

    private void Update()
    {
        _line.SetPosition(0, this.transform.position);
        _line.SetPosition(1, _targetObj.transform.position);
    }

    public void ActivateLine()
    {
        _label.SetActive(true);
    }

    public void DeactiveateLine()
    {
        _label.SetActive(false);
    }
}
