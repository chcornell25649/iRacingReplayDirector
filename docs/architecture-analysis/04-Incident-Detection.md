# 04-Incident-Detection

## Overview

Incident detection is a crucial feature of the iRacing Replay Director that automatically identifies racing incidents and notable moments during race sessions. This enables the application to switch cameras appropriately and highlight important race events.

## Incident Detection Architecture

### Core Components
- **Incidents Class** - Stores and manages detected incidents
- **ReplayControl Class** - Uses incidents for camera direction decisions
- **RuleIncident** - Specific rule for handling incident-based camera switching
- **DataSample** - Contains telemetry data used for incident detection

### Data Flow
1. **Telemetry Processing** - Raw race data processed for incidents
2. **Incident Identification** - Detection of racing events
3. **Incident Storage** - Incidents saved for camera control
4. **Camera Decision** - Incident-based camera switching rules

## Incident Detection Logic

### Incident Types Detected
Based on the code, incidents primarily include:
- **Collision events** - Car-to-car contact
- **Accidents** - Major incidents affecting racing
- **Race incidents** - Notable events that merit camera attention

### Detection Methods
The system identifies incidents through:
1. **Speed/Position Changes** - Sudden deceleration or position changes
2. **Telemetry Analysis** - Acceleration, braking data
3. **Driver Behavior** - Unusual driving patterns
4. **Race State Changes** - Caution periods, restarts

## Incident Storage and Management

### Incident Storage
Incidents are stored in an `Incidents` class that likely:
- Tracks incident time periods
- Stores incident type information
- Manages incident severity levels
- Provides incident filtering capabilities

### Data Structure
```csharp
// From ReplayControl.cs - Incident handling
var ruleUnlimitedIncident = new RuleIncident(cameraControl, removalEdits, incidents, 999);
var ruleLimitedIncident = new RuleIncident(cameraControl, removalEdits, incidents, Settings.Default.IgnoreIncidentsBelowPosition);
```

## Rule-Based Incident Handling

### RuleIncident Class
Located in the direction rules system, this handles incident detection:

**Key Properties:**
- **Camera Control Integration** - Uses camera control for switching
- **Removal Edits** - Manages video editing based on incidents
- **Incidents Collection** - Reference to incident data
- **Position Threshold** - Minimum position for incident consideration

### Incident Filtering
The system implements filtering based on:
- **Position Threshold** - `Settings.Default.IgnoreIncidentsBelowPosition`
- **Severity Levels** - Not all incidents are treated equally
- **Time Periods** - Some incidents may be ignored during specific race periods

## Incident Detection Limitations

### Current Limitations
1. **Limited Detection Granularity** - May not detect all types of racing incidents
2. **Telemetry-Based Detection** - Relies heavily on telemetry data quality
3. **No Machine Learning** - No advanced pattern recognition for incident detection
4. **Static Thresholds** - Fixed rules without adaptive learning

### Potential Improvements for IRD 2.0
1. **Enhanced Telemetry Analysis** - Better detection of subtle incidents
2. **Machine Learning Integration** - Pattern recognition for incident prediction
3. **Multi-Event Detection** - Consider multiple telemetry signals together
4. **User-Defined Rules** - Allow customization of incident detection parameters

## Integration with Camera Control

### Camera Switching Based on Incidents
When incidents are detected, the system:
1. **Identifies Incident Camera** - Uses configured incident camera for the track
2. **Triggers Camera Change** - Switches to incident camera
3. **Maintains Duration** - Keeps camera on incident for appropriate time
4. **Resumes Normal Flow** - Returns to normal camera after incident

### Rule Chain Integration
```csharp
// From ReplayControl.cs
var ruleLimitedIncident = new RuleIncident(cameraControl, removalEdits, incidents, Settings.Default.IgnoreIncidentsBelowPosition);
var ruleUnlimitedIncident = new RuleIncident(cameraControl, removalEdits, incidents, 999);
```

## Missing Incident Features

### Currently Unsupported
1. **Incident Classification** - No detailed incident categorization
2. **Incident Severity Scoring** - No scoring system for incident importance
3. **Incident Timeline** - No detailed time-based incident analysis
4. **Advanced Filtering** - No sophisticated filtering rules for incidents

## Data Sources for Incident Detection

### Telemetry Variables Used
- **Speed** - Sudden changes in vehicle speed
- **Acceleration** - Braking or acceleration patterns
- **Position** - Driver positions in race
- **Lap Time** - Performance changes over time
- **Race Status** - Caution, restart, etc.

### Analysis Process
1. **Raw Telemetry** - Data from iRacing SDK
2. **Pattern Recognition** - Detection of abnormal patterns
3. **Context Analysis** - Racing context for incident significance
4. **Event Logging** - Recording incidents for later review

## Incident Detection Dependencies

### Core Dependencies
- **iRacing SDK** - Provides telemetry data for incident detection
- **Session Data** - Race state information for context
- **Camera Configuration** - Incident-specific camera settings
- **Settings** - Configuration for incident thresholds and behavior

## Future Improvements for IRD 2.0

### Enhanced Detection Capabilities
1. **Multi-Telemetry Fusion** - Combine multiple telemetry signals
2. **Context Awareness** - Consider race state and position when detecting incidents
3. **Machine Learning Models** - Train on incident patterns from historical data
4. **Real-time Confidence Scoring** - Rate confidence in incident detection