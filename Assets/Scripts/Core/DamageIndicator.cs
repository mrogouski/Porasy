using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    private float lifetime = 1.5f;
    private float minDistance = 0.8f;
    private float maxDistance = 1f;
    private float angle = 45f;
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    public TextMeshProUGUI tmpText;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        SetupDisplayLocation();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            Destroy(gameObject);
        }

        transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifetime));
    }

    private void SetupDisplayLocation()
    {
        Quaternion rotation = Camera.main.transform.rotation;
        transform.rotation = rotation;
        initialPosition = transform.position + new Vector3(0, 1.5f, 0);
        float distance = Random.Range(minDistance, maxDistance);
        finalPosition = initialPosition + (Quaternion.Euler(0, angle, 0) * new Vector3(distance, 0, distance));
        transform.localScale = Vector3.zero;
    }

    public void SetDamageText(float damage)
    {
        tmpText.text = damage.ToString();
        tmpText.color = Color.red;
    }

    public void SetLocation(Vector3 location)
    {
        initialPosition = location;
    }
}
