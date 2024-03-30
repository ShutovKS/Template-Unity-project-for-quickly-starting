﻿using ProjectStateMachine.Base;
using Services.WindowsService;

namespace ProjectStateMachine.States
{
    public class ResourcesLoadingState : IState<GameBootstrap>, IEnterable
    {
        private readonly IWindowService windowService;
        public GameBootstrap Initializer { get; }

        public ResourcesLoadingState(GameBootstrap initializer,
            IWindowService windowService)
        {
            this.windowService = windowService;
            Initializer = initializer;
        }

        public async void OnEnter()
        {
            await windowService.Open(WindowID.Loading);

            Initializer.StateMachine.SwitchState<GameMainMenuState>();
        }

        private void LoadResources()
        {
        }
    }
}