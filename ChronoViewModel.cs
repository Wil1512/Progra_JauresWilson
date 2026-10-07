using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Progra_JauresWilson
{
    public class ChronoViewModel : INotifyPropertyChanged
    {
        private readonly ChronoModel _model;
        private readonly DispatcherTimer _timer;

        private double _secondsAngle;
        public double SecondsAngle
        {
            get => _secondsAngle;
            set
            {
                if (_secondsAngle != value)
                {
                    _secondsAngle = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _timeDisplay = "00:00";
        public string TimeDisplay
        {
            get => _timeDisplay;
            set
            {
                if (_timeDisplay != value)
                {
                    _timeDisplay = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ResetCommand { get; }

        public ChronoViewModel()
        {
            _model = new ChronoModel();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            StartCommand = new RelayCommand(_ => Start(), _ => !_model.IsRunning);
            StopCommand = new RelayCommand(_ => Stop(), _ => _model.IsRunning);
            ResetCommand = new RelayCommand(_ => Reset(), _ => _model.TotalSeconds > 0 || _model.IsRunning);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _model.TotalSeconds++;
            UpdateProperties();
        }

        private void Start()
        {
            _model.IsRunning = true;
            _timer.Start();
        }

        private void Stop()
        {
            _model.IsRunning = false;
            _timer.Stop();
        }

        private void Reset()
        {
            _timer.Stop();
            _model.IsRunning = false;
            _model.TotalSeconds = 0;
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            // 360° / 60 secondes = 6° par seconde
            SecondsAngle = (_model.TotalSeconds % 60) * 6;

            TimeSpan time = TimeSpan.FromSeconds(_model.TotalSeconds);
            TimeDisplay = time.ToString(@"mm\:ss");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
