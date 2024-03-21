using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trailRenderer;
    public float speed = 1500f;
    public float damage = 34f;
    void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        //Destroy(gameObject, 3.0f);
        Invoke("BulletDisable", 3.0f);
    }
    void BulletDisable()
    {
        if(this.gameObject.activeSelf)
          this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        rb.AddForce(tr.forward * speed);
    }
    private void OnDisable()
    {
        rb.Sleep();
        trailRenderer.Clear();
        tr.position = Vector3.zero;
    }
}
