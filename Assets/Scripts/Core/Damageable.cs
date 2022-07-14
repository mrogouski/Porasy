using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private bool _fadeOut;
    private StateManager _stateManager;

    public GameObject healthBar;
    public GameObject damageTextPrefab;
    private float fadeOutRate;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public bool isInvulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out _stateManager))
        {
            fadeOutRate = _stateManager.CharacterStats.fadeOutRate;
            healthBar.GetComponent<HealthIndicator>().SetMaxHealth(_stateManager.CharacterStats.maxHealth);
            currentHealth = _stateManager.CharacterStats.maxHealth;
        }
        else
        {
            if (TryGetComponent(out PlayerController playerController))
            {
                if (healthBar != null)
                {
                    healthBar.GetComponent<HealthIndicator>().SetMaxHealth(playerController.characterStats.maxHealth);
                }
                currentHealth = playerController.characterStats.maxHealth;
            }
        }
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
        if (isInvulnerable || currentHealth <= 0)
        {
            Debug.Log($"Target is invulnerable or dead");
            return;
        }

        if (_stateManager != null)
        {
            _stateManager.GotHit = true;
        }

        Debug.Log($"Hit Damage: {damage}");
        currentHealth -= damage;
        healthBar.GetComponent<HealthIndicator>().SetHealth(currentHealth);
        DisplayDamage(damage);
        //_animator.SetTrigger(_hashHit); // TODO: Fix animation reference

        if (currentHealth <= 0)
        {
            if (_stateManager != null)
            {
                _stateManager.IsDead = true;
            }

            _fadeOut = true;
            //_animator.SetTrigger(DeathHash);
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
