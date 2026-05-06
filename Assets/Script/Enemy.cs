using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*
    [SerializeField, Header("弾オブジェクト")]
    private GameObject _bullet;

    [SerializeField, Header("弾の発射する時間")]
    private float _ShootTime;

    [SerializeField, Header("体力")]
	private int _hp;//プレイヤーの体力を格納する変数

    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    private GameObject _player;
    private Rigidbody2D _rigid;
    private float _shootCount;
    private bool _bAttack;
    */

    //Protectedは継承したクラスからアクセスできる変数。Enemyクラスを継承したクラスからアクセスできるようにするために、protectedに変更する。
    [SerializeField, Header("弾オブジェクト")]
    protected GameObject[] _bullet;

    [SerializeField, Header("弾の発射する時間")]
    protected float _ShootTime;

    [SerializeField, Header("体力")]
	private int _hp;//プレイヤーの体力を格納する変数

    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    protected GameObject _player;
    protected Rigidbody2D _rigid;
    protected float _shootCount;
    protected bool _bAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(FindAnyObjectByType<Player>())//Playerがいない状態でスポーンしたときにエラーを避けるため
        {
            _player = FindAnyObjectByType<Player>().gameObject;
        }
        _rigid = GetComponent<Rigidbody2D>();
        _shootCount = 0;
        _bAttack = false;
        _Initialize();//継承先でStartの処理を追加できるようにするために、_Initialize関数を呼び出すコード。
    }

    protected virtual void _Initialize()//Enemyクラスを継承したクラスで初期化処理をするために、virtualに変更する。
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //_Shooting();
        _Move();
        _Attack();
    }
/*
    private void _Shooting()
    {

        if (!_bAttack)
        {
            return;
        }

        if (_player == null)
        {
            return;
        }

        _shootCount += Time.deltaTime;
        if (_shootCount < _ShootTime)
        {
            return;
        }

        //弾の生成
        GameObject bulletObj = Instantiate(_bullet);
        //座標を敵の位置にするコード
        bulletObj.transform.position = transform.position;
        Vector3 direction = _player.transform.position - transform.position;
        //第一引数は、生成するオブジェクトを指定するコード。第二引数は、生成されたオブジェクトの位置を指定するコード。第三引数は、生成されたオブジェクトの回転を指定するコード。
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, direction);
        _shootCount = 0.0f;
    }
*/
    //Virtualは継承したクラスで上書きできる関数。だいたい同じでも一部違うときに上書きできるようにするために、virtualに変更する。
    protected virtual void _Attack()
    {
        Debug.Log("攻撃");
        
    }

    //isTriggerがオンのコライダーに他のオブジェクトが入ったときに一度だけ呼び出される関数。
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			_hp -= 1;//プレイヤーの体力を1減らすコード。
			if (_hp <= 0)
			{
				Destroy(gameObject);
			}
		}
	}

    protected virtual void _Move()
    {
        _rigid.linearVelocity = Vector2.down * _moveSpeed;//敵の移動を制御するコード。Vector2.downは下方向を表すベクトルで、_moveSpeedは敵の移動速度を表す変数
    }

    // これだけでOK（一番下のOnWillRenderObjectを消してこれに書き換え）
    private void OnBecameVisible()
    {
        _bAttack = true;
    }

    /*
    private void OnWillRenderObject()
    {
        if (GetComponent<Camera>().current.name == "Main Camera")
        {
            _bAttack = true;
        }
    }

    */


}
