<div align="center">

# Trouble aboard the SS Hammerhead
A text adventure written using NetAF.

[![main-ci](https://github.com/benpollarduk/ss-hammerhead/actions/workflows/main-ci.yml/badge.svg)](https://github.com/benpollarduk/ss-hammerhead/actions/workflows/main-ci.yml)
[![GitHub release](https://img.shields.io/github/release/benpollarduk/ss-hammerhead.svg)](https://github.com/benpollarduk/ss-hammerhead/releases)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_SSHammerhead&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=benpollarduk_SSHammerhead)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_SSHammerhead&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=benpollarduk_SSHammerhead)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_SSHammerhead&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=benpollarduk_SSHammerhead)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_SSHammerhead&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=benpollarduk_SSHammerhead)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=benpollarduk_SSHammerhead&metric=bugs)](https://sonarcloud.io/summary/new_code?id=benpollarduk_SSHammerhead)
[![License](https://img.shields.io/github/license/benpollarduk/ss-hammerhead.svg)](https://opensource.org/licenses/MIT)

</div>

## Overview
*Trouble aboard the SS Hammerhead* is a text adventure set within a small ship that is drifting through space. Can you unravel the mystery or succumb to the ship? It aims to provide an extended tutorial for the [NetAF](https://github.com/benpollarduk/netaf) library.

> **NOTE:** *Trouble aboard the SS Hammerhead* is a short text adventure that is very early in development and remains a work in progress.
>
> Very little work has been done on the writing and story so far.
>
> Currently all effort is being spent making sure that the various mechanics and ideas are being tested to ensure that NetAF is capable of providing as rich of an experience as desired.

### Story
After years of absence, the SS Hammerhead reappeared in the delta quadrant of the CTY-1 solar system. A ship was hurriedly prepared and scrambled and made contact 27 days later.
You enter the outermost airlock, and it closes behind you. With a sense of foreboding you see your ship detach from the airlock and retreat to a safe distance.

### Premise
You take the role of Naomi Martin a 32-year-old shuttle mechanic. Throughout the course of the story you must navigate through the ship and interact with the various items and the environment
to progress through the story and unravel the mysteries of the ship.

![image](https://github.com/user-attachments/assets/79b3b6cd-7ecd-4d4e-a2b8-47a6f2b06732)

Take control of the mighty *Spider Maintenance Bot* and use it to explore inaccessible regions of the ship from a different perspective:

![image](https://github.com/user-attachments/assets/429e50d4-57ba-4c51-9126-bf06a11cb629)

The *SS Hammerhead* itself is a multi-level ship, you will need to work your way towards the bridge as you progress through the game. What will you find when you get there, and importantly,
are you truly aboard on your own?

![image](https://github.com/user-attachments/assets/a244495a-dc2f-4196-b95b-eb3935c9770e)

### Commands
The following NetAF commands are supported for interacting with game elements:
* **Drop X** - drop an item.
* **Examine X** - allows items, characters and environments to be examined.
* **Take X** - take an item.
* **Talk to X** - talk to a NPC, where X is the NPC.
* **Use X on Y** - use an item. Items can be used on a variety of targets. Where X is the item and Y is the target.
* **N, S, E, W, U, D** - traverse through the rooms of the ship.

The following NetAF global commands are also supported:
* **About** - display version information.
* **CommandsOn / CommandsOff** - toggle commands on/off.
* **Exit** - exit the game.
* **Help** - display the help screen.
* **KeyOn / KeyOff** - turn the Key on/off.
* **Map** - display the map.
* **New** - start a new game.

There are many other commands that can be used to interact with the environment throughout the course of the game.

## Getting Started

### Clone the repo
Clone the repo.
```bash
git clone https://github.com/benpollarduk/ss-hammerhead.git
```
Build and run the solution.

## For Open Questions
Visit https://github.com/benpollarduk/ss-hammerhead/issues
