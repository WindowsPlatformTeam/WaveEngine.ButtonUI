using System;
using System.Runtime.Serialization;
using WaveEngine.Common.Attributes;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;

namespace ButtonExample.Components
{
    [DataContract(Namespace = "WaveEngine.Components.Gestures")]
    public class ButtonUI : Component
    {
        private static int _instances;

        private string _texturePath = null;

        private bool _backToTexturePath = false;

        private string _pressedTexturePath = null;

        public ButtonUI() : base("Buttons" + _instances++)
        {
        }

        [RequiredComponent]
        public TouchGestures TouchGestures = null;

        [RequiredComponent]
        public Sprite Sprite = null;

        [RenderPropertyAsAsset(AssetType.Texture)]
        [DataMember]
        public string PressedTexturePath
        {
            get { return _pressedTexturePath; }
            set { _pressedTexturePath = value; }
        }

        public event EventHandler Click;

        protected override void Initialize()
        {
            base.Initialize();

            TouchGestures.TouchPressed -= OnTouchGesturesTouchPressed;
            TouchGestures.TouchPressed += OnTouchGesturesTouchPressed;
            TouchGestures.TouchReleased -= OnTouchGesturesTouchReleased;
            TouchGestures.TouchReleased += OnTouchGesturesTouchReleased;

            if (Sprite != null && !string.IsNullOrEmpty(Sprite.TexturePath))
            {
                _texturePath = Sprite.TexturePath;
            }
        }

        private void OnTouchGesturesTouchReleased(object sender, GestureEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_texturePath) && _backToTexturePath)
            {
                _backToTexturePath = false;
                ChangeSpriteTexturePath(_texturePath);
            }

            if (Click != null)
            {
                Click(sender, e);
            }
        }

        private void OnTouchGesturesTouchPressed(object sender, GestureEventArgs e)
        {
            // Asking for !this.backToBackgroundImage avoids to execute the if when has been done once before 
            if (!string.IsNullOrWhiteSpace(_pressedTexturePath) && !_backToTexturePath)
            {
                ChangeSpriteTexturePath(_pressedTexturePath);
                _backToTexturePath = true;
            }
        }

        private void ChangeSpriteTexturePath(string imagePath)
        {
            if (Sprite != null)
            {
                Sprite.TexturePath = imagePath;
            }
        }
    }
}
