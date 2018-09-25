﻿///////////////////////////////////////////////
//
//  Title   : キャンバスのフェード処理
//  Auther  : Shun Sakai 
//  Date    : 2018/08/16
//  Update  : リファクタリング
//  Memo    : フェード処理用のクラス
//
///////////////////////////////////////////////
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasGroup))]

// キャンバスフェード
public class CanvasFader : MonoBehaviour
{

    //フェード用のキャンバスとそのアルファ値
    private CanvasGroup _canvasGroupEntity;
    private CanvasGroup _canvasGroup
    {
        get
        {
            if (_canvasGroupEntity == null)
            {
                _canvasGroupEntity = GetComponent<CanvasGroup>();
                if (_canvasGroupEntity == null)
                {
                    _canvasGroupEntity = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return _canvasGroupEntity;
        }
    }
    public float Alpha
    {
        get
        {
            return _canvasGroup.alpha;
        }
        set
        {
            _canvasGroup.alpha = value;
        }
    }

    //フェードの状態
    private enum FadeState
    {
        None, FadeIn, FadeOut
    }
    private FadeState _fadeState = FadeState.None;

    //フェードしているか
    public bool IsFading
    {
        get { return _fadeState != FadeState.None; }
    }

    //フェード時間
    [SerializeField]
    private float _duration;
    public float Duration { get { return _duration; } }

    //タイムスケールを無視するか
    [SerializeField]
    private bool _ignoreTimeScale = true;

    //フェード終了後のコールバック
    private event Action _onFinished = null;

    //=================================================================================
    //更新
    //=================================================================================

    private void Update()
    {
        if (!IsFading)
        {
            return;
        }

        float fadeSpeed = 1f / _duration;
        if (_ignoreTimeScale)
        {
            fadeSpeed *= Time.unscaledDeltaTime;
        }
        else
        {
            fadeSpeed *= Time.deltaTime;
        }

        Alpha += fadeSpeed * (_fadeState == FadeState.FadeIn ? 1f : -1f);

        //フェード終了判定
        if (Alpha > 0 && Alpha < 1)
        {
            return;
        }

        _fadeState = FadeState.None;
        this.enabled = false;

        if (_onFinished != null)
        {
            _onFinished();
        }
    }

    //=================================================================================
    //開始
    //=================================================================================

    // オブジェクトのフェード開始
    public static void Begin(GameObject target, bool isFadeOut, float duration)
    {
        CanvasFader canvasFader = target.GetComponent<CanvasFader>();
        if (canvasFader == null)
        {
            canvasFader = target.AddComponent<CanvasFader>();
        }
        canvasFader.enabled = true;


        canvasFader.Play(isFadeOut, duration);
    }

    // 再生処理
    public void Play(bool isFadeOut, float duration, bool ignoreTimeScale = true, Action onFinished = null)
    {
        this.enabled = true;

        _ignoreTimeScale = ignoreTimeScale;
        _onFinished = onFinished;

        Alpha = isFadeOut ? 1 : 0;
        _fadeState = isFadeOut ? FadeState.FadeOut : FadeState.FadeIn;

        _duration = duration;
    }

    //=================================================================================
    //停止
    //=================================================================================

    // フェード停止処理
    public void Stop()
    {
        _fadeState = FadeState.None;
        this.enabled = false;
    }

}


