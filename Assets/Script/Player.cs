using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;//外部の処理を使いたいときに処理が入っているファイルの場所を追加する

public class Player : MonoBehaviour
{
    //変数宣言するフィールド

	//Unity上で変更できるようにするための宣言
	[SerializeField, Header("移動速度")]
	//privateはこのクラス内でしかアクセスできない変数。pablicはどこからでもアクセスできる変数。
	//floatは小数点を扱う変数
	private float _speed;//プレイヤーの移動速度を格納する変数

	[SerializeField, Header("弾オブジェクト")]
	//GameObjectはUnityのオブジェクトを扱うための型
	private GameObject _bullet;//プレイヤーが発射する弾のプレハブを格納する変数

	[SerializeField, Header("弾の発射する時間")]
	private float _shootTime;//弾を発射する間隔を格納する変数

	[SerializeField, Header("体力")]
	private int _hp;//プレイヤーの体力を格納する変数

	[SerializeField, Header("点滅時間")]
	private float _damageTime;//点滅時間を格納する変数

	[SerializeField, Header("点滅周期")]
	private float _damageCycle;//点滅周期を格納する変数

	//Vector2はxとyの二次元のベクトルを扱う型(float x, float y)
	private Vector2 _inputVelocity;//プレイヤーの移動方向を格納する変数
	
	//スクリプト上でRigidbody2Dを扱うための変数
	private Rigidbody2D _rigid;//プレイヤーの物理挙動を制御するための変数

	private SpriteRenderer _spriteRenderer;//プレイヤーのスプライトを制御するための変数
	
	private float _shootCount;//弾を発射する間隔を管理するための変数

	private float _damageTimeCount;//ダメージを受けたときの点滅を管理するための変数

	private bool _bDamage;//ダメージを受けたときの点滅を管理するための変数
	



	//クラスが行うコードを記述する場所
	//voidは返り値がない関数を宣言するためのキーワード。
	//Startメソッドはゲーム開始時に一度だけ呼び出される関数。
	void Start()
	{
		_inputVelocity = Vector2.zero;
		//<>の中のRigidbody2Dを取得して_rigidに代入するコード
		_rigid = GetComponent<Rigidbody2D>();
		_shootCount = 0;
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_damageTimeCount = 0;
		_bDamage = false;
	}

	//Updateメソッドは毎フレーム呼び出される関数。
	void Update()
	{
		_Move();
		_Shooting();
		_Damage();
	}
	
	//Moveメソッドはプレイヤーの移動を処理する関数。
	private void _Move()
	{
		//_rigid．velocityはオブジェクトに与えられる力の方向を大きさを設定するときに使う
		_rigid.linearVelocity = _inputVelocity * _speed;
	}

	private void _Shooting()
	{
		_shootCount += Time.deltaTime;//Time.deltaTimeは前のフレームからの経過時間を表す変数。これを_shootCountに加算することで、弾を発射する間隔を管理するコード。
		//if (_shootCount >= _shootTime) return;
		if (_shootCount <= _shootTime)
		{
			return;
		}//_shootCountが_shootTime以上になったら弾を発射するコード。

	//Instantiateはオブジェクトを生成する関数。引数には生成するオブジェクトを指定する。
	//gameObjectは生成されたオブジェクトを表す変数。
		GameObject bulletObj = Instantiate(_bullet);
		bulletObj.transform.position = transform.position + new Vector3(0f, transform.lossyScale.y / 2.0f, 0f);//弾の位置をプレイヤーの位置に設定するコード。
		_shootCount = 0.0f;//弾を発射した後、_shootCountをリセットするコード。
	}

	//isTriggerがオンのコライダーに他のオブジェクトが入ったときに一度だけ呼び出される関数。
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")//弾や敵に当たったときの処理をするコード。
		{

			if(!_bDamage)
			{
				_hp -= 1;//プレイヤーの体力を1減らすコード。
				_bDamage = true;
				if(_hp <= 0)
				{
					Destroy(gameObject);
				}
			}
			/*
			_hp -= 1;//プレイヤーの体力を1減らすコード。
			
			if (_hp <= 0)
			{
				Destroy(gameObject);
			}
			*/
		}
	}

	private void _Damage()
	{
		if (!_bDamage)
		{
			return;
		}

		_damageTimeCount += Time.deltaTime;
		
		float value = Mathf.Repeat(_damageTimeCount, _damageCycle);

		_spriteRenderer.enabled = value >= _damageCycle * 0.5f;//点滅の周期を制御するコード。Mathf.Repeatは第一引数を第二引数で割った余りを返す関数。これを_damageTimeCountに加算することで、点滅の周期を管理するコード。

		if (_damageTimeCount >= _damageTime)
		{
			_bDamage = false;
			_spriteRenderer.enabled = true;
			_damageTimeCount = 0;
		}
	}
	//OnMoveメソッドは入力された移動方向を処理する関数。
	public void OnMove(InputAction.CallbackContext context)//引数にはInputAction.CallbackContext型のcontextを受け取る。これは入力イベントの情報を含むオブジェクト。
	{
		//context.ReadValue<Vector2>()は入力された値をVector2型で取得するコード。
		_inputVelocity = context.ReadValue<Vector2>();//キースティック入力など取得した値を_inputVelocityに代入するコード。
	}

	//intで宣言してるのでint型の値を返す関数を宣言する
	public int GetHp()
	{
		return _hp;//プレイヤーの体力を返すコード。
	}

	public bool IsDamage()
	{
		return _bDamage;//プレイヤーがダメージを受けたときの処理をするコード。
	}


}
