using System;

namespace Workshop
{
    public static class Actions
    {
        /// <summary>
        /// ABORT
        /// Stops execution of the action list.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("ABORT")]
        public static void Abort() { }

        /// <summary>
        /// ABORT IF
        /// Stops execution of the action list if the action’s condition evaluates to true, if it does not, the execution continues with the next action.
        /// Definitions:
        /// 
        /// Condition - Specifies whether the execution is stopped. Can use most Boolean based Value Syntax.
        /// </summary>
        [WorkshopCode("ABORT IF")]
        public static void AbortIf() { }

        /// <summary>
        /// ABORT IF CONDITION IS FALSE
        /// Stops execution of the action list if at least one condition in the condition list is false. If all conditions are true, execution continues with the next action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("ABORT IF CONDITION IS FALSE")]
        public static void AbortIfConditionIsFalse() { }

        /// <summary>
        /// ABORT IF CONDITION IS TRUE
        /// Stops execution of the action list if all conditions in the condition list is true. If any are false, execution continues with the next action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("ABORT IF CONDITION IS TRUE")]
        public static void AbortIfConditionIsTrue() { }

        /// <summary>
        /// ALLOW BUTTON
        /// Undoes the effect of the disallow button action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose button is being reenabled. Can use most Player based Value Syntax.
        /// Button - The logical button that is being reenabled.
        /// </summary>
        [WorkshopCode("ALLOW BUTTON")]
        public static void AllowButton() { }

        /// <summary>
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
        /// </summary>
        [WorkshopCode("APPLY IMPLUSE")]
        public static void ApplyImpluse() { }

        /// <summary>
        /// BIG MESSAGE
        /// Displays a large message above the reticle that is visible to specific players.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the message. Can use most Value Syntax to select multiple players to specify.
        /// Header - The message to be displayed. Can use most String based Value Syntax to specify.
        /// </summary>
        [WorkshopCode("BIG MESSAGE", true)]
        public static void BigMessage(Array<Player> playersVisibleTo, StringValue header) {}

        /// <summary>
        /// CHASE GLOBAL VARIABLE AT RATE
        /// Gradually modifies the value of a global variable at a specific rate. (A global variable is a variable that belongs to the game itself.)
        /// Definitions:
        /// 
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the global variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Rate - The amount of change that will happen to the variable’s value each second. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Rate or nothing.
        /// </summary>
        [WorkshopCode("CHASE GLOBAL VARIABLE AT RATE")]
        public static void ChaseGlobalVariableAtRate() { }

        /// <summary>
        /// CHASE GLOBAL VARIABLE OVER TIME
        /// Gradually modifies the value of a global variable over time. (A global variable is a variable that belongs to the game itself.)
        /// Definitions:
        /// 
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the global variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Duration - The amount of time, in seconds, over which the variable’s value will approach the destination. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Duration or nothing.
        /// </summary>
        [WorkshopCode("CHASE GLOBAL VARIABLE OVER TIME")]
        public static void ChaseGlobalVariableOverTime() { }

        /// <summary>
        /// CHASE PLAYER VARIABLE AT RATE
        /// Gradually modifies the value of a player variable at a specific rate. (A player variable is a variable that belongs to a specific player.)
        /// Definitions:
        /// 
        /// Player - The player whose variable will gradually change. If multiple players are provided, each of their variables will change independently.
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the player variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Rate - The amount of change that will happen to the variable’s value each second. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Rate or nothing.
        /// </summary>
        [WorkshopCode("CHASE PLAYER VARIABLE AT RATE")]
        public static void ChasePlayerVariableAtRate() { }

        /// <summary>
        /// CHASE PLAYER VARIABLE OVER TIME
        /// Gradually modifies the value of a player variable over time. (A player variable is a variable that belongs to a specific player.)
        /// Definitions:
        /// 
        /// Player - The player whose variable will gradually change. If multiple players are provided, each of their variables will change independently.
        /// Variable - The variable the action will manipulate. Can use most Variable based Value Syntax.
        /// Destination - The value that the player variable will eventually reach. The type of this value may be either a number or a vector, through the variable’s existing value must be of the same type before the chase begins. Can use most Number or Vector based Value Syntax to specify.
        /// Duration - The amount of time, in seconds, over which the variable’s value will approach the destination. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continuously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can specify a Destination and Duration or nothing.
        /// </summary>
        [WorkshopCode("CHASE PLAYER VARIABLE OVER TIME")]
        public static void ChasePlayerVariableOverTime() { }

        /// <summary>
        /// CLEAR STATUS
        /// Clears a status that was applied from a set status action from one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players from whom the status will be removed. Can use most Player based Value Syntax.
        /// Status - The Status to be removed from the player or players. Values include Hacked, Burning, Knocked Down, Asleep, Frozen, Unkillable, Invincible, Phased Out, Rooted, or Stunned.
        /// </summary>
        [WorkshopCode("CLEAR STATUS")]
        public static void ClearStatus() { }

        /// <summary>
        /// COMMUNICATE
        /// Causes one or more players to use an emote, voice line, or other equipped communication.
        /// Definitions:
        /// 
        /// Player - The player or players to perform the communication. Can use most Player based Value Syntax.
        /// Type - The type of communication. Can use any equipped emote, equipped voice line, or any other communication effect.
        /// </summary>
        [WorkshopCode("COMMUNICATE")]
        public static void Communicate() { }

        /// <summary>
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
        /// </summary>
        [WorkshopCode("CREATE EFFECT")]
        public static void CreateEffect(Array<Player> visibleTo, CreateEffectType type, Color color, Vector position, float radius, ReevaluationValue reevaluation) {}

        /// <summary>
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
        /// </summary>
        [WorkshopCode("CREATE HUD TEXT")]
        public static void CreateHudText() { }

        /// <summary>
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
        /// </summary>
        [WorkshopCode("CREATE ICON")]
        public static void CreateIcon() { }

        /// <summary>
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
        /// </summary>
        [WorkshopCode("CREATE IN-WORLD TEXT")]
        public static void CreateInWorldText() { }

