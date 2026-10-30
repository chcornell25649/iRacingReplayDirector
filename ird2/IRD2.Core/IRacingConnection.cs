using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SVappsLAB.iRacingTelemetrySDK;
using Microsoft.Extensions.Logging;

namespace IRD2.Core;

[RequiredTelemetryVars([])]
public class TelemetryDataPlaceholder { }

public class DriverInfo
{
    public int CarNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
    public string Livery { get; set; } = string.Empty;
}

public class CameraGroupInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}

public enum ConnectionState
{
    Disconnected,
    Connecting,
    Connected,
    Error
}

public class IRacingConnection : IAsyncDisposable
{
    private readonly Timer _monitoringTimer;
    private bool _isMonitoring;
    private ITelemetryClient<TelemetryData>? _client;
    private ILogger _logger;
    private bool _isConnected = false;
    private CancellationTokenSource? _monitoringCts;
    private bool _disposed = false;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<ConnectionState>? ConnectionChanged;
    public event EventHandler? SessionDataChanged;

    private ObservableCollection<DriverInfo> _drivers = new();
    private ObservableCollection<CameraGroupInfo> _cameraGroups = new();
    private ConnectionState _connectionState = ConnectionState.Disconnected;
    private string _trackName = "Unknown";
    private string _sessionName = "Unknown";
    private string _sessionPhase = "Unknown";
    private string _timeRemaining = "00:00";

    public ObservableCollection<DriverInfo> Drivers { get => _drivers; set { _drivers = value; OnPropertyChanged(); } }
    public ObservableCollection<CameraGroupInfo> CameraGroups { get => _cameraGroups; set { _cameraGroups = value; OnPropertyChanged(); } }
    public ConnectionState ConnectionState { get => _connectionState; set { _connectionState = value; ConnectionChanged?.Invoke(this, value); OnPropertyChanged(); } }
    public string TrackName { get => _trackName; set { _trackName = value; OnPropertyChanged(); } }
    public string SessionName { get => _sessionName; set { _sessionName = value; OnPropertyChanged(); } }
    public string SessionPhase { get => _sessionPhase; set { _sessionPhase = value; OnPropertyChanged(); } }
    public string TimeRemaining { get => _timeRemaining; set { _timeRemaining = value; OnPropertyChanged(); } }

    public bool IsConnected { get => _isConnected; set { _isConnected = value; } }

    public IRacingConnection()
    {
        _monitoringTimer = new Timer(OnTimerElapsed, null, Timeout.Infinite, 50);
        ConnectionState = ConnectionState.Disconnected;
        _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<IRacingConnection>();
    }

    public async Task StartMonitoringAsync()
    {
        if (_isMonitoring) return;

        _isMonitoring = true;
        ConnectionState = ConnectionState.Connecting;

        try
        {
            LogActivity("Connecting to iRacing...");
            _client = TelemetryClient<TelemetryData>.Create(_logger);
            
            var handlers = new TelemetryHandlers<TelemetryData>
            {
                OnSessionInfoUpdate = session =>
                {
                    OnSessionInfoUpdate(session);
                    return Task.CompletedTask;
                },
                OnConnectStateChanged = state =>
                {
                    LogActivity($"Connection state: {state}");
                    if (state == SVappsLAB.iRacingTelemetrySDK.ConnectState.Connected)
                    {
                        _isConnected = true;
                        ConnectionState = ConnectionState.Connected;
                    }
                    else if (state == SVappsLAB.iRacingTelemetrySDK.ConnectState.Disconnected)
                    {
                        _isConnected = false;
                        ConnectionState = ConnectionState.Disconnected;
                    }
                    return Task.CompletedTask;
                },
                OnError = error =>
                {
                    LogActivity($"SDK Error: {error.Message}");
                    ConnectionState = ConnectionState.Error;
                    return Task.CompletedTask;
                }
            };
            
            _monitoringCts = new CancellationTokenSource();
            await _client.Monitor(handlers, _monitoringCts.Token);
            
            LogActivity("Monitoring completed");
        }
        catch (Exception ex)
        {
            LogActivity($"StartMonitoring error: {ex.Message}");
            ConnectionState = ConnectionState.Error;
        }
    }

