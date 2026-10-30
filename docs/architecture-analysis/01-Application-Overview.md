# 01-Application-Overview

## Overview

The iRacing Replay Director (IRD) is a Windows application that automates the process of capturing, analyzing, and creating highlight reels from iRacing replay sessions. It connects to the iRacing SDK to monitor session data, automatically switches cameras during races, and integrates with video capture software to create professional-quality highlight videos.

## Architecture

The application is built on .NET Framework 4.7.2 and uses Windows Forms for its UI. It follows a modular architecture with distinct phases for:
1. **Session monitoring** - Connecting to iRacing and monitoring session data
2. **Data capture** - Recording race data and telemetry
3. **Incident detection** - Identifying racing incidents and notable moments
4. **Camera control** - Automatically switching cameras based on racing events
5. **Video processing** - Encoding and transcoding videos with overlays
6. **Plugin architecture** - Extensible overlay system

## Key Components

### Core Application Files
- **Main.cs** - Main application form and user interface
- **Program.cs** - Application entry point
- **iRacingReplay.cs** - Core race replay processing phase
- **CameraControl.cs** - Camera switching logic
- **ReplayControl.cs** - Direction/routing control logic

### Key Classes
- **iRacingEvents** - SDK event handling for iRacing connection
- **IRacingReplay** - Main race processing workflow
- **CameraControl** - Camera switching management
- **ReplayControl** - Camera direction rules engine
- **TrackCameras** - Track and camera configuration management

## Main Workflow

1. **Initialization** - Application starts, connects to iRacing SDK
2. **Session Monitoring** - Waits for iRacing to start a session
3. **Data Capture** - Records race data and telemetry
4. **Incident Analysis** - Identifies racing events and incidents
5. **Camera Control** - Switches cameras based on racing events
6. **Video Processing** - Encodes video with overlays
7. **Post-processing** - Cleanup and shutdown

## Dependencies

- **iRacingSDK** - Official iRacing SDK for session data and camera control
- **iRacingReplayOverlay** - Core replay processing components
- **MediaFoundation.Net** - Video processing library
- **H.InputSimulator** - Input simulation for OBS control
- **HotkeyListener** - Hotkey handling
- **Newtonsoft.Json** - JSON serialization
- **YamlDotNet** - YAML processing

## Technologies Used

- **.NET Framework 4.7.2**
- **Windows Forms** (UI framework)
- **C# 5.0**
- **iRacing SDK** for real-time data access
- **Video Capture Software Integration** (OBS, etc.)

## Limitations

- Tied to Windows Forms UI framework
- Legacy .NET Framework target
- Dependency on iRacing SDK for session data
- Limited cross-platform compatibility