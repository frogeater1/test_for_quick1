using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClearSky;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using Utilities;

namespace Demo.Unit
{
    public class Wizard : Unit
    {
        Animator anim;

        public Vector2 inputMovement;
        
        public bool isDie;


        void Awake()
        {
            anim = GetComponent<Animator>();
        }


        private void Update()
        {
            if (!isDie)
            {
                if (Mathf.Approximately(inputMovement.magnitude, 0))
                {
                    Idle();
                }
                else
                {
                    transform.position += new Vector3(inputMovement.x * moveSpeed * Time.deltaTime, 0, inputMovement.y * moveSpeed * Time.deltaTime);
                    transform.localScale = inputMovement.x < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
                    Run();
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!isDie)
            {
                if (other.GetComponent<Monster>())
                {
                    Die();
                    isDie = true;
                }
            }
        }


        public async UniTaskVoid Skill()
        {
            Attack();
            var mousePos = Mouse.current.position.ReadValue();
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            skillList[0].释放(mousePos);
        }


        void ResetAnimation()
        {
            anim.SetBool("isLookUp", false);
            anim.SetBool("isRun", false);
            anim.SetBool("isJump", false);
        }

        public void Idle()
        {
            ResetAnimation();
            anim.SetTrigger("idle");
        }

        public void Attack()
        {
            ResetAnimation();
            anim.SetTrigger("attack");
        }

        public void TripOver()
        {
            ResetAnimation();
            anim.SetTrigger("tripOver");
        }

        public void Hurt()
        {
            ResetAnimation();
            anim.SetTrigger("hurt");
        }

        public void Die()
        {
            ResetAnimation();
            anim.SetTrigger("die");
        }

        public void LookUp()
        {
            ResetAnimation();
            anim.SetBool("isLookUp", true);
        }

        public void Run()
        {
            ResetAnimation();
            anim.SetBool("isRun", true);
        }

        public void Jump()
        {
            ResetAnimation();
            anim.SetBool("isJump", true);
        }
    }
}