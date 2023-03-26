# Reboot Troubleshooter

[Releases](https://github.com/NSouth/RebootTroubleshooter/releases/latest)

Sometimes I wake my Windows PC from sleep or hibernation and am surprised to find that it has rebooted. Why? I don't know. This is a simple app to help me (and others) understand why a recent reboot occurred. It scans the Windows Event Log for reboot events and attempts to display them in a human-readable format. 

# Screenshots
The main window with reboot events:  
![Reboot Troubleshooter - Main Window](/images/ScreenshotMain.png?raw=true "Main Window")

Details flyout with more info:  
![Reboot Troubleshooter - Details View](/images/ScreenshotDetails.png?raw=true "Main Window")

## Development Notes about ChatGPT
I used this as a test for leveraging ChatGPT in my coding. ChatGPT generated much of the code for how I query the Event Log, which events are detected, and their human-readable descriptions. Overall, it was neat! However, it definitely wasn't smooth sailing. The AI got a few things dead wrong that I had to look up and correct. There were also a few times when I asked for an iterative change to existing code and it rewrote parts I didn't request to have changed. This was with ChatGPT 3.5, so maybe 4 would be better!
