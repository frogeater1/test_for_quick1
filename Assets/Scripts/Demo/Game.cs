using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Demo
{
    public class Game : MonoSingletonBase<Game>
    {
        public Camera mainCamera;
        public InputMgr inputMgr;


        public Unit.Wizard me;
    }
}