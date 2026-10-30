using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using IRD2.Core;

namespace IRD2.UI;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute();
    }

    public void Execute(object? parameter)
    {
        _execute();
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly IRacingConnection _connection;
    private string _selectedDriverName = string.Empty;
    private string _selectedCameraGroupName = string.Empty;
    private string _connectionStatus = "Disconnected";
    private string _trackName = "Unknown";
    private string _sessionName = "Unknown";
    private string _sessionPhase = "Unknown";
    private string _timeRemaining = "00:00";
    private string _activityLog = string.Empty;
    private bool _isConnected = false;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<string> DriverNames { get; } = new();
    public ObservableCollection<string> CameraGroupNames { get; } = new();
    public string SelectedDriverName { get => _selectedDriverName; set { _selectedDriverName = value; OnPropertyChanged(); SwitchToSelectedDriverCommand?.Execute(null); } }
    public string SelectedCameraGroupName { get => _selectedCameraGroupName; set { _selectedCameraGroupName = value; OnPropertyChanged(); SwitchToSelectedCameraCommand?.Execute(null); } }
    public string ConnectionStatus { get => _connectionStatus; set { _connectionStatus = value; OnPropertyChanged(); } }
    public string TrackName { get => _trackName; set { _trackName = value; OnPropertyChanged(); } }
    public string SessionName { get => _sessionName; set { _sessionName = value; OnPropertyChanged(); } }
    public string SessionPhase { get => _sessionPhase; set { _sessionPhase = value; OnPropertyChanged(); } }
    public string TimeRemaining { get => _timeRemaining; set { _timeRemaining = value; OnPropertyChanged(); } }
    public string ActivityLog { get => _activityLog; set { _activityLog = value; OnPropertyChanged(); } }
    public ICommand SwitchToSelectedDriverCommand { get; }
    public ICommand SwitchToSelectedCameraCommand { get; }

    public MainWindowViewModel()
    {
        _connection = new IRacingConnection();
        _connection.ConnectionChanged += (sender, state) => OnConnectionChanged(state);
        _connection.SessionDataChanged += (sender, args) => OnSessionDataChanged();
        
        SwitchToSelectedDriverCommand = new RelayCommand(() => SwitchToSelectedDriver(), () => !string.IsNullOrEmpty(SelectedDriverName) && _isConnected);
        SwitchToSelectedCameraCommand = new RelayCommand(() => SwitchToSelectedCamera(), () => !string.IsNullOrEmpty(SelectedCameraGroupName) && _isConnected);
        
        LogActivity("MainWindowViewModel constructor called");
        LogActivity($"Connection state: {_connection.ConnectionState}");
        
        _ = StartMonitoringAsync();
    }

    private async Task StartMonitoringAsync()
    {
        await _connection.StartMonitoringAsync();
        if (_connection.ConnectionState == ConnectionState.Connected)
        {
            LogActivity("Connected to iRacing");
        }
    }

    private void OnConnectionChanged(ConnectionState state)
    {
        _isConnected = state == ConnectionState.Connected;
        ConnectionStatus = state.ToString();
        LogActivity($"Connection state: {state}");
        
        SwitchToSelectedDriverCommand?.Execute(null);
        SwitchToSelectedCameraCommand?.Execute(null);
    }

    private void OnSessionDataChanged()
    {
        TrackName = _connection.TrackName;
        SessionName = _connection.SessionName;
        SessionPhase = _connection.SessionPhase;
        TimeRemaining = _connection.TimeRemaining;
        
        DriverNames.Clear();
        foreach (var driver in _connection.Drivers)
        {
            DriverNames.Add($"{driver.CarNumber} {driver.Name}");
        }
        
        CameraGroupNames.Clear();
        foreach (var group in _connection.CameraGroups)
        {
            CameraGroupNames.Add(group.Name);
        }
        
        LogActivity($"Session updated: {TrackName} - {SessionName}");
    }

    public void SwitchToSelectedDriver()
    {
        if (!string.IsNullOrEmpty(SelectedDriverName) && _isConnected)
        {
            var driver = _connection.Drivers.FirstOrDefault(d => d.Name.Contains(SelectedDriverName.Split(" ")[1]));
            if (driver != null)
            {
                var driverIndex = _connection.Drivers.IndexOf(driver);
                _connection.CameraOnDriver(driverIndex);
            }
        }
    }

    public void SwitchToSelectedCamera()
    {
        if (!string.IsNullOrEmpty(SelectedCameraGroupName) && _isConnected)
        {
            _connection.SwitchCamera(SelectedCameraGroupName);
        }
    }

    private void LogActivity(string message)
    {
        ActivityLog = $"{DateTime.Now:HH:mm:ss} - {message}\n{ActivityLog}";
    }

    public async Task CleanupAsync()
    {
        _connection.StopMonitoring();
        await _connection.DisposeAsync();
    }
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}