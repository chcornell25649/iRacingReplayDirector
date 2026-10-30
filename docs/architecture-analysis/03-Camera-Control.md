# 03-Camera-Control

## Overview

Camera control is a core feature of the iRacing Replay Director application. It automatically switches cameras during races based on racing events, incidents, and predefined track camera configurations.

## Camera Configuration

### TrackCameras System
The application uses a sophisticated camera configuration system:

**Key Files:**
- `TrackCameras.cs` - Core camera configuration classes
- `ConfigureTrackCameras.cs` - UI for configuring camera settings

**TrackCamera Structure:**
- `TrackName` - Name of the track
- `CameraName` - Name of the camera
- `Ratio` - Selection probability ratio for camera switching
- `CameraNumber` - Actual camera number used in iRacing
- `IsRaceStart` - Camera used at race start
- `IsIncident` - Camera used for incidents
- `IsLastLap` - Camera used for last lap

### Camera Selection Rules
Cameras are configured with specific roles for different racing situations:
- **Race Start** - Initial camera position
- **Incident** - Cameras for racing incidents
- **Last Lap** - Cameras for final lap coverage

## Camera Control Components

### CameraControl Class
Located in `Phases/Direction/CameraControl.cs`, this class handles all camera switching logic:

**Key Methods:**
- `FindACamera()` - Selects camera based on camera angle and ratios
- `CameraOnDriver()` - Switches camera to specific driver
- `CameraOnPosition()` - Switches camera to specific position
- `ReplayCameraControlTask()` - Async task for replay camera switching

### Camera Control Process
1. **Initialization** - Sets up camera groups from session data
2. **Camera Selection** - Chooses appropriate camera based on racing state
3. **Camera Switching** - Sends commands to iRacing SDK
4. **Verification** - (Implied) Camera switching occurs

## Camera Group Discovery

The application discovers available camera groups through iRacing SDK:

**Process:**
1. Get `CameraInfo.Groups` from session data
2. Map camera names to group numbers
3. Configure track-specific cameras
4. Assign camera numbers to track cameras

### Example Camera Group Mapping
```csharp
// From ReplayControl.cs
foreach (var tc in cameras)
    tc.CameraNumber = (short)sessionData.CameraInfo.Groups.First(g => g.GroupName.ToLower() == tc.CameraName.ToLower()).GroupNum;
```

## Driver Selection for Camera Control

### Driver-Based Camera Switching
The system supports switching cameras to specific drivers:

**Methods Used:**
- `CameraOnDriver(carNumber, group, camera)` - Switch to specific driver
- `CameraOnPosition(position, group, camera)` - Switch to specific position

### Camera Selection Logic
The `FindACamera()` method implements weighted random selection:

1. **Camera Angle Filtering** - Filters cameras by desired angle
2. **Ratio Weighting** - Uses configured ratios for probability selection
3. **Adjustment Handling** - Applies adjustments for camera preferences

## Camera Switching Implementation

### Camera Switching Process
1. **Event Detection** - Incidents, battles, or time periods trigger camera change
2. **Camera Selection** - Chooses appropriate camera based on current situation
3. **Command Execution** - Sends camera change to iRacing SDK
4. **Timing Control** - Manages camera duration and transitions

### Camera Switching Methods
**In CameraControl.cs:**
```csharp
public void CameraOnDriver(short carNumber, short group, short camera = 0)
{
    iRacing.Replay.CameraOnDriver(carNumber, group, camera);
}

public void CameraOnPositon(short carPosition, short group, short camera = 0)
{
    iRacing.Replay.CameraOnPositon(carPosition, group, camera);
}
```

## Camera Control Architecture

### Key Components
1. **CameraControl** - Main camera switching logic
2. **ReplayControl** - Direction rules that determine when to switch
3. **TrackCameras** - Camera configuration storage
4. **IRacingReplay** - Overall replay processing workflow

### Architecture Flow
1. **Session Initialization** - Camera groups configured from session data
2. **Direction Rules Engine** - Determines when to change cameras
3. **Camera Selection** - Chooses appropriate camera based on rules
4. **Command Execution** - Sends commands to iRacing SDK

## Potential Camera Switching Issues

### Common Problems
1. **Camera Unavailability** - Cameras not available in current session
2. **Configuration Mismatch** - Track cameras not configured for specific track
3. **Timing Issues** - Camera changes too frequent or infrequent
4. **SDK Limitations** - iRacing SDK restrictions on camera switching

### Error Handling
The system relies on iRacing SDK's built-in error handling, with no explicit verification of camera changes in the provided code.

## Camera Control Limitations

### Known Limitations
- **Limited Camera Verification** - No confirmation that cameras actually switched
- **No Camera Priority System** - All cameras have equal probability based on ratios
- **No Fallback Mechanisms** - No alternative camera selection if primary fails
- **No Camera State Tracking** - No persistent tracking of camera state

### Missing Features
- Camera change confirmation
- Camera state persistence
- Advanced camera priority rules
- Dynamic camera adjustment based on camera usage