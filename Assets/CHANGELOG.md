## [1.0.0] 2024-07-05
### First Release
- Added base tweener implementations:
  - Tweener and TargetTweener - tween by elapsed time;
  - AnimationCurveTweener - tweens using AnimationCurve calculated by elapsed time;
- Added a number of basic tweeners including: 
  - CanvasGroupTweener - to animate canvas transparency;
  - ColorTweener - to animate Graphics' color;
  - SpriteRendererColorTweener - to animate SpriteRenderer's color;
  - LocalPositionTweener - to animate Transform's local position;
  - LocalRotationTweener - to animate Transform's local rotation;
  - ScaleTweener - to animate Transform's local scale.
## [1.0.1] 2024-07-05
### Updated manifest
## [1.1.0] 2025-02-15
### Added option to specify if tween should be performed using DeltaTime or Unscaled DeltaTime. Also added MultipleSpriteRendererColorTweener
## [1.2.0] 2025-03-13
### Added CompositeTweener wrapper to allow multiple Tweeners to be played simultaneously more easily
## [1.3.0] 2025-06-14
### Added speed parameter for OnDemand Tweeners. Added Stop to CompositeTweener
## [1.4.0] 2025-06-18
### Added Duration property to composite tweeners
## [1.5.0] 2025-06-19
### Added ability to set position for CompositeTweener and IsPlaying property for Tweeners and CompositeTweener
## [1.5.1] 2025-10-20
### Fixed IsPlaying property reset