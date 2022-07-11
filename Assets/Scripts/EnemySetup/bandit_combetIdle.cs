using UnityEngine;

namespace EnemySetup
{
    
    public class bandit_combetIdle : StateMachineBehaviour
    {
        public bool isAttacked = false;

        private EnemyController controller;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            controller = animator.GetComponent<EnemyController>();
            TimerManagement.setTimer(() => EnemyAttack(animator), 3f);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (controller.AleartMode)
                controller.followTarget();
            else
                controller.movement();
        }
        public void EnemyAttack(Animator animator)
        {
            animator.SetBool("Attack", true);
        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
            //animator.ResetTrigger("Attack");
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
