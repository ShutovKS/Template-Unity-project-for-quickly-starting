﻿using ProjectStateMachine.Base;
using Services.AssetsAddressables;
using Services.WindowsService;
using UI.MainMenuScreen.Scripts;
using UnityEngine.AddressableAssets;

namespace ProjectStateMachine.States
{
    public class GameMainMenuState : IState<GameBootstrap>, IEnterable, IExitable
    {
        private readonly IWindowService _windowService;
        public GameBootstrap Initializer { get; }

        public GameMainMenuState(GameBootstrap initializer,
            IWindowService windowService)
        {
            _windowService = windowService;
            Initializer = initializer;
        }

        public void OnEnter()
        {
            var asyncOperation = Addressables.LoadSceneAsync(AssetsAddressableConstants.MAIN_MENU_SCENE);

            asyncOperation.Completed += _ => { OpenMainMenuWindow(); };
        }

        private async void OpenMainMenuWindow()
        {
            var mainMenuScreen = await _windowService.OpenAndGetComponent<MainMenuScreen>(WindowID.MainMenu);

            mainMenuScreen.OnStartGameButtonClicked += OnStartGameButtonClicked;
            mainMenuScreen.OnTestButtonClicked += OnTestButtonClicked;

            CloseLoadingWindow();
        }

        private void OnTestButtonClicked()
        {
            Initializer.StateMachine.SwitchState<GameplayState, bool>(true);
        }

        private void OnStartGameButtonClicked()
        {
            Initializer.StateMachine.SwitchState<GameplayState, bool>(false);
        }

        private void CloseLoadingWindow()
        {
            _windowService.Close(WindowID.Loading);
        }

        private void CloseMainMenuWindow()
        {
            _windowService.Close(WindowID.MainMenu);

            _windowService.Open(WindowID.Loading);
        }

        public void OnExit()
        {
            CloseMainMenuWindow();
        }
    }
}