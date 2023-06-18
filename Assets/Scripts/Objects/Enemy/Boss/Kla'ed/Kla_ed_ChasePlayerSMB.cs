using Enemy;
using UnityEngine;

public class Kla_ed_ChasePlayerSMB : AbsChasePlayerSMB
{
    protected override int GetNumberOfAttacks() => MiniBossKla_edCtrl.Instance.GetNumberOfAttacksChaseBehaviour();
    
    protected override float GetSpeedChase() => MiniBossKla_edCtrl.Instance.GetSpeedChase();

    protected override void SetShipModelAnimationChase(bool isOn) 
        => MiniBossKla_edCtrl.Instance.MinibossKlaEdModelShipAnimation.SetIsChasePlayer(isOn);

    protected override void UnSetAnimation() => MiniBossKla_edCtrl.Instance.SetIsChasePlayer(false);

    protected override Vector3 GetCameraPosition() => MiniBossKla_edCtrl.Instance.GetCameraPosition();
}
