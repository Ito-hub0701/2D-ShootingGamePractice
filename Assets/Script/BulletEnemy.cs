using UnityEngine;

public class BulletEnemy : Enemy//Enemyクラスを継承しているBulletEnemyクラス。Enemyクラスの機能を引き継いでいる。
{
    //overrideは継承したクラスで上書きする関数。
    protected override void _Attack()//Enemyクラスの_Attack関数を上書きしている。BulletEnemyクラスの_Attack関数はEnemyクラスの_Attack関数を上書きしているため、BulletEnemyクラスの_Attack関数が呼び出される。
    {
        //base._Attack();//Enemyクラスの_Attack関数を呼び出すコード。
        _Shooting();
    }

//継承してるので、変数や関数はEnemyクラスのものを使える。
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
        GameObject bulletObj = Instantiate(_bullet[0]);
        //座標を敵の位置にするコード
        bulletObj.transform.position = transform.position;
        Vector3 direction = _player.transform.position - transform.position;
        //第一引数は、生成するオブジェクトを指定するコード。第二引数は、生成されたオブジェクトの位置を指定するコード。第三引数は、生成されたオブジェクトの回転を指定するコード。
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, direction);
        _shootCount = 0.0f;
    }
}