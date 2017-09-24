This is **SimpleROHook updated to work with 2017-09-20ragexe from iRO RE:Start**. It *may* also support current (Renewal?) clients from other servers.

I have only tested the Bowling Bash gutterline and M2E functionality; judging by the logging console, some other stuff is definitely still wrong.

This fork also updates the project to Visual Studio 2015 (+ runtime), because I don't have earlier versions installed. Thus, you will need the [Visual C++ 2015 Redistributable](https://www.microsoft.com/en-us/download/details.aspx?id=53587) (32-bit version, `vc_redist.x86.exe`, even with 64-bit Windows) to use this.

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

As far as I'm aware, iRO's GM team tolerates gutterline display client edits, but keep in mind that client edits are generally at least a gray area. Use this tool at your own responsibility. I also have no idea how they feel about *M2E*.

### Usage

* Run SimpleROHookCS.exe
  * Only clients started *after* SimpleROHook will be affected. You'll get a logging console for previously started ones, but no actual functionality.
* Right click the SimpleROHook icon in the system tray.
* Uncheck *Window* > *NPC Logger* as it's useless.
* Check *3D Map Grid* > *Show BBE* to show gutterlines.
* The display is a little glitchy on uneven terrain. Adjusting *3D Map Grid* > *Ground Z Bias* can help.
* *3D Map Grid* > *Alpha Level* controls the opacity of the gutterline overlay.
* Feel free to close the console window that pops up when you run a client.

#### Ground skill effect display (M2E)

* Check *3D Map Grid* > *Show M2E* to mark cells currently affected by ground targeting skills (Storm Gust etc.).
* You can edit `config.ini` to set colors for specific skills.
* To find the ID of a certain skill, toggle *Debug Information* > *Show Object Information* and (have someone) cast it.
  * Besides a bunch of other crap, you will see a `SKILL INFO` section on the game screen for each affected cell. The value of `m_job` is the cell ID.
  * For example, if you cast a vertical or horizontal *Fire Wall*, you will see three `SKILL INFO` sections with `m_job = 7f`. `7f` is your skill ID, so you would change the `Skill007F` line in `config.ini` to change its color (or add one if you've previously removed it).

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