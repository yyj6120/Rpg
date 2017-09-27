using UnityEngine;
namespace Rpg.Character
{
    public class CharacterAnimator : CharacterMotor
    {
        int baseLayer { get { return animator.GetLayerIndex("Base Layer"); } }
        int fullbodyLayer { get { return animator.GetLayerIndex("FullBody"); } }

        public virtual void UpdateAnimator()
        {
            if (animator == null || !animator.enabled) return;
            LayerControl();
            ActionsControl();
            LocomotionAnimation();
        }

        public void LocomotionAnimation()
        {
            animator.SetBool("IsGrounded",isGrounded);
            animator.SetBool("IsCrouching", isCrouching);
            animator.SetFloat("InputVertical", !stopMove && !lockMovement ? speed : 0f, 0.25f, Time.deltaTime);
            animator.SetFloat("GroundDistance", groundDistance);

            if (!isGrounded)
               animator.SetFloat("VerticalVelocity", verticalVelocity);
            
        }
        public void OnAnimatorMove()
        {
            if (!this.enabled) return;
            
            if (isGrounded)
            {
                transform.rotation = animator.rootRotation;

                var speedDir = new Vector2(direction, speed);
                var strafeSpeed = (isSprinting ? 1.5f : 1f) * Mathf.Clamp(speedDir.magnitude, 0f, 1f);

                if (isStrafing)
                {
                    if (strafeSpeed <= 0.5f)
                        ControlSpeed(strafeWalkSpeed);               
                    else if (strafeSpeed > 0.5f && strafeSpeed <= 1f)
                        ControlSpeed(strafeRunningSpeed);               
                    else                 
                        ControlSpeed(strafeSprintSpeed);                  
                }
                else if (!isStrafing)
                {               
                    if (speed <= 0.5f)
                        ControlSpeed(freeWalkSpeed);
                    else if (speed > 0.5 && speed <= 1f)
                        ControlSpeed(freeRunningSpeed);
                    else
                        ControlSpeed(freeSprintSpeed);

                    if (isCrouching)
                        ControlSpeed(freeCrouchSpeed);
                }
            }
        }

        public bool IsAnimatorTag(string tag)
        {
            if (animator == null)
                return false;
            if (baseLayerInfo.IsTag(tag))
                return true;
            if (fullBodyInfo.IsTag(tag))
                return true;

            return false;
        }

        public void LayerControl()
        {
            baseLayerInfo = animator.GetCurrentAnimatorStateInfo(baseLayer);
            fullBodyInfo = animator.GetCurrentAnimatorStateInfo(fullbodyLayer);
        }

        public void ActionsControl()
        {
            landHigh = baseLayerInfo.IsName("LandHigh");      
            customAction = IsAnimatorTag("CustomAction");
            AttackAction = IsAnimatorTag("Attack");
        }

        public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget target, MatchTargetWeightMask weightMask, float normalisedStartTime, float normalisedEndTime)
        {
            if (animator.isMatchingTarget || animator.IsInTransition(0))
                return;

            float normalizeTime = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);

            if (normalizeTime > normalisedEndTime)
                return;

            animator.MatchTarget(matchPosition, matchRotation, target, weightMask, normalisedStartTime, normalisedEndTime);
        }
    }
}
