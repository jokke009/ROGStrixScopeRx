using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Model
{
    public abstract class BaseReoprter
    {
        private const int BufferSize = 5;
        private float _previousRawValue;

        private bool _hasUpdate;

        public byte KeyBinding { get; set; }


        protected readonly Queue<float> _history = new Queue<float>(BufferSize);
        public float RawValue
        {
            get => _previousRawValue;
            set
            {
                if (value != _previousRawValue)
                {
                    _previousRawValue = value;
                    _hasUpdate = true;
                }
            }
        }
        public bool HasUpdate
        {
            get
            {
                bool result = _hasUpdate;
                _hasUpdate = false;
                return result;
            }
        }
        private void UpdateAverage()
        {
            _history.Enqueue(RawValue);
            if (_history.Count > BufferSize)
            {
                _history.Dequeue();
            }
        }
        public float AverageValue
        {
            get
            {
                if (_history.Count == 0)
                {
                    return 0; // Return default value if buffer is empty
                }
                return _history.Sum() / _history.Count;
            }
        }
        public virtual byte Get8BitValue()
        {
            if (RawValue >= 1) return 0;
            byte val = (byte)(RawValue * 255);
            return val;
        }

        public virtual Color GetVuColor()
        {
            // Interpolate colors: Red (1, 0, 0) -> Green (0, 1, 0) -> Blue (0, 0, 1)
            try
            {

                // Normalize value to range [0, 1]
                float normalizedValue = RawValue / 100f;

                // Interpolate colors: Green (0, 255, 0) -> Red (255, 0, 0) -> Blue (0, 0, 255)
                if (normalizedValue < 0.5f)
                {
                    // Transition from Green to Red
                    float t = normalizedValue / 0.5f; // Normalize to range [0, 1] within this segment
                    return Color.FromArgb((int)(255 * t), 255 - (int)(255 * t), 0);
                }
                else
                {
                    // Transition from Red to Blue
                    float t = (normalizedValue - 0.5f) / 0.5f; // Normalize to range [0, 1] within this segment
                    return Color.FromArgb(255 - (int)(255 * t), 0, (int)(255 * t));
                }
            }
            catch (Exception e)
            {
                {
                    Console.WriteLine(e.ToString());
                }
            }
            finally
            {
                
            }
            return Color.White;
        }
    }
}