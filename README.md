# VRCHypeRate

A simple implementation for taking HypeRate.io heartrate values and sending them into VRChat over OSC.

## Setup
Download the latest release from the [Release](https://github.com/VolcanicArts/VRCHypeRate/releases/latest) page.

You will need a HypeRate API key to use this project, and you can request one in their [Discord](https://discord.gg/eTwfgU29cU) server.

Create a config file where `VRCHypeRate.exe` is located and fill in the information accordingly:
```json
{
	"id": "",
	"apikey": ""
}
```

## Parameters

`HeartrateNormalised` is the heartrate normalised to a value of `1` when your BPM is `60`. This is meant for use as an animation speed adjustment.

`HeartrateOnes`, `HeartrateTens`, `HeartrateHundreds` are values between 0 and 9 and are meant for use of changing material overrides to the correct number to display your heartrate.

`HeartrateEnabled` is `true` whenever the websocket is connected and heartrate values are being sent, and is `false` if the websocket fails to connect or disconnects.

## License
This app is licenced under the [GNU General Public License V3](https://www.gnu.org/licenses/gpl-3.0.en.html). Please see [the license file](LICENSE) for more information.
