using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    public Transform _espada;
    public Transform _posicionHip;
    public Transform _posicionMano;
    public Animator _animator;
    public void EspadaGuardada()
    {
        _espada.SetParent(_posicionHip);
        _espada.localPosition = Vector3.zero;
        _espada.localRotation = Quaternion.Euler(Vector3.zero);
        _espada.localScale = Vector3.one;
        _animator.SetBool("GuardarEspada", false);
        _animator.SetLayerWeight(1, 0.0f);
    }

    public void EspadaEnMano()
    {
        _espada.SetParent(_posicionMano);
        _espada.localPosition = Vector3.zero;
        _espada.localRotation = Quaternion.Euler(Vector3.zero);
        _espada.localScale = Vector3.one;
        _animator.SetBool("SacarEspada", false);
    }
}
