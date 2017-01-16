#region Using Statements
using System;
using ButtonExample.Components;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
#endregion

namespace ButtonExample
{
    public class MyScene : Scene
    {
        private ButtonUI _playButtonUI;
        private Spinner _cubeSpinner;

        protected override void CreateScene()
        {
            Load(WaveContent.Scenes.MyScene);
        }

        protected override void Start()
        {
            base.Start();

            _playButtonUI = EntityManager.Find("playButton").FindComponent<ButtonUI>();
            _playButtonUI.Click += OnPlayButtonClicked;

            _cubeSpinner = EntityManager.Find("cube").FindComponent<Spinner>();
        }

        protected override void End()
        {
            base.End();

            _playButtonUI.Click -= OnPlayButtonClicked;
        }

        private void OnPlayButtonClicked(object sender, EventArgs e)
        {
            _cubeSpinner.IsActive = !_cubeSpinner.IsActive;
        }
    }
}
