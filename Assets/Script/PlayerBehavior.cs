using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodVFX;
    public int RoundsWin { get; private set; } = 0;
    [SerializeField] public KeyCode ShootButton;
    public bool hasShoot { get; private set; } = false;
    public bool isDead { get; private set; } = false;
    public bool ControlLock { get; private set; } = false;
    [SerializeField] private AudioSource gunSFX;
    [SerializeField] private AudioSource winSFX;
    private Vector3 initialTransform;
    private Quaternion initialRotaation;
    Rigidbody rb;
    private bool hasForce = false;
    private const float forcePush = 300f;

    // Update is called once per frame
    private void Awake()
    {
        initialTransform = transform.position;
        initialRotaation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (ControlLock) return;
        if (isDead) return;

        if (Input.GetKey(ShootButton))
        {
            gunSFX.Play();
            hasShoot = true;
            ControlLock = true;
        }

    }

    private void FixedUpdate()
    {
        if(isDead && !hasForce)
        {
            rb.AddForce(forcePush * transform.right);
            hasForce = true;
        }
    }

    public void IsDead()
    {
        isDead = true;
        bloodVFX.Play();
    }

    public void WinRound()
    {
        RoundsWin++;
    }
    public void Reset()
    {
        rb.velocity.Set(0, 0, 0);
        transform.rotation = initialRotaation;
        transform.position = initialTransform;
        hasShoot = false;
        isDead = false;
        ControlLock = false;
        hasForce = false;
        bloodVFX.Stop();
    }
    public void LockControl()
    {
        ControlLock = true;
    }

    public void HasWon()
    {
            winSFX.Play();
    }

    public void ResetScore()
    {
        RoundsWin = 0;
    }
}
