using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] private GameObject fillObject;
    bool isDead = false;


    private void Start()
    {
        fillObject.GetComponent<Slider>().maxValue = health;
    }
    public bool IsDead()
    {
        return isDead;
    }


    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        UpdateHealthBar(health);
        // Debug.Log($"{gameObject.name} took damage: {damage}. Current health: {health}");
        if (health == 0)
        {
            DieMethod();
        }
    }
    public void SetActiveHealthBar(bool setActive)
    {
        if (fillObject != null)
        {
            fillObject.SetActive(setActive);
        }
        else
        {
            Debug.LogWarning("FillObject is not assigned in the Health script on " + gameObject.name);
        }
    }
    private void UpdateHealthBar(float health)
    {
        if (fillObject != null)
        {
            fillObject.GetComponent<Slider>().value = health;
        }
        else
        {
        Debug.LogWarning("FillImage is not assigned in the Health script on " + gameObject.name);
        }

    }
    private void DieMethod()
    {
        if (isDead == true)
        {
            return;
        }
        isDead = true;
        SetActiveHealthBar(false);
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<ActionScheduler>().CancelCurrentAction();
        GetComponent<Combat>().enabled = false;
    }
   
}