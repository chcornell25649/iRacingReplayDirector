# 08-IRD2-Recommendations

## Overview

This document outlines recommendations for building iRacing Replay Director 2.0 (IRD 2.0) based on the analysis of the legacy iRacing Replay Director application. These recommendations focus on the priority components you specified.

## Priority Component Focus

### 1. iRacing SDK Connection
### 2. Camera Control  
### 3. Automatic Direction Rules
### 4. Incident Detection
### 5. Replay Control
### 6. OBS Recording (WebSocket Integration)

## Component-by-Component Recommendations

### 1. iRacing SDK Connection
**Key Improvements:**
- Enhanced connection stability with robust reconnection logic
- Better session state monitoring for replay vs live detection
- Comprehensive telemetry data validation
- Graceful error handling for SDK disconnections

**Technical Recommendations:**
- Upgrade to latest iRacing SDK version
- Implement connection heartbeat monitoring
- Add automatic fallback mechanisms
- Create detailed logging for SDK connection issues

### 2. Camera Control
**Key Improvements:**
- Camera verification to confirm switches actually occurred
- Enhanced camera group discovery and mapping
- Multi-camera support for complex racing scenarios
- Camera priority system for user configuration

**Technical Recommendations:**
- Add camera switching confirmation callbacks
- Implement comprehensive camera state tracking
- Create camera validation system
- Add camera cooldown mechanisms
- Develop camera preference weighting system

### 3. Automatic Direction Rules
**Key Improvements:**
- Rule priority enhancement allowing user customization
- Dynamic rule weighting based on race progression
- Multi-event decision making considering multiple racing events
- Improved rule conflict resolution system

**Technical Recommendations:**
- Implement rule chaining with better veto management
- Add rule configuration UI for user-defined priorities
- Create rule performance metrics tracking
- Develop rule testing framework for validation

### 4. Incident Detection
**Key Improvements:**
- Multi-telemetry fusion combining multiple signals
- Machine learning integration for incident pattern recognition
- Context-aware detection considering race state and conditions
- Confidence scoring for incident detection accuracy

**Technical Recommendations:**
- Implement advanced signal processing for anomaly detection
- Add machine learning model integration points
- Create incident severity scoring system
- Develop incident classification algorithms

### 5. Replay Control
**Key Improvements:**
- Automated replay processing streamlining end-to-end workflow
- Enhanced video overlay capabilities
- Processing optimization for speed and efficiency
- Automated quality checks and validation

**Technical Recommendations:**
- Implement modular replay processing pipeline
- Add automated overlay generation and insertion
- Create batch processing queue system
- Develop quality assurance checkpoints

### 6. OBS Recording (WebSocket Integration)
**Key Improvements:**
- Direct API access to OBS for recording control
- More precise recording management
- Real-time recording status feedback
- Robust error management for recording failures

**Technical Recommendations:**
- Implement OBS WebSocket API integration
- Create recording state management system
- Add recording quality configuration options
- Develop error recovery mechanisms

## Modern Architecture Approach

### Cross-Platform Design
- Target .NET 10 with Windows-specific focus
- Modern UI framework (Windows App SDK)
- Modular component design

## Technology Stack Recommendations

### Core Framework
- **Target Framework**: .NET 10
- **UI Framework**: Windows App SDK
- **Build System**: MSBuild with modern tooling

### Integration Libraries
- **iRacing SDK**: Latest version compatibility
- **Video Processing**: FFmpeg.NET
- **Input Simulation**: Modern libraries

## Conclusion

IRD 2.0 will be built upon a solid foundation while addressing critical improvements in reliability and functionality.