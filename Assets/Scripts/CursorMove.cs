using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMove : MonoBehaviour
{
    //movimento do cursor e hitmark quando houver dano
    Camera mainCam;
    
    SpriteRenderer sr;
    public GameObject hitmarker;
    
    void Start()
    {
        //Cursor.visible = false;
        mainCam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        HitmarkDisable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Cursor
            Vector3 mousePos = Input.mousePosition;
            Vector2 CursorMousePos = mainCam.ScreenToWorldPoint(mousePos);
            this.transform.position = CursorMousePos;
        #endregion    

        hitmarker.transform.position = this.transform.position;
        
    }

    public void HitmarkEnable()
    {
        hitmarker.SetActive(true);
        Invoke("HitmarkDisable", 0.1f);
        
    }
    public void HitmarkDisable()
    {
        hitmarker.SetActive(false);
    }
}
