using UnityEngine;

public class Boss : Enemy//Enemyクラスの機能を引き継いでいる。
{
    [SerializeField, Header("移動範囲")]
    private float _limitPosY;
	[SerializeField, Header("通常攻撃回数")]
	private int _normalAttackCount;

	[SerializeField, Header("扇弾の弾数")]
	private int _ougiBulletNum;

	[SerializeField, Header("扇の角度")]
	private float _ougiAngle;

	[SerializeField, Header("扇弾の攻撃回数")]
	private int _ougiAttackCount;

	enum AttackMode
	{
		Normal,
		Ougi,
		LeftRight,
	}

	private int _currentNormalAttackCount;
	private AttackMode _attackMode;

	protected override void _Initialize()
	{
		_currentNormalAttackCount = 0;
		_attackMode = AttackMode.Normal;
	}

	protected override void _Move()//Enemyクラスの_Attack関数を上書きしている。BulletEnemyクラスの_Attack関数はEnemyクラスの_Attack関数を上書きしているため、BulletEnemyクラスの_Attack関数が呼び出される。
	{
		if(transform.position.y <= _limitPosY)
		{
			_rigid.linearVelocity = Vector2.zero;
			_bAttack = true;
			return;
		}
		base._Move();
		_bAttack = false;
	}

	protected override void _Attack()
	{
		//base._Attack();//Enemyクラスの_Attack関数を呼び出すコード。
		switch(_attackMode)
		{
			case AttackMode.Normal:
				_NormalShooting();
				break;
			case AttackMode.Ougi:
				_OugiShooting();
				break;
			case AttackMode.LeftRight:
				//_NormalShooting();
				break;
		}
	}

	private void _NormalShooting()
	{
		_shootCount += Time.deltaTime;
		if (_shootCount < _ShootTime)
		{
			return;
		}

		GameObject bulletObj = Instantiate(_bullet[0]);
		bulletObj.transform.position = transform.position;
		bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, Vector2.down);

		_shootCount = 0f;
		_currentNormalAttackCount++;

		if(_currentNormalAttackCount >= _normalAttackCount)
		{
			_attackMode = AttackMode.Ougi;
			_currentNormalAttackCount = 0;
		}
	}

	private void _OugiShooting()
	{
		_shootCount += Time.deltaTime;
		if (_shootCount < _ShootTime)
		{
			return;
		}

		for(int i = 0; i < _ougiBulletNum; i++)
		{
			float angleRange = Mathf.Deg2Rad * _ougiAngle;
			float theta = angleRange / (_ougiBulletNum - 1) * i - Mathf.Deg2Rad *(90f + _ougiAngle / 2f);
			GameObject bullet = Instantiate(_bullet[1]);
			bullet.transform.position = transform.position;
			Vector3 dir = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
			bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
		}

		_shootCount = 0f;
		_currentNormalAttackCount++;

		if(_currentNormalAttackCount >= _ougiAttackCount)
		{
			_attackMode = AttackMode.Normal;
			_currentNormalAttackCount = 0;
		}
	}

}