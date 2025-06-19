using System;
using System.Collections.Generic;

namespace JinToliq.SimpleTween
{
  public readonly struct CompositeTweener
  {
    private readonly IReadOnlyList<Tweener> _tweeners;
    private readonly Tweener _longest;

    public bool IsEmpty => _tweeners is null || _tweeners.Count == 0;
    public float Duration => IsEmpty ? 0 : _longest.Duration;
    public bool IsAnyPlaying => !IsEmpty && _longest.IsPlaying;

    public CompositeTweener(IReadOnlyList<Tweener> tweeners)
    {
      _tweeners = tweeners;
      _longest = null;
      if (IsEmpty)
        return;

      _longest = tweeners[0];
      for (var i = 1; i < _tweeners.Count; i++)
      {
        var tweener = _tweeners[i];
        if (tweener.Duration > _longest.Duration)
          _longest = _tweeners[i];
      }
    }

    public float Play(Action onComplete, float speed = 1f)
    {
      if (IsEmpty)
      {
        onComplete?.Invoke();
        return 0;
      }

      foreach (var item in _tweeners)
      {
        if (item == _longest)
          item.Play(speed, onComplete);
        else
          item.Play(speed);
      }

      return _longest.Duration;
    }

    public float PlayReverse(Action onComplete, float speed = 1f)
    {
      if (IsEmpty)
      {
        onComplete?.Invoke();
        return 0;
      }

      foreach (var item in _tweeners)
      {
        if (item == _longest)
          item.PlayReverse(speed, onComplete);
        else
          item.PlayReverse(speed);
      }

      return _longest.Duration;
    }

    public void SetTweenPosition(float time)
    {
      if (time is < 0 or > 1)
        throw new ArgumentOutOfRangeException(nameof(time));

      if (IsEmpty)
        return;

      foreach (var tweener in _tweeners)
      {
        if (tweener.IsPlaying)
          Stop(false);

        tweener.SetTweenPosition(time);
      }
    }

    public void Stop(bool reset)
    {
      if (IsEmpty)
        return;

      foreach (var tweener in _tweeners)
        tweener.Stop(reset);
    }
  }
}