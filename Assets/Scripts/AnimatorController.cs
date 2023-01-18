using UnityEngine;


    public class AnimatorController
    {
        private string _idle = "Idle";
        private string _run = "Run";
        private string _attack = "Attack";
        private string _dead = "isDead";
        private string _jump = "Jump";
        private string _hurt = "Hurt";
        private Animator _animator;
        public AnimatorController(Animator animator)
        {
            _animator = animator;
        }

        public void OnRun(bool value)
        {
            _animator.SetBool(_run, value);
        }

        public void OnJump()
        {
            _animator.SetTrigger(_jump);
        }

        public void OnDie(bool value)
        {
            _animator.SetBool(_dead, value);
        }

        public void OnHurt()
        {
            _animator.SetTrigger(_hurt);
        }

        public void OnAttack()
        {
            _animator.SetTrigger(_attack);
        }

    }