        /// <summary>
        /// DAMAGE
        /// Applies instantaneous damage to one or more players, possibly killing the players.
        /// Definitions:
        /// 
        /// Player - The player or players who will receive damage. Can use most Player based Value Syntax to select one or multiple players.
        /// Damager - The player who will receive credit for the damage. A damager of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        /// Amount - The amount of damage to apply. This amount may be modified by buffs, debuffs, or armor. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("DAMAGE")]
        public static void Damage(Player player, Player? damager, float amount) { }

        /// <summary>
        /// DECLARE MATCH DRAW
        /// Instantly ends the match in a draw. This action has no effect in free-for-all modes.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DECLARE MATCH DRAW")]
        public static void DeclareMatchDraw() { }

        /// <summary>
        /// DECLARE PLAYER VICTORY
        /// Instantly ends the match with the specific player as the winner. This action only has an effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Player - The winning player. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("DECLARE PLAYER VICTORY")]
        public static void DeclarePlayerVictory() { }

        /// <summary>
        /// DECLARE ROUND VICTORY
        /// Declare a team as the current round winner. This only works in the control and elimination game modes.
        /// Definitions:
        /// 
        /// Team - Round winning team. Can use most Team based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("DECLARE ROUND VICTORY")]
        public static void DeclareRoundVictory() { }

        /// <summary>
        /// DECLARE TEAM VICTORY
        /// Instantly ends the match with the specified team as the winner. This action has no effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Team - The winning team. Can use most Team based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("DECLARE TEAM VICTORY")]
        public static void DeclareTeamVictory() { }

        /// <summary>
        /// DESTROY ALL EFFECTS
        /// Destroys all effect entities created by create effect.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DESTROY ALL EFFECTS")]
        public static void DestroyAllEffects() { }

        /// <summary>
        /// DESTROY ALL ICONS
        /// Destroys all icon entities created by create icon.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DESTROY ALL ICONS")]
        public static void DestroyAllIcons() { }

        /// <summary>
        /// DESTROY ALL IN-WORLD TEXT
        /// Destroys all in-world text created by the create in-world effect.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DESTROY ALL IN-WORLD TEXT")]
        public static void DestroyAllInWorldText() { }

        /// <summary>
        /// DESTROY EFFECT
        /// Destroys an effect entity that was created by create effect.
        /// Definitions:
        /// 
        /// Entity - Specifies which effect entity to destroy. This entity may be the last created entity or a variable into which last created entity was earlier stored.
        /// </summary>
        [WorkshopCode("DESTROY EFFECT")]
        public static void DestroyEffect() { }

        /// <summary>
        /// DESTROY HUD TEXT
        /// Destroys hud text that was created by create hud text.
        /// Definitions:
        /// 
        /// Text ID - Specifies which hud text to destroy. This ID may be last text ID or a variable into which last text ID was earlier stored.
        /// </summary>
        [WorkshopCode("DESTROY HUD TEXT")]
        public static void DestroyHudText() { }

        /// <summary>
        /// DESTROY ICON
        /// Destroys an icon entity that was created by create icon.
        /// Definitions:
        /// 
        /// Text ID - Specifies which icon to destroy. This ID may be last text ID or a variable into which last create entity was earlier stored.
        /// </summary>
        [WorkshopCode("DESTROY ICON")]
        public static void DestroyIcon() { }

        /// <summary>
        /// DISABLE BUILT-IN GAME MODE ANNOUNCER
        /// Disables game mode announcements from the announcer until reenabled or the match ends.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DISABLE BUILT-IN GAME MODE ANNOUNCER")]
        public static void DisableBuiltInGameModeAnnouncer() { }

        /// <summary>
        /// DISABLE BUILT-IN GAME MODE COMPLETION
        /// Disables completion of the match from the game mode itself, only allowing the match to be completed by scripting commands.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DISABLE BUILT-IN GAME MODE COMPLETION")]
        public static void DisableBuiltInGameModeCompletion() { }

        /// <summary>
        /// DISABLE BUILT-IN GAME MODE MUSIC
        /// Disables all game-mode music until reenabled or the match ends.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DISABLE BUILT-IN GAME MODE MUSIC")]
        public static void DisableBuiltInGameModeMusic() { }

        /// <summary>
        /// DISABLE BUILT-IN GAME MODE RESPAWNING
        /// Disables automatic respawning for one or more players, only allowing respawning by scripting commands.
        /// Definitions:
        /// 
        /// Player - The player or players whose respawning is affected. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("DISABLE BUILT-IN GAME MODE RESPAWNING")]
        public static void DisableBuiltInGameModeRespawning() { }

        /// <summary>
        /// DISABLE BUILT-IN GAME MODE SCORING
        /// Disables changes to player and team scores from the game mode itself, only allowing scores to be changed by scripting commands.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("DISABLE BUILT-IN GAME MODE SCORING")]
        public static void DisableBuiltInGameModeScoring() { }

        /// <summary>
        /// DISABLE DEATH SPECTATE ALL PLAYERS
        /// Undoes the effect of the enable death spectate all players action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose default death spectate behavior is restored. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("DISABLE DEATH SPECTATE ALL PLAYERS")]
        public static void DisableDeathSpectateAllPlayers() { }

        /// <summary>
        /// DISABLE DEATH SPECTATE TARGET HUD
        /// Undoes the effect of the enable death spectate target hud action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will revert to seeing their own HUD while death spectating. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("DISABLE DEATH SPECTATE TARGET HUD")]
        public static void DisableDeathSpectateTargetHud() { }

        /// <summary>
        /// DISALLOW BUTTON
        /// Disables a logical button for one or more players such that pressing it has no effect.
        /// Definitions:
        /// 
        /// Player - The player executing this rule, as specified by the event. May be the same as the attacker or victim. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("DISALLOW BUTTON")]
        public static void DisallowButton() { }

        /// <summary>
        /// ENABLE BUILT-IN GAME MODE ANNOUNCER
        /// Undoes the effect of the disable built-in game mode announcer action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("ENABLE BUILT-IN GAME MODE ANNOUNCER")]
        public static void EnableBuiltInGameModeAnnouncer() { }

        /// <summary>
        /// ENABLE BUILT-IN GAME MODE COMPLETION
        /// Undoes the effect of the disable built-in game mode completion action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("ENABLE BUILT-IN GAME MODE COMPLETION")]
        public static void EnableBuiltInGameModeCompletion() { }

