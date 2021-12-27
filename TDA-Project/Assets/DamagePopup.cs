using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMesh text;
    [SerializeField] private Transform DMPop;

    private void Awake()
    {
        text = transform.GetComponent<TextMesh>();
    }

    private void setup(int damage)
    {
        text.text= damage.ToString();
    }
    private void Start()
    {
        Instantiate(DMPop, Vector3.zero, Quaternion.identity);
    }
}


