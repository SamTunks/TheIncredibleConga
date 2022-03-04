# The Incredible Conga

A game for the GMTK2021 game jam. Based on the theme 'Joined Together', face the pressure to lead a conga carefully around a house, picking up new followers and watching out for furniture.

# Gameplay

The player controls a character who leads a conga, using their mouse to determine direction. They must call out to other partygoers to join the conga, who are constantly entering the house, they do so by pressing the spacebar to call out in a cone in front of them. For each partygoer added to the conga, the score increases and it becomes more challenging to not get caught up in your own conga.

After 30 seconds the rival conga will enter the building causing further difficulty as you avoid it's path.

# Successes

Within the 48 hour time limit, our team of two managed to achieve a well-rounded concept for a game well within the theme of the jam. Complete with an example level, animation and music.

When implementing followers, we opted for each one to be ignorant of the player and only track the locations of the follower directly in front of them, becoming _their_ 'leader'. This meant it was particularly easy to write an instance of a follower and manage the queue of positions as this was generated and consumed by the same follower instance. It ensured that there was no list of the positions being created indefinitely without a follower to consume them.

We also chose to give followers different states at points in their lifetime, where they would have differing behaviours depending on if they were entering the house, idle, joining the conga or in the conga. This allowed for careful control of what a follower should do throughout the game and minimised strange behaviour. For example, when a follower joins the conga the first move to the player's position before being attached to the back of the conga line. This looks far more natural than the follower simply teleporting to the back.

Throughout the project, we endeavoured to write clean and well organized code, using github to work collaboratively and manage merge conflicts should they arise.

# Shortcomings

Naturally, due to the time limit, there were features we had hoped to flesh out further and ideas that were beyond the scope of the jam.

One such feature was the rival conga. This had been central to the game’s character since we began as it added necessary depth to the challenge of the game. We had hoped to make the rival conga more dynamic in the way it could move around the house as well as on-screen effects to signal this was a major event. Initially, the rival conga would be a breakaway conga from the player’s once it reached a given size, with the ability to recapture this conga for bonus points. In order to get this concept implemented within the jam’s time limit, we compromised by adding fixed nodes the conga would path around after 30 seconds of gameplay.
