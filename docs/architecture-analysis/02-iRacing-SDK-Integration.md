# 02-iRacing-SDK-Integration

## Overview

The iRacing Replay Director application integrates with the official iRacing SDK to monitor race sessions, capture telemetry data, and control camera settings during replay sessions. This integration is fundamental to the application's core functionality.

## SDK Connection Mechanism

### Connection Setup
The application uses the `iRacingEvents` class to establish communication with the iRacing SDK:

**Key Files:**
- `Main.cs` - Main application class that manages iRacing events
- `iRacingEvents.cs` - SDK event handling (not directly found, but referenced in Main.cs)

### Connection Lifecycle
1. **Initialization** - Application starts and creates `iRacingEvents` instance
2. **Event Subscription** - Subscribes to SDK events:
   - `NewSessionData` - When new session data is available
   - `Connected` - When connected to iRacing
   - `Disconnected` - When disconnected from iRacing
3. **Start Listening** - Begins monitoring for iRacing sessions
4. **Session Monitoring** - Continuously watches for session state changes

### Session Data Access

The application receives comprehensive session data through the iRacing SDK:

**SessionData Properties:**
- `WeekendInfo` - Track information, session type, etc.
- `CameraInfo` - Available camera groups and positions
- `DriverInfo` - Driver information including car numbers and positions
- `Telemetry` - Real-time racing data

**Methods Used:**
- `iRacing.Replay.CameraOnDriver()` - Switch camera to specific driver
- `iRacing.Replay.CameraOnPosition()` - Switch camera to specific position
- `iRacing.Replay.SetCameraGroup()` - Set camera group settings

## Telemetry Reception and Processing

### Data Flow
1. **Real-time Telemetry** - SDK continuously streams race telemetry
2. **Data Parsing** - Application parses telemetry data for racing events
3. **Event Detection** - Identifies incidents, battles, and key moments
4. **Camera Control** - Uses telemetry to drive camera switching decisions

### Key Telemetry Variables Used
- **Car positions** - Driver positioning for battle detection
- **Speed and acceleration** - For incident detection
- **Lap times** - For pace lap detection
- **Race state** - Restart, caution periods, etc.

## Session Detection

### Replay vs Live Detection
The application determines if iRacing is in replay mode through session data:

**Detection Logic:**
- Check `SessionData.WeekendInfo.SessionType` to identify replay sessions
- Verify session is in "replay" mode rather than live racing
- Monitor for valid replay data streams

### Session Data Access Points
- `curSession.WeekendInfo.TrackDisplayName` - Track name for camera configuration
- `curSession.CameraInfo.Groups` - Available camera groups
- `curSession.DriverInfo` - Driver information

## Camera Control Integration

The iRacing SDK provides direct camera control through these methods:

### Camera Control Methods
- **`CameraOnDriver(carNumber, group, camera)`** - Switch camera to specific driver
- **`CameraOnPosition(position, group, camera)`** - Switch camera to specific position
- **`SetCameraGroup(groupNumber)`** - Set active camera group

### Camera Group Management
1. **Camera Group Discovery** - Reads available camera groups from `CameraInfo.Groups`
2. **Camera Assignment** - Maps camera names to group numbers
3. **Camera Selection** - Uses configured track cameras for decision making

## SDK Integration Architecture

### Key Classes
- **Main** (in Main.cs) - Coordinates SDK integration
- **iRacingEvents** - Handles SDK connection events
- **IRacingReplay** - Orchestrates replay processing
- **CameraControl** - Manages camera switching logic

### SDK Event Flow
1. **Connected Event** - Application is notified of iRacing connection
2. **Disconnected Event** - Application is notified of iRacing disconnection  
3. **NewSessionData Event** - New race session data is received

### Data Processing Pipeline
1. **Raw Data** - iRacing SDK telemetry
2. **Parsed Data** - Application processes session data
3. **Processed Data** - Used for camera decisions and incident detection
4. **Control Commands** - Camera switching commands sent back to iRacing

## Limitations

- **Platform Dependency** - Tied to Windows and iRacing SDK
- **SDK Version** - Requires specific iRacing SDK version
- **Real-time Performance** - Must maintain responsiveness during race data processing
- **Session Data Accuracy** - Relies on SDK for accurate race state information

## Missing References

The actual iRacingEvents.cs file is not directly visible in the codebase, but it's referenced and used heavily in Main.cs for SDK connection management.