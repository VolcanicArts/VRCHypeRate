# VRCHypeRate

A simple OSC implementation for taking HypeRate.io and sending it into VRChat.

You will need a HypeRate API key to use this project, and you can request one in their [Discord](https://discord.gg/eTwfgU29cU) server.

Next, create a config file where the code has been complied into an executable. Example:
```json
{
	"id": "",
	"apikey": "",
	"mode": ""
}
```
`id` being your HypeRate ID, and `apikey` being your API key. Mode is explained below

## Modes
`normalised`

If you'd like to use this, create an animation in Unity that lasts 1 second, this represents a single heartbeat.
Have a paramater on your avatar called `Heartrate`. This is the heartrate normalised to 1, where 1 is 60bpm.
Assign that parameter to the speed of your animation. Now the animation will play at default speed when your heartrate is 60bpm.
To use this mode, type `normalised` into the `mode` field of your config.

`individual`

This mode is meant for users that want to display their heartrate as a number. This requires more effort as you will need material animations to switch digits. The parameter names are:
`HeartrateOnes`
`HeartrateTens`
`HeartrateHundreds`
To use this mode, type `individual` into the `mode` field of your config.

## State
`HeartrateEnabled` is an OSC boolean that gets toggled between true/false based on whether the program is running and connected to the HypeRate websocket. You can use this to toggle your heartrate object in Unity on/off