        /// <summary>
        /// ENABLE BUILT-IN GAME MODE MUSIC
        /// Undoes the effect of the disable built-in game mode music action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("ENABLE BUILT-IN GAME MODE MUSIC")]
        public static void EnableBuiltInGameModeMusic() { }

        /// <summary>
        /// ENABLE BUILT-IN GAME MODE RESPAWNING
        /// Undoes the effect of the disable built-in game mode respawning for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose respawning is affected. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("ENABLE BUILT-IN GAME MODE RESPAWNING")]
        public static void EnableBuiltInGameModeRespawning() { }

        /// <summary>
        /// ENABLE BUILT-IN GAME MODE SCORING
        /// Undoes the effect of the disable built-in game mode scoring action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("ENABLE BUILT-IN GAME MODE SCORING")]
        public static void EnableBuiltInGameModeScoring() { }

        /// <summary>
        /// ENABLE DEATH SPECTATE ALL PLAYERS
        /// Allows one or more players to spectate all players when dead, as opposed to only allies.
        /// Definitions:
        /// 
        /// Player - The player or players who will be allowed to spectate all players. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("ENABLE DEATH SPECTATE ALL PLAYERS")]
        public static void EnableDeathSpectateAllPlayers() { }

        /// <summary>
        /// ENABLE DEATH SPECTATE TARGET HUD
        /// Allows one or more players to see their target’s HUD when dead instead of their own while death spectating.
        /// Definitions:
        /// 
        /// Player - The player or players who will begin seeing their spectate’s target’s hud while death spectating. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("ENABLE DEATH SPECTATE TARGET HUD")]
        public static void EnableDeathSpectateTargetHud() { }

        /// <summary>
        /// GO TO ASSEMBLE HEROES
        /// Go to the assemble heroes phase of the game mode. Only works if a game is in progress.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("GO TO ASSEMBLE HEROES")]
        public static void GoToAssembleHeroes() { }

        /// <summary>
        /// HEAL
        /// Provides an instantaneous heal to one or more players. This heal will not resurrect dead players.
        /// Definitions:
        /// 
        /// Player - The player or players whose health will be restored. Can use most Player based Value Syntax for this value.
        /// Healer - The player who will receive credit for the healing. A healer of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        /// Amount - The amount of healing to apply. This amount may be modified by buffs or debuffs, healing is capped by each player’s max health. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("HEAL")]
        public static void Heal() { }

        /// <summary>
        /// KILL
        /// Instantly kills one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will be killed. Can use most Player based Value Syntax for this value.
        /// Killer - The player who will receive credit for the kill. A killer of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("KILL")]
        public static void Kill() { }

        /// <summary>
        /// LOOP
        /// Restarts the action list from the beginning. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("LOOP")]
        public static void Loop() { }

        /// <summary>
        /// LOOP IF
        /// Restarts the action list from the beginning if this action’s condition evaluates to true. If it does not, execution continues with the next action. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// Definitions:
        /// 
        /// Condition - Specifies whether the loop will occur. Can use most Conditional based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("LOOP IF")]
        public static void LoopIf() { }

        /// <summary>
        /// LOOP IF CONDITION IF FALSE
        /// Restarts the action list from the beginning if at least one condition in the condition list is false. If all conditions are true, execution continues with the next action. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("LOOP IF CONDITION IF FALSE")]
        public static void LoopIfConditionIfFalse() { }

        /// <summary>
        /// LOOP IF CONDITION IF TRUE
        /// Restarts the action list from the beginning if all conditions in the condition list is true. If any are false, execution continues with the next action. To prevent an infinite loop, a wait action must execute between the start of the action list and this action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("LOOP IF CONDITION IF TRUE")]
        public static void LoopIfConditionIfTrue() { }

        /// <summary>
        /// MODIFY GLOBAL VARIABLE
        /// Modifies the value of a global variable, which is a variable that belongs to the game itself.
        /// Definitions:
        /// 
        /// Variable - Variable specified by a single alphabetic letter (A through Z).
        /// Operation - The way in which the variable’s value will be changed. Options include standard arithmetic operations as well as array operations for appending and removing values.
        /// Value - The value used for the modification. For arithmetic operations, this is the second of two operands, with the other being the variable’s existing value. For array operations, this is the value to append or remove. Various Value Syntax can be used.
        /// </summary>
        [WorkshopCode("MODIFY GLOBAL VARIABLE")]
        public static void ModifyGlobalVariable() { }

        /// <summary>
        /// MODIFY PLAYER SCORE
        /// Modifies the score (kill count) of one or more players. This action only has an effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Player - The player whose score will change. Can use most Player based Value Syntax for this value.
        /// Score - The amount the score will increase or decrease. If positive, the score will increase. If negative, the score will decrease. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("MODIFY PLAYER SCORE")]
        public static void ModifyPlayerScore() { }

        /// <summary>
        /// MODIFY PLAYER VARIABLE
        /// Modifies the value of a player variable, which is a variable that belongs to a specific player.
        /// Definitions:
        /// 
        /// Player - The player whose variable will be modified. If multiple players are provided, each of their variables will be set. Can use most Player based Value Syntax for this value.
        /// Variable - Variable specified by a single alphabetic letter (A through Z).
        /// Operation - The way in which the variable’s value will be changed. Options include standard arithmetic operations as well as array operations for appending and removing values.
        /// Value - The value used for the modification. For arithmetic operations, this is the second of two operands, with the other being the variable’s existing value. For array operations, this is the value to append or remove. Various Value Syntax can be used.
        /// </summary>
        [WorkshopCode("MODIFY PLAYER VARIABLE")]
        public static void ModifyPlayerVariable() { }

        /// <summary>
        /// MODIFY TEAM SCORE
        /// Modifies the score of one or both teams. This action has not effect in free-for-all modes or modes without a team score.
        /// Definitions:
        /// 
        /// Team - The team whose score will be changed. Can use most Player based Value Syntax for this value.
        /// Score - The amount the score will increase or decrease. If positive, the score will increase. If negative, the score will decrease. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("MODIFY TEAM SCORE")]
        public static void ModifyTeamScore() { }

