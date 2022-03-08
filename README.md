# VRCHypeRate

A simple OSC implementation for taking HypeRate.io and sending it into VRChat.

You will need a HypeRate API key to use this project, and you can request one in their [Discord](https://discord.gg/eTwfgU29cU) server 

If you'd like to use this, create an animation in Unity that lasts 1 second, this represents a single heartbeat.
Have a paramater on your avatar called "Heartrate". This is the heartrate normalised to 1, where 1 is 60bpm.
Assign that parameter to the speed of your animation. Now the animation will play at default speed when your heartrate is 60bpm.

Next, create a config file where the code has been complied into an executable. Example:
```json
{
	"id": "",
	"apikey": ""
}
```
`id` being your HypeRate ID, and `apikey` being your API key.

After starting VRChat, start the executable and see your heartrate being represented in VRChat!
