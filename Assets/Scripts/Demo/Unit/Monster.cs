using DG.Tweening;
using UnityEngine;

namespace Demo.Unit
{
    public class Monster : Unit
    {
        void Start()
        {
            Run();
        }

        private void Run()
        {
            var s = DOTween.Sequence();
            s.Append(transform.DOScaleY(0.9f, 0.3f));
            s.Append(transform.DOScaleY(1.1f, 0.3f));
            s.SetLoops(-1);
        }

        private void Attack()
        {
            var s = DOTween.Sequence();
            s.Append(transform.DOMoveX(transform.position.x, 0.3f));
            s.Append(transform.DOScaleY(1.1f, 0.3f));
            s.SetLoops(-1);
        }


        // Update is called once per frame
        void Update()
        {
            transform.localScale = Game.Instance.me.transform.position.x - transform.position.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            transform.position = Vector3.MoveTowards(transform.position, Game.Instance.me.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}