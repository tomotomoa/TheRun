using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public LayerMask blocklayer; //ブロックレイヤー
    private Rigidbody2D rbody; //プレイヤー制御用Rigidbody2D

    private const float MOVE_SPEED = 3; //速度制御用
    private float moveSpeed; //移動速度
    private float jumpPower = 400; //ジャンプの力 
    private bool goJump = false; //ジャンプしたか否か
    private bool canJump = false; //ブロックに設置しているか否か

    public enum MOVE_DIR { //移動方向定義
        STOP,
        LEFT,
        RIGHT,
    }

    private MOVE_DIR moveDirection = MOVE_DIR.STOP;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        canJump = 
            Physics2D.Linecast ( transform.position - (transform.right * 0.3f )  , transform.position - ( transform.up * 0.1f ) , blocklayer ) || 
            Physics2D.Linecast ( transform.position + (transform.right * 0.3f )  , transform.position - ( transform.up * 0.1f ) , blocklayer );
    }

    void FixedUpdate () {
        //移動方向で処理を分岐
        switch(moveDirection) {
        case MOVE_DIR.STOP: //停止
            moveSpeed = 0;
            break;
        
        case MOVE_DIR.LEFT: //左に移動
            moveSpeed = MOVE_SPEED * -1;
            transform.localScale = new Vector2 (-1, 1);
            break;

        case MOVE_DIR.RIGHT: //右に移動
            moveSpeed = MOVE_SPEED * 1;
            transform.localScale = new Vector2(1, 1);
            break;
        }

        rbody.velocity = new Vector2( moveSpeed, rbody.velocity.y);

        //ジャンプ処理
        if ( goJump ) {
            rbody.AddForce (Vector2.up * jumpPower );
            goJump = false;
        }
    }

    //左ボタンを押した
    public void PushLeftButton () {
        Debug.Log("左");
        moveDirection = MOVE_DIR.LEFT;
    }
    public void PushRightButton () {
        Debug.Log("右");
        moveDirection = MOVE_DIR.RIGHT;
    }

    //移動ボタンを離した
    public void ReleaseMoveButton () {
        Debug.Log("停止");
        moveDirection = MOVE_DIR.STOP;
    }

    public void PushJumpButton () {
        Debug.Log("ジャンプ");
        if(canJump) {
            goJump = true;
        }
        Debug.Log(goJump);
    }

}
