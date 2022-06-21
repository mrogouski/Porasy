using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private bool _fadeOut;
    private Animator _animator;
    private int _hashHit = Animator.StringToHash("Hit");
    private int _hashDeath = Animator.StringToHash("Death");

    private bool GetHit;
    private bool IsDead;
    public GameObject healthBar;
    public GameObject damageTextPrefab;
    private float fadeOutRate;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public bool isInvulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out StateManager stateManager))
        {
            fadeOutRate = stateManager.CharacterStats.fadeOutRate;
            healthBar.GetComponent<HealthIndicator>().SetMaxHealth(stateManager.CharacterStats.maxHealth);
            currentHealth = stateManager.CharacterStats.maxHealth;
        }
        else
        {
            healthBar.GetComponent<HealthIndicator>().SetMaxHealth(GetComponent<PlayerController>().characterStats.maxHealth);
            currentHealth = GetComponent<PlayerController>().characterStats.maxHealth;
        }


        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeOut)
        {
            FadeOut();
        }
    }

    public void InflictDamage(float damage)
    {
        if (isInvulnerable || IsDead)
        {
            Debug.Log($"Target is invulnerable or dead");
            return;
        }

        GetHit = true;

        Debug.Log($"Hit Damage: {damage}");
        currentHealth -= damage;
        healthBar.GetComponent<HealthIndicator>().SetHealth(currentHealth);
        DisplayDamage(damage);
        //_animator.SetTrigger(_hashHit); // TODO: Fix animation reference

        if (currentHealth <= 0)
        {
            IsDead = true;
            _fadeOut = true;
            _animator.SetTrigger(_hashDeath);
            healthBar.SetActive(false);
            var characterController = GetComponent<CharacterController>();
            characterController.detectCollisions = false;
            StartCoroutine(DissolveBodyAfterDeath());
        }
    }

    private void DisplayDamage(float damage)
    {
        var indicator = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        indicator.GetComponent<DamageIndicator>().SetDamageText(damage);
    }

    private IEnumerator DissolveBodyAfterDeath()
    {
        _fadeOut = true;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void FadeOut()
    {
        Color agentColor = GetComponentInChildren<Renderer>().material.color;
        float fadeAmount = agentColor.a - (fadeOutRate * Time.deltaTime);

        agentColor = new Color(agentColor.r, agentColor.g, agentColor.b, fadeAmount);
        GetComponentInChildren<Renderer>().material.color = agentColor;

        if (agentColor.a <= 0)
        {
            _fadeOut = false;
        }
    }
}
