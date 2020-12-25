using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_block : MonoBehaviour
{
    public Transform[] arrTran_top, arrTran_bottom, arrTran_mid, arr_start, arr_decor;
    public int int_current;
    private float flt_tmp;    
    private int i;
    // Start is called before the first frame update
    private void Start()
    {
        fn_blockPos();
        if (arr_start.Length > 0) { for (i = 0; i < arr_start.Length; i++) arr_start[i].gameObject.SetActive(false); }
        Invoke("fn_startBlock", 15f);
    }
    public void fn_startBlock() { for (i = 0; i < arr_start.Length; i++) arr_start[i].gameObject.SetActive(true); }
    public void fn_blockPos()
    {
        for (i = 0; i < arrTran_top.Length; i++)
        {
            flt_tmp = Random.Range(0.0f, 1.0f);
            arrTran_top[i].localPosition = Random.Range(0.0f, 1.0f) * (-0.6f) * Vector3.up;
            arrTran_bottom[i].localPosition = Random.Range(0.0f, 1.0f) * (0.6f) * Vector3.up;
            arrTran_mid[i].localPosition = (Random.Range(-0.6f, 0.6f)) * Vector3.up;
        }
        for (i = 0; i < arr_decor.Length; i++)
        {
            arr_decor[i].localPosition = Random.Range(-8f, 8f) * Vector3.right + arr_decor[i].localPosition.y * Vector3.up;
            arr_decor[i].localScale = Vector3.right * (Random.Range(-1, 1) > 0 ? 1f : -1f) + Vector3.up + Vector3.forward;
        }            
    }
}
