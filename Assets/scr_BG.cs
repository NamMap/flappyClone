using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_BG : MonoBehaviour
{
    public Transform tran_Root, tran_tip, tran_RootC, tran_tipC, tran_RootB, tran_tipB, tran_tmp;
    public Transform[] arrTran_top, arrTran_bottom;
    public int int_currentBlock;
    public float flt_rateC, flt_rateB;
    public scr_player scr_p;
    private AudioSource as_score;
    private void Start()
    {
        as_score = GetComponent<AudioSource>();
        //arrTran_top[int_currentBlock].GetComponent<SpriteRenderer>().color = Color.blue;
        //arrTran_bottom[int_currentBlock].GetComponent<SpriteRenderer>().color = Color.blue;
        scr_p.tran_top = arrTran_top[int_currentBlock];
        scr_p.tran_bottom = arrTran_bottom[int_currentBlock];
    }
    public void fn_move(Vector3 _v3_move) {
        tran_Root.Translate(_v3_move);
        if (tran_Root.localPosition.x <= -48f) fn_switch();
        if (arrTran_top[int_currentBlock].position.x <= -5.5f) fn_blockFocus();
        tran_RootB.Translate(_v3_move * flt_rateB);
        if (tran_RootB.localPosition.x <= -48f) fn_switchB();
        tran_RootC.Translate(_v3_move * flt_rateC);
        if (tran_RootC.localPosition.x <= -48f) fn_switchC();        
    }
    public void fn_blockFocus()
    {
        //arrTran_top[int_currentBlock].GetComponent<SpriteRenderer>().color = Color.white;
        //arrTran_bottom[int_currentBlock].GetComponent<SpriteRenderer>().color = Color.white;
        int_currentBlock = int_currentBlock == arrTran_top.Length - 1 ? 0 : int_currentBlock + 1;
        as_score.Play();
        scr_p.tran_top = arrTran_top[int_currentBlock];
        scr_p.tran_bottom = arrTran_bottom[int_currentBlock];
        scr_p.fn_addScore();
        //arrTran_top[int_currentBlock].GetComponent<SpriteRenderer>().color = Color.blue;
        //arrTran_bottom[int_currentBlock].GetComponent<SpriteRenderer>().color = Color.blue;
    }
    public void fn_switch() {
        tran_tmp = tran_Root;
        tran_Root = tran_Root.GetChild(1);
        tran_Root.parent = tran_Root.parent.parent;
        tran_tmp.parent = tran_tip;
        tran_tmp.localPosition = Vector3.right * 16f;
        tran_tip = tran_tmp;
        tran_tip.GetComponent<scr_block>().fn_blockPos();
    }
    public void fn_switchB() {
        tran_tmp = tran_RootB;
        tran_RootB = tran_RootB.GetChild(0);
        tran_RootB.parent = tran_RootB.parent.parent;
        tran_tmp.parent = tran_tipB;
        tran_tmp.localPosition = Vector3.right * 16f;
        tran_tipB = tran_tmp;
    }
    public void fn_switchC() {
        tran_tmp = tran_RootC;
        tran_RootC = tran_RootC.GetChild(1);
        tran_RootC.parent = tran_RootC.parent.parent;
        tran_tmp.parent = tran_tipC;
        tran_tmp.localPosition = Vector3.right * 16f;
        tran_tipC = tran_tmp;
        tran_tipC.GetComponent<scr_cloud>().fn_posC();
    }
}
