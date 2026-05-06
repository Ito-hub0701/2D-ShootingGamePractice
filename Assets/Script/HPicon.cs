using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HPicon : MonoBehaviour
{

    [SerializeField, Header("HPアイコンの画像")]
    private GameObject _hpIcon;

    private Player _player;

    private int _beforeHP;

    //Ｌｉｓｔ型の変数を宣言する。Listは同じ型の複数の値を格納できるデータ構造 
    private List<GameObject> hpIconList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_player = FindObjectOfType<Player>();//Playerクラスのオブジェクトを探して_playerに代入するコード。
        _player = FindAnyObjectByType<Player>();//Playerクラスのオブジェクトを探して_playerに代入するコード。
        _beforeHP = _player.GetHp();//プレイヤーの体力を_beforeHPに代入するコード。
        hpIconList = new List<GameObject>();//hpIconListを初期化するコード。
        _CreateHpIcon();//HPアイコンを生成するコード。
    }

    private void _CreateHpIcon()
    {
        for(int i = 0; i < _player.GetHp(); i++)
        {
            GameObject icon = Instantiate(_hpIcon);//_hpIconを元にアイコンを生成するコード。
            icon.transform.SetParent(transform);//生成したアイコンをHPアイコンの子オブジェクトにするコード。
            hpIconList.Add(icon);//生成したアイコンをhpIconListに追加するコード。
        }
    }

    // Update is called once per frame
    void Update()
    {
        _ShowHpIcon();//HPアイコンを表示するコード。
    }

    private void _ShowHpIcon()
    {
        if(_beforeHP == _player.GetHp())
        {
            return;//プレイヤーの体力が変わっていない場合は何もしないコード。
        }

        for(int i = 0; i < hpIconList.Count; i++)
        {
           
            hpIconList[i].SetActive(i < _player.GetHp());//プレイヤーの体力がiより大きい場合はアイコンを表示するコード。
        }
        _beforeHP = _player.GetHp();//beforeHPにプレイヤーの体力を代入するコード。
        
    }
}
