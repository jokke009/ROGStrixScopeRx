using System;
using System.Collections.Generic;
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
            if(RawValue >= 1) return 0;
            byte val = (byte)(RawValue * 255);
            return val;
        }

    }
}
