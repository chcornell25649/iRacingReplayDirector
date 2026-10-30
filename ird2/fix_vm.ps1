`$content = Get-Content "c:\Users\chcor\Iracing Replay Director Github\iRacingReplayDirector\ird2\IRD2.UI\MainWindowViewModel.cs" -Raw
`$content = `$$content.Replace("LogActivity(\\\"MainWindowViewModel constructor called\\\")", 'LogActivity("MainWindowViewModel constructor called")')
Set-Content "c:\Users\chcor\Iracing Replay Director Github\iRacingReplayDirector\ird2\IRD2.UI\MainWindowViewModel.cs" `$$content
