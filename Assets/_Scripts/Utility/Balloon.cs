using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float duration = 2f;

    private TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        
        StartCoroutine(Poof());
    }

    private IEnumerator Poof()
    {
        float timer = 0;

        while (timer < duration)
        {
            transform.position += Vector3.up * 100f * Time.deltaTime;
            text.alpha = text.alpha * (1 - (timer / duration));
            
            timer += Time.deltaTime;
            yield return null;
        }
        
        gameObject.SetActive(false);
    }

    public void SetText(string t)
    {
        text.text = t;
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
