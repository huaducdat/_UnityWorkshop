using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private float dame = 1;
    [SerializeField]
    protected string hitTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDame(float Dame)
    {
        dame = Dame;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(hitTag))
        {
            IActor health = other.gameObject.GetComponent<IActor>();
            if(health != null)
            {
                health.TakeDame(dame);
            }
        }
    }

}
