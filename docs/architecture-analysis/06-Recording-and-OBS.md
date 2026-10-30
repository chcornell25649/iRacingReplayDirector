# 06-Recording-and-OBS

## Overview

The recording and OBS integration functionality of iRacing Replay Director enables users to capture race replays and integrate with video capture software like OBS (Open Broadcaster Software). This feature is essential for creating highlight videos and professional-quality race recordings.

## Video Recording Integration

### Recording Control
The application provides integration with video capture software through:

**Key Features:**
- **Alt-F9 Hotkey** - Standard recording hotkey used by the application
- **OBS Integration** - Designed to work with OBS video capture software
- **Video Capture Verification** - Validation that recording software is properly configured
- **Fast Recording Option** - High-speed recording mode for faster processing

### Recording Workflow
1. **Setup** - Configure video capture software (OBS)
2. **Recording Start** - Application triggers recording via hotkey
3. **Data Capture** - Simultaneously captures race data and video
4. **Recording Stop** - Application stops recording when complete
5. **Post-processing** - Video transcoding and overlay processing

## OBS Integration Mechanism

### Hotkey Control
The application uses input simulation to trigger OBS recording:

**Key Components:**
- **H.InputSimulator** - Library for simulating keyboard input
- **Alt-F9 Hotkey** - Default OBS recording trigger
- **Input Simulation** - Sends keystrokes to trigger recording

### Integration Process
1. **OBS Setup** - Video capture software configured for Alt-F9 recording
2. **Application Trigger** - iRacing Replay Director sends Alt-F9
3. **Recording Start** - OBS begins recording the iRacing window
4. **Recording Stop** - Application sends Alt-F9 again to stop
5. **Verification** - Ensures recording process worked properly

## Video Capture Software Integration

### Supported Software
The application is designed to work with:
- **OBS Studio** - Primary supported video capture software
- **NVIDIA ShadowPlay** - Alternative capture option
- **Other capture solutions** - Any software using Alt-F9 hotkey

### Capture Verification
The application includes verification functionality:
- **Verify Video Capture Button** - Test video capture setup
- **Working Folder Configuration** - Directory for captured files
- **File Monitoring** - Tracks capture progress and completion

## Recording Modes

### Standard Recording
- **Normal Speed Recording** - Full-quality capture at normal speed
- **Detailed Telemetry** - Captures all race data
- **Full Video Capture** - Complete race replay recording

### Fast Recording
- **Pause/Resume Mode** - Uses pause/resume to speed up recording
- **Reduced Processing Time** - Faster overall workflow
- **Memory Efficient** - Lower resource requirements

## Video Processing Capabilities

### Overlay Integration
The application supports video overlay functionality:

**Features:**
- **Race Data Overlay** - Includes race information in videos
- **Leaderboards** - Driver standings overlay
- **Fastest Laps** - Highlight fastest lap times
- **Race Messages** - Important race information display

### Video Encoding
The application handles video encoding with:

**Configuration Options:**
- **Video Bit Rate** - Adjustable encoding quality
- **Transcoding Process** - Converts captured videos
- **Highlight Only** - Focuses on important race moments
- **Shutdown After Completion** - Automatic system shutdown

## Recording Architecture

### Core Components
1. **Capture Phase** - Data and video capture
2. **Processing Phase** - Video overlay and encoding
3. **Transcoding Phase** - Video conversion and optimization
4. **Post-processing Phase** - Cleanup and finalization

### Data Flow
1. **iRacing Data** - Telemetry from iRacing SDK
2. **Video Capture** - From video capture software
3. **Overlay Data** - Race information for video overlay
4. **Processed Video** - Final encoded video with overlays

## Integration Limitations

### Current Limitations
1. **Fixed Hotkey** - Only supports Alt-F9 for OBS integration
2. **Manual Setup Required** - Video capture software must be configured manually
3. **No Direct API** - Cannot directly control OBS via API
4. **File-Based Workflow** - Relies on file system for communication

### Dependency Requirements
- **OBS Studio** - Required for video capture
- **Video Capture Software** - Any software using Alt-F9
- **iRacing** - Must be running for recording
- **Windows Platform** - Windows-specific integration

## Future Improvements for IRD 2.0

### Enhanced Recording Features
1. **Multiple Capture Software Support** - Direct support for various capture tools
2. **API Integration** - Direct OBS API access for better control
3. **Cloud Recording** - Support for cloud-based recording options
4. **Automated Setup** - Wizard for video capture software configuration

### Video Processing Enhancements
1. **Advanced Overlay System** - More sophisticated video overlay capabilities
2. **Real-time Processing** - Live video processing during capture
3. **Multi-Format Support** - Support for additional video formats
4. **Quality Optimization** - Automatic quality adjustment based on resources