using System.Collections.Generic;
using UnityEngine;

enum UIStates
{
    main,
    pause, 
    gameOver
}

public class UIStateFactory
{
    Dictionary<UIStates, UIBaseState> _states = new Dictionary<UIStates, UIBaseState>();
    UIStateMachine _context;

    public UIStateFactory(UIStateMachine currentContext)
    {
        _context = currentContext;
        _states[UIStates.main] = new MainState(_context, this);
        _states[UIStates.pause] = new PauseState(_context, this);
        _states[UIStates.gameOver] = new GameOverState(_context, this);
    }

    public UIBaseState Idle() { return _states[UIStates.main]; }
    public UIBaseState Walk() { return _states[UIStates.pause]; }
    public UIBaseState Run() { return _states[UIStates.gameOver]; }
}
