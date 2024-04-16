using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo
{
    public class InputMgr : MonoBehaviour
    {
        public void OnMove(InputValue value)
        {
            Game.Instance.me.inputMovement = value.Get<Vector2>();
        }

        public void OnAttack(InputValue value)
        {
            Game.Instance.me.Skill().Forget();
        }
    }
}