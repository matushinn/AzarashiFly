using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzarashiController : MonoBehaviour {

    Rigidbody2D rb2D;

    Animator animator;
    float angle;

    bool isDead;


    public float maxHeight;
    public float flapVelocity;

    public float relativeVelocityX;
    //Spriteのオブジェクト参照
    public GameObject sprite;

    public bool IsDead()
    {
        return isDead;
    }

    //オブジェクトが生成された瞬間
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //Animatorのコンポーネント取得
        animator = sprite.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//最高高度に達していない場合に限りタップの入力を受け付ける
        if(Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }

        //角度反映
        ApplyAngle();

        //Angleが水平以上だったら、アニメーターのFlapフラグをtrueにする
        animator.SetBool("flap", angle >= 0.0f);

    }

    public void Flap()
    {
        //死んだら羽ばたけない
        if (isDead) return;

        //重力が聞いていないときは操作しない
        if (rb2D.isKinematic) return;

        //Velocityを直接書き換えて上方向に加速
        rb2D.velocity = new Vector2(0.0f, flapVelocity);
    }

    void ApplyAngle()
    {

        //現在の速度、相対速度から進んでいる角度を求める
        float targetAngle;

        //死亡したら常に下に向く
        if (isDead)
        {
            targetAngle = -90.0f;
        }
        else
        {
            targetAngle = Mathf.Atan2(rb2D.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
        }
        //回転アニメをスムージング
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        //Rotationの反映
        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        //クラッシュエフェクト
        Camera.main.SendMessage("Clash");

        //何かにぶつかったら死亡フラグを立てる
        isDead = true;
    }

    public void SetSteerActive(bool active)
    {
        //RigidBodyのオン、オフに切り替える
        rb2D.isKinematic = !active;
    }


}
