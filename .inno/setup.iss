#define MyAppURL "https://github.com/benpollarduk/SSHammerhead"
#define CompanyName "NetAF"
#define AppName "SSHammerhead"
#define ConsoleAppName "SSHammerhead - Console"
#define ConsoleAppExeName "SSHammerhead.Console.exe"
#define WPFAppName "SSHammerhead - WPF"
#define WPFAppExeName "SSHammerhead.WPF.exe"
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
Source: "..\SSHammerhead.Console\bin\Release\*"; DestDir:"{app}\Console"; Flags: ignoreversion recursesubdirs; Excludes: "*.log";
Source: "..\SSHammerhead.WPF\bin\Release\net8.0-windows\*"; DestDir:"{app}\WPF"; Flags: ignoreversion recursesubdirs; Excludes: "*.log";

[Icons]
Name: "{commondesktop}\{#ConsoleAppName}"; Filename: "{app}\Console\{#ConsoleAppExeName}"; WorkingDir: "{app}"
Name: "{commondesktop}\{#WPFAppName}"; Filename: "{app}\WPF\{#WPFAppExeName}"; WorkingDir: "{app}"
