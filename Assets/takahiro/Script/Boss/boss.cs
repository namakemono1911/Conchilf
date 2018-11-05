///////////////////////////////////////////////
//
//  Title   : ボス処理
//  Auther  : Shun Sakai 
//  Date    : 2018/10/17
//  Update  : ボス本体の処理
//  Memo    : タカヒロから引き継ぎ
//
///////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

// ボス処理
public class boss : MonoBehaviour {

	[System.Serializable]
	public struct BossInfo
	{
		public enemyTypeManager.EnemyTypeInfo standardInfo;	// 情報
		// 他にボスに必要な情報
	}

	// ボスパラメータ
	[SerializeField]
	private BossInfo bossInfo;                      // ボス情報
    [SerializeField]
    private Enemy_gun gun_Right;                    // 銃口(右)
    [SerializeField]
    private Enemy_gun gun_Left;                     // 銃口(左)


    // 通常メンバ
    private int                 SumDamage;          // 総合ダメージ
    private float               bossTimer;          // ボス用タイマー
    private bool                CountEnable;        // カウントフラグ


    private bossState           bossState;          // ボスステート
    private bossAnimation       bossAnimation;      // アニメーション情報
    private playerController[]  playerControllers;  // プレイヤー情報
    private enemyBullet         Bullet;             // バレット情報
    private int                 RoopNum;            // ループ数

    private bool                KusoFlug;           // 初回の例外処理用くそふらぐ

    // 前回アニメーション情報
    private bossAnimation.BOSS_ANIMATION_TYPE beforeAnimation;

    // getter
    // ボスステータス情報
    public enemyTypeManager.EnemyTypeInfo BossStatus
    {
        get { return bossInfo.standardInfo; }
    }
    // ボスステート
	public bossState state
	{
		get { return bossState; }
	}
    // ボス個体情報
	public BossInfo info
	{
		get { return bossInfo; }
	}
    // アニメーション管理
	public bossAnimation myAnimation
	{
		get { return bossAnimation; }
	}
    // 前回のアニメーション
    public bossAnimation.BOSS_ANIMATION_TYPE befor
    {
        get { return beforeAnimation; }
    }
    // タイマー
    public float timer
    {
        get { return bossTimer; }
    }
    // 敵の弾
    public enemyBullet bullet
    {
        get { return Bullet; }
    }
   
    // プレイヤー情報
	public playerController[] players
	{
		get { return playerControllers; }
	}
    // 銃(右)
    public Enemy_gun bulletInstance_Right
    {
        get { return gun_Right; }

    }
    // 銃(左)
    public Enemy_gun bulletInstance_Left
    {
        get { return gun_Left; }

    }

    /////////////////////////////////////////
    /// 基本メソッド 
    ///////////////////////////////////////// 

    // 初回処理
    private void Awake()
	{
		playerControllers = GameObject.Find("UICanvasHight").transform.GetComponentsInChildren<playerController>();
	}

	// スタート処理
	void Start () {

        // 初期化
        SumDamage   = Common.Initialize.INIT_INT;
        CountEnable = false;
        KusoFlug    = false;
        RoopNum     = 0;
        bossTimer   = 0;

        // パラメータ初期化処理
        ParamaterInit();
	}

    // フィクスド更新処理
    private void FixedUpdate()
    {


        // 初回処理のみ例外
        if(KusoFlug == true)
        {
            KusoFlug = false;
            return;
        }

        // タイプ更新
        ParamaterUpdate();

        // 向き修復
        // LookAt(Camera.main.transform.position);

        // 3ループで天井打ち
        if(RoopNum  == 3)
        {
            // 天井打ちかショックウェーブかをHPで切り替え

            RoopNum = 0;

            if (bossInfo.standardInfo.hp < 3)
            {
                ChangeState(new BossStateWaitToShockWave(this), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
            }
            else
            {
                ChangeState(new BossStateWaitToShotUp(this), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);
            }

        }


        // タイマー処理
        if (!CountEnable)
        {
            return;
        }

        bossTimer += Time.deltaTime;
    }

    /////////////////////////////////////////
    /// 内部メソッド
    ///////////////////////////////////////// 
    
    // パラメータ初期化処理
    private void ParamaterInit()
    {
        // アニメーションマネージャを取得
        bossAnimation = GetComponent<bossAnimation>();

        // バレット情報を取得
        Bullet = GetComponent<enemyBullet>();
        // 最大弾数セット
        Bullet.setMaxBullet(bossInfo.standardInfo.bulletNum);
        // リロード処理
        Bullet.reloadBullet();

        // 初期ステート（同時打ち）
        ChangeState(new BossStateWaitToDubleAtk(this), bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0);

        // ステート初期化
        bossState.initState();
    }

    // パラメータ更新処理
    private void ParamaterUpdate()
    {
        // 状態遷移
        bossState.updateState();
    }

    // プレイヤーの弾と当たったか
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "playerBullet")
        {
            bossState.hitBullet(-1, false);
        }
    }

    /////////////////////////////////////////
    /// 外部メソッド
    ///////////////////////////////////////// 

    // ステート変更
    public void ChangeState(bossState NewState, bossAnimation.BOSS_ANIMATION_TYPE befor)
    {
        // ステート無し -> 破棄
        if (bossState != null)
            Destroy(bossState);

        KusoFlug = true;

        // 前回アニメーション情報格納
        beforeAnimation = befor;

        bossState = NewState;
        bossState.initState();

    }

    // ボス情報を取得
    public BossInfo getBossInfo()
    {
        return bossInfo;
    }

    // ボス情報をセット
    public void setBossInfo(BossInfo buf)
    {
        bossInfo = buf;
    }

    // 生死判定
    public bool isDeth()
    {
        if(bossInfo.standardInfo.hp <= SumDamage)
        {
            return true;
        }

        return false;
    }

    // 方向転換(Y軸のみ)
    public void LookAt(Vector3 at)
    {
        Vector3 p = at;
        p.y = transform.position.y;
        transform.LookAt(p);
    }

    // ダメージ加算
    public void Add_Damage(int num){    SumDamage += num; }

    // タイマーリセット
    public void timerReset(){   bossTimer = 0.0f; }
    // タイマースタート
    public void timerStart(){   CountEnable = true; }
    // タイマーストップ
    public void timerStop() {   CountEnable = false; }

    public void RoopCountUp() { RoopNum += 1; }

    // 前回のモーションを再度再生
    public void changeStateBefor()
    {
        // 待機モーション
        if (beforeAnimation == bossAnimation.BOSS_ANIMATION_TYPE.ANIMATION_WAIT_0)
        {
            ChangeState(new BossStateWait(this), beforeAnimation);
        }

       
    }

    // 前に障害物があるかどうか
    public bool isFailure()
    {
        return false;
    }
}
