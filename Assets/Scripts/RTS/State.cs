using UnityEngine;

namespace RTS{
    public abstract class State{
        protected Entity _context;

        protected State(Entity context){
            _context = context;
        }

        /// <summary>
        ///     Start is called once before the first update
        /// </summary>
        public abstract void Start();

        /// <summary>
        ///     Update is called every frame
        /// </summary>
        public abstract void Update();

        /// <summary>
        ///     Update is called once before the next state's Start
        /// </summary>
        public abstract void Quit();

        /// <summary>
        ///     This should be called alongside/before Update
        /// </summary>
        public abstract void CheckTransitions();

        protected bool CheckPlayerInputs(){
            if (!_context.IsPlayer) return false;
            if (!Input.GetKeyDown(KeyCode.H) && !Input.GetMouseButtonDown(0)) return false;
            if (Input.GetKeyDown(KeyCode.H))
                _context.CurrentState = new Hold(_context);
            else if (Input.GetMouseButtonDown(0))
                _context.CurrentState = new Move(_context);
            return true;
        }
    }
}