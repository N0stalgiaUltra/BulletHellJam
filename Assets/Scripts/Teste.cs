using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Katana;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Katana.transform.rotation.z < 90f || Katana.transform.rotation.z > -90f)
        {
            Debug.Log("frente");
        }
        else if(Katana.transform.rotation.z > 90f || Katana.transform.rotation.z < -90f)
            Debug.Log("tras");
    }
}
