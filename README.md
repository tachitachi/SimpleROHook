This is **SimpleROHook updated to work with 2017-09-20ragexe from iRO RE:Start**. It *may* also support current Renewal clients from other servers.

I have only tested the Bowling Bash gutterline functionality; judging by the logging console, some other stuff is definitely still wrong.

This fork also updates the project to Visual Studio 2015 (+ runtime), because I don't have earlier versions installed.

### Build instructions

* Install the [August 2007 DirectX SDK](https://www.microsoft.com/en-us/download/details.aspx?id=13287). Other versions **will not work**.
  * Note this will overwrite your `DXSDK_DIR` environment variable. If you've had the June 2010 SDK installed before, you'll probably want to set it back.
  * Set `DXSDK_AUG07_DIR` to the August 2007 DirectX SDK directory, e.g. `C:\Program Files (x86)\Microsoft DirectX SDK (August 2007)`.
* Clone the repository.
* Open in **Visual Studio 2015** (may also work in 2017, untested).
* Select `Release-iRO/Mixed Platforms` build configuration.
* Build Solution.

### Disclaimers

I did the minimum work necessary to make Bowling Bash gutterline display work on the client mentioned above, with only a few minutes of testing. If this ends up crashing your client, I'd appreciate a heads up, but don't yell at me if that loses you an MvP or something.

As far as I'm aware, iRO's GM team tolerates gutterline display client edits, but keep in mind that client edits are generally at least a gray area. Use this tool at your own responsibility.

### Original README

    SimpleROHook

    Simply extend Ragnarok Online.
    For example, display font on client screen,it can placed easily in 3D map.

    Copyright (C) 2014 redcat Planetleaf.com Lab. All rights reserved.

                              http://lab.planetleaf.com/memory-of-rcx


    SimpleROHook is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.