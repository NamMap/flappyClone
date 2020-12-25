using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_player : MonoBehaviour
{
    public GameObject go_hit;
    public Text uiTxt_score, uiTxt_Instruct;
    public Vector3 v3_move, v3_tmp;
    public float flt_jump, flt_speed, flt_gravity, flt_spin;
    public scr_BG scr_floor;
    public Transform tran_top, tran_bottom;
    public AudioSource as_flap, as_hit;
    private float flt_deltaTime, flt_time;
    private Transform tran_bird;
    private Animator ani_bird;    
    private float flt_borderX, flt_borderY, flt_tmp;
    private bool boo_dead, boo_start, boo_hit;
    private int i, int_score;
    private Vector3[] arr_v3Border = new Vector3[4] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };//topLeft, topRight, botLeft, botRight
    void Start() {
        //v3_move = Vector3.up * flt_jump * 0.5f;
        flt_borderX = 0.925f;
        flt_borderY = 9.5f;
        ani_bird = transform.GetChild(0).GetComponent<Animator>();
        tran_bird = ani_bird.transform;        
    }

    // Update is called once per frame
    void Update() {
        flt_deltaTime = flt_time != Time.time ? Time.time - flt_time : Time.deltaTime;
        if (boo_dead || !boo_start) {
            if (!boo_start)
            {
                flt_time = Time.time;
                if (Input.GetKeyDown(KeyCode.Space)) { 
                    boo_start = true;
                    uiTxt_Instruct.gameObject.SetActive(false);
                }
                else return;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                else return;
            }
        }        
        if (Input.GetKeyDown(KeyCode.Space) && !boo_hit) {
            v3_move += Vector3.up * flt_jump;
            as_flap.Play();
        }
        v3_move += Vector3.down * flt_deltaTime * flt_gravity;
        tran_bird.localRotation = Quaternion.Euler(Vector3.forward * (Mathf.Clamp(v3_move.y, -9.8f, 9.8f) / 9.8f) * flt_spin);
        flt_tmp = transform.position.y + v3_move.y * flt_deltaTime;
        if (flt_tmp <= 5.5f) transform.Translate(v3_move * flt_deltaTime);
        else {
            v3_tmp = Vector3.up * (5.5f - transform.position.y);
            transform.Translate(v3_tmp);
        }
        if (!boo_hit) scr_floor.fn_move(Vector3.left * flt_speed * flt_deltaTime);
        if (transform.position.y < -2.7f) {
            boo_dead = true;
            v3_tmp = transform.position;
            v3_tmp.y = -2.7f;
            transform.position = v3_tmp;
            ani_bird.SetTrigger("trg_dead");
            tran_bird.localRotation = Quaternion.identity;
            uiTxt_Instruct.text = "Tap <b> Space Bar </b> to replay";
            uiTxt_Instruct.gameObject.SetActive(true);
            as_hit.Play();
        }
        if (!boo_hit) fn_checkBlock();
        flt_time = Time.time;
    }
    public void fn_checkBlock() {
        for (i= 0; i < arr_v3Border.Length; i++) {
            if (i < 2) {
                arr_v3Border[i] = tran_top.position + Vector3.down * flt_borderY;
                arr_v3Border[i].x += flt_borderX * (i % 2 == 0 ? -1f : 1f);
            }
            else {
                arr_v3Border[i] = tran_bottom.position + Vector3.up * flt_borderY;
                arr_v3Border[i].x += flt_borderX * (i % 2 == 0 ? -1f : 1f);
            }
            arr_v3Border[i].z = transform.position.z;
        }
        if (transform.position.y > arr_v3Border[0].y || transform.position.y < arr_v3Border[3].y) {
            if(transform.position.x <= tran_top.position.x) {
                if (transform.position.x + 0.5f > arr_v3Border[0].x) {
                    boo_hit = true;
                    Debug.Log("Collide! FRONT");
                    go_hit.transform.position = transform.position + Vector3.forward * 0.5f;
                    go_hit.SetActive(true);
                }
            }
            else {
                if (transform.position.x - 0.5f < arr_v3Border[1].x) {
                    boo_hit = true;
                    Debug.Log("Collide! BACK");
                    go_hit.transform.position = transform.position - Vector3.forward * 0.5f;
                    go_hit.SetActive(true);
                }
            }
        }
        else
        {
            if(transform.position.x <= arr_v3Border[0].x) {
                if (Vector3.Distance(transform.position, arr_v3Border[0]) < 0.5f || Vector3.Distance(transform.position, arr_v3Border[2]) < 0.5f) {
                    boo_hit = true;
                    Debug.Log("Collide! CORNER FRONT");
                    go_hit.transform.position = transform.position + ((Vector3.Distance(transform.position, arr_v3Border[0]) < 0.5f ? arr_v3Border[0] : arr_v3Border[1]) - transform.position).normalized * 0.5f;
                    go_hit.SetActive(true);
                }
            }
            else if(transform.position.x < arr_v3Border[1].x) {
                if (transform.position.y + 0.5f > arr_v3Border[0].y || transform.position.y - 0.5f < arr_v3Border[2].y) {
                    boo_hit = true;
                    Debug.Log("Collide! TOP or BOTTOM");
                    go_hit.transform.position = transform.position + (transform.position.y + 0.5f > arr_v3Border[0].y ? Vector3.up : Vector3.down) * 0.5f;
                    go_hit.SetActive(true);
                }
            }
            else {
                if (Vector3.Distance(transform.position, arr_v3Border[1]) < 0.5f || Vector3.Distance(transform.position, arr_v3Border[3]) < 0.5f) {
                    boo_hit = true;
                    Debug.Log("Collide! CORNER BACK");
                    go_hit.transform.position = transform.position + ((Vector3.Distance(transform.position, arr_v3Border[1]) < 0.5f ? arr_v3Border[1] : arr_v3Border[3]) - go_hit.transform.position).normalized * 0.5f;
                    go_hit.SetActive(true);
                }
            }
        }
        if (boo_hit)
        {
            ani_bird.SetTrigger("trg_hit");
            as_hit.Play();
        }
    }
    public void fn_addScore()
    {
        int_score += 1;
        uiTxt_score.text = int_score.ToString();
    }
}