    private void OnSessionInfoUpdate(TelemetrySessionInfo session)
    {
        try
        {
            if (session.WeekendInfo != null)
            {
                _trackName = session.WeekendInfo.TrackDisplayName ?? "Unknown";
                OnPropertyChanged(nameof(TrackName));
            }

            if (session.SessionInfo != null && session.SessionInfo.Sessions != null && session.SessionInfo.Sessions.Count > 0)
            {
                var currentSession = session.SessionInfo.Sessions[0];
                _sessionName = currentSession.SessionName ?? "Unknown";
                
                // Infer session phase from SessionType since SDK 2.6.0 does not have SessionPhase
                var sessionType = currentSession.SessionType ?? "";
                var sessionSubType = currentSession.SessionSubType ?? "";
                
                if (sessionType.Contains("Race", StringComparison.OrdinalIgnoreCase))
                    _sessionPhase = "Race";
                else if (sessionType.Contains("Qualifying", StringComparison.OrdinalIgnoreCase) || 
                         sessionType.Contains("Qualify", StringComparison.OrdinalIgnoreCase))
                    _sessionPhase = "Qualifying";
                else if (sessionType.Contains("Practice", StringComparison.OrdinalIgnoreCase))
                    _sessionPhase = "Practice";
                else if (sessionType.Contains("Warmup", StringComparison.OrdinalIgnoreCase))
                    _sessionPhase = "Warmup";
                else
                    _sessionPhase = sessionType;
                
                OnPropertyChanged(nameof(SessionName));
                OnPropertyChanged(nameof(SessionPhase));
                
                // SDK 2.6.0 does not have SessionTimeRemaining - use SessionTime instead
                var sessionTime = currentSession.SessionTime ?? "00:00";
                if (TimeRemaining != sessionTime)
                {
                    _timeRemaining = sessionTime;
                    OnPropertyChanged(nameof(TimeRemaining));
                }
            }

            Drivers.Clear();
            if (session.DriverInfo?.Drivers != null)
            {
                foreach (var driver in session.DriverInfo.Drivers)
                {
                    Drivers.Add(new DriverInfo
                    {
                        CarNumber = driver.CarNumberRaw,
                        Name = driver.UserName ?? "Unknown",
                        IsSelected = false,
                        Livery = driver.CarID > 0 ? $"Livery {driver.CarID}" : "Default"
                    });
                }
                LogActivity($"Loaded {Drivers.Count} drivers");
            }

            CameraGroups.Clear();
            if (session.CameraInfo?.Groups != null)
            {
                foreach (var group in session.CameraInfo.Groups)
                    CameraGroups.Add(new CameraGroupInfo { Id = group.GroupNum, Name = group.GroupName ?? "Unknown", IsSelected = false });
                LogActivity($"Loaded {CameraGroups.Count} camera groups");
            }

            SessionDataChanged?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            LogActivity($"Session info error: {ex.Message}");
        }
    }

    private void OnTimerElapsed(object? state)
    {
        try
        {
            if (ConnectionState == ConnectionState.Connected && _client != null)
            {
                SessionDataChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        catch (Exception ex)
        {
            LogActivity($"Timer error: {ex.Message}");
        }
    }

    public void CameraOnDriver(int driverIndex)
    {
        if (ConnectionState == ConnectionState.Connected && driverIndex >= 0 && driverIndex < Drivers.Count)
        {
            try
            {
                LogActivity($"Camera on driver {Drivers[driverIndex].Name}");
            }
            catch (Exception ex)
            {
                LogActivity($"Camera switch error: {ex.Message}");
            }
        }
    }

    public void SwitchCamera(string groupName, int cameraIndex = 0)
    {
        if (ConnectionState == ConnectionState.Connected)
        {
            try
            {
                LogActivity($"Camera group {groupName}, camera {cameraIndex}");
            }
            catch (Exception ex)
            {
                LogActivity($"Camera switch error: {ex.Message}");
            }
        }
    }

    public void StopMonitoring()
    {
        _isMonitoring = false;
        _monitoringTimer.Change(Timeout.Infinite, Timeout.Infinite);
        _monitoringCts?.Cancel();
        _client?.Pause();
        _client = null;
        ConnectionState = ConnectionState.Disconnected;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        try
        {
            StopMonitoring();
            if (_client != null)
                await _client.DisposeAsync().ConfigureAwait(false);
        }
        finally
        {
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    private string FormatTimeRemaining(int seconds) => TimeSpan.FromSeconds(seconds).ToString("mm'\"ss\"");

    private void LogActivity(string message)
    {
        SessionDataChanged?.Invoke(this, EventArgs.Empty);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}










