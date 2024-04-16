using System;
using UnityEngine;

namespace Demo.Bullet
{
    public class Bullet : MonoBehaviour
    {
        public float moveSpeed;
        public Vector3 target;

        public virtual void Load(cfg.Bullet cfg)
        {
            moveSpeed = cfg.moveSpeed;
        }

        public virtual void 发射(Vector3 targetPos)
        {
            target = targetPos;
        }

        public void Update()
        {
            if (transform.position.Equals(target))
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            }
        }
    }
}