using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cloud : MonoBehaviour
{
    public Transform[] arrTran_cloud;
    int i;
    float flt_tmp;
    private void Start() { fn_posC(); }
    public void fn_posC() {
        for (i = 0; i < arrTran_cloud.Length; i++)
        {
            arrTran_cloud[i].gameObject.SetActive(Random.Range(0f, 100f) > 25f);
            arrTran_cloud[i].localPosition = Vector3.right * Random.Range(-2f, 2f) + Vector3.up * Random.Range(-1f, 1f);
        }
    }
}
