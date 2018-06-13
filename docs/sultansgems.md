---
layout: default
title: Sultan's Gems
description: A simple Match 3 game in which the player tries to achieve a high score within a certain number of moves by matching similar pieces.
permalink: /SultansGems/
unityPlayer: yes
---

## Screenshots

<table style="width:100%" height="20%" cellspacing="5" cellpadding="5">
  <tr>
    <th><img src="{{site.baseurl}}/assets/images/SultansGems/screenshot1.png" style="width:25% height:100%"></th>
    <th><img src="{{site.baseurl}}/assets/images/SultansGems/screenshot2.png" style="width:25% height:100%"></th>
    <th><img src="{{site.baseurl}}/assets/images/SultansGems/screenshot3.png" style="width:25% height:100%"></th>
    <th><img src="{{site.baseurl}}/assets/images/SultansGems/screenshot4.png" style="width:25% height:100%"></th>
  </tr>
</table>
<p></p>

## What's Included

* Level data is stored as customs assets. Board shape, pieces to initially place, points per stone and which stones are valid can all be assigned in the editor.
* Modular approach in which new levels can easily be added.
* Player data is saved to disk (via Binary Serialization).
* Localization is incorporated throughout.
* Optimized assets (Sprite Packer).
* iOS Launch Screen.
* A complete write-up of the game design can be found in [GameDesignDocument.pdf](https://github.com/defuncart/game-in-a-week/blob/master/SultansGems/GameDesignDocument.pdf).

## Play Online

The game can be played in the browser below:

<div id="gameContainer" style="width: 405px; height: 720px; margin: auto"></div>
<p></p>

## Conclusion

Although the mathematics of idle games are quite complex, and game balancing wasn't even considered due to time constraints, from an implementation point of view, *Idle Capitalist* is quite a well-functioning idle clicker. This prototype could easily be extended to form a full game.

## Further Reading

* A full list of credits can be found in [Credits.txt](https://github.com/defuncart/game-in-a-week/blob/master/SultansGems/Credits.txt).
* A number of principles utilized in this project are explained in more detail in various [#50-Unity-Tips](https://github.com/defuncart/50-unity-tips) articles.
