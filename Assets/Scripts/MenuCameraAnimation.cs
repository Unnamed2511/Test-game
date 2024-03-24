using UnityEngine;
using DG.Tweening;

public class MenuCameraAnimation : MonoBehaviour
{
    private void Start()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(18, 60).SetEase(Ease.OutSine));
        sequence.Append(transform.DOMoveY(-9, 60).SetEase(Ease.InCubic));
        sequence.Append(transform.DOMoveY(0, 60).SetEase(Ease.OutSine));
    }
}
