using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpHandler : MonoBehaviour
{
    private TextMesh dmgnumber;
    public static int damage;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        Destroy(gameObject, 1f);
    }
}