        /// <summary>
        /// PAUSE MATCH TIME
        /// Pauses the match time, players, objective logic, and game mode advancement criteria are unaffected by the pause.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("PAUSE MATCH TIME")]
        public static void PauseMatchTime() { }

        /// <summary>
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
        /// </summary>
        [WorkshopCode("PLAY EFFECT")]
        public static void PlayEffect(Array<Player> visibleTo, PlayEffectType type, Color color, Vector position, float radius) { }

        /// <summary>
        /// PRELOAD HERO
        /// Preemptively loads the specified hero or heroes into memory using the skins of the specified player or players, available memory permitting. Useful whenever rapid hero changing is possible and the next hero is known.
        /// Definitions:
        /// 
        /// Player - The player or players who will begin preloading a hero or heroes. Only one preload hero action will be active at a time for a given player. Can use most Player based Value Syntax for this value.
        /// Hero - The hero or heroes to begin preloading for the specified player or players. When multiple heroes are specified in an array, the Heroes towards the beginning of the array are prioritized. Can use most Hero based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("PRELOAD HERO")]
        public static void PreloadHero() { }

        /// <summary>
        /// PRESS BUTTON
        /// Forces one or more players to press a button virtually for a single frame.
        /// Definitions:
        /// 
        /// Player - The player or players for whom virtual button input will be forced. Can use most Player based Value Syntax for this value.
        /// Button - The button to be pressed.
        /// </summary>
        [WorkshopCode("PRESS BUTTON")]
        public static void PressButton() { }

        /// <summary>
        /// RESET PLAYER HERO AVAILABILITY
        /// Restores the list of heroes available to one or more players to the list specified by the game settings. If a player’s current hero becomes unavailable, the player is forced to choose a different hero and respawn at an appropriate spawn location.
        /// Definitions:
        /// 
        /// Player - The player or players whose hero list is being reset. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("RESET PLAYER HERO AVAILABILITY")]
        public static void ResetPlayerHeroAvailability() { }

        /// <summary>
        /// RESPAWN
        /// Respawns one or more players at an appropriate spawn location with full health, even if they were already alive.
        /// Definitions:
        /// 
        /// Player - The player or players to respawn. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("RESPAWN")]
        public static void Respawn() { }

        /// <summary>
        /// RESURRECT
        /// Instantly resurrects one or more players at the location they died with no transition.
        /// Definitions:
        /// 
        /// Player - The player or players who will be resurrected. Can use most Player based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("RESURRECT")]
        public static void Resurrect() { }

        /// <summary>
        /// SET ABILITY 1 ENABLED
        /// Enables or disables ability 1 for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to ability 1 is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use ability 1. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        /// </summary>
        [WorkshopCode("SET ABILITY 1 ENABLED")]
        public static void SetAbility1Enabled() { }

        /// <summary>
        /// SET ABILITY 2 ENABLED
        /// Enables or disables ability 2 for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to ability 2 is affected. Expects a Boolean Value such as True, False, or Compare. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use ability 2. Can use most Boolean based Value Syntax.
        /// </summary>
        [WorkshopCode("SET ABILITY 2 ENABLED")]
        public static void SetAbility2Enabled() { }

        /// <summary>
        /// SET AIM SPEED
        /// Sets the aim speed of one or more players to a percentage of their normal aim speed.
        /// Definitions:
        /// 
        /// Player - The player or players whose aim will be set. Can use most Player based Value Syntax for this value.
        /// Turn Speed Percent - The percentage of normal aim speed to which the player or players will set their aim speed. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SET AIM SPEED")]
        public static void SetAimSpeed() { }

        /// <summary>
        /// SET DAMAGE DEALT
        /// Sets the damage dealt to one or more players of a percentage of their raw damage dealt. NOTE: Negative values do not heal enemies. Damage values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose damage dealt will be set. Can use most Player based Value Syntax for this value.
        /// Damage Dealt Percent - The percentage of raw damage dealt to which the player or players will set their damage dealt. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SET DAMAGE DEALT")]
        public static void SetDamageDealt() { }

        /// <summary>
        /// SET DAMAGE RECEIVED
        /// Sets the damage received of one or more players to a percentage of their raw damage received. NOTE: Negative values do not heal enemies. Damage values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose damage received will be set. Can use most Player based Value Syntax for this value.
        /// Damage Received Percent - The percentage of raw damage received to which the player or players will set their damage received. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SET DAMAGE RECEIVED")]
        public static void SetDamageReceived() { }

        /// <summary>
        /// SET FACING
        /// Sets the facing of one or more players to the specified direction.
        /// Definitions:
        /// 
        /// Player - The player or players whose facing will be set. Can use most Player based Value Syntax for this value.
        /// Direction - The unit direction in which the player or players will face. This value is normalized internally. Can use most Vector based Value Syntax.
        /// *Relative - Specifies direction is relative to world coordinates or the local coordinates of the player or players.
        /// </summary>
        [WorkshopCode("SET FACING", true)]
        public static void SetFacing(Player player, Vector direction, RelativeTo relative) { }

        /// <summary>
        /// SET GLOBAL VARIABLE
        /// Stores a value into a global variable, which a variable that belongs to the game itself.
        /// Definitions:
        /// 
        /// Variable - Specifies which Global Variable to store the value into. Specified by a single alphabetic letter (A through Z).
        /// Value - The value that will be stored. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        /// </summary>
        [WorkshopCode("SET GLOBAL VARIABLE")]
        public static void SetGlobalVariable() { }

        /// <summary>
        /// SET GLOBAL VARIABLE INDEX
        /// Finds or creates an array on a global variable, which is a variable that belongs to the game itself, then stores a value in the array at the specified index.
        /// Definitions:
        /// 
        /// Variable - Specifies which global variable’s value is the array to modify, if the variable’s value is not an array, then its value becomes an empty array. Specified by a single alphabetic letter (A through Z).
        /// Index - The index of the array to modify. If the index is beyond the end of the array, the array is extended with the new elements given a value of zero. Can use most Number based Value Syntax with this value.
        /// Value - The value that will be stored into the array. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        /// </summary>
        [WorkshopCode("SET GLOBAL VARIABLE INDEX")]
        public static void SetGlobalVariableIndex() { }

        /// <summary>
        /// SET GRAVITY
        /// Sets the movement gravity for one or more players to a percentage of regular movement gravity.
        /// Definitions:
        /// 
        /// Player - The player or players whose movement gravity will be set. Can use most Player based Value Syntax with this value.
        /// Gravity Percent - The percentage of regular movement gravity to which the player or players will set their personal movement gravity. Can use most Number based Value Sytax with this value. 100% is the normal gravity level of the game. Less than that will decrease gravity (allowing higher jumps), higher amounts will result in higher gravity (causing shorter jumps).
        /// </summary>
        [WorkshopCode("SET GRAVITY")]
        public static void SetGravity() { }

        /// <summary>
        /// SET HEALING DEALT
        /// Sets the healing dealt to one or more players of a percentage of their raw damage dealt. NOTE: Negative values do not damage enemies. Healing values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose healing dealt will be set. Can use most Player based Value Syntax for this value.
        /// Healing Dealt Percent - The percentage of raw healing dealt to which the player or players will set their healing dealt. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SET HEALING DEALT")]
        public static void SetHealingDealt() { }

        /// <summary>
        /// SET HEALING RECEIVED
        /// Sets the healing received of one or more players to a percentage of their raw healing received. NOTE: Negative values do not damage enemies. Healing values of 0 or lower will not trigger script events.
        /// Definitions:
        /// 
        /// Player - The player or players whose healing received will be set. Can use most Player based Value Syntax for this value.
        /// Healing Received Percent - The percentage of raw healing received to which the player or players will set their healing received. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SET HEALING RECEIVED")]
        public static void SetHealingReceived() { }

        /// <summary>
        /// SET INVISIBLE
        /// Causes one or more players to become invisible to either all other players or just enemies.
        /// Definitions:
        /// 
        /// Player - The player or players who will become invisible. Can use most Player based Value Syntax for this value.
        /// Invisible to - Specifies for whom the player or players will be invisible. Can be set to All, Enemies, or None.
        /// </summary>
        [WorkshopCode("SET INVISIBLE")]
        public static void SetInvisible() { }

        /// <summary>
        /// SET MATCH TIME
        /// Sets the current match time (which is visible at the top of the screen). This can be used to shorten or extend the duration of a match or to change the duration of assemble heroes or setup.
        /// Definitions:
        /// 
        /// Time - The match time in seconds. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET MATCH TIME")]
        public static void SetMatchTime() { }

        /// <summary>
        /// SET MAX HEALTH
        /// Sets the max health of one or more players as a percentage of their raw max health. This action will ensure that a player’s current health will not exceed the new max health.
        /// Definitions:
        /// 
        /// Player - The player or players whose max health will be set. Can use most Player based Value Syntax for this value.
        /// Health Percent - The percentage of raw max health to which the player or players will set their max health. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET MAX HEALTH")]
        public static void SetMaxHealth() { }

        /// <summary>
        /// SET MOVE SPEED
        /// Sets the move speed of one or more players to a percentage of their raw move speed.
        /// Definitions:
        /// 
        /// Player - The player or players whose move speed will be set. Can use most Player based Value Syntax for this value.
        /// Health Percent - The percentage of raw move speed to which the player or players will set their move speed. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET MOVE SPEED")]
        public static void SetMoveSpeed(Player player, float percent) { }

        /// <summary>
        /// SET OBJECTIVE DESCRIPTION
        /// Sets the text at the top center of the screen that normally describes the objective to a message visible to specific players.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the message. Can use most Number based Player Syntax for this value.
        /// Header - The message to be displayed. Can use most String based Value Syntax for this value.
        /// Reevaluation - Specifies which of this action’s inputs will be continously reevaluated. The message will keep asking for and using new values from reevaluated inputs. Can choose “Visible to and String” or “String”
        /// </summary>
        [WorkshopCode("SET OBJECTIVE DESCRIPTION")]
        public static void SetObjectiveDescription() { }

        /// <summary>
        /// SET PLAYER ALLOWED HEROES
        /// Sets the list of heroes available to one or more players. If a player’s current hero becomes unavailable, the player is forced to choose a different hero and respawn at an appropriate spawn location.
        /// Definitions:
        /// 
        /// Player - The player or players whose hero list is being set. Can use most Player based Value Syntax for this value.
        /// Hero - The hero or heroes that will be available. If no heroes are provided, the action has no effect. Can use most Hero based Value Syntax for this value including compatible Arrays.
        /// </summary>
        [WorkshopCode("SET PLAYER ALLOWED HEROES")]
        public static void SetPlayerAllowedHeroes(Array<Player> players, Array<Hero> heroes) { }

        /// <summary>
        /// SET PLAYER SCORE
        /// Sets the score (kill count) of one or more players. This action only has an effect in free-for-all modes.
        /// Definitions:
        /// 
        /// Player - The player or players whose score will be set. Can use most Player based Value Syntax for this value.
        /// Score - The score that will be set. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET PLAYER SCORE")]
        public static void SetPlayerScore() { }

        /// <summary>
        /// SET PLAYER VARIABLE
        /// Stores a value into a player variable, which is a variable that belongs to a specific player.
        /// Definitions:
        /// 
        /// Player - The player or players whose variable will be set. If multiple players are provided, each of their variables will be set. Can use most Player based Value Syntax for this value.
        /// Variable - Specifies which Player Variable to store the value into. Specified by a single alphabetic letter (A through Z).
        /// Value - The value that will be stored. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        /// </summary>
        [WorkshopCode("SET PLAYER VARIABLE")]
        public static void SetPlayerVariable<T>(Player player, char variable, T value) { }

        /// <summary>
        /// SET PLAYER VARIABLE AT INDEX
        /// Finds or creates an array on a player variable, which is a variable that belongs to a specific player, then stores a value in the array at the specified index.
        /// Definitions:
        /// 
        /// Player - The player or players whose variable will be set. If multiple players are provided, each of their variables will be set. Can use most Player based Value Syntax for this value.
        /// Variable - Specifies which player variable’s value is the array to modify, if the variable’s value is not an array, then its value becomes an empty array. Specified by a single alphabetic letter (A through Z).
        /// Index - The index of the array to modify. If the index is beyond the end of the array, the array is extended with the new elements given a value of zero. Can use most Number based Value Syntax with this value.
        /// Value - The value that will be stored into the array. Nearly any Value syntax can be used, however it is most common with Number based syntax.
        /// </summary>
        [WorkshopCode("SET PLAYER VARIABLE AT INDEX")]
        public static void SetPlayerVariableAtIndex() { }

