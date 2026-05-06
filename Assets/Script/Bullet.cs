using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField, Header("弾の速度")]
    private float _speed;//弾の速度を格納する変数
    [SerializeField, Header("弾の威力")]
    private int _power;//弾の威力を格納する変数

    private Rigidbody2D _rigid;//弾の物理挙動を制御するための変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();//Rigidbody2Dコンポーネントを取得して_rigidに代入するコード 
        
    }

    // Update is called once per frame
    void Update()
    {
        _Move();//弾の移動を制御するコード。
        
    }

    private void _Move()
    {
        //transform.upはローカル座標での弾の上方向を表すベクトルで、「弾」から見た座標系での上方向を指す。
        //弾の移動を制御するコード。transform.upは弾の上方向を表すベクトルで、_speedは弾の速度を表す変数。これにより、弾は自分の上方向に_speedの速さで移動することになる。
        _rigid.linearVelocity = transform.up * _speed;//弾の移動を制御するコード。
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>().IsDamage())//PlayerクラスのIsDamage関数を呼び出して、プレイヤーがダメージを受けたときの処理をするコード。
            {
                return;
            }
            else if(collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);//弾が敵に当たったときの処理をするコード。
            }
        }
    }
}
