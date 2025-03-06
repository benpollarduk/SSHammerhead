<div align="center">

# Trouble aboard the SS Hammerhead
A text adventure written using NetAF.

[![main-ci](https://github.com/benpollarduk/ss-hammerhead/actions/workflows/main-ci.yml/badge.svg)](https://github.com/benpollarduk/ss-hammerhead/actions/workflows/main-ci.yml)
[![GitHub release](https://img.shields.io/github/release/benpollarduk/ss-hammerhead.svg)](https://github.com/benpollarduk/ss-hammerhead/releases)
[![License](https://img.shields.io/github/license/benpollarduk/ss-hammerhead.svg)](https://opensource.org/licenses/MIT)
[![Play Now](https://img.shields.io/badge/Play-Now-brightgreen?style=for-the-badge)](https://benpollarduk.github.io/SSHammerhead/)

</div>

## Overview
*Trouble aboard the SS Hammerhead* is a text adventure set within a small ship that is drifting through space. Can you unravel the mystery and make it out alive before you succumb to whatever horrors lie in wait on the ship? 

While a game in its own right, *Trouble aboard the SS Hammerhead* aims to provide an extended tutorial for the [NetAF](https://github.com/benpollarduk/netaf) library.

> **NOTE:** *Trouble aboard the SS Hammerhead* is very early in development and remains a work in progress.
>
> Very little work has been done on the writing and overall story so far.
>
> Currently all effort is being spent making sure that the various mechanics and ideas are being tested to ensure that NetAF is capable of providing as rich of an experience as desired.

### Story
After years of absence, the SS Hammerhead reappeared in the delta quadrant of the CTY-1 solar system. A ship was hurriedly prepared and scrambled and made contact 27 days later.
You enter the outermost airlock, and it closes behind you. With a sense of foreboding you see your ship detach from the airlock and retreat to a safe distance.

### Premise
You take the role of Naomi Martin a 32-year-old shuttle mechanic. Throughout the course of the story you must navigate through the ship and interact with the various items and the environment
to progress through the story and unravel the mysteries of the ship.

![image](https://github.com/user-attachments/assets/5c1f74e7-0d34-4250-909d-6d2d6ba82f26)

Take control of the mighty *Spider Maintenance Bot* and use it to explore inaccessible regions of the ship from a different perspective:

![image](https://github.com/user-attachments/assets/a701d488-9cf9-4510-b96b-34c59f94630a)

The *SS Hammerhead* itself is a multi-level ship, you will need to work your way towards the bridge as you progress through the game. What will you find when you get there, and importantly,
are you truly aboard on your own?

![image](https://github.com/user-attachments/assets/eba11609-3b24-4083-868a-d30c743b87d9)

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

![image](https://github.com/user-attachments/assets/51369b53-7176-42a4-85e1-84cb761b902a)

## Getting Started

### Clone the repo
Clone the repo.
```bash
git clone https://github.com/benpollarduk/ss-hammerhead.git
```
Build and run either *SSHammerhead.Console* or *SSHammerhead.Blazor* to run the console or web app respectively.

### Play in Browser
You can now play in the browser. This is a trial feature and can be clunky. The HTML frame builders from NetAF aren't currently sophisticated enough to handle all of the various frames so the console frame builders have been used to provide an emulation of the console of sorts.

To play in the browser click [here](https://benpollarduk.github.io/SSHammerhead/).

## For Open Questions
Visit https://github.com/benpollarduk/ss-hammerhead/issues