        /// <summary>
        /// SET PRIMARY FIRE ENABLED
        /// Enables or disables primary fire for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to primary fire is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use primary fire. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        /// </summary>
        [WorkshopCode("SET PRIMARY FIRE ENABLED")]
        public static void SetPrimaryFireEnabled() { }

        /// <summary>
        /// SET PROJECTILE GRAVITY
        /// Sets the projectile gravity for one or more players to a percentage of regular projectile gravity.
        /// Definitions:
        /// 
        /// Player - The player or players whose projectile gravity will be set. Can use most Player based Value Syntax for this value.
        /// Projectile Gravity Percent - The percentage of the regular projectile gravity to which the player or players will set their personal projectile gravity. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET PROJECTILE GRAVITY")]
        public static void SetProjectileGravity() { }

        /// <summary>
        /// SET PROJECTILE SPEED
        /// Sets the projectile speed for one or more players to a percentage of regular projectile speed.
        /// Definitions:
        /// 
        /// Player - The player or players whose projectile speed will be set. Can use most Player based Value Syntax for this value.
        /// Projectile Speed Percent - The percentage of the regular projectile speed to which the player or players will set their personal projectile speed. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET PROJECTILE SPEED")]
        public static void SetProjectileSpeed() { }

        /// <summary>
        /// SET RESPAWN MAX TIME
        /// Sets the duration between death and respawn for one or more players that are already dead when this action is executed, the change takes effect on their next death.
        /// Definitions:
        /// 
        /// Player - The player or players whose respawn max time will is being defined. Can use most Player based Value Syntax for this value.
        /// Time - The duration between death and respawn in seconds. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET RESPAWN MAX TIME")]
        public static void SetRespawnMaxTime() { }

        /// <summary>
        /// SET SECONDARY FIRE ENABLED
        /// Enables or disables secondary fire for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to secondary fire is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use secondary fire. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        /// </summary>
        [WorkshopCode("SET SECONDARY FIRE ENABLED")]
        public static void SetSecondaryFireEnabled() { }

        /// <summary>
        /// SET SLOW MOTION
        /// Sets the simulation rate for the entire game, including all players, projectiles, effects, and game mode logic.
        /// Definitions:
        /// 
        /// Speed Percent - The simulation rate as a percentage of normal speed. Only rates up to 100% are allowed. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET SLOW MOTION")]
        public static void SetSlowMotion() { }

        /// <summary>
        /// SET STATUS
        /// Applies a status to one or more players. This status will remain in effect for the specified duration or until it is cleared by the clear status action.
        /// Definitions:
        /// 
        /// Player - The player or players to whom the status will be applied. Can use most Player based Value Syntax for this value.
        /// Assister - Specifies a player to be awarded assist credit should the affected player or players be killed while the status is in effect. An assister of null indicates no player will receive credit. Can use most Player based Value Syntax for this value.
        /// Status - The Status to be applied from the player or players. These behave similarly to statuses applied from hero abilities. Values include Hacked, Burning, Knocked Down, Asleep, Frozen, Unkillable, Invincible, Phased Out, Rooted, or Stunned.
        /// Duration - The duration of the status effect in seconds. To have a status that lasts until a clear status action is executed, proivide an arbitrarily long duration such as 9999. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SET STATUS")]
        public static void SetStatus() { }

        /// <summary>
        /// SET TEAM SCORE
        /// Sets the score for one or both teams. This action has no effect in free-for-all modes or modes without a team score.
        /// Definitions:
        /// 
        /// Team - The team or teams whose score will be set. Can use most Team based Value Syntax for this value.
        /// Score - The score that will be set. Can use most Number based Value Syntax for this value.
        /// </summary>
        [WorkshopCode("SET TEAM SCORE")]
        public static void SetTeamScore() { }

        /// <summary>
        /// SET ULTIMATE ABILITY ENABLED
        /// Enables or disables the ultimate ability of one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose access to their ultimate ability is affected. Can use most Player based Value Syntax for this value.
        /// Enabled - Specifies whether the player or players are able to use their ultimate ability. Expects a Boolean Value such as True, False, or Compare. Can use most Boolean based Value Syntax.
        /// </summary>
        [WorkshopCode("SET ULTIMATE ABILITY ENABLED")]
        public static void SetUltimateAbilityEnabled() { }

        /// <summary>
        /// SET ULTIMATE CHARGE
        /// Sets the ultimate charge or one or more players as a percentage of maximum charge.
        /// Definitions:
        /// 
        /// Player - The player or players whose ultimate charge will be set. Can use most Player based Value Syntax for this value.
        /// Charge Percent - The percentage of maximum charge. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SET ULTIMATE CHARGE")]
        public static void SetUltimateCharge() { }

        /// <summary>
        /// SKIP
        /// Skips execution of a certain number of actions in the action list.
        /// Definitions:
        /// 
        /// Number of actions - The number of action to skip, not including this action. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SKIP")]
        public static void Skip() { }

        /// <summary>
        /// SKIP IF
        /// Skips execution of a certain number of actions in the action list if this action’s condition evaluates to true. If it does not, execution continues with the next action.
        /// Definitions:
        /// 
        /// Condition - Specifies whether the loop will occur. Can use most Conditional based Value Syntax for this value.
        /// Number of actions - The number of action to skip, not including this action. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("SKIP IF")]
        public static void SkipIf() { }

        /// <summary>
        /// SMALL MESSAGE
        /// Displays a small message beneath the reticle that is visible to specific players.
        /// Definitions:
        /// 
        /// Visible to - One or more players who will see the message. Can use most Player based Value Syntax.
        /// Header - The message to be displayed. Can use most String based Value Syntax to specify.
        /// </summary>
        [WorkshopCode("SMALL MESSAGE")]
        public static void SmallMessage() { }

        /// <summary>
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
        /// </summary>
        [WorkshopCode("START ACCELERATING", true)]
        public static void StartAccelerating(Player player, Vector direction, float rate, float maxSpeed, RelativeTo relativeTo, ReevaluationValue reevaluation) { }

        /// <summary>
        /// START CAMERA
        /// Places your camera at a location, facing a direction.
        /// Definitions:
        /// 
        /// Player - The player executing this rule. As specified by the event, may be the same as the attacker or the victim. Can use most Player based Value Syntax.
        /// Eye Position - The position of the camrea, reevaluates continously. Can use most Vector based Value Syntax to specify.
        /// Look at position - Where the camera looks at, reevaluates continously. Can use most Vector based Value Syntax to specify.
        /// Blend Speed - How fast to blend the camera movement as positions change. 0 means do not blend at all and just change positions instantly. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("START CAMERA")]
        public static void StartCamera() { }

        /// <summary>
        /// START DAMAGE MODIFICATION
        /// Starts modifying how much damage one or more receivers will receive from one or more damagers. A reference to this damage modification can be obtained from the last damage modification ID value. This action will fail if too many damage modifications have been started.
        /// Definitions:
        /// 
        /// Receivers - The player or players whose incoming damage will be modified. Can use most Player based Value Syntax.
        /// Damagers - The player or players whose outgoing damage will be modified (when attacking the receivers). Can use most Player based Value Syntax.
        /// Damage Percent - The percentage of damage that will apply to receivers when attacked by damagers. Can use most Number based Value Syntax to specify.
        /// Reevaluation - Specifies which of this action’s inputs will be continously reevaluated. This action will keep asking for and using new values from reevaluated inputs. Can choose from “Receivers, Damagers, and Damage Percent”, “Receivers and Damagers”, or “None”.
        /// </summary>
        [WorkshopCode("START DAMAGE MODIFICATION")]
        public static void StartDamageModification() { }

        /// <summary>
        /// START DAMAGE OVER TIME
        /// Starts an instance of damage over time, this DOT will persist for the specified duration or until stopped by script. To obtain a reference to this DOT, use the last damage over time to value.
        /// Definitions:
        /// 
        /// Receivers - One or more players who will receive the damage over time. Can use most Player based Value Syntax.
        /// Damager - The player who will receive credit for the damage. A damager of null indicates no player will receive credit. Can use most Player based Value Syntax.
        /// Duration - The duration of the damage over time in seconds. To have a DOT that lasts until stopped by script, provide an arbitrarily long duration such as 9999. Can use most Number based Value Syntax to specify.
        /// Damage Per Second - The damage per second for the damage over time. Can use most Number based Value Syntax to specify.
        /// </summary>
        [WorkshopCode("START DAMAGE OVER TIME")]
        public static void StartDamageOverTime() { }

        /// <summary>
        /// START FACING
        /// Starts turning one or more players to face the specified direction.
        /// Definitions:
        /// 
        /// Player - The player or players who will start turning. Can use most Player based Value Syntax.
        /// Direction - The unit direction in which the player or players will eventually face. Can use most Vector based Value Syntax.
        /// Turn Rate - The turn rate in degrees per second. Can use most Number based Value Syntax to specify.
        /// Relative - Specifies whether direction is relative to the world coordinates or the local coordinates of the player or players.
        /// Reevaluation - Specifies which of this actions inputs will be continously reevaluated. This action will keep asking for and using new values from reevaluated inputs. You can choose from “Direction and Turn Rate” or “None”.
        /// </summary>
        [WorkshopCode("START FACING")]
        public static void StartFacing() { }

        /// <summary>
        /// START FORCING PLAYER TO BE HERO
        /// Starts forcing one or more players to be a specified hero and, if necessary, respawns them immediately in their current locaiton. This will be the only hero available to the player or players until, the stop forcing player to be hero action is executed.
        /// Definitions:
        /// 
        /// Player - The player or players who will be forced to be a specified hero. Can use most Player based Value Syntax.
        /// Hero - The hero that the player or players will be forced to be. Can use most Hero based Value Syntax.
        /// </summary>
        [WorkshopCode("START FORCING PLAYER TO BE HERO")]
        public static void StartForcingPlayerToBeHero() { }

        /// <summary>
        /// START FORCING SPAWN ROOM
        /// Forces a team to spawn in a particular spawn room, regardless of the sapwn room normally used by the game mode. This action only has an effect in Assault, Hybrid, and Payload Maps.
        /// Definitions:
        /// 
        /// Team - The team whose spawn room will be forced. Can use most Team based Value Syntax.
        /// Room - The number of the spawn room to be forced. 0 is the first spawn room, 1 is the second, and 2 is the third. If this specified spawn room does not exist. Players will use the normal spawn room. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("START FORCING SPAWN ROOM")]
        public static void StartForcingSpawnRoom() { }

        /// <summary>
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
        /// </summary>
        [WorkshopCode("START FORCING THROTTLE")]
        public static void StartForcingThrottle() { }

        /// <summary>
        /// START HEAL OVER TIME
        /// Starts an instance of damage over time, this HOT will persist for the specified duration or until stopped by script. To obtain a reference to this HOT, use the last damage over time to value.
        /// Definitions:
        /// 
        /// Player - One or more players who will receive the heal over time. Can use most Player based Value Syntax.
        /// Healer - The player who will receive credit for the heal. A damager of null indicates no player will receive credit. Can use most Player based Value Syntax.
        /// Duration - The duration of the heal over time in seconds. To have a HOT that lasts until stopped by script, provide an arbitrarily long duration such as 9999. Can use most Number based Value Syntax to specify.
        /// Damage Per Second - The heal per second for the heal over time. Can use most Number based Value Syntax to specify.
        /// </summary>
        [WorkshopCode("START HEAL OVER TIME")]
        public static void StartHealOverTime() { }

        /// <summary>
        /// START HOLDING BUTTON
        /// Forces one or more players to hold a button virtually until stopped by the stop holding button action.
        /// Definitions:
        /// 
        /// Player - The player or players who are holding a button virtually. Can use most Player based Value Syntax.
        /// Button - The logical button that is being held virtually.
        /// </summary>
        [WorkshopCode("START HOLDING BUTTON")]
        public static void StartHoldingButton() { }

        /// <summary>
        /// STOP ACCELERATING
        /// Stops the acceleration started by the start accelerating action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will stop accelerating. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP ACCELERATING")]
        public static void StopAccelerating(Player player) { }

        /// <summary>
        /// STOP ALL DAMAGE MODIFICATIONS
        /// Stops the all damage modifications that were started using the start damage modification action.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("STOP ALL DAMAGE MODIFICATIONS")]
        public static void StopAllDamageModifications() { }

        /// <summary>
        /// STOP ALL DAMAGE OVER TIME
        /// Stops all damage over time started by the start damage over time or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose scripted damage over time will stop. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP ALL DAMAGE OVER TIME")]
        public static void StopAllDamageOverTime() { }

        /// <summary>
        /// STOP ALL HEAL OVER TIME
        /// Stops all heal over time started by the start heal over time or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose scripted heal over time will stop. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP ALL HEAL OVER TIME")]
        public static void StopAllHealOverTime() { }

        /// <summary>
        /// STOP CAMERA
        /// Stops all forced camera positions started by the start camera or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players whose forced camera positions will stop. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP CAMERA")]
        public static void StopCamera() { }

        /// <summary>
        /// STOP CHASING GLOBAL VARIABLE
        /// Stops an in-progress chase of a global variable, leaving it at its current value.
        /// Definitions:
        /// 
        /// Variable - Specifies which global variable to stop modifying. Specified by a single alphabetic letter (A through Z).
        /// </summary>
        [WorkshopCode("STOP CHASING GLOBAL VARIABLE")]
        public static void StopChasingGlobalVariable() { }

        /// <summary>
        /// STOP CHASING PLAYER VARIABLE
        /// Stops an in-progress chase of a player variable, leaving it at its current value.
        /// Definitions:
        /// 
        /// Player - The player whose variable will stop changing. If multiple players are provided, each of their variables will stop changing. Can use most Player based Value Syntax.
        /// Variable - Specifies which player variable to stop modifying. Specified by a single alphabetic letter (A through Z).
        /// </summary>
        [WorkshopCode("STOP CHASING PLAYER VARIABLE")]
        public static void StopChasingPlayerVariable() { }

        /// <summary>
        /// STOP DAMAGE MODIFICATION
        /// Stops a damage modification that was started by the start damage modification action
        /// Definitions:
        /// 
        /// Damage modification ID - Specifies which damage modification instance to stop, this ID may be the last damage modification ID or a variable into which last damage modification ID was earlier stored. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP DAMAGE MODIFICATION")]
        public static void StopDamageModification() { }

        /// <summary>
        /// STOP DAMAGE OVER TIME
        /// Stops an instance of damage over time that was started by the start damage over time action
        /// Definitions:
        /// 
        /// Damage Over Time ID - Specifies which damage over time instance to stop, this ID may be the last damage over time ID or a variable into which last damage over time ID was earlier stored. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP DAMAGE OVER TIME")]
        public static void StopDamageOverTime() { }

        /// <summary>
        /// STOP FACING
        /// Stops the turning started by the start facing action for or one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who will stop turning. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP FACING")]
        public static void StopFacing() { }

        /// <summary>
        /// STOP FORCING PLAYER TO BE HERO
        /// Stops forcing one or more players to be a specified hero. This will not respawn the player or players, but it will restore their availablity the next time they go to select a hero.
        /// Definitions:
        /// 
        /// Player - The player or players who will no longer be forced to be a specific hero. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP FORCING PLAYER TO BE HERO")]
        public static void StopForcingPlayerToBeHero() { }

        /// <summary>
        /// STOP FORCING SPAWN ROOM
        /// Undoes the effect of start forcing spawn room action for the specified team.
        /// Definitions:
        /// 
        /// Team - The team that will resume using their normal spawn room. Can use most Team based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP FORCING SPAWN ROOM")]
        public static void StopForcingSpawnRoom() { }

        /// <summary>
        /// STOP FORCING THROTTLE
        /// Undoes the effect of start forcing throttle action for one or more players
        /// Definitions:
        /// 
        /// Player - The player or players whose movement inout will be restored. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP FORCING THROTTLE")]
        public static void StopForcingThrottle() { }

        /// <summary>
        /// STOP HEAL OVER TIME
        /// Stops an instance of heal over time that was started by the start heal over time action
        /// Definitions:
        /// 
        /// Heal Over Time ID - Specifies which heal over time instance to stop, this ID may be the last heal over time ID or a variable into which last heal over time ID was earlier stored. Can use most Number based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP HEAL OVER TIME")]
        public static void StopHealOverTime() { }

        /// <summary>
        /// STOP HOLDING BUTTON
        /// Undoes the effect of the start holding button action for one or more players.
        /// Definitions:
        /// 
        /// Player - The player or players who are no longer holding a button virtually. Can use most Player based Value Syntax.
        /// </summary>
        [WorkshopCode("STOP HOLDING BUTTON")]
        public static void StopHoldingButton() { }

        /// <summary>
        /// TELEPORT
        /// Teleports one or more players to the specified location.
        /// Definitions:
        /// 
        /// Player - The player or players to teleport. Can use most Player based Value Syntax.
        /// Position - The position to which the player or players will teleport. If a player is providedm the position of the player is used. Can use most Vector based Value Syntax.
        /// </summary>
        [WorkshopCode("TELEPORT", true)]
        public static void Teleport(Player player, Vector position) { }

        /// <summary>
        /// UNPAUSE MATCH TIME
        /// Unpauses the match time.
        /// There are no definitions to this action.
        /// </summary>
        [WorkshopCode("UNPAUSE MATCH TIME")]
        public static void UnpauseMatchTime() { }

        /// WAIT
        /// Pauses the execution of the action list, unless the wait is interrupted. The remainder of the actions will execute after the pause.
        /// Definitions:
        /// 
        /// Time - The duration of the pause. A minimum value of 0.250 seconds is required. Can use most Number based Value Syntax.
        /// Wait Behavior - Specifies if and how the wait can be interrupted. If the condition list is ignored, the wait will not be interrupted, otherwise, the condition list will determine if and when the action list will abort or restart.
        // [WorkshopCode("WAIT")]
        // public static void Wait() { }
    }
}
