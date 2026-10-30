# 07-Dependencies

## Overview

The iRacing Replay Director application relies on several key dependencies for its functionality. These dependencies span across .NET libraries, third-party tools, and native components that enable the application's core features.

## NuGet Package Dependencies

### Core Dependencies
Based on the project file, the application uses the following NuGet packages:

1. **iRacingSDK** (Version 1.18.5) - Official iRacing SDK for session data and camera control
2. **H.InputSimulator** (Version 1.5.0) - Input simulation for OBS hotkey control
3. **HotkeyListener** (Version 1.9.0) - Hotkey detection and handling
4. **MediaFoundation.Net** (Version 2.0.0.32) - Video processing capabilities
5. **Newtonsoft.Json** (Version 13.0.3) - JSON serialization for configuration
6. **YamlDotNet** (Version 16.3.0) - YAML processing for configuration files

### Dependency Analysis

**iRacingSDK**
- **Purpose** - Core iRacing integration for session data and camera control
- **Usage** - Main SDK for connecting to iRacing and retrieving race information
- **Version** - 1.18.5

**H.InputSimulator**
- **Purpose** - Simulate keyboard input for OBS integration
- **Usage** - Sends Alt-F9 keypresses to trigger OBS recording
- **Version** - 1.5.0

**HotkeyListener**
- **Purpose** - Detect and handle keyboard hotkeys
- **Usage** - Manages application hotkeys and user configuration
- **Version** - 1.9.0

**MediaFoundation.Net**
- **Purpose** - Video processing and manipulation
- **Usage** - Video transcoding and processing functionality
- **Version** - 2.0.0.32

**Newtonsoft.Json**
- **Purpose** - JSON serialization for configuration management
- **Usage** - Configuration file handling and data serialization
- **Version** - 13.0.3

**YamlDotNet**
- **Purpose** - YAML processing for configuration files
- **Usage** - Configuration file parsing and handling
- **Version** - 16.3.0

## Native Dependencies

### Required Native Components
The application requires several Windows-native dependencies:

1. **Windows Forms** - UI framework for Windows desktop applications
2. **.NET Framework 4.7.2** - Core runtime environment
3. **Media Foundation** - Windows video processing framework
4. **Keyboard Input Simulation** - Windows API for input simulation

## Third-Party Libraries`n`n### Video Capture Software Integration`nThe application is specifically designed to work with:`n`n1. **OBS Studio** - Primary video capture software`n2. **NVIDIA ShadowPlay** - Alternative capture solution`n3. **Other capture applications** - Any software using standard hotkey controls`n`n## Missing References`n`n### Not Found Dependencies`nSeveral components mentioned in the code appear to be either:`n1. **Not present in the current codebase** - Such as specific iRacingEvents implementation`n2. **External libraries** - Possibly located in separate repositories or projects`n3. **Legacy components** - Possibly removed or restructured in the current codebase`n`n## Compatibility Analysis`n`n### .NET Framework Limitations`nThe application targets .NET Framework 4.7.2, which creates several compatibility constraints:`n`n**Current Issues:**`n- **Outdated Framework** - No longer actively supported`n- **Windows-Only** - Cannot run on non-Windows platforms`n- **No .NET Core/.NET 5+ Support** - Incompatible with modern .NET versions`n- **Security Concerns** - Uses deprecated runtime features`n`n### Cross-Platform Considerations`nThe application's dependencies create significant cross-platform barriers:`n`n1. **Windows Forms UI** - Requires Windows desktop environment`n2. **Windows API Dependencies** - Uses Windows-specific features`n3. **Input Simulation** - Relies on Windows input APIs`n4. **Media Foundation** - Windows-specific video processing`n`n## Dependency Compatibility with .NET 10`n`n### Issues for IRD 2.0 Migration`nWhen migrating to .NET 10, several compatibility issues must be addressed:`n`n1. **iRacingSDK Compatibility**`n   - Verify compatibility with newer .NET versions`n   - Update to latest SDK if available`n`n2. **Windows Forms Migration**`n   - Consider migrating to newer UI frameworks (WinUI, WPF, or MAUI)`n   - Ensure Windows-specific APIs work correctly`n`n3. **H.InputSimulator Compatibility**`n   - Verify with .NET 10`n   - May require replacement with modern input simulation`n`n4. **MediaFoundation.Net Compatibility**`n   - Check for modern alternatives`n   - May require replacement with newer video processing libraries`n`n5. **System Requirements**`n   - All Windows-specific dependencies must be verified for .NET 10`n   - Input simulation APIs may require updates`n`n## Potential Replacement Libraries`n`n### Modern Alternatives`nFor IRD 2.0, consider replacing legacy dependencies with:`n`n1. **Input Simulation** - System.Windows.Input or modern input libraries`n2. **Video Processing** - FFmpeg.NET or similar cross-platform libraries`n3. **UI Framework** - Windows App SDK or MAUI for modern desktop apps`n4. **Configuration** - System.Text.Json or other modern serialization`n`n## Dependency Limitations`n`n### Known Issues`n1. **Legacy Framework** - .NET Framework 4.7.2 is outdated`n2. **Platform Dependency** - Tied to Windows desktop environment`n3. **Third-party Integration** - Reliance on specific external tools`n4. **Security Concerns** - Outdated components may have vulnerabilities`n`n## Migration Considerations`n`n### Compatibility Planning`nWhen building IRD 2.0, these dependencies should be carefully evaluated:`n`n1. **iRacing SDK** - Should maintain compatibility with iRacing's current API`n2. **Input Simulation** - Needs modern replacement if required`n3. **Video Processing** - May require new cross-platform libraries`n4. **Configuration Management** - Modern serialization libraries preferred`n`n## Future-Proofing Considerations`n`n### Modern Architecture`nFor future-proofing, consider:`n`n1. **Modular Design** - Dependency injection for easier replacement`n2. **Cross-Platform Support** - Design for multiple runtime environments`n3. **Cloud Integration** - Consider cloud-based video processing`n4. **API-First Approach** - Modern API-based integrations where possible
