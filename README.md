# ASUS Charge Switch Windows Battery Charge Limit App

<img src="ASUS%20Charge%20Icon.png" alt="ASUS Charge Switch: 80% and 100% battery charging" width="25%">

A small Windows desktop app for switching a compatible ASUS laptop's battery charge limit between **80%** and **100%**. Enable the 80% limit for everyday use, or allow a full charge before travelling.

**Experimental:** hardware compatibility has not been independently verified. This is an unofficial project, not affiliated with ASUS.

## Download for Windows

[Download ASUS Charge Switch.exe](https://github.com/Dwalzinski/asus-charge-switch/releases/latest/download/ASUS-Charge-Switch.exe)

[Release notes and downloads](https://github.com/Dwalzinski/asus-charge-switch/releases/latest)

## How to use

1. Download the EXE and place it in a folder or on your desktop.
2. Run it and accept the Windows administrator prompt.
3. Choose **Smart charging ON 80%** to request the battery protection limit.
4. Choose **Smart charging OFF 100%** to allow a full charge.
5. Return to 80% manually when finished.

Opening the app alone does not change the charging limit. The app reports whether the ASUS firmware accepted the request; this is not proof that the battery has reached the requested percentage.

## Requirements and compatibility

- Windows x64 with .NET Framework 4.x and Windows Forms.
- A compatible ASUS device with the ASUS System Control Interface driver and ATKACPI interface.
- Administrator permission.

Originally created for an ASUS ROG Flow Z13 owner. Support for that device or other ASUS models remains unverified. This is not a universal Windows smart charging switch.

MyASUS or G-Helper may override the limit. Persistence after restart is not guaranteed. Setting 80% does not actively discharge a battery that is already above 80%. The app is unsigned.

## Build from source

In PowerShell from this directory:

```powershell
& "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo /target:winexe /platform:x64 /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /win32manifest:app.manifest /win32icon:ChargeSwitch.ico /out:ASUS-Charge-Switch.exe ChargeSwitch.cs
```

The program sends only the battery-limit device command (0x00120057) with a value of 80 or 100 through the ASUS ATKACPI interface. See the [G-Helper ASUS interface reference](https://github.com/seerge/g-helper/blob/main/app/AsusACPI.cs).

## Troubleshooting

If the interface cannot be opened, check that the ASUS System Control Interface driver is installed and the app has administrator permission. If firmware rejects the command, your model or driver may not support this control. Report your ASUS model, Windows version, and error text in an issue; do not post serial numbers or device IDs.

## Website download link

Use this stable link for the latest published release:

https://github.com/Dwalzinski/asus-charge-switch/releases/latest/download/ASUS-Charge-Switch.exe

Suggested link text: **Download ASUS Charge Switch for Windows (80% / 100% battery limit)**.

