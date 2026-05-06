using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField, Header("敵オブジェクト")]
    //GameObject[]は配列を扱うための型。
    private GameObject[] _enemy;//複数の敵のプレハブを格納するための変数

    [SerializeField, Header("敵の生成する時間")]
    private float[] _spawnTime;

    private float _spawnCount;
    private int _spawnNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnCount = 0;
        _spawnNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        _Spawn();
    }

    private void _Spawn()
    {
        //敵の生成を制御するコード。_spawnNumが_enemyの長さより大きい場合は何もしないコード。
        if (_spawnNum > _enemy.Length - 1)
        {
            return;
        }
        _spawnCount += Time.deltaTime;
        if (_spawnCount >= _spawnTime[_spawnNum])
        {
            Instantiate(_enemy[_spawnNum]);//敵の生成を制御するコード。第一引数は、生成するオブジェクトを指定するコード。第二引数は、生成されたオブジェクトの位置を指定するコード。第三引数は、生成されたオブジェクトの回転を指定するコード。
            _spawnCount = 0.0f;
            _spawnNum++;
        }
    }
}
