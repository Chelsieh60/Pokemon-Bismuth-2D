using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpbar : MonoBehaviour
{
    [SerializeField] GameObject health;
   
    public void SetHp(float hpNormal)
    {
        health.transform.localScale = new Vector3(hpNormal, 1f);
    }

    public IEnumerator SetHpSmooth(float newHp)
    {
        float currhealth = health.transform.localScale.x;
        float amount = currhealth - newHp;

        while (currhealth - newHp > Mathf.Epsilon)
        {
            currhealth -= amount * Time.deltaTime;
            health.transform.localScale = new Vector3(currhealth, 1f);
            yield return null;
        }
        health.transform.localScale = new Vector3(newHp, 1f);
    }
}
