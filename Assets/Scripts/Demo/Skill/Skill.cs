using UnityEditor;
using UnityEngine;
using Tools = Utilities.Tools;

namespace Demo.Skill
{
    public abstract class Skill : MonoBehaviour
    {
        public Bullet.Bullet bulletPrefab;

        public virtual void Load(cfg.Skill cfg)
        {
        }

        public virtual void 释放(Vector2 mousePos)
        {
            if (Physics.Raycast(Game.Instance.mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, 1000, LayerMask.GetMask("Ground")))
            {
                transform.forward = hit.point - transform.position;
            }
            //todo
        }
    }
}