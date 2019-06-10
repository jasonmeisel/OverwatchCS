using System;

namespace Workshop
{
    public static class Actions
    {
        public static void Wait(float seconds) { }
        public static void ApplyImpulse() { }

        public static void DebugLog<T>(T value) { }


        /// ABORT
        /// Stops execution of the action list.
        /// There are no definitions to this action.
        [WorkshopCodeName("ABORT")]
        public static void Abort() { }

        /// ABORT IF
        /// Stops execution of the action list if the action’s condition evaluates to true, if it does not, the execution continues with the next action.
        /// Definitions:
        /// 
        /// Condition - Specifies whether the execution is stopped. Can use most Boolean based Value Syntax.
        [WorkshopCodeName("ABORT IF")]
        public static void AbortIf() { }

        /// ABORT IF CONDITION IS FALSE
        /// Stops execution of the action list if at least one condition in the condition list is false. If all conditions are true, execution continues with the next action.
        /// There are no definitions to this action.
        [WorkshopCodeName("ABORT IF CONDITION IS FALSE")]
        public static void AbortIfConditionIsFalse() { }

        /// ABORT IF CONDITION IS TRUE
        /// Stops execution of the action list if all conditions in the condition list is true. If any are false, execution continues with the next action.
        /// There are no definitions to this action.
        [WorkshopCodeName("ABORT IF CONDITION IS TRUE")]
        public static void AbortIfConditionIsTrue() { }

        /// ALLOW BUTTON
        /// Undoes the effect of the disallow button action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose button is being reenabled. Can use most Player based Value Syntax.
        /// Button - The logical button that is being reenabled.
        [WorkshopCodeName("ALLOW BUTTON")]
        public static void AllowButton() { }

        /// APPLY IMPLUSE
        /// Applies an instantaneous change in velocity to the movement of one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose velocity will be changed. Can use most Player based Value Syntax.
        /// Direction - The unit direction in which the impulse will be applied. This value is normalized internally. Can use most Vector based Value Syntax.
        /// Speed - The magnitude of the change to the velocities of the player or players. Can use most Number based Value Syntax.
        /// Relative - Specifies whether the direction is relative to world coordinates or the local coordinates of the player or players.
        /// 
        /// To World - Relative to the world’s coordinate system.
        /// To Player - Relative to the player’s local coordinate system (which moves and rotates with the player).
        /// 
        /// 
        /// Motion - Specifies whether existing velocity that is counter to direction should first be canceled out before applying the impulse.
        /// 
        /// Cancel Contrary Motion - If the target is moving against the direction of the impulse, this relative velocity is negated before the impulse is applied.
        /// Incorporate Contrary Motion - The impulse is added directly to the velocity of the target, so if the target is moving against the direction of the impulse, it might seem like the impulse has less of an effect.
        [WorkshopCodeName("APPLY IMPLUSE")]
        public static void ApplyImpluse() { }

        /// BIG MESSAGE
        /// Displays a large message above the reticle that is visible to specific players.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the message. Can use most Value Syntax to select multiple players to specify.
        /// Header - The message to be displayed. Can use most String based Value Syntax to specify.
        [WorkshopCodeName("BIG MESSAGE")]
        public static void BigMessage() { }

        /// CHASE GLOBAL VARIABLE AT RATE
        /// Gradually modifies the value of a global variable at a specific rate. (A global variable is a variable that belongs to the game itself.)
        /// Definitions:
        /// 
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the global variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Rate - The amount of change that will happen to the variable’s value each second. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Rate or nothing.
        [WorkshopCodeName("CHASE GLOBAL VARIABLE AT RATE")]
        public static void ChaseGlobalVariableAtRate() { }

        /// CHASE GLOBAL VARIABLE OVER TIME
        /// Gradually modifies the value of a global variable over time. (A global variable is a variable that belongs to the game itself.)
        /// Definitions:
        /// 
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the global variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Duration - The amount of time, in seconds, over which the variable’s value will approach the destination. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Duration or nothing.
        [WorkshopCodeName("CHASE GLOBAL VARIABLE OVER TIME")]
        public static void ChaseGlobalVariableOverTime() { }

        /// CHASE PLAYER VARIABLE AT RATE
        /// Gradually modifies the value of a player variable at a specific rate. (A player variable is a variable that belongs to a specific player.)
        /// Definitions:
        /// 
        /// Player - The player whose variable will gradually change. If multiple players are provided, each of their variables will change independently.
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the player variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Rate - The amount of change that will happen to the variable’s value each second. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Rate or nothing.
        [WorkshopCodeName("CHASE PLAYER VARIABLE AT RATE")]
        public static void ChasePlayerVariableAtRate() { }

        /// CHASE PLAYER VARIABLE OVER TIME
        /// Gradually modifies the value of a player variable over time. (A player variable is a variable that belongs to a specific player.)
        /// Definitions:
        /// 
        /// Player - The player whose variable will gradually change. If multiple players are provided, each of their variables will change independently.
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the player variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Duration - The amount of time, in seconds, over which the variable’s value will approach the destination. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Duration or nothing.
        [WorkshopCodeName("CHASE PLAYER VARIABLE OVER TIME")]
        public static void ChasePlayerVariableOverTime() { }

        /// CLEAR STATUS
        /// Clears a status that was applied from a set status action from one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players from whom the status will be removed. Can use most Player based Value Syntax.
        /// Status - The Status to be removed from the player or players. Values include Hacked, Burning, Knocked Down, Asleep, Frozen, Unkillable, Invincible, Phased Out, Rooted, or Stunned.
        [WorkshopCodeName("CLEAR STATUS")]
        public static void ClearStatus() { }

        /// COMMUNICATE
        /// Causes one or more players to use an emote, voice line, or other equipped communication.
        /// Definitions:
        /// 
        /// Player - The player or players to perform the communication. Can use most Player based Value Syntax.
        /// Type - The type of communication. Can use any equipped emote, equipped voice line, or any other communication effect.
        [WorkshopCodeName("COMMUNICATE")]
        public static void Communicate() { }

        /// CREATE EFFECT
        /// Creates an in-world effect entity. This effect entity will persist until destroyed, to obtain a reference to this entity, use the last created entity value. This action will fail if too many entities have been created.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will be able to see the effect. Can use most Value Syntax to select one or multiple players.
        /// Type - The type of the effect to be created.
        /// 
        /// Sphere
        /// Light Shaft
        /// Orb
        /// Ring
        /// Cloud
        /// Sparkles
        /// Good Aura
        /// Bad Aura
        /// Energy Sound
        /// Pick-Up Sound
        /// Good Aura Sound
        /// Bad Aura Sound
        /// Sparkles Sound
        /// Smoke Sound
        /// Decal Sound
        /// Beacon Sound
        /// 
        /// 
        /// Color - The color of the effect to be created. IF a particular team is chosen, the effect will either be red or blue, depending on whether the team is hostile to the viewer. Does not apply to sound effects.
        /// Position - The effect’s position. If this value is a player, then the effect will move along with the player, otherwise, the value is interpreted as a position in the world. Can use most Player or Vector based Value Syntax.
        /// Radius - The effect’s radius in meters. Sound effects have their volume affected instead.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated, the effect will keep asking for and using new values from reevaluated inputs.
        /// 
        /// Visible to, position, and scale
        /// Position and scale
        /// Visible to
        /// None
        [WorkshopCodeName("CREATE EFFECT")]
        public static void CreateEffect() { }

        /// CREATE HUD TEXT
        /// Creates HUD text visible to specific players at specific location on the screen. This text will persist until destroyed. To obtain a reference to this text, use the last text ID value. This action will fail if too many text elements have been created.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the HUD text. Can use most Value Syntax to select one or multiple players.
        /// Header - The header text to be displayed (can be blank). Can use most String based Value Syntax to populate.
        /// Subheader - The subheader text to be displayed (can be blank). Can use most String based Value Syntax to populate.
        /// Text - The body text to be displayed (can be blank). Can use most String based Value Syntax to populate.
        /// Location - The location on the screen where text will appear. You can choose left, top, or right.
        /// Sort Order - The Sort Order of the text relative to other text in the same location. Text with a higher sort order will come after text with a lower sort order. Can use most Number based Value Syntax.
        /// Header Color - The color of the Header text to be created. If a particular team is chosen, the effect will either be red or blue.
        /// Subheader Color - The color of the Subheader text to be created. If a particular team is chosen, the effect will either be red or blue.
        /// Text Color - The color of the body text to be created. If a particular team is chosen, the effect will either be red or blue.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated, the text will keep asking for and using new values from reevaluated inputs.
        /// 
        /// Visible to and String
        /// String
        /// None
        [WorkshopCodeName("CREATE HUD TEXT")]
        public static void CreateHudText() { }

        /// CREATE ICON
        /// Creates an in-world entity. This icon entity will persist until destroyed. To obtain a reference to this entity, use the last created entity value. This action will fail if too many entities have been created.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will be able to see the icon. Can use most Value Syntax to select one or multiple players.
        /// Position - The icon’s position. If this value is a player, then the icon will appear above the player’s head, otherwise, the value is interpreted as a position in the world. Can use most Player or Vector based Value Syntax.
        /// Icon - The icon to be created.
        /// 
        /// Arrow: Down
        /// Arrow: Left
        /// Arrow: Right
        /// Arrow: Up
        /// Asterisk
        /// Bolt
        /// Checkmark
        /// Circle
        /// Club
        /// Diamond
        /// Dizzy
        /// Exclamation Mark
        /// Eye
        /// Fire
        /// Flag
        /// Halo
        /// Happy
        /// Heart
        /// Moon
        /// No
        /// Plus
        /// Poison
        /// Poison 2
        /// Question Mark
        /// Radioactive
        /// Recycle
        /// Ring Thick
        /// Ring Thin
        /// Sad
        /// Skull
        /// Spade
        /// Spiral
        /// Stop
        /// Trashcan
        /// Warning
        /// X
        /// 
        /// 
        /// Reevaluation - Specifies which of this action’s inputs will be continously reevaluated, the icon will keep asking for and using new values from reevaluated inputs.
        /// 
        /// Visible to and position
        /// Position
        /// Visible to
        /// None
        /// 
        /// 
        /// Icon Color - The color of the icon to be created. IF a particular team is chosen, the icon will either be red or blue, depending on whether the team is hostile to the viewer.
        /// Show when offscreen - Should this icon still appear even when it is behind you? Can use most Boolean based Value Syntax to specify.
        [WorkshopCodeName("CREATE ICON")]
        public static void CreateIcon() { }

        /// CREATE IN-WORLD TEXT
        /// Creates in-world text visible to specific players at specific position in the world. This text will persist until destroyed. To obtain a reference to this text, use the last text ID value. This action will fail if too many text elements have been created.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the HUD text. Can use most Value Syntax to select one or multiple players.
        /// Header - The header text to be displayed (can be blank). Can use most String based Value Syntax to populate.
        /// Position - The text’s position. If this value is a player, then the text will appear above the player’s head. Otherwise, the value is interpreted as a position in the world. Can use most Player or Vector based Value Syntax.
        /// Scale - The text’s scale. Can use most Number based Value Syntax.
        /// Clipping - Specifies whether the text can be seen through walls or is instead clipped.
        /// 
        /// Clip Against Surfaces - The text may be partially or completely obscured by walls, floors, ceilings, players, or other solid objects.
        /// Do not clip - The text will always be fully visible. Even if it is behind a wall or solid object.
        /// 
        /// 
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated, the text will keep asking for and using new values from reevaluated inputs.
        /// 
        /// Visible to, Position, and String
        /// Visible to and String
        /// None
        [WorkshopCodeName("CREATE IN-WORLD TEXT")]
        public static void CreateInWorldText() { }

        /// DAMAGE
        /// Applies instantaneous damage to one or more players, possibly killing the players.
        /// Definitions:
        /// 
        /// Player - The player or players who will receive damage. Can use most Player based Value Syntax to select one or multiple players.
        /// Damager - The player who will receive credit for the damage. A damager of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        /// Amount - The amount of damage to apply. This amount may be modified by buffs, debuffs, or armor. Can use most Number based Value Syntax.
        [WorkshopCodeName("DAMAGE")]
        public static void Damage() { }

        /// DECLARE MATCH DRAW
        /// Instantly ends the match in a draw. This action has no effect in free-for-all modes.
        /// There are no definitions to this action.
        [WorkshopCodeName("DECLARE MATCH DRAW")]
        public static void DeclareMatchDraw() { }

        /// DECLARE PLAYER VICTORY
        /// Instantly ends the match with the specific player as the winner. This action only has an effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Player - The winning player. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("DECLARE PLAYER VICTORY")]
        public static void DeclarePlayerVictory() { }

        /// DECLARE ROUND VICTORY
        /// Declare a team as the current round winner. This only works in the control and elimination game modes.
        /// Definitions:
        /// 
        /// Team - Round winning team. Can use most Team based Value Syntax for this value.
        [WorkshopCodeName("DECLARE ROUND VICTORY")]
        public static void DeclareRoundVictory() { }

        /// DECLARE TEAM VICTORY
        /// Instantly ends the match with the specified team as the winner. This action has no effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Team - The winning team. Can use most Team based Value Syntax for this value.
        [WorkshopCodeName("DECLARE TEAM VICTORY")]
        public static void DeclareTeamVictory() { }

        /// DESTROY ALL EFFECTS
        /// Destroys all effect entities created by create effect.
        /// There are no definitions to this action.
        [WorkshopCodeName("DESTROY ALL EFFECTS")]
        public static void DestroyAllEffects() { }

        /// DESTROY ALL ICONS
        /// Destroys all icon entities created by create icon.
        /// There are no definitions to this action.
        [WorkshopCodeName("DESTROY ALL ICONS")]
        public static void DestroyAllIcons() { }

        /// DESTROY ALL IN-WORLD TEXT
        /// Destroys all in-world text created by the create in-world effect.
        /// There are no definitions to this action.
        [WorkshopCodeName("DESTROY ALL IN-WORLD TEXT")]
        public static void DestroyAllInWorldText() { }

        /// DESTROY EFFECT
        /// Destroys an effect entity that was created by create effect.
        /// Definitions:
        /// 
        /// Entity - Specifies which effect entity to destroy. This entity may be the last created entity or a variable into which last created entity was earlier stored.
        [WorkshopCodeName("DESTROY EFFECT")]
        public static void DestroyEffect() { }

        /// DESTROY HUD TEXT
        /// Destroys hud text that was created by create hud text.
        /// Definitions:
        /// 
        /// Text ID - Specifies which hud text to destroy. This ID may be last text ID or a variable into which last text ID was earlier stored.
        [WorkshopCodeName("DESTROY HUD TEXT")]
        public static void DestroyHudText() { }

        /// DESTROY ICON
        /// Destroys an icon entity that was created by create icon.
        /// Definitions:
        /// 
        /// Text ID - Specifies which icon to destroy. This ID may be last text ID or a variable into which last create entity was earlier stored.
        [WorkshopCodeName("DESTROY ICON")]
        public static void DestroyIcon() { }

        /// DISABLE BUILT-IN GAME MODE ANNOUNCER
        /// Disables game mode announcements from the announcer until reenabled or the match ends.
        /// There are no definitions to this action.
        [WorkshopCodeName("DISABLE BUILT-IN GAME MODE ANNOUNCER")]
        public static void DisableBuiltInGameModeAnnouncer() { }

        /// DISABLE BUILT-IN GAME MODE COMPLETION
        /// Disables completion of the match from the game mode itself, only allowing the match to be completed by scripting commands.
        /// There are no definitions to this action.
        [WorkshopCodeName("DISABLE BUILT-IN GAME MODE COMPLETION")]
        public static void DisableBuiltInGameModeCompletion() { }

        /// DISABLE BUILT-IN GAME MODE MUSIC
        /// Disables all game-mode music until reenabled or the match ends.
        /// There are no definitions to this action.
        [WorkshopCodeName("DISABLE BUILT-IN GAME MODE MUSIC")]
        public static void DisableBuiltInGameModeMusic() { }

        /// DISABLE BUILT-IN GAME MODE RESPAWNING
        /// Disables automatic respawning for one or more players, only allowing respawning by scripting commands.
        /// Definitions:
        /// 
        /// Player - The player or players whose respawning is affected. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("DISABLE BUILT-IN GAME MODE RESPAWNING")]
        public static void DisableBuiltInGameModeRespawning() { }

        /// DISABLE BUILT-IN GAME MODE SCORING
        /// Disables changes to player and team scores from the game mode itself, only allowing scores to be changed by scripting commands.
        /// There are no definitions to this action.
        [WorkshopCodeName("DISABLE BUILT-IN GAME MODE SCORING")]
        public static void DisableBuiltInGameModeScoring() { }

        /// DISABLE DEATH SPECTATE ALL PLAYERS
        /// Undoes the effect of the enable death spectate all players action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose default death spectate behavior is restored. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("DISABLE DEATH SPECTATE ALL PLAYERS")]
        public static void DisableDeathSpectateAllPlayers() { }

        /// DISABLE DEATH SPECTATE TARGET HUD
        /// Undoes the effect of the enable death spectate target hud action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will revert to seeing their own HUD while death spectating. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("DISABLE DEATH SPECTATE TARGET HUD")]
        public static void DisableDeathSpectateTargetHud() { }

        /// DISALLOW BUTTON
        /// Disables a logical button for one or more players such that pressing it has no effect.
        /// Definitions:
        /// 
        /// Player - The player executing this rule, as specified by the event. May be the same as the attacker or victim. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("DISALLOW BUTTON")]
        public static void DisallowButton() { }

        /// ENABLE BUILT-IN GAME MODE ANNOUNCER
        /// Undoes the effect of the disable built-in game mode announcer action.
        /// There are no definitions to this action.
        [WorkshopCodeName("ENABLE BUILT-IN GAME MODE ANNOUNCER")]
        public static void EnableBuiltInGameModeAnnouncer() { }

        /// ENABLE BUILT-IN GAME MODE COMPLETION
        /// Undoes the effect of the disable built-in game mode completion action.
        /// There are no definitions to this action.
        [WorkshopCodeName("ENABLE BUILT-IN GAME MODE COMPLETION")]
        public static void EnableBuiltInGameModeCompletion() { }

        /// ENABLE BUILT-IN GAME MODE MUSIC
        /// Undoes the effect of the disable built-in game mode music action.
        /// There are no definitions to this action.
        [WorkshopCodeName("ENABLE BUILT-IN GAME MODE MUSIC")]
        public static void EnableBuiltInGameModeMusic() { }

        /// ENABLE BUILT-IN GAME MODE RESPAWNING
        /// Undoes the effect of the disable built-in game mode respawning for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose respawning is affected. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("ENABLE BUILT-IN GAME MODE RESPAWNING")]
        public static void EnableBuiltInGameModeRespawning() { }

        /// ENABLE BUILT-IN GAME MODE SCORING
        /// Undoes the effect of the disable built-in game mode scoring action.
        /// There are no definitions to this action.
        [WorkshopCodeName("ENABLE BUILT-IN GAME MODE SCORING")]
        public static void EnableBuiltInGameModeScoring() { }

        /// ENABLE DEATH SPECTATE ALL PLAYERS
        /// Allows one or more players to spectate all players when dead, as opposed to only allies.
        /// Definitions:
        /// 
        /// Player - The player or players who will be allowed to spectate all players. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("ENABLE DEATH SPECTATE ALL PLAYERS")]
        public static void EnableDeathSpectateAllPlayers() { }

        /// ENABLE DEATH SPECTATE TARGET HUD
        /// Allows one or more players to see their target’s HUD when dead instead of their own while death spectating.
        /// Definitions:
        /// 
        /// Player - The player or players who will begin seeing their spectate’s target’s hud while death spectating. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("ENABLE DEATH SPECTATE TARGET HUD")]
        public static void EnableDeathSpectateTargetHud() { }

        /// GO TO ASSEMBLE HEROES
        /// Go to the assemble heroes phase of the game mode. Only works if a game is in progress.
        /// There are no definitions to this action.
        [WorkshopCodeName("GO TO ASSEMBLE HEROES")]
        public static void GoToAssembleHeroes() { }

        /// HEAL
        /// Provides an instantaneous heal to one or more players. This heal will not resurrect dead players.
        /// Definitions:
        /// 
        /// Player - The player or players whose health will be restored. Can use most Player based Value Syntax for this value.
        /// Healer - The player who will receive credit for the healing. A healer of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        /// Amount - The amount of healing to apply. This amount may be modified by buffs or debuffs, healing is capped by each player’s max health. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("HEAL")]
        public static void Heal() { }

        /// KILL
        /// Instantly kills one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will be killed. Can use most Player based Value Syntax for this value.
        /// Killer - The player who will receive credit for the kill. A killer of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("KILL")]
        public static void Kill() { }

        /// LOOP
        /// Restarts the action list from the beginning. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// There are no definitions to this action.
        [WorkshopCodeName("LOOP")]
        public static void Loop() { }

        /// LOOP IF
        /// Restarts the action list from the beginning if this action’s condition evaluates to true. If it does not, execution continues with the next action. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// Definitions:
        /// 
        /// Condition - Specifies whether the loop will occur. Can use most Conditional based Value Syntax for this value.
        [WorkshopCodeName("LOOP IF")]
        public static void LoopIf() { }

        /// LOOP IF CONDITION IF FALSE
        /// Restarts the action list from the beginning if at least one condition in the condition list is false. If all conditions are true, execution continues with the next action. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// There are no definitions to this action.
        [WorkshopCodeName("LOOP IF CONDITION IF FALSE")]
        public static void LoopIfConditionIfFalse() { }

        /// LOOP IF CONDITION IF TRUE
        /// Restarts the action list from the beginning if all conditions in the condition list is true. If any are false, execution continues with the next action. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// There are no definitions to this action.
        [WorkshopCodeName("LOOP IF CONDITION IF TRUE")]
        public static void LoopIfConditionIfTrue() { }

        /// MODIFY GLOBAL VARIABLE
        /// Modifies the value of a global variable, which is a variable that belongs to the game itself.
        /// Definitions:
        /// 
        /// Variable - Variable specified by a single alphabetic letter (A through Z).
        /// Operation - The way in which the variable’s value will be changed. Options include standard arithmetic operations as well as array operations for appending and removing values.
        /// Value - The value used for the modification. For arithmetic operations, this is the second of two operands, with the other being the variable’s existing value. For array operations, this is the value to append or remove. Various Value Syntax can be used.
        [WorkshopCodeName("MODIFY GLOBAL VARIABLE")]
        public static void ModifyGlobalVariable() { }

        /// MODIFY PLAYER SCORE
        /// Modifies the score (kill count) of one or more players. This action only has an effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Player - The player whose score will change. Can use most Player based Value Syntax for this value.
        /// Score - The amount the score will increase or decrease. If positive, the score will increase. If negative, the score will decrease. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("MODIFY PLAYER SCORE")]
        public static void ModifyPlayerScore() { }

        /// MODIFY PLAYER VARIABLE
        /// Modifies the value of a player variable, which is a variable that belongs to a specific player.
        /// Definitions:
        /// 
        /// Player - The player whose variable will be modified. If multiple players are provided, each of their variables will be set. Can use most Player based Value Syntax for this value.
        /// Variable - Variable specified by a single alphabetic letter (A through Z).
        /// Operation - The way in which the variable’s value will be changed. Options include standard arithmetic operations as well as array operations for appending and removing values.
        /// Value - The value used for the modification. For arithmetic operations, this is the second of two operands, with the other being the variable’s existing value. For array operations, this is the value to append or remove. Various Value Syntax can be used.
        [WorkshopCodeName("MODIFY PLAYER VARIABLE")]
        public static void ModifyPlayerVariable() { }

        /// MODIFY TEAM SCORE
        /// Modifies the score of one or both teams. This action has not effect in free-for-all modes or modes without a team score.
        /// Definitions:
        /// 
        /// Team - The team whose score will be changed. Can use most Player based Value Syntax for this value.
        /// Score - The amount the score will increase or decrease. If positive, the score will increase. If negative, the score will decrease. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("MODIFY TEAM SCORE")]
        public static void ModifyTeamScore() { }

        /// PAUSE MATCH TIME
        /// Pauses the match time, players, objective logic, and game mode advancement criteria are unaffected by the pause.
        /// There are no definitions to this action.
        [WorkshopCodeName("PAUSE MATCH TIME")]
        public static void PauseMatchTime() { }

        /// PLAY EFFECT
        /// Plays an effect at a position in the world. The lifetime of this effect is short, so it does not need to be updated or destroyed.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will be able to see the effect. Can use most Value Syntax to select one or multiple players.
        /// Type - The type of the effect to be created.
        /// 
        /// Good Explosion
        /// Bad Explosion
        /// Ring Explosion
        /// Good Pickup Effect
        /// Bad Pickup Effect
        /// Debuff Impact Sound
        /// Buff Impact Sound
        /// Ring Explosion Sound
        /// Buff Explosion Sound
        /// Explosion Sound
        /// 
        /// 
        /// Color - The color of the effect to be created. IF a particular team is chosen, the effect will either be red or blue, depending on whether the team is hostile to the viewer.
        /// Position - The effect’s position. If this value is a player, then the effect will move along with the player, otherwise, the value is interpreted as a position in the world. Can use most Player or Vector based Value Syntax.
        /// Radius - The effect’s radius in meters. Can use most Number based Value Syntax.
        [WorkshopCodeName("PLAY EFFECT")]
        public static void PlayEffect() { }

        /// PRELOAD HERO
        /// Preemptively loads the specified hero or heroes into memory using the skins of the specified player or players, available memory permitting. Useful whenever rapid hero changing is possible and the next hero is known.
        /// Definitions:
        /// 
        /// Player - The player or players who will begin preloading a hero or heroes. Only one preload hero action will be active at a time for a given player. Can use most Player based Value Syntax for this value.
        /// Hero - The hero or heroes to begin preloading for the specified player or players. When multiple heroes are specified in an array, the Heroes towards the beginning of the array are prioritized. Can use most Hero based Value Syntax for this value.
        [WorkshopCodeName("PRELOAD HERO")]
        public static void PreloadHero() { }

        /// PRESS BUTTON
        /// Forces one or more players to press a button virtually for a single frame.
        /// Definitions:
        /// 
        /// Player - The player or players for whom virtual button input will be forced. Can use most Player based Value Syntax for this value.
        /// Button - The button to be pressed.
        [WorkshopCodeName("PRESS BUTTON")]
        public static void PressButton() { }

        /// RESET PLAYER HERO AVAILABILITY
        /// Restores the list of heroes available to one or more players to the list specified by the game settings. If a player’s current hero becomes unavailable, the player is forced to choose a different hero and respawn at an appropriate spawn location.
        /// Definitions:
        /// 
        /// Player - The player or players whose hero list is being reset. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("RESET PLAYER HERO AVAILABILITY")]
        public static void ResetPlayerHeroAvailability() { }

        /// RESPAWN
        /// Respawns one or more players at an appropriate spawn location with full health, even if they were already alive.
        /// Definitions:
        /// 
        /// Player - The player or players to respawn. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("RESPAWN")]
        public static void Respawn() { }

        /// RESURRECT
        /// Instantly resurrects one or more players at the location they died with no transition.
        /// Definitions:
        /// 
        /// Player - The player or players who will be resurrected. Can use most Player based Value Syntax for this value.
        [WorkshopCodeName("RESURRECT")]
        public static void Resurrect() { }

        /// SET ABILITY 1 ENABLED
        /// Enables or disables ability 1 for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to ability 1 is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use ability 1. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        [WorkshopCodeName("SET ABILITY 1 ENABLED")]
        public static void SetAbility1Enabled() { }

        /// SET ABILITY 2 ENABLED
        /// Enables or disables ability 2 for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to ability 2 is affected. Expects a Boolean Value such as True, False, or Compare. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use ability 2. Can use most Boolean based Value Syntax.
        [WorkshopCodeName("SET ABILITY 2 ENABLED")]
        public static void SetAbility2Enabled() { }

        /// SET AIM SPEED
        /// Sets the aim speed of one or more players to a percentage of their normal aim speed.
        /// Definitions:
        /// 
        /// Player - The player or players whose aim will be set. Can use most Player based Value Syntax for this value.
        /// Turn Speed Percent - The percentage of normal aim speed to which the player or players will set their aim speed. Can use most Number based Value Syntax.
        [WorkshopCodeName("SET AIM SPEED")]
        public static void SetAimSpeed() { }

        /// SET DAMAGE DEALT
        /// Sets the damage dealt to one or more players of a percentage of their raw damage dealt. NOTE: Negative values do not heal enemies. Damage values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose damage dealt will be set. Can use most Player based Value Syntax for this value.
        /// Damage Dealt Percent - The percentage of raw damage dealt to which the player or players will set their damage dealt. Can use most Number based Value Syntax.
        [WorkshopCodeName("SET DAMAGE DEALT")]
        public static void SetDamageDealt() { }

        /// SET DAMAGE RECEIVED
        /// Sets the damage received of one or more players to a percentage of their raw damage received. NOTE: Negative values do not heal enemies. Damage values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose damage received will be set. Can use most Player based Value Syntax for this value.
        /// Damage Received Percent - The percentage of raw damage received to which the player or players will set their damage received. Can use most Number based Value Syntax.
        [WorkshopCodeName("SET DAMAGE RECEIVED")]
        public static void SetDamageReceived() { }

        /// SET FACING
        /// Sets the facing of one or more players to the specified direction.
        /// Definitions:
        /// 
        /// Player - The player or players whose facing will be set. Can use most Player based Value Syntax for this value.
        /// Direction - The unit direction in which the player or players will face. This value is normalized internally. Can use most Vector based Value Syntax.
        /// *Relative - Specifies direction is relative to world coordinates or the local coordinates of the player or players.
        [WorkshopCodeName("SET FACING")]
        public static void SetFacing() { }

        /// SET GLOBAL VARIABLE
        /// Stores a value into a global variable, which a variable that belongs to the game itself.
        /// Definitions:
        /// 
        /// Variable - Specifies which Global Variable to store the value into. Specified by a single alphabetic letter (A through Z).
        /// Value - The value that will be stored. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        [WorkshopCodeName("SET GLOBAL VARIABLE")]
        public static void SetGlobalVariable() { }

        /// SET GLOBAL VARIABLE INDEX
        /// Finds or creates an array on a global variable, which is a variable that belongs to the game itself, then stores a value in the array at the specified index.
        /// Definitions:
        /// 
        /// Variable - Specifies which global variable’s value is the array to modify, if the variable’s value is not an array, then its value becomes an empty array. Specified by a single alphabetic letter (A through Z).
        /// Index - The index of the array to modify. If the index is beyond the end of the array, the array is extended with the new elements given a value of zero. Can use most Number based Value Syntax with this value.
        /// Value - The value that will be stored into the array. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        [WorkshopCodeName("SET GLOBAL VARIABLE INDEX")]
        public static void SetGlobalVariableIndex() { }

        /// SET GRAVITY
        /// Sets the movement gravity for one or more players to a percentage of regular movement gravity.
        /// Definitions:
        /// 
        /// Player - The player or players whose movement gravity will be set. Can use most Player based Value Syntax with this value.
        /// Gravity Percent - The percentage of regular movement gravity to which the player or players will set their personal movement gravity. Can use most Number based Value Sytax with this value. 100% is the normal gravity level of the game. Less than that will decrease gravity (allowing higher jumps), higher amounts will result in higher gravity (causing shorter jumps).
        [WorkshopCodeName("SET GRAVITY")]
        public static void SetGravity() { }

        /// SET HEALING DEALT
        /// Sets the healing dealt to one or more players of a percentage of their raw damage dealt. NOTE: Negative values do not damage enemies. Healing values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose healing dealt will be set. Can use most Player based Value Syntax for this value.
        /// Healing Dealt Percent - The percentage of raw healing dealt to which the player or players will set their healing dealt. Can use most Number based Value Syntax.
        [WorkshopCodeName("SET HEALING DEALT")]
        public static void SetHealingDealt() { }

        /// SET HEALING RECEIVED
        /// Sets the healing received of one or more players to a percentage of their raw healing received. NOTE: Negative values do not damage enemies. Healing values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose healing received will be set. Can use most Player based Value Syntax for this value.
        /// Healing Received Percent - The percentage of raw healing received to which the player or players will set their healing received. Can use most Number based Value Syntax.
        [WorkshopCodeName("SET HEALING RECEIVED")]
        public static void SetHealingReceived() { }

        /// SET INVISIBLE
        /// Causes one or more players to become invisible to either all other players or just enemies.
        /// Definitions:
        /// 
        /// Player - The player or players who will become invisible. Can use most Player based Value Syntax for this value.
        /// Invisible to - Specifies for whom the player or players will be invisible. Can be set to All, Enemies, or None.
        [WorkshopCodeName("SET INVISIBLE")]
        public static void SetInvisible() { }

        /// SET MATCH TIME
        /// Sets the current match time (which is visible at the top of the screen). This can be used to shorten or extend the duration of a match or to change the duration of assemble heroes or setup.
        /// Definitions:
        /// 
        /// Time - The match time in seconds. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET MATCH TIME")]
        public static void SetMatchTime() { }

        /// SET MAX HEALTH
        /// Sets the max health of one or more players as a percentage of their raw max health. This action will ensure that a player’s current health will not exceed the new max health.
        /// Definitions:
        /// 
        /// Player - The player or players whose max health will be set. Can use most Player based Value Syntax for this value.
        /// Health Percent - The percentage of raw max health to which the player or players will set their max health. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET MAX HEALTH")]
        public static void SetMaxHealth() { }

        /// SET MOVE SPEED
        /// Sets the move speed of one or more players to a percentage of their raw move speed.
        /// Definitions:
        /// 
        /// Player - The player or players whose move speed will be set. Can use most Player based Value Syntax for this value.
        /// Health Percent - The percentage of raw move speed to which the player or players will set their move speed. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET MOVE SPEED")]
        public static void SetMoveSpeed() { }

        /// SET OBJECTIVE DESCRIPTION
        /// Sets the text at the top center of the screen that normally describes the objective to a message visible to specific players.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the message. Can use most Number based Player Syntax for this value.
        /// Header - The message to be displayed. Can use most String based Value Syntax for this value.
        /// Reevaluation - Specifies which of this action’s inputs will be continously reevaluated. The message will keep asking for and using new values from reevaluated inputs. Can choose “Visible to and String” or “String”
        [WorkshopCodeName("SET OBJECTIVE DESCRIPTION")]
        public static void SetObjectiveDescription() { }

        /// SET PLAYER ALLOWED HEROES
        /// Sets the list of heroes available to one or more players. If a player’s current hero becomes unavailable, the player is forced to choose a different hero and respawn at an appropriate spawn location.
        /// Definitions:
        /// 
        /// Player - The player or players whose hero list is being set. Can use most Player based Value Syntax for this value.
        /// Hero - The hero or heroes that will be available. If no heroes are provided, the action has no effect. Can use most Hero based Value Syntax for this value including compatible Arrays.
        [WorkshopCodeName("SET PLAYER ALLOWED HEROES")]
        public static void SetPlayerAllowedHeroes() { }

        /// SET PLAYER SCORE
        /// Sets the score (kill count) of one or more players. This action only has an effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Player - The player or players whose score will be set. Can use most Player based Value Syntax for this value.
        /// Score - The score that will be set. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET PLAYER SCORE")]
        public static void SetPlayerScore() { }

        /// SET PLAYER VARIABLE
        /// Stores a value into a player variable, which is a variable that belongs to a specific player.
        /// Definitions:
        /// 
        /// Player - The player or players whose variable will be set. If multiple players are provided, each of their variables will be set. Can use most Player based Value Syntax for this value.
        /// Variable - Specifies which Player Variable to store the value into. Specified by a single alphabetic letter (A through Z).
        /// Value - The value that will be stored. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        [WorkshopCodeName("SET PLAYER VARIABLE")]
        public static void SetPlayerVariable() { }

        /// SET PLAYER VARIABLE AT INDEX
        /// Finds or creates an array on a player variable, which is a variable that belongs to a specific player, then stores a value in the array at the specified index.
        /// Definitions:
        /// 
        /// Player - The player or players whose variable will be set. If multiple players are provided, each of their variables will be set. Can use most Player based Value Syntax for this value.
        /// Variable - Specifies which player variable’s value is the array to modify, if the variable’s value is not an array, then its value becomes an empty array. Specified by a single alphabetic letter (A through Z).
        /// Index - The index of the array to modify. If the index is beyond the end of the array, the array is extended with the new elements given a value of zero. Can use most Number based Value Syntax with this value.
        /// Value - The value that will be stored into the array. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        [WorkshopCodeName("SET PLAYER VARIABLE AT INDEX")]
        public static void SetPlayerVariableAtIndex() { }

        /// SET PRIMARY FIRE ENABLED
        /// Enables or disables primary fire for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to primary fire is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use primary fire. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        [WorkshopCodeName("SET PRIMARY FIRE ENABLED")]
        public static void SetPrimaryFireEnabled() { }

        /// SET PROJECTILE GRAVITY
        /// Sets the projectile gravity for one or more players to a percentage of regular projectile gravity.
        /// Definitions:
        /// 
        /// Player - The player or players whose projectile gravity will be set. Can use most Player based Value Syntax for this value.
        /// Projectile Gravity Percent - The percentage of the regular projectile gravity to which the player or players will set their personal projectile gravity. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET PROJECTILE GRAVITY")]
        public static void SetProjectileGravity() { }

        /// SET PROJECTILE SPEED
        /// Sets the projectile speed for one or more players to a percentage of regular projectile speed.
        /// Definitions:
        /// 
        /// Player - The player or players whose projectile speed will be set. Can use most Player based Value Syntax for this value.
        /// Projectile Speed Percent - The percentage of the regular projectile speed to which the player or players will set their personal projectile speed. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET PROJECTILE SPEED")]
        public static void SetProjectileSpeed() { }

        /// SET RESPAWN MAX TIME
        /// Sets the duration between death and respawn for one or more players that are already dead when this action is executed, the change takes effect on their next death.
        /// Definitions:
        /// 
        /// Player - The player or players whose respawn max time will is being defined. Can use most Player based Value Syntax for this value.
        /// Time - The duration between death and respawn in seconds. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET RESPAWN MAX TIME")]
        public static void SetRespawnMaxTime() { }

        /// SET SECONDARY FIRE ENABLED
        /// Enables or disables secondary fire for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to secondary fire is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use secondary fire. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        [WorkshopCodeName("SET SECONDARY FIRE ENABLED")]
        public static void SetSecondaryFireEnabled() { }

        /// SET SLOW MOTION
        /// Sets the simulation rate for the entire game, including all players, projectiles, effects, and game mode logic.
        /// Definitions:
        /// 
        /// Speed Percent - The simulation rate as a percentage of normal speed. Only rates up to 100% are allowed. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET SLOW MOTION")]
        public static void SetSlowMotion() { }

        /// SET STATUS
        /// Applies a status to one or more players. This status will remain in effect for the specified duration or until it is cleared by the clear status action.
        /// Definitions:
        /// 
        /// Player - The player or players to whom the status will be applied. Can use most Player based Value Syntax for this value.
        /// Assister - Specifies a player to be awarded assist credit should the affected player or players be killed while the status is in effect. An assister of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        /// Status - The Status to be applied from the player or players. These behave similarly to statuses applied from hero abilities. Values include Hacked, Burning, Knocked Down, Asleep, Frozen, Unkillable, Invincible, Phased Out, Rooted, or Stunned.
        /// Duration - The duration of the status effect in seconds. To have a status that lasts until a clear status action is executed, proivide an arbitrarily long duration such as 9999. Can use most Number based Value Syntax.
        [WorkshopCodeName("SET STATUS")]
        public static void SetStatus() { }

        /// SET TEAM SCORE
        /// Sets the score for one or both teams. This action has no effect in free-for-all modes or modes without a team score.
        /// Definitions:
        /// 
        /// Team - The team or teams whose score will be set. Can use most Team based Value Syntax for this value.
        /// Score - The score that will be set. Can use most Number based Value Syntax for this value.
        [WorkshopCodeName("SET TEAM SCORE")]
        public static void SetTeamScore() { }

        /// SET ULTIMATE ABILITY ENABLED
        /// Enables or disables the ultimate ability of one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to their ultimate ability is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use their ultimate ability. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        [WorkshopCodeName("SET ULTIMATE ABILITY ENABLED")]
        public static void SetUltimateAbilityEnabled() { }

        /// SET ULTIMATE CHARGE
        /// Sets the ultimate charge or one or more players as a percentage of maximum charge.
        /// Definitions:
        /// 
        /// Player - The player or players whose ultimate charge will be set. Can use most Player based Value Syntax for this value.
        /// Charge Percent - The percentage of maximum charge. Can use most Number based Value Syntax.
        [WorkshopCodeName("SET ULTIMATE CHARGE")]
        public static void SetUltimateCharge() { }

        /// SKIP
        /// Skips execution of a certain number of actions in the action list.
        /// Definitions:
        /// 
        /// Number of actions - The number of action to skip, not including this action. Can use most Number based Value Syntax.
        [WorkshopCodeName("SKIP")]
        public static void Skip() { }

        /// SKIP IF
        /// Skips execution of a certain number of actions in the action list if this action’s condition evaluates to true. If it does not, execution continues with the next action.
        /// Definitions:
        /// 
        /// Condition - Specifies whether the loop will occur. Can use most Conditional based Value Syntax for this value.
        /// Number of actions - The number of action to skip, not including this action. Can use most Number based Value Syntax.
        [WorkshopCodeName("SKIP IF")]
        public static void SkipIf() { }

        /// SMALL MESSAGE
        /// Displays a small message beneath the reticle that is visible to specific players.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the message. Can use most Player based Value Syntax.
        /// Header - The message to be displayed. Can use most String based Value Syntax to specify.
        [WorkshopCodeName("SMALL MESSAGE")]
        public static void SmallMessage() { }

        /// START ACCELERATING
        /// Starts accelerating one or more players in a specified location.
        /// Definitions:
        /// 
        /// Player - The player or players that will begin accelerating. Can use most Player based Value Syntax.
        /// Direction - The unit direction in which the acceleration will be applied. This value is normalized internally. Can use most Vector based Value Syntax to specify.
        /// Rate - The rate of acceleration in meters per second squared. This value may need to be quite high in order to overcome gravity and/or surface friction. Can use most Number based Value Syntax.
        /// Max Speed - The speed at which acceleration will stop for the player or players. It may not be possible to reach this speed due to gravity and/or surface friction. Can use most Number based Value Syntax.
        /// Relative - Specifies whether direction is relavtive to the world coordinates or the local coordinates of the player or players.
        /// Reevaluation - Specifies which of this actions inputs will be continously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can choose from “Direction, Rate, and Max Speed” or “None”.
        [WorkshopCodeName("START ACCELERATING")]
        public static void StartAccelerating() { }

        /// START CAMERA
        /// Places your camera at a location, facing a direction.
        /// Definitions:
        /// 
        /// Player - The player executing this rule. As specified by the event, may be the same as the attacker or the victim. Can use most Player based Value Syntax.
        /// Eye Position - The position of the camrea, reevaluates continously. Can use most Vector based Value Syntax to specify.
        /// Look at position - Where the camera looks at, reevaluates continously. Can use most Vector based Value Syntax to specify.
        /// Blend Speed - How fast to blend the camera movement as positions change. 0 means do not blend at all and just change positions instantly. Can use most Number based Value Syntax.
        [WorkshopCodeName("START CAMERA")]
        public static void StartCamera() { }

        /// START DAMAGE MODIFICATION
        /// Starts modifying how much damage one or more receivers will receive from one or more damagers. A reference to this damage modification can be obtained from the last damage modification ID value. This action will fail if too many damage modifications have been started.
        /// Definitions:
        /// 
        /// Receivers - The player or players whose incoming damage will be modified. Can use most Player based Value Syntax.
        /// Damagers - The player or players whose outgoing damage will be modified (when attacking the receivers). Can use most Player based Value Syntax.
        /// Damage Percent - The percentage of damage that will apply to receivers when attacked by damagers. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continously reevaluated. This action will keep asking for and using new values from reevaluated inputs. Can choose from “Receivers, Damagers, and Damage Percent”, “Receivers and Damagers”, or “None”.
        [WorkshopCodeName("START DAMAGE MODIFICATION")]
        public static void StartDamageModification() { }

        /// START DAMAGE OVER TIME
        /// Starts an instance of damage over time, this DOT will persist for the specified duration or until stopped by script. To obtain a reference to this DOT, use the last damage over time to value.
        /// Definitions:
        /// 
        /// Receivers - One or more players who will receive the damage over time. Can use most Player based Value Syntax.
        /// Damager - The player who will receive credit for the damage. A damager of null indicates no player will receive credit. Can use most Player based Value Syntax.
        /// Duration - The duration of the damage over time in seconds. To have a DOT that lasts until stopped by script, provide an arbitrarily long duration such as 9999. Can use most Number based Value Syntax to specify.
        /// Damage Per Second - The damage per second for the damage over time. Can use most Number based Value Syntax to specify.
        [WorkshopCodeName("START DAMAGE OVER TIME")]
        public static void StartDamageOverTime() { }

        /// START FACING
        /// Starts turning one or more players to face the specified direction.
        /// Definitions:
        /// 
        /// Player - The player or players who will start turning. Can use most Player based Value Syntax.
        /// Direction - The unit direction in which the player or players will eventually face. Can use most Vector based Value Syntax.
        /// Turn Rate - The turn rate in degrees per second. Can use most Number based Value Syntax to specify.
        /// Relative - Specifies whether direction is relative to the world coordinates or the local coordinates of the player or players.
        /// Reevaluation - Specifies which of this actions inputs will be continously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can choose from “Direction and Turn Rate” or “None”.
        [WorkshopCodeName("START FACING")]
        public static void StartFacing() { }

        /// START FORCING PLAYER TO BE HERO
        /// Starts forcing one or more players to be a specified hero and, if necessary, respawns them immediately in their current locaiton. This will be the only hero available to the player or players until, the stop forcing player to be hero action is executed.
        /// Definitions:
        /// 
        /// Player - The player or players who will be forced to be a specified hero. Can use most Player based Value Syntax.
        /// Hero - The hero that the player or players will be forced to be. Can use most Hero based Value Syntax.
        [WorkshopCodeName("START FORCING PLAYER TO BE HERO")]
        public static void StartForcingPlayerToBeHero() { }

        /// START FORCING SPAWN ROOM
        /// Forces a team to spawn in a particular spawn room, regardless of the sapwn room normally used by the game mode. This action only has an effect in Assault, Hybrid, and Payload Maps.
        /// Definitions:
        /// 
        /// Team - The team whose spawn room will be forced. Can use most Team based Value Syntax.
        /// Room - The number of the spawn room to be forced. 0 is the first spawn room, 1 is the second, and 2 is the third. If this specified spawn room does not exist. Players will use the normal spawn room. Can use most Number based Value Syntax.
        [WorkshopCodeName("START FORCING SPAWN ROOM")]
        public static void StartForcingSpawnRoom() { }

        /// START FORCING THROTTLE
        /// Defines minimum and maximum movement input values for one or more players. Possibly forcing or preventing movement.
        /// Definitions:
        /// 
        /// Player - The player or players whose movement whill be forced or limited. Can use most Player based Value Syntax.
        /// Min Forward - Sets the minimum run forward amount. 0 allows the player or players to stop while 1 forces full forward movement. Can use most Number based Value Syntax.
        /// Max Forward - Sets the maximum run forward amount. 0 allows the player or players to stop while 1 forces full forward movement. Can use most Number based Value Syntax.
        /// Min Backward - Sets the minimum run backward amount. 0 allows the player or players to stop while 1 forces full forward movement. Can use most Number based Value Syntax.
        /// Max Backward - Sets the maximum run backward amount. 0 allows the player or players to stop while 1 forces full forward movement. Can use most Number based Value Syntax.
        /// Min Sideways - Sets the minimum run sideways amount. 0 allows the player or players to stop while 1 forces full forward movement. Can use most Number based Value Syntax.
        /// Max Forward - Sets the maximum run sideways amount. 0 allows the player or players to stop while 1 forces full forward movement. Can use most Number based Value Syntax.
        [WorkshopCodeName("START FORCING THROTTLE")]
        public static void StartForcingThrottle() { }

