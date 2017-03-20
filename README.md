# Hudsun
Small application for displaying the build status from a Jenkins/Hudson using an USB connected notifier lamp.

It was developed for working together with the following hardware:
[USB Mail Melder](http://www.getdigital.de/USB-Mail-Melder.html)

Currently it has two modes:

## Jekins/Hudsun mode
In this mode the software is polling in an interval of 5 seconds on a configured Jenkins/Hudson job visualizing the status. When the build is currently running, the lamp will blink, if the build is finished, the light will be continuously on. The project can be changed using the tray icon.

## Cheerlights mode
Additionally the light can be used as a "Cheerlight" implementing the [Cheerlight API](http://cheerlights.com/cheerlights-api/).
*"CheerLights is an “Internet of Things” project created by Hans Scharler that allows people’s lights all across the world to synchronize to one color set by Twitter. This is a way to connect physical things with social networking experiences."*. See [CheerLights](http://cheerlights.com/about/) website.

## Future plans
Currently the implementation is not really robust against wrong inputs. Also it could be better configurable, some things are currently hardcoded. In future the different displaying modes should be plugins that can be extended.