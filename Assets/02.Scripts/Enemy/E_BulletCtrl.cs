using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BulletCtrl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ConstantForce constantForce;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        constantForce = GetComponent<ConstantForce>();
        constantForce.relativeForce = Vector3.zero;
    }
    private void OnEnable()
    {
        constantForce.relativeForce = new Vector3(0f, 0f, 100f);
        Invoke("BulletDisable", 3.0f);
    }
    void BulletDisable()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        constantForce.relativeForce = Vector3.zero;
        trailRenderer.Clear();
        rb.Sleep();
    }

}