        /// START HEAL OVER TIME
        /// Starts an instance of damage over time, this HOT will persist for the specified duration or until stopped by script. To obtain a reference to this HOT, use the last damage over time to value.
        /// Definitions:
        /// 
        /// Player - One or more players who will receive the heal over time. Can use most Player based Value Syntax.
        /// Healer - The player who will receive credit for the heal. A damager of null indicates no player will receive credit. Can use most Player based Value Syntax.
        /// Duration - The duration of the heal over time in seconds. To have a HOT that lasts until stopped by script, provide an arbitrarily long duration such as 9999. Can use most Number based Value Syntax to specify.
        /// Damage Per Second - The heal per second for the heal over time. Can use most Number based Value Syntax to specify.
        [WorkshopCodeName("START HEAL OVER TIME")]
        public static void StartHealOverTime() { }

        /// START HOLDING BUTTON
        /// Forces one or more players to hold a button virtually until stopped by the stop holding button action.
        /// Definitions:
        /// 
        /// Player - The player or players who are holding a button virtually. Can use most Player based Value Syntax.
        /// Button - The logical button that is being held virtually.
        [WorkshopCodeName("START HOLDING BUTTON")]
        public static void StartHoldingButton() { }

        /// STOP ACCELERATING
        /// Stops the acceleration started by the start accelerating action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will stop accelerating. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP ACCELERATING")]
        public static void StopAccelerating() { }

        /// STOP ALL DAMAGE MODIFICATIONS
        /// Stops the all damage modifications that were started using the start damage modification action.
        /// There are no definitions to this action.
        [WorkshopCodeName("STOP ALL DAMAGE MODIFICATIONS")]
        public static void StopAllDamageModifications() { }

        /// STOP ALL DAMAGE OVER TIME
        /// Stops all damage over time started by the start damage over time or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose scripted damage over time will stop. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP ALL DAMAGE OVER TIME")]
        public static void StopAllDamageOverTime() { }

        /// STOP ALL HEAL OVER TIME
        /// Stops all heal over time started by the start heal over time or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose scripted heal over time will stop. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP ALL HEAL OVER TIME")]
        public static void StopAllHealOverTime() { }

        /// STOP CAMERA
        /// Stops all forced camera positions started by the start camera or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose forced camera positions will stop. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP CAMERA")]
        public static void StopCamera() { }

        /// STOP CHASING GLOBAL VARIABLE
        /// Stops an in-progress chase of a global variable, leaving it at its current value.
        /// Definitions:
        /// 
        /// Variable - Specifies which global variable to stop modifying. Specified by a single alphabetic letter (A through Z).
        [WorkshopCodeName("STOP CHASING GLOBAL VARIABLE")]
        public static void StopChasingGlobalVariable() { }

        /// STOP CHASING PLAYER VARIABLE
        /// Stops an in-progress chase of a player variable, leaving it at its current value.
        /// Definitions:
        /// 
        /// Player - The player whose variable will stop changing. If multiple players are provided, each of their variables will stop changing. Can use most Player based Value Syntax.
        /// Variable - Specifies which player variable to stop modifying. Specified by a single alphabetic letter (A through Z).
        [WorkshopCodeName("STOP CHASING PLAYER VARIABLE")]
        public static void StopChasingPlayerVariable() { }

        /// STOP DAMAGE MODIFICATION
        /// Stops a damage modification that was started by the start damage modification action
        /// Definitions:
        /// 
        /// Damage modification ID - Specifies which damage modification instance to stop, this ID may be the last damage modification ID or a variable into which last damage modification ID was earlier stored. Can use most Number based Value Syntax.
        [WorkshopCodeName("STOP DAMAGE MODIFICATION")]
        public static void StopDamageModification() { }

        /// STOP DAMAGE OVER TIME
        /// Stops an instance of damage over time that was started by the start damage over time action
        /// Definitions:
        /// 
        /// Damage Over Time ID - Specifies which damage over time instance to stop, this ID may be the last damage over time ID or a variable into which last damage over time ID was earlier stored. Can use most Number based Value Syntax.
        [WorkshopCodeName("STOP DAMAGE OVER TIME")]
        public static void StopDamageOverTime() { }

        /// STOP FACING
        /// Stops the turning started by the start facing action for or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will stop turning. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP FACING")]
        public static void StopFacing() { }

        /// STOP FORCING PLAYER TO BE HERO
        /// Stops forcing one or more players to be a specified hero. This will not respawn the player or players, but it will restore their availablity the next time they go to select a hero.
        /// Definitions:
        /// 
        /// Player - The player or players who will no longer be forced to be a specific hero. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP FORCING PLAYER TO BE HERO")]
        public static void StopForcingPlayerToBeHero() { }

        /// STOP FORCING SPAWN ROOM
        /// Undoes the effect of start forcing spawn room action for the specified team.
        /// Definitions:
        /// 
        /// Team - The team that will resume using their normal spawn room. Can use most Team based Value Syntax.
        [WorkshopCodeName("STOP FORCING SPAWN ROOM")]
        public static void StopForcingSpawnRoom() { }

        /// STOP FORCING THROTTLE
        /// Undoes the effect of start forcing throttle action for one or more players
        /// Definitions:
        /// 
        /// Player - The player or players whose movement inout will be restored. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP FORCING THROTTLE")]
        public static void StopForcingThrottle() { }

        /// STOP HEAL OVER TIME
        /// Stops an instance of heal over time that was started by the start heal over time action
        /// Definitions:
        /// 
        /// Heal Over Time ID - Specifies which heal over time instance to stop, this ID may be the last heal over time ID or a variable into which last heal over time ID was earlier stored. Can use most Number based Value Syntax.
        [WorkshopCodeName("STOP HEAL OVER TIME")]
        public static void StopHealOverTime() { }

        /// STOP HOLDING BUTTON
        /// Undoes the effect of the start holding button action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who are no longer holding a button virtually. Can use most Player based Value Syntax.
        [WorkshopCodeName("STOP HOLDING BUTTON")]
        public static void StopHoldingButton() { }

        /// TELEPORT
        /// Teleports one or more players to the specified location.
        /// Definitions:
        /// 
        /// Player - The player or players to teleport. Can use most Player based Value Syntax.
        /// Position - The position to which the player or players will teleport. If a player is providedm the position of the player is used. Can use most Vector based Value Syntax.
        [WorkshopCodeName("TELEPORT")]
        public static void Teleport() { }

        /// UNPAUSE MATCH TIME
        /// Unpauses the match time.
        /// There are no definitions to this action.
        [WorkshopCodeName("UNPAUSE MATCH TIME")]
        public static void UnpauseMatchTime() { }

        /// WAIT
        /// Pauses the execution of the action list, unless the wait is interrupted. The remainder of the actions will execute after the pause.
        /// Definitions:
        /// 
        /// Time - The duration of the pause. A minimum value of 0.250 seconds is required. Can use most Number based Value Syntax.
        /// Wait Behavior - Specifies if and how the wait can be interrupted. If the condition list is ignored, the wait will not be interrupted, otherwise, the condition list will determine if and when the action list will abort or restart.
        // [WorkshopCodeName("WAIT")]
        // public static void Wait() { }
    }
}
