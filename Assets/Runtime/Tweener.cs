using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace JinToliq.SimpleTween
{
  public enum PlayMode
  {
    ByDemand = 0,
    OnStart = 1,
    OnEnable = 2,
  }

  public enum UpdateMode
  {
    DeltaTime = 0,
    UnscaledDeltaTime = 1,
  }

  public abstract class Tweener : MonoBehaviour
  {
    public event Action Complete;
    public event Action ReverseComplete;

    [SerializeField] private PlayMode _mode;
    [SerializeField] private UpdateMode _update;
    [SerializeField] private bool _resetOnDisable;
    [SerializeField] private bool _loop;
    public float Duration;
    private Coroutine _routine;

    public bool IsPlaying => _routine != null;

    protected virtual void Start()
    {
      if (_mode == PlayMode.OnStart)
        Play();
    }

    protected virtual void OnEnable()
    {
      if (_mode == PlayMode.OnEnable)
        Play();
    }

    protected virtual void OnDisable()
    {
      if (_resetOnDisable)
        Tween(0);
    }

    public void SetTweenPosition(float value) => Tween(value);

    public void Play(float speed = 1f, Action onComplete = null)
    {
      if (_routine != null)
        StopCoroutine(_routine);

      _routine = StartCoroutine(Routine(speed, false, Complete, onComplete));
    }

    public void PlayReverse(float speed = 1f, Action onComplete = null)
    {
      if (_routine != null)
        StopCoroutine(_routine);

      _routine = StartCoroutine(Routine(speed, true, ReverseComplete, onComplete));
    }

    public void Stop(bool reset)
    {
      if (_routine != null)
        StopCoroutine(_routine);

      if (reset)
        SetTweenPosition(0);
    }

    protected virtual void OnBeforePlay() {}

    protected abstract void Tween(float factor);

    private IEnumerator Routine(float speed, bool isReverse, Action onCompleteEvent = null, Action onComplete = null)
    {
      OnBeforePlay();
      var realDuration = Duration / speed;
      foreach (var time in isReverse ? CoroutineTimeline.DoLinearReverse(realDuration) : CoroutineTimeline.DoLinear(realDuration))
      {
        Tween(time);
        yield return null;
      }

      onCompleteEvent?.Invoke();
      onComplete?.Invoke();

      if (_loop)
      {
        yield return null;
        _routine = StartCoroutine(Routine(speed, isReverse, onCompleteEvent, onComplete));
      }
      else
      {
        _routine = null;
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float GetDeltaTime() => _update is UpdateMode.DeltaTime ? Time.deltaTime : Time.unscaledDeltaTime;
  }
}