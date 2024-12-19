#define MyAppURL "https://github.com/benpollarduk/SSHammerhead"
#define CompanyName "NetAF"
#define AppName "SSHammerhead"
#define AppExeName "SSHammerhead.exe"
#define ApplicationVersion "1.0.0"

[Setup]
AppName={#AppName}
AppVersion={#ApplicationVersion}
AppVerName={#AppName} {#ApplicationVersion}
DefaultDirName={commonpf}\{#CompanyName}\{#AppName}
DefaultGroupName={app}
DisableProgramGroupPage=yes
OutputBaseFilename={#AppName}_{#ApplicationVersion}_Setup
InfoBeforeFile=Eula.txt
AlwaysShowDirOnReadyPage=yes

[Files]
Source: "..\SSHammerhead\bin\Release\*"; DestDir:"{app}"; Flags: ignoreversion recursesubdirs; Excludes: "*.log";

[Icons]
Name: "{commondesktop}\{#AppName}"; Filename: "{app}\{#AppExeName}"; WorkingDir: "{app}"
