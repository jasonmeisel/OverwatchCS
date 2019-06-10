using System;

namespace Workshop
{
    public static class Values
    {
        /// ABSOLUTE VALUE
        /// The absolute value is a measure of how far the number is from zero. If you think of a number line, with zero in the center, all you’re really doing is asking how far away you are from this zero point. For example the Absolute Value of 4 is 4 and the absolute value of -6 is 6.
        /// Definitions:
        /// 
        /// Value - You can specify any Value Syntax to define the Absolute Value.
        [WorkshopCodeName("ABSOLUTE VALUE")]
        public static void AbsoluteValue() { }

        /// ADD
        /// The sum of two numbers or vectors. This value will add the two specified values.
        /// Definitions:
        /// 
        /// Value - You can specify any Value Syntax to define either addend.
        [WorkshopCodeName("ADD")]
        public static void Add() { }

        /// ALL DEAD PLAYERS
        /// An array containing all dead players on a team in a match. A player is defined as being dead when they are eliminated but have not yet respawned back into the game.
        /// Definitions:
        /// 
        /// Team - You can specify any Team Syntax to define the array.
        [WorkshopCodeName("ALL DEAD PLAYERS")]
        public static void AllDeadPlayers() { }

        /// ALL HEROES
        /// An array of all heroes in Overwatch. Not to be confused with the All Players array
        /// There are no definitions to this value.
        [WorkshopCodeName("ALL HEROES")]
        public static void AllHeroes() { }

        /// ALL LIVING PLAYERS
        /// An array containing all living players on a team in a match. A player is defined as being alive when they are spawned into the game but have not yet been eliminated since spawning.
        /// Definitions:
        /// 
        /// Team - You can specify any Team Syntax to define the array.
        [WorkshopCodeName("ALL LIVING PLAYERS")]
        public static void AllLivingPlayers() { }

        /// ALL PLAYERS
        /// An array containing all players on a team in a match.
        /// Definitions:
        /// 
        /// Team - You can specify any Team Syntax to define the array.
        [WorkshopCodeName("ALL PLAYERS")]
        public static void AllPlayers() { }

        /// ALL PLAYERS NOT ON OBJECTIVE
        /// An array containing all players occupying neither a payload nor a control point (either on a team or in a match).
        /// Definitions:
        /// 
        /// Team - You can specify any Team Syntax to define the array.
        [WorkshopCodeName("ALL PLAYERS NOT ON OBJECTIVE")]
        public static void AllPlayersNotOnObjective() { }

        /// ALL PLAYERS ON OBJECTIVE
        /// An array containing all players occupying either a payload or a control point (either on a team or in a match).
        /// Definitions:
        /// 
        /// Team - You can specify any Team Syntax to define the array.
        [WorkshopCodeName("ALL PLAYERS ON OBJECTIVE")]
        public static void AllPlayersOnObjective() { }

        /// ALLOWED HEROES
        /// The array of heroes from which the specified player is currently allowed to select.
        /// Definitions:
        /// 
        /// Player - You can specify any Player Syntax to define the array.
        [WorkshopCodeName("ALLOWED HEROES")]
        public static void AllowedHeroes() { }

        /// ALTITUDE OF
        /// The player’s current height in meters above a surface. Results in a 0 whenever the place is on a surface.
        /// Definitions:
        /// 
        /// Player - You can specify any Player Syntax to define the array.
        [WorkshopCodeName("ALTITUDE OF")]
        public static void AltitudeOf() { }

        /// AND
        /// Whether both of the two inputs are true or equivalent to true.
        /// Definitions:
        /// 
        /// Value - You can specify any Value Syntax to define both of the required values.
        [WorkshopCodeName("AND")]
        public static void And() { }

        /// ANGLE DIFFERENCE
        /// The difference between two angles, after the angles are wrapped within +/- 180 of each other, the result is positive if the second angle is greater than the first angle, otherwise the result is zero or negative.
        /// Definitions:
        /// 
        /// Angle - You can specify any Angle Syntax to define both of the required values.
        /// 
        /// Examples:
        /// 
        /// (Angle1, Angle2) = result
        /// 
        /// (5,  100) =   95;
        /// (5, -100) = -105;
        /// (5,  190) = -175; Note:  190 converted to -170
        /// 
        /// (5, -190) =  165; Note: -190 converted to  170
        /// 
        /// ( 5,  5) = 0;
        /// (-5, -5) = 0;
        [WorkshopCodeName("ANGLE DIFFERENCE")]
        public static void AngleDifference() { }

        /// APPEND TO ARRAY
        /// A copy of an array with one or more values appended to the end.
        /// Definitions:
        /// 
        /// Array - You must specify the Array Syntax you are adding the value to.
        /// Value - You must specify the Value Syntax that you are adding to the array.
        [WorkshopCodeName("APPEND TO ARRAY")]
        public static void AppendToArray() { }

        /// ARRAY CONTAINS
        /// Whether the specified array contains the specified value.
        /// Definitions:
        /// 
        /// Array - You must specify the Array Syntax you are comparing the value to.
        /// Value - You must specify the Value Syntax that you are comparing to the array.
        [WorkshopCodeName("ARRAY CONTAINS")]
        public static void ArrayContains() { }

        /// ARRAY SLICE
        /// A copy of the specified array containing only values from a specified index range.
        /// Definitions:
        /// 
        /// Array - You must specify the Array Syntax you are comparing the value to.
        /// Start Index - The first index of the range. Can use most Value Syntax to specify with.
        /// Count - The number of elements in the resulting array. The resulting array will contain fewer elements if the specified range exceeds the bounds of the array. Can use any Number-based Value Syntax to specify with.
        [WorkshopCodeName("ARRAY SLICE")]
        public static void ArraySlice() { }

        /// ATTACKER
        /// The player that dealt damage for the event currently being processed by this rule. May be the same as the victim or the event player.
        /// There are no definitions to this value.
        [WorkshopCodeName("ATTACKER")]
        public static void Attacker() { }

        /// BACKWARD
        /// Shorthand for the direction vector(0, 0, -1) which points backwards.
        /// There are no definitions to this value.
        [WorkshopCodeName("BACKWARD")]
        public static void Backward() { }

        /// CLOSEST PLAYER TO
        /// The player closest to a position, optionally restricted by team.
        /// Definitions:
        /// 
        /// Center - The position to which to measure proximity. Can use most Value Syntax related to reporting a position in the map.
        /// Team - You can specify any Team Syntax to restrict which players is reported when defining this value.
        [WorkshopCodeName("CLOSEST PLAYER TO")]
        public static void ClosestPlayerTo() { }

        /// COMPARE
        /// Whether the comparison of the two inputs is true.
        /// Definitions:
        /// 
        /// Value - The left hand side of the comparison. This may be any value type if the operation is == or =!, otherwise real numbers are expected. Can use most Value Syntax for the comparison.
        /// Condition - One of the standard conditions to use for comparison. See the Condition section for details.
        /// Value - The right hand side of the comparison. This may be any value type if the operation is == or =!, otherwise real numbers are expected. Can use most Value Syntax for the comparison.
        [WorkshopCodeName("COMPARE")]
        public static void Compare() { }

        /// CONTROL MODE SCORING PERCENTAGE
        /// The score percentage for the specified team in the control mode.
        /// Definitions:
        /// 
        /// Team - You can specify any Team Syntax to define which team reported when defining this value.
        [WorkshopCodeName("CONTROL MODE SCORING PERCENTAGE")]
        public static void ControlModeScoringPercentage() { }

        /// CONTROL MODE SCORING TEAM
        /// The team that is currently accumulating score percentage in control mode Results in all if neither team is accumulating score.
        /// There are no definitions to this value.
        [WorkshopCodeName("CONTROL MODE SCORING TEAM")]
        public static void ControlModeScoringTeam() { }

        /// COSIGN FROM DEGREES
        /// The cosine of a specified angle in degrees. The cosine of the angle is equal to the length of the adjacent side divided by the length of the hypotenuse.
        /// Definitions:
        /// 
        /// Angle - You can specify any Angle Syntax to define this value.
        [WorkshopCodeName("COSIGN FROM DEGREES")]
        public static void CosignFromDegrees() { }

        /// COSIGN FROM RADIANS
        /// The cosine of a specified angle in radians. The cosine of the angle is equal to the length of the adjacent side divided by the length of the hypotenuse. A radian is a unit of angle, equal to an angle at the center of a circle whose arc is equal in length to the radius.
        /// Definitions:
        /// 
        /// Angle - You can specify any Angle Syntax to define this value.
        [WorkshopCodeName("COSIGN FROM RADIANS")]
        public static void CosignFromRadians() { }

        /// COUNT OF
        /// The number of elements in the specified array.
        /// Definitions:
        /// 
        /// Array - You must specify the Array Syntax you are counting the elements to.
        [WorkshopCodeName("COUNT OF")]
        public static void CountOf() { }

        /// CROSS PRODUCT
        /// The cross product of the specified values.
        /// Definitions:
        /// 
        /// Value - You must specify the first Value Syntax to compare to the second.
        /// Value - You must specify the first Value Syntax to compare to the second.
        [WorkshopCodeName("CROSS PRODUCT")]
        public static void CrossProduct() { }

        /// CURRENT ARRAY ELEMENT
        /// The current array element being considered. Only meaningful during the evaluation of values such as filtered array and sorted array.
        /// There are no definitions to this value.
        [WorkshopCodeName("CURRENT ARRAY ELEMENT")]
        public static void CurrentArrayElement() { }

        /// DIRECTION FROM ANGLES
        /// The unit-length direction vector corresponding to the specified angles.
        /// Definitions:
        /// 
        /// Horizontal Angle - The horizontal angle in degrees used to construct the resulting vector. Most angle based Value Syntax can be used here.
        /// Vertical Angle - The vertical angle in degrees used to construct the resulting vector. Most angle based Value Syntax can be used here.
        [WorkshopCodeName("DIRECTION FROM ANGLES")]
        public static void DirectionFromAngles() { }

        /// DIRECTION TOWARDS
        /// The unit-length direction vector from position to another.
        /// Definitions:
        /// 
        /// Start Pos - The position from which the resulting direction vector will point. Most positional based Value Syntax can be used here.
        /// End Pos - The position to which the resulting direction vector will point. Most positional based Value Syntax can be used here.
        [WorkshopCodeName("DIRECTION TOWARDS")]
        public static void DirectionTowards() { }

        /// DISTANCE BETWEEN
        /// The distance between two positions in meters.
        /// Definitions:
        /// 
        /// Start Pos - One of the two positions used in the distance measurement. Most positional based Value Syntax can be used here.
        /// End Pos - One of the two positions used in the distance measurement. Most positional based Value Syntax can be used here.
        [WorkshopCodeName("DISTANCE BETWEEN")]
        public static void DistanceBetween() { }

        /// DIVIDE
        /// The ratio of two numbers or vectors. A vector divided by a number will yield a scaled vector. Division by zero results in zero.
        /// Definitions:
        /// 
        /// Value - The left-hand operand, may be any value that results in a number or a vector. Any Value Syntax may be used here.
        /// Value - The right-hand operand, may be any value that results in a number or a vector. Any Value Syntax may be used here.
        [WorkshopCodeName("DIVIDE")]
        public static void Divide() { }

        /// DOT PRODUCT
        /// The dot product of the specified values. The dot product tells you what amount of one vector goes in the direction of another.
        /// Definitions:
        /// 
        /// Value - One of the two vector operands of the dot product. Any positional based Syntax may be used here.
        /// Value - One of the two vector operands of the dot product. Any positional based Syntax may be used here.
        [WorkshopCodeName("DOT PRODUCT")]
        public static void DotProduct() { }

        /// DOWN
        /// Shorthand for the direction vector(0, -1, 0) which points down.
        /// There are no definitions to this value.
        [WorkshopCodeName("DOWN")]
        public static void Down() { }

        /// EMPTY ARRAY
        /// An array with no elements.
        /// There are no definitions to this value.
        [WorkshopCodeName("EMPTY ARRAY")]
        public static void EmptyArray() { }

        /// ENTITY EXISTS
        /// Whether the specified player, icon entity, or effect entity still exists. Useful for determining if a player has left the match or an entity has been destroyed.
        /// Definitions:
        /// 
        /// Entity - The player, icon entity, or effect entity whose existance to check.
        [WorkshopCodeName("ENTITY EXISTS")]
        public static void EntityExists() { }

        /// EVENT PLAYER
        /// The player executing the rule, as specified by the event, may be the same as the attacker or victim.
        /// There are no definitions to this value.
        [WorkshopCodeName("EVENT PLAYER")]
        public static void EventPlayer() { }

        /// FACING DIRECTION OF
        /// The unit-length directional vector of a player’s current facing relative to the world. This value includes both horizontal and vertical facing.
        /// Definitions:
        /// 
        /// Player - The player whose facing direction to acquire. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("FACING DIRECTION OF")]
        public static void FacingDirectionOf() { }

        /// FALSE
        /// The Boolean value of false.
        /// There are no definitions to this value.
        [WorkshopCodeName("FALSE")]
        public static void False() { }

        /// FARTHEST PLAYER FROM
        /// The player farthest to a position, optionally restricted by team.
        /// Definitions:
        /// 
        /// Center - The position to which to measure proximity. Can use most Value Syntax related to reporting a position in the map.
        /// Team - You can specify any Team Syntax to restrict which players is reported when defining this value.
        [WorkshopCodeName("FARTHEST PLAYER FROM")]
        public static void FarthestPlayerFrom() { }

        /// FILTERED ARRAY
        /// A copy of the specified array with any values that do not match the specified condition removed.
        /// Definitions:
        /// 
        /// Array - The array whose copy will be filtered. Can use most Array Syntax to specify.
        /// Condition - The condition that is evaluated for each element of the copied array. If the condition is true, the element is kept in the copied array. Use the current array element value to reference the element of the array currently being considered.
        [WorkshopCodeName("FILTERED ARRAY")]
        public static void FilteredArray() { }

        /// FIRST OF
        /// The value at the started of the specified array. Results in a 0 if the specified array is empty.
        /// Definitions:
        /// 
        /// Array - The array from which the value is acquired. Can use most Array Syntax to specify.
        [WorkshopCodeName("FIRST OF")]
        public static void FirstOf() { }

        /// FLAG POSITION
        /// The position of a specific team’s flag in Capture the Flag.
        /// Definitions:
        /// 
        /// Team - The team whose flag position to acquire.
        [WorkshopCodeName("FLAG POSITION")]
        public static void FlagPosition() { }

        /// FORWARD
        /// Shorthand for the direction vector(0, 0, 1) which points forward.
        /// There are no definitions to this value.
        [WorkshopCodeName("FORWARD")]
        public static void Forward() { }

        /// GLOBAL VARIABLE
        /// The current value of a global variable, which is a variable which belongs to the custom game itself.
        /// Definitions:
        /// 
        /// Variable - Variable specified by a single alphabetic letter (A through Z).
        [WorkshopCodeName("GLOBAL VARIABLE")]
        public static void GlobalVariable() { }

        /// HAS SPAWNED
        /// Whether an entity has spawned in the world. Results in false for players who have not chosen a hero yet.
        /// Definitions:
        /// 
        /// Entity - The player, icon entity, or effect entity whose presence in world to check.
        [WorkshopCodeName("HAS SPAWNED")]
        public static void HasSpawned() { }

        /// HAS STATUS
        /// Whether the specified player has the specified status, either from the set status action or from a non-scripted game mechanic.
        /// Definitions:
        /// 
        /// Player - The player whose status to check.
        /// Status - The status to check for. Values include Hacked, Burning, Knocked Down, Asleep, Frozen, Unkillable, Invincible, Phased Out, Rooted, or Stunned.
        [WorkshopCodeName("HAS STATUS")]
        public static void HasStatus() { }

        /// HEALTH
        /// The current health of a player including armor and shields.
        /// Definitions:
        /// 
        /// Player - The player whose health to acquire.
        [WorkshopCodeName("HEALTH")]
        public static void Health() { }

        /// HEALTH PERCENT
        /// The current health of a player as a percentage including armor and shields.
        /// Definitions:
        /// 
        /// Player - The player whose health percentage to acquire.
        [WorkshopCodeName("HEALTH PERCENT")]
        public static void HealthPercent() { }

        /// HERO
        /// A hero constant. Specifies one of the available heroes by name in the game.
        /// Definitions:
        /// 
        /// Hero - A hero constant. (i.e. Tracer, Reaper, Mercy, Reinhardt. etc.)
        [WorkshopCodeName("HERO")]
        public static void Hero() { }

        /// HERO ICON STRING
        /// Converts a hero parameter into a string that shows up as an icon.
        /// Definitions:
        /// 
        /// Value - The hero that will be converted as an icon.
        [WorkshopCodeName("HERO ICON STRING")]
        public static void HeroIconString() { }

        /// HERO OF
        /// The Current Hero of a Player.
        /// Definitions:
        /// 
        /// Player - The player whose hero to acquire. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("HERO OF")]
        public static void HeroOf() { }

        /// HORIZONTAL ANGLE FROM DIRECTION
        /// The horizontal angle in degrees corresponding to the specified direction vector.
        /// Definitions:
        /// 
        /// Direction - The direction vector from which to acquire a horizontal angle in degrees. The vector is unitized before calculation begins. Can use most Vector based Value Syntax to retrieve this value.
        [WorkshopCodeName("HORIZONTAL ANGLE FROM DIRECTION")]
        public static void HorizontalAngleFromDirection() { }

        /// HORIZONTAL ANGLE TOWARDS
        /// The horizontal angle in degrees from a player’s current forward direction to the specified position. The result is positive if the position is on the player’s left, otherwise the result is zero or negative.
        /// Definitions:
        /// 
        /// Player - The player whose current facing angle begins. Can use most player based Value Syntax to retrieve this value.
        /// Position - The position in the world in where the angle ends.
        [WorkshopCodeName("HORIZONTAL ANGLE TOWARDS")]
        public static void HorizontalAngleTowards() { }

        /// HORIZONTAL FACING ANGLE OF
        /// The directional angle in degrees of a player’s current facing relative to the world. This value increases as the player rotates to the left (wrapping around at +/- 180).
        /// Definitions:
        /// 
        /// Player - The player whose facing direction to acquire. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("HORIZONTAL FACING ANGLE OF")]
        public static void HorizontalFacingAngleOf() { }

        /// HORIZONTAL SPEED OF
        /// The current horizontal speed of a player in meters per second. This measurement excludes all vertical motion.
        /// Definitions:
        /// 
        /// Player - The player whose facing direction to acquire. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("HORIZONTAL SPEED OF")]
        public static void HorizontalSpeedOf() { }

        /// INDEX OF ARRAY VALUE
        /// The index of a value within an array or -1 if no such value can be found.
        /// Definitions:
        /// 
        /// Array - The array in which to search for the specified value. Can use most Array based Value Syntax to retrieve this value.
        /// Value - The value for which to search. Can use most Number based Value Syntax to retrive this value.
        [WorkshopCodeName("INDEX OF ARRAY VALUE")]
        public static void IndexOfArrayValue() { }

        /// IS ALIVE
        /// Determines whether a player is alive. Returns a Boolean value.
        /// Definitions:
        /// 
        /// Player - The player whose life to check. Can use most player based Value Syntax to retrive this value.
        [WorkshopCodeName("IS ALIVE")]
        public static void IsAlive() { }

        /// IS ASSEMBLING HEROES
        /// Whether the match is currently in its assemble heroes phase.
        /// There are no definitions to this value.
        [WorkshopCodeName("IS ASSEMBLING HEROES")]
        public static void IsAssemblingHeroes() { }

        /// IS BETWEEN ROUNDS
        /// Whether the match is between rounds.
        /// There are no definitions to this value.
        [WorkshopCodeName("IS BETWEEN ROUNDS")]
        public static void IsBetweenRounds() { }

        /// IS BUTTON HELD
        /// Whether a player is holding a specific button.
        /// Definitions:
        /// 
        /// Player - The player whose button to check. Can use most player based Value Syntax to retrieve this value.
        /// Button - The button to check. Designed by any action inputs by ability but not directional inputs. (i.e. Primary Fire, Secondary Fire, Ultimate Ability, Jump, Crouch, etc.)
        [WorkshopCodeName("IS BUTTON HELD")]
        public static void IsButtonHeld() { }

        /// IS COMMUNICATING
        /// Whether a player is using a specific communication type (such as emote, using a voice line, etc.).
        /// Definitions:
        /// 
        /// Player - The player whose communication status to check. Can use most player based Value Syntax to retrieve this value.
        /// Type - The type of communication to consider. The duration of emotes is exact, the duration of voice lines is assumed to be 4 seconds, and all other durations are assumed to be 2 seconds. Any of the four emote slots, four voice lines slots, or any standard communication (Need healing, Ultimate Status, etc.) can be designated.
        [WorkshopCodeName("IS COMMUNICATING")]
        public static void IsCommunicating() { }

        /// IS COMMUNICATING ANY
        /// Whether a player is using any communication type (such as emoting, using a voice line, etc.)
        /// Definitions:
        /// 
        /// Player - The player whose communication status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS COMMUNICATING ANY")]
        public static void IsCommunicatingAny() { }

        /// IS COMMUNICATING ANY EMOTE
        /// Whether a player is using a emote.
        /// Definitions:
        /// 
        /// Player - The player whose emoting status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS COMMUNICATING ANY EMOTE")]
        public static void IsCommunicatingAnyEmote() { }

        /// IS COMMUNICATING ANY VOICE LINE
        /// Whether a player is using a voice line. (The duration of a voice line is assumed to be 4 seconds.)
        /// Definitions:
        /// 
        /// Player - The player whose voice line status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS COMMUNICATING ANY VOICE LINE")]
        public static void IsCommunicatingAnyVoiceLine() { }

        /// IS CONTROL MODE POINT LOCKED
        /// Whether the point is locked in control mode.
        /// There are no definitions to this value.
        [WorkshopCodeName("IS CONTROL MODE POINT LOCKED")]
        public static void IsControlModePointLocked() { }

        /// IS CROUCHING
        /// Whether a player is crouching.
        /// Definitions:
        /// 
        /// Player - The player whose crouching status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS CROUCHING")]
        public static void IsCrouching() { }

        /// IS CTF MODE IN SUDDEN DEATH
        /// Whether the current game of capture the flag is in sudden death.
        /// There are no definitions to this value.
        [WorkshopCodeName("IS CTF MODE IN SUDDEN DEATH")]
        public static void IsCtfModeInSuddenDeath() { }

        /// IS DEAD
        /// Whether a player is dead.
        /// Definitions:
        /// 
        /// Player - The player whose death to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS DEAD")]
        public static void IsDead() { }

        /// IS FIRING PRIMARY
        /// Whether the specified player’s primary weapon attack is being used.
        /// Definitions:
        /// 
        /// Player - The player whose primary weapon attack to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS FIRING PRIMARY")]
        public static void IsFiringPrimary() { }

        /// IS FIRING SECONDARY
        /// Whether the specified player’s secondary weapon attack is being used.
        /// Definitions:
        /// 
        /// Player - The player whose secondary weapon attack to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS FIRING SECONDARY")]
        public static void IsFiringSecondary() { }

        /// IS FLAG AT BASE
        /// Whether a specific team’s flag is at its base in capture the flag.
        /// Definitions:
        /// 
        /// Team - The player whose flag to check. Can use most team based Value Syntax to retrive this value.
        [WorkshopCodeName("IS FLAG AT BASE")]
        public static void IsFlagAtBase() { }

        /// IS GAME IN PROGRESS
        /// Whether the main phase of the match is in progress (during which time combat and scoring are allowed).
        /// There are no definitions to this value.
        [WorkshopCodeName("IS GAME IN PROGRESS")]
        public static void IsGameInProgress() { }

        /// IS HERO BEING PLAYED
        /// Whether a specific hero is being played (either on a team or in the match).
        /// Definitions:
        /// 
        /// Hero - The hero to check for play. Can use most team based Value Syntax to retrive this value. Any applicable Hero based Value Syntax can be used.
        /// Team - The team or teams on which to check for the hero being played. Can use most team based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS HERO BEING PLAYED")]
        public static void IsHeroBeingPlayed() { }

        /// IS IN AIR
        /// Whether a player is airborne.
        /// Definitions:
        /// 
        /// Player - The player whose airborne status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS IN AIR")]
        public static void IsInAir() { }

        /// IS IN LINE OF SIGHT
        /// Whether two positions have line of sight with each other.
        /// Definitions:
        /// 
        /// Start Pos - The start position for the line of sight check. Most positional based Value Syntax can be used here.
        /// End Pos - The end position for the line of sight check. Most positional based Value Syntax can be used here.
        /// Barriers - Defines how barriers affect line of sight, when considering whether a barrier belongs to an enemy, the allegiance of the player provided to start pos (if any) is used. Can be set to “Barriers do not block LOS”, Enemy barriers block LOS", and “All barriers block LOS”.
        [WorkshopCodeName("IS IN LINE OF SIGHT")]
        public static void IsInLineOfSight() { }

        /// IS IN SETUP
        /// Whether the match is currently in its setup phase.
        /// There are no definitions to this value.
        [WorkshopCodeName("IS IN SETUP")]
        public static void IsInSetup() { }

        /// IS IN SPAWN ROOM
        /// Whether a specific player is in the spawn room (and is thus being healed and able to change heroes).
        /// Definitions:
        /// 
        /// Player - The player whose spawn room status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS IN SPAWN ROOM")]
        public static void IsInSpawnRoom() { }

        /// IS IN VIEW ANGLE
        /// Whether a location is within view of a player.
        /// Definitions:
        /// 
        /// Player - The player whose view to use for the check. Can use most player based Value Syntax to retrieve this value.
        /// Location - The location to test if it’s within view. Most positional based Value Syntax can be used here.
        /// View Angle - The view angle to compare against in degrees. Can use most angle based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS IN VIEW ANGLE")]
        public static void IsInViewAngle() { }

        /// IS MATCH COMPLETE
        /// Whether the match has finished.
        /// There are no definitions to this value.
        [WorkshopCodeName("IS MATCH COMPLETE")]
        public static void IsMatchComplete() { }

        /// IS MOVING
        /// Whether a specific player is moving (as defined by having a non-zero constant speed).
        /// Definitions:
        /// 
        /// Player - The player whose moving status status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS MOVING")]
        public static void IsMoving() { }

        /// IS OBJECTIVE COMPLETE
        /// Whether the specified objective has been completed Results in false if the game mode is not assault, escort, or assault/escort (hybrid).
        /// Definitions:
        /// 
        /// Number - The index of the objective to consider, starting at 0 and counting up. Each control point, payload checkpoint, and payload destination has its own index. Can use most number based Value Syntax to retrieve this value. Value must be in the form of an integer (whole number).
        [WorkshopCodeName("IS OBJECTIVE COMPLETE")]
        public static void IsObjectiveComplete() { }

        /// IS ON GROUND
        /// Whether a player is on the ground (or other walkable surface).
        /// Definitions:
        /// 
        /// Player - The player whose ground status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS ON GROUND")]
        public static void IsOnGround() { }

        /// IS ON OBJECTIVE
        /// Whether a specific player is currently occupying a payload or capture point.
        /// Definitions:
        /// 
        /// Player - The player whose objective status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS ON OBJECTIVE")]
        public static void IsOnObjective() { }

        /// IS ON WALL
        /// Whether a player is on a wall (climbing or riding).
        /// Definitions:
        /// 
        /// Player - The player whose wall status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS ON WALL")]
        public static void IsOnWall() { }

        /// IS PORTRAIT ON FIRE
        /// Whether a specific player’s portrait is on fire.
        /// Definitions:
        /// 
        /// Player - The player whose portrait to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS PORTRAIT ON FIRE")]
        public static void IsPortraitOnFire() { }

        /// IS STANDING
        /// Whether a player is standing (defined as both not moving and not in the air).
        /// Definitions:
        /// 
        /// Player - The player whose standing status to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS STANDING")]
        public static void IsStanding() { }

        /// IS TEAM ON DEFENSE
        /// Whether the specified team is currently on defense in a standard match.
        /// Definitions:
        /// 
        /// Team - The team whose role to check. Can use most Team Based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS TEAM ON DEFENSE")]
        public static void IsTeamOnDefense() { }

        /// IS TEAM ON OFFENSE
        /// Whether the specified team is currently on offense in a standard match.
        /// Definitions:
        /// 
        /// Team - The team whose role to check. Can use most Team Based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS TEAM ON OFFENSE")]
        public static void IsTeamOnOffense() { }

        /// IS TRUE FOR ALL
        /// Whether the specified condition evaluates to true for every value in the specified array.
        /// Definitions:
        /// 
        /// Array - The array whose values will be considered. Can use most Array Based Value Syntax to retrieve this value.
        /// Condition - The condition that is evaluated for each element of the specified array, Use the current array element value to reference the element of the array currently being considered. Can use most Comparative based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS TRUE FOR ALL")]
        public static void IsTrueForAll() { }

        /// IS TRUE FOR ANY
        /// Whether the specified condition evaluates to true for any value in the specified array.
        /// Definitions:
        /// 
        /// Array - The array whose values will be considered. Can use most Array Based Value Syntax to retrieve this value.
        /// Condition - The condition that is evaluated for each element of the specified array, Use the current array element value to reference the element of the array currently being considered. Can use most Comparative based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS TRUE FOR ANY")]
        public static void IsTrueForAny() { }

        /// IS USING ABILITY 1
        /// Whether the specified player is using ability 1.
        /// Definitions:
        /// 
        /// Player - The player whose ability 1 usage to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS USING ABILITY 1")]
        public static void IsUsingAbility1() { }

        /// IS USING ABILITY 2
        /// Whether the specified player is using ability 2.
        /// Definitions:
        /// 
        /// Player - The player whose ability 2 usage to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS USING ABILITY 2")]
        public static void IsUsingAbility2() { }

        /// IS USING ULTIMATE
        /// Whether the specified player is using an ultimate ability.
        /// Definitions:
        /// 
        /// Player - The player whose ultimate ability usage to check. Can use most player based Value Syntax to retrieve this value.
        [WorkshopCodeName("IS USING ULTIMATE")]
        public static void IsUsingUltimate() { }

        /// IS WAITING FOR PLAYERS
        /// Whether the match is waiting for players to join before starting.
        /// There are no definitions to this value.
        [WorkshopCodeName("IS WAITING FOR PLAYERS")]
        public static void IsWaitingForPlayers() { }

        /// LAST CREATED ENTITY
        /// A reference to the last effect or icon entity created by the event player (or created at the global level).
        /// There are no definitions to this value.
        [WorkshopCodeName("LAST CREATED ENTITY")]
        public static void LastCreatedEntity() { }

        /// LAST DAMAGE OVER TIME ID
        /// An ID representing the most recent damage over time action that was executed by the event player (or executed at the global level).
        /// There are no definitions to this value.
        [WorkshopCodeName("LAST DAMAGE OVER TIME ID")]
        public static void LastDamageOverTimeId() { }

        /// LAST HEAL OVER TIME ID
        /// An ID representing the most recent heal over time action that was executed by the event player (or executed at the global level).
        /// There are no definitions to this value.
        [WorkshopCodeName("LAST HEAL OVER TIME ID")]
        public static void LastHealOverTimeId() { }

        /// LAST OF
        /// The value at the end of the specified array. Results in a 0 if the specified array is empty.
        /// Definitions:
        /// 
        /// Array - The array from which the value is created. Can use most Array based Value Syntax to provide this data.
        [WorkshopCodeName("LAST OF")]
        public static void LastOf() { }

        /// LAST TEXT ID
        /// A reference to the last piece of text created by the event player (or created at the global level) via the create HUD text or create in-world text action.
        /// There are no definitions to this value.
        [WorkshopCodeName("LAST TEXT ID")]
        public static void LastTextId() { }

        /// LEFT
        /// Shorthand for the directional vector(1, 0, 0), which points to the left.
        /// There are no definitions to this value.
        [WorkshopCodeName("LEFT")]
        public static void Left() { }

        /// LOCAL VECTOR OF
        /// The vector in local coordinates corresponding to the provided vector in world coordinates.
        /// Definitions:
        /// 
        /// World Vector - The vector in world coordinates that will be converted to local coordinates. Can use most Vector based Value Syntax to provide this data.
        /// Relative player - The player to whom the resulting vector will be relative. Can use most Player based Value Syntax to provide this data.
        /// Transformation - Specifies whether the vector should receive a rotation and a translation (usually applied to positions) or only a rotation (usually applied to directions and velocities). You can choose from Rotation or Rotation and Translation. Rotation is that the resulting vector will be rotated to the new frame of reference. Use this option when the provided vector is in a direction or velocity. Rotation and translation is that the resulting vector will be rotated and translated to the new frame of reference. Use this option when the provided vector is a position.
        [WorkshopCodeName("LOCAL VECTOR OF")]
        public static void LocalVectorOf() { }

        /// MATCH ROUND
        /// The current round of the match, counting up from 1. This will return a numerical value
        /// There are no definitions to this value.
        [WorkshopCodeName("MATCH ROUND")]
        public static void MatchRound() { }

        /// MATCH TIME
        /// The amount of time in seconds remaining in the current game mode phase. This will return a numerical value.
        /// There are no definitions to this value.
        [WorkshopCodeName("MATCH TIME")]
        public static void MatchTime() { }

        /// MAX
        /// The greater of the two numbers. This will return a numerical value of two number values compared.
        /// Definitions:
        /// 
        /// Value - The left-hand operand. May be any value that results in a number. Can use any Number based Value syntax to compare with.
        /// Value - The right-hand operand. May be any value that results in a number. Can use any Number based Value syntax to compare with.
        [WorkshopCodeName("MAX")]
        public static void Max() { }

        /// MAX HEALTH
        /// The max health of a player, including armor and shields.
        /// Definitions:
        /// 
        /// Player - The player whose max health to compare. Can use any Player based Value syntax to provide with.
        [WorkshopCodeName("MAX HEALTH")]
        public static void MaxHealth() { }

        /// MIN
        /// The lesser of the two numbers. This will return a numerical value of two number values compared.
        /// Definitions:
        /// 
        /// Value - The left-hand operand. May be any value that results in a number. Can use any Number based Value syntax to compare with.
        /// Value - The right-hand operand. May be any value that results in a number. Can use any Number based Value syntax to compare with.
        [WorkshopCodeName("MIN")]
        public static void Min() { }

        /// MODULO
        /// The remainder of the left-hand operand divided by the right-hand operand. Any number modulo zero will result in zero. This will return a numerical value of two number values compared. For example 7 divided by 2 will result in 1 for the Modulo.
        /// Definitions:
        /// 
        /// Value - The left-hand operand. May be any value that results in a number. Can use any Number based Value syntax.
        /// Value - The right-hand operand. May be any value that results in a number. Can use any Number based Value syntax.
        [WorkshopCodeName("MODULO")]
        public static void Modulo() { }

        /// MULTIPLY
        /// The product of two numbers or vectors. A vector multiplied by a number will yield a scaled vector.
        /// Definitions:
        /// 
        /// Value - The left-hand operand. May be any value that results in a number or a vector. Can use any Number based or Vector based Value syntax to multiply with.
        /// Value - The left-hand operand. May be any value that results in a number or a vector. Can use any Number based or Vector based Value syntax to multiply with.
        [WorkshopCodeName("MULTIPLY")]
        public static void Multiply() { }

        /// NEAREST WALKABLE POSITION
        /// The position closest to the specified position that can be stood on and is accessible from a spawn point.
        /// Definitions:
        /// 
        /// Position - The position from which to search for the nearest walkable position. Can use any Vector based Value syntax to divide with.
        [WorkshopCodeName("NEAREST WALKABLE POSITION")]
        public static void NearestWalkablePosition() { }

        /// NORMALIZE
        /// The unit-length normalization of a vector.
        /// Definitions:
        /// 
        /// Vector - The vector to normalize. Can use any Vector based Value syntax to divide with.
        [WorkshopCodeName("NORMALIZE")]
        public static void Normalize() { }

        /// NOT
        /// Whether the input is false (or the equivalent to false)
        /// Definitions:
        /// 
        /// Value - When this input is false (or equivalent to false), then the not value is true. Otherwise, the not value is false. Can use most Boolean-based Value Syntax to provide this value.
        [WorkshopCodeName("NOT")]
        public static void Not() { }

        /// NULL
        /// The absence of a player, used when no player is desired for a particular input, equivalent to the real number 0 for the purposes of comparison and debugging.
        /// There are no definitions to this value.
        [WorkshopCodeName("NULL")]
        public static void Null() { }

        /// NUMBER
        /// A real number constant.
        /// Definitions:
        /// 
        /// Number - A real number constant. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER")]
        public static void Number() { }

        /// NUMBER OF DEAD PLAYERS
        /// The number of dead players on a team or in the match.
        /// Definitions:
        /// 
        /// Team - The team or teams on which to count players. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF DEAD PLAYERS")]
        public static void NumberOfDeadPlayers() { }

        /// NUMBER OF DEATHS
        /// The number of deaths a specific player has earned. This value only accumulates while a game is in progress.
        /// Definitions:
        /// 
        /// Player - The player whose death count to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF DEATHS")]
        public static void NumberOfDeaths() { }

        /// NUMBER OF ELIMINATIONS
        /// The number of eliminations a specific player has earned. This value only accumulates while a game is in progress.
        /// Definitions:
        /// 
        /// Player - The player whose elimination count to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF ELIMINATIONS")]
        public static void NumberOfEliminations() { }

        /// NUMBER OF FINAL BLOWS
        /// The number of final blows a specific player has earned. This value only accumulates while a game is in progress.
        /// Definitions:
        /// 
        /// Player - The player whose final blow count to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF FINAL BLOWS")]
        public static void NumberOfFinalBlows() { }

        /// NUMBER OF HEROES
        /// The number of players playing a specific hero on a team or in the match.
        /// Definitions:
        /// 
        /// Hero - The hero to check for play. Can use most Hero based Value Syntax to provide this value.
        /// Team - The team or teams on which to check for the hero being played. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF HEROES")]
        public static void NumberOfHeroes() { }

        /// NUMBER OF LIVING PLAYERS
        /// The number of living players on a team or in the match.
        /// Definitions:
        /// 
        /// Team - The team or teams on which to count players. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF LIVING PLAYERS")]
        public static void NumberOfLivingPlayers() { }

        /// NUMBER OF PLAYERS
        /// The number of players on a team or in the match.
        /// Definitions:
        /// 
        /// Team - The team or teams on which to count players. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF PLAYERS")]
        public static void NumberOfPlayers() { }

        /// NUMBER OF PLAYERS ON OBJECTIVE
        /// The number of players occupying a payload or a control point (either on a team or in the match).
        /// Definitions:
        /// 
        /// Team - The team or teams on which to count players. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("NUMBER OF PLAYERS ON OBJECTIVE")]
        public static void NumberOfPlayersOnObjective() { }

        /// OBJECTIVE INDEX
        /// The control point, payload checkpoint, or payload destination currently active (either 0, 1, or 2). Valid in Assault, Assault/Escort (Hybrid), Escort, and Control.
        /// There are no definitions to this value.
        [WorkshopCodeName("OBJECTIVE INDEX")]
        public static void ObjectiveIndex() { }

        /// OBJECTIVE POSITION
        /// The position in the world of the specified objective (either a control point, a payload checkpoint, or a payload destination) Valid in Assault, Assault/Escort (Hybrid), Escort, and Control.
        /// Definitions:
        /// 
        /// Number - The index of the objective to consider, starting at 0 and counting up. Each control point, payload checkpoint, and payload destination as its own index. Can use most Number based Value Syntax to provide this value, but must output in a integer of 0, 1, or 2.
        [WorkshopCodeName("OBJECTIVE POSITION")]
        public static void ObjectivePosition() { }

        /// OPPOSITE TEAM OF
        /// The team opposite the specified team.
        /// Definitions:
        /// 
        /// Team - The team whose opposite to acquire. If all, the result will be all. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("OPPOSITE TEAM OF")]
        public static void OppositeTeamOf() { }

        /// OR
        /// Whether either of the two inputs are true (or equivalent to true).
        /// Definitions:
        /// 
        /// Value - One of the two inputs considered. If either one is true (or equivalent to true), then the OR value is true. Can use most Boolean based Value Syntax to provide this value.
        /// Value - One of the two inputs considered. If either one is true (or equivalent to true), then the OR value is true. Can use most Boolean based Value Syntax to provide this value.
        [WorkshopCodeName("OR")]
        public static void Or() { }

        /// PAYLOAD POSITION
        /// The position in the world of the active payload.
        /// There are no definitions to this value.
        [WorkshopCodeName("PAYLOAD POSITION")]
        public static void PayloadPosition() { }

        /// PAYLOAD PROGRESS PERCENTAGE
        /// The current progress towards the destination for the active payload (expressed as a percentage).
        /// There are no definitions to this value.
        [WorkshopCodeName("PAYLOAD PROGRESS PERCENTAGE")]
        public static void PayloadProgressPercentage() { }

        /// PLAYER CARRYING FLAG
        /// The player carrying a particular team’s flag in capture the flag. Results in null if no player is carrying the flag.
        /// Definitions:
        /// 
        /// Team - The team whose whose flag to check. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("PLAYER CARRYING FLAG")]
        public static void PlayerCarryingFlag() { }

        /// PLAYER CLOSEST TO RETICLE
        /// The player closest to the reticle of the specified player, optionally restricted by team.
        /// Definitions:
        /// 
        /// Player - The player from whose reticle to search for the closest player. Can use most Player based Value Syntax to provide this value.
        /// Team - The team or teams on which to search for the closest player. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("PLAYER CLOSEST TO RETICLE")]
        public static void PlayerClosestToReticle() { }

        /// PLAYER VARIABLE
        /// The current value of a player variable, which is a variable that belongs to a specific player.
        /// Definitions:
        /// 
        /// Player - The player whose variable to acquire. Can use most Player based Value Syntax to provide this value.
        /// Variable - Variable specified by a single alphabetic letter (A through Z).
        [WorkshopCodeName("PLAYER VARIABLE")]
        public static void PlayerVariable() { }

        /// PLAYERS IN SLOT
        /// The player or array of players who occupy a specific slot in the game.
        /// Definitions:
        /// 
        /// Slot - The slot number from each to acquire a player or players. In team games, each team has slots 0 through 5. In free-for-all games, slots are numbered 0 through 11. Can use most Number based Value Syntax to provide this value.
        /// Team - The team or teams from which to acquire a player or players. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("PLAYERS IN SLOT")]
        public static void PlayersInSlot() { }

        /// PLAYERS IN VIEW ANGLE
        /// The players who are within a specific view angle of a specific player’s reticle, optionally restricted by team.
        /// Definitions:
        /// 
        /// Player - The player whose view to use for the check. Can use most Player based Value Syntax to provide this value.
        /// Team - The team or teams on which to consider players. Can use most Team based Value Syntax to provide this value.
        /// View Angle - The view angle to compare against in degrees. Can use most Angle based Value Syntax to provide this value.
        [WorkshopCodeName("PLAYERS IN VIEW ANGLE")]
        public static void PlayersInViewAngle() { }

        /// PLAYERS ON HERO
        /// The array of players playing a specific hero on a team or in the match.
        /// Definitions:
        /// 
        /// Hero - The hero to check for play. Can use most Hero based Value Syntax to provide this value.
        /// Team - The team or teams on which to check for the hero being played. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("PLAYERS ON HERO")]
        public static void PlayersOnHero() { }

        /// PLAYERS WITHIN RADIUS
        /// An array containing all players within a certain distance of a position, optionally restricted by team and line of sight.
        /// Definitions:
        /// 
        /// Center - The center position from which to measure distance. Can use most Vector based Value Syntax to provide this value.
        /// Radius - The radius in meters inside which players must be in order to be included in the resulting array. Can use most Number based Value Syntax to provide this value.
        /// Team - The team or teams to which a player must belong to be included in the resulting array. Can use most Team based Value Syntax to provide this value.
        /// LOS Check - Specifies whether and how a player must pass a line-of-sight check to be included in the resulting array. You can choose from Off, Surfaces, Surfaces and Enemy Barriers, and Surfaces and All Barriers. Off will result in the line of sight is never blocked, allowing results through walls. Surfaces will result in line of sight is blocked by ceilings, walls, floors, platforms, and any fixed object that blocks projectiles. Surfaces and Enemy Barriers will result in line of sight is blocked by ceilings, walls, floors, platforms, any fixed object that blocks projectiles, and barriers created by the enemy team. Surfaces and All Barriers will result in line of sight is blocked by ceilings, walls, floors, platforms, any fixed object that blocks projectiles, and all barriers.
        [WorkshopCodeName("PLAYERS WITHIN RADIUS")]
        public static void PlayersWithinRadius() { }

        /// POINT CAPTURE PERCENTAGE
        /// The current progress towards capture for the active control point (expressed as a percentage).
        /// There are no definitions to this value.
        [WorkshopCodeName("POINT CAPTURE PERCENTAGE")]
        public static void PointCapturePercentage() { }

        /// POSITION OF
        /// The current position of a player as a vector.
        /// Definitions:
        /// 
        /// Player - The player whose position to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("POSITION OF")]
        public static void PositionOf() { }

        /// RAISE TO POWER
        /// The left-hand operand raised to the power of the right-hand operand. For example 2 ^ 3 = 8
        /// Definitions:
        /// 
        /// Value - The left-hand operand. May be any value that results in a number. Can use most Number based Value Syntax to provide this value.
        /// Value - The right-hand operand. May be any value that results in a number. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("RAISE TO POWER")]
        public static void RaiseToPower() { }

        /// RANDOM INTEGER
        /// A random integer between the specified min and max, inclusive.
        /// Definitions:
        /// 
        /// MIN - The smallest integer allowed. If a real number is provided to this input, it is rounded to the nearest integer. Can use most Number based Value Syntax to provide this value.
        /// MAX - The largest integer allowed. If a real number is provided to this input, it is rounded to the nearest integer. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("RANDOM INTEGER")]
        public static void RandomInteger() { }

        /// RANDOM REAL
        /// A random real number between the specified min and max.
        /// Definitions:
        /// 
        /// MIN - The smallest real number allowed. Can use most Number based Value Syntax to provide this value.
        /// MAX - The largest real number allowed. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("RANDOM REAL")]
        public static void RandomReal() { }

        /// RANDOM VALUE IN ARRAY
        /// A random value from the specified array.
        /// Definitions:
        /// 
        /// Array - The array from which to randomly take a value. If a non-array value is provided, the result is simply the provided value. Can use most Array based Value Syntax to provide this value.
        [WorkshopCodeName("RANDOM VALUE IN ARRAY")]
        public static void RandomValueInArray() { }

        /// RANDOMIZED ARRAY
        /// A copy of the specified array with the values in a random order
        /// Definitions:
        /// 
        /// Array - The array whose copy will be randomized. Can use most Array based Value Syntax to provide this value.
        [WorkshopCodeName("RANDOMIZED ARRAY")]
        public static void RandomizedArray() { }

        /// RAY CAST HIT NORMAL - New!
        /// The surface normal at the ray cast hit position (or from end pos to start pos if no hit occurs).
        /// Definitions:
        /// 
        /// Start POS - The start position for the ray cast. If a player is provided. A position 2 meters above the player’s feet is used. Can use most Vector based Value Syntax to provide this value.
        /// End POS - The end position for the ray cast. If a player is provided. A position 2 meters above the player’s feet is used. Can use most Vector based Value Syntax to provide this value.
        /// Players to include - Which players can be hit by this ray cast. Can use most Player based Value Syntax to provide this value.
        /// Players to exclude - Which players cannot be hit by this ray cast. This list takes precedence over players to include. Can use most Player based Value Syntax to provide this value.
        /// Include player owned objects - Whether player owned objects (such as barriers or turrets) should be included in the ray cast. Can use most Boolean based Value Syntax to provide this value.
        [WorkshopCodeName("RAY CAST HIT NORMAL - New!")]
        public static void RayCastHitNormal() { }

        /// RAY CAST HIT PLAYER - New!
        /// The player hit by the ray cast (or null if no player is hit).
        /// Definitions:
        /// 
        /// Start POS - The start position for the ray cast. If a player is provided. A position 2 meters above the player’s feet is used. Can use most Vector based Value Syntax to provide this value.
        /// End POS - The end position for the ray cast. If a player is provided. A position 2 meters above the player’s feet is used. Can use most Vector based Value Syntax to provide this value.
        /// Players to include - Which players can be hit by this ray cast. Can use most Player based Value Syntax to provide this value.
        /// Players to exclude - Which players cannot be hit by this ray cast. This list takes precedence over players to include. Can use most Player based Value Syntax to provide this value.
        /// Include player owned objects - Whether player owned objects (such as barriers or turrets) should be included in the ray cast. Can use most Boolean based Value Syntax to provide this value.
        [WorkshopCodeName("RAY CAST HIT PLAYER - New!")]
        public static void RayCastHitPlayer() { }

        /// RAY CAST HIT POSITION - New!
        /// The position where the ray cast hits a surface, object, or player (or the end POS if no hit occurs).
        /// Definitions:
        /// 
        /// Start POS - The start position for the ray cast. If a player is provided. A position 2 meters above the player’s feet is used. Can use most Vector based Value Syntax to provide this value.
        /// End POS - The end position for the ray cast. If a player is provided. A position 2 meters above the player’s feet is used. Can use most Vector based Value Syntax to provide this value.
        /// Players to include - Which players can be hit by this ray cast. Can use most Player based Value Syntax to provide this value.
        /// Players to exclude - Which players cannot be hit by this ray cast. This list takes precedence over players to include. Can use most Player based Value Syntax to provide this value.
        /// Include player owned objects - Whether player owned objects (such as barriers or turrets) should be included in the ray cast. Can use most Boolean based Value Syntax to provide this value.
        [WorkshopCodeName("RAY CAST HIT POSITION - New!")]
        public static void RayCastHitPosition() { }

        /// REMOVE FROM ARRAY
        /// A copy of an array with one or more values removed (if found).
        /// Definitions:
        /// 
        /// Array - The array from which to remove values. Can use most Array based Value Syntax to provide this value.
        /// Value - The value to remove from the array (if found), if this value itself an array, each matching element is removed. Can use most Array based or Number based Value Syntax to provide this value.
        [WorkshopCodeName("REMOVE FROM ARRAY")]
        public static void RemoveFromArray() { }

        /// RIGHT
        /// Shorthand for the directional vector (-1, 0, 0), which points to the right.
        /// There are no definitions to this value.
        [WorkshopCodeName("RIGHT")]
        public static void Right() { }

        /// ROUND TO INTEGER
        /// The integer to which the specified value rounds.
        /// Definitions:
        /// 
        /// Value - The real number to round. Can use most Number based Value Syntax to provide this value.
        /// Rounding Type - Determines the direction in which the value will be rounded. You can round up, down, or to the nearest integer.
        [WorkshopCodeName("ROUND TO INTEGER")]
        public static void RoundToInteger() { }

        /// SCORE OF
        /// The current score of a player. Results in 0 if the game mode is not free-for-all.
        /// Definitions:
        /// 
        /// Player - The player whose score to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("SCORE OF")]
        public static void ScoreOf() { }

        /// SINE FROM DEGREES
        /// Sine of the specified angle in degrees. The sine is the ratio of the length of the side that is opposite  that angle to the length of the longest side of the triangle (the hypotenuse).
        /// Definitions:
        /// 
        /// Angle - Angle in degrees. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("SINE FROM DEGREES")]
        public static void SineFromDegrees() { }

        /// SINE FROM RADIANS
        /// Sine of the specified angle in radians. The sine is the ratio of the length of the side that is opposite  that angle to the length of the longest side of the triangle (the hypotenuse). A radian is a unit of angle, equal to an angle at the center of a circle whose arc is equal in length to the radius.
        /// Definitions:
        /// 
        /// Angle - Angle in radians. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("SINE FROM RADIANS")]
        public static void SineFromRadians() { }

        /// SLOT OF
        /// The slot number of the specified player. In team games, each team has slots 0 through 5. In free-for-all games, slots are numbers 0 through 11.
        /// Definitions:
        /// 
        /// Player - The player whose slot number to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("SLOT OF")]
        public static void SlotOf() { }

        /// SORTED ARRAY
        /// A copy of the specified array with the values sorted according to the value rank that is evaluated for each element.
        /// Definitions:
        /// 
        /// Array - The array whose copy will be sorted. Can use most Array based Value Syntax to provide this value.
        /// Value Rank - The value that is evaluated for each element of the copied array. The array is sorted by this rank in ascending order. Use the current array element value to reference the element of the array currently being considered. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("SORTED ARRAY")]
        public static void SortedArray() { }

        /// SPEED OF
        /// The current speed of a player in meters per second.
        /// Definitions:
        /// 
        /// Player - The player whose speed to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("SPEED OF")]
        public static void SpeedOf() { }

        /// SPEED OF IN DIRECTION
        /// The current speed of a player in a specific direction in meters per second.
        /// Definitions:
        /// 
        /// Player - The player whose speed to acquire. Can use most Player based Value Syntax to provide this value.
        /// Direction - The direction of travel in which to measure the player’s speed. Can use most Vector based Value Syntax to provide this value.
        [WorkshopCodeName("SPEED OF IN DIRECTION")]
        public static void SpeedOfInDirection() { }

        /// SQUARE ROOT
        /// The square root of the specified value. For example the square root of 9 is 3.
        /// Definitions:
        /// 
        /// Value - The real number value whose square root will be computed. Negative values result in zero. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("SQUARE ROOT")]
        public static void SquareRoot() { }

        /// STRING
        /// Text formed from a selection of strings and specified values.
        /// Definitions:
        /// 
        /// String - How the string will be structured using a series of text and phrases.
        /// {0} - The first value in the string.
        /// {1} - The second value in the string.
        /// {2} - The third value in the string.
        [WorkshopCodeName("STRING")]
        public static void String() { }

        /// SUBTRACT
        /// The difference between two numbers or vectors.
        /// Definitions:
        /// 
        /// Value - The left-hand operand. May be any value that results in a number or a vector. Can use most Number based Value Syntax to provide this value.
        /// Value - The right-hand operand. May be any value that results in a number or a vector. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("SUBTRACT")]
        public static void Subtract() { }

        /// TEAM
        /// A team constant. The all option represents both teams in a team or all players in a free-for-all game.
        /// Definitions:
        /// 
        /// Team - Specifies which team the value outputs to. This can be set to All, Team 1, or Team 2.
        [WorkshopCodeName("TEAM")]
        public static void Team() { }

        /// TEAM OF
        /// The team of a player. If the game mode is free-for-all, the team is considered to be all.
        /// Definitions:
        /// 
        /// Player - The player whose team to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("TEAM OF")]
        public static void TeamOf() { }

        /// TEAM SCORE
        /// The current score for the specified team. Results in a 0 in free-for-all game modes.
        /// Definitions:
        /// 
        /// Team - The team whose score to acquire. Can use most Team based Value Syntax to provide this value.
        [WorkshopCodeName("TEAM SCORE")]
        public static void TeamScore() { }

        /// THROTTLE OF
        /// The directional input of a player, represented by a vector with a horizontal input on the X component (positive to the left) and vertical input on the Z component (positive upward).
        /// Definitions:
        /// 
        /// Player - The player whose directional input to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("THROTTLE OF")]
        public static void ThrottleOf() { }

        /// TOTAL TIME PLAYED
        /// The total time in seconds that have elapsed since the game instance was created (including setup time and transitions).
        /// There are no definitions to this value.
        [WorkshopCodeName("TOTAL TIME PLAYED")]
        public static void TotalTimePlayed() { }

        /// TRUE
        /// The Boolean value of true.
        /// There are no definitions to this value.
        [WorkshopCodeName("TRUE")]
        public static void True() { }

        /// ULTIMATE CHARGE PERCENT
        /// The current ultimate ability charge percentage of a player.
        /// Definitions:
        /// 
        /// Player - The player whose ultimate charge percentage to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("ULTIMATE CHARGE PERCENT")]
        public static void UltimateChargePercent() { }

        /// UP
        /// Shorthand for the directional vector(0, 1, 0). Which points upward.
        /// There are no definitions to this value.
        [WorkshopCodeName("UP")]
        public static void Up() { }

        /// VALUE IN ARRAY
        /// The value found at a specific element of an array. Results in a 0 if the element does not exist.
        /// Definitions:
        /// 
        /// Array - The array whose element to acquire. Can use most Array based Value Syntax to provide this value.
        /// Index - The index whose element to acquire. Can use most Number based Value Syntax to provide this value.
        [WorkshopCodeName("VALUE IN ARRAY")]
        public static void ValueInArray() { }

        /// VECTOR
        /// A vector composed of three real numbers (X, Y, Z) where X is left, Y is Up, and Z is forward. Vectors are used for position, direction, and velocity.
        /// Definitions:
        /// 
        /// X - The X value of the Vector. Can use most Number based Value Syntax to provide this value.
        /// Y - The Y value of the vector. Can use most Number based Value Syntax to provide this value.
        /// Z - The Z value of the Vector. Can use most Number based Value Syntax to provide this value.
        /// 
        ///  Live Capture Button:
        /// If you are using this value to populate for another value in a condition or action, you can click the live capture button to collect the current position your hero or spectator ghost in the game environment.
        [WorkshopCodeName("VECTOR")]
        public static void Vector() { }

        /// VECTOR TOWARDS
        /// The displacement vector from one position to another.
        /// Definitions:
        /// 
        /// Start Pos - The start position for the line of sight check. Most positional based Value Syntax can be used here.
        /// End Pos - The end position for the line of sight check. Most positional based Value Syntax can be used here.
        [WorkshopCodeName("VECTOR TOWARDS")]
        public static void VectorTowards() { }

        /// VELOCITY OF
        /// The current velocity of a player as a vector. If the player is on a surface, the Y component of this velocity will be 0m even when traveling up or down a slope.
        /// Definitions:
        /// 
        /// Player - The player whose velocity to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("VELOCITY OF")]
        public static void VelocityOf() { }

        /// VERTICAL ANGLE FROM DIRECTION
        /// The vertical angle in degrees corresponding to the specified direction vector.
        /// Definitions:
        /// 
        /// Direction - The direction vector from which to acquire a vertical angle in degrees. The vector is unitized before calculations begins. Can use most Vector based Value Syntax to provide this value.
        [WorkshopCodeName("VERTICAL ANGLE FROM DIRECTION")]
        public static void VerticalAngleFromDirection() { }

        /// VERTICAL ANGLE TOWARDS
        /// The vertical angle in degrees from a player’s current forward direction to the specified position. The result is positive if the position is below the player. Otherwise, the result is zero or negative.
        /// Definitions:
        /// 
        /// Position - The player whose current facing the angle begins. Can use most Player based Value Syntax to provide this value.
        /// Position - The direction vector from which to acquire a vertical angle in degrees. The vector is unitized before calculations begins. Can use most Vector based Value Syntax to provide this value.
        [WorkshopCodeName("VERTICAL ANGLE TOWARDS")]
        public static void VerticalAngleTowards() { }

        /// VERTICAL FACING ANGLE OF
        /// The vertical angle in degrees, of a player’s current facing relative to the world. This value increases as the player looks down.
        /// Definitions:
        /// 
        /// Player - The player whose vertical facing angle to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("VERTICAL FACING ANGLE OF")]
        public static void VerticalFacingAngleOf() { }

        /// VERTICAL SPEED OF
        /// The current vertical speed of a player in meters per second. This measurement excludes all horizontal motion, including motion while traveling up and down slopes.
        /// Definitions:
        /// 
        /// Player - The player whose vertical speed to acquire. Can use most Player based Value Syntax to provide this value.
        [WorkshopCodeName("VERTICAL SPEED OF")]
        public static void VerticalSpeedOf() { }

        /// VICTIM
        /// The player that received damage for the event currently being processed by this rule. May be the same as the attacker or the event player.
        /// There are no definitions to this value.
        [WorkshopCodeName("VICTIM")]
        public static void Victim() { }

        /// WORLD VECTOR OF
        /// The vector in the world coordinates corresponding to the provided vector in local coordinates.
        /// Definitions:
        /// 
        /// Local vector - The vector in local coordinates that will be converted to world coordinates. Can use most Vector based Value Syntax to provide this value.
        /// Relative Player - The player to whom the local vector is relative. Can use most Player based Value Syntax to provide this value.
        /// Local vector - Specifies whether the vector should receive a rotation and a translation (usually applied to positions) or only a rotation (usually applied to directions and velocities). Can select rotation or rotation and translation.
        [WorkshopCodeName("WORLD VECTOR OF")]
        public static void WorldVectorOf() { }

        /// X COMPONENT OF
        /// The X Component of the specified Vector, usually representing a leftward amount.
        /// Definitions:
        /// 
        /// Value - The vector from which to acquire the X component. Can use most Vector based Value Syntax to provide this value.
        [WorkshopCodeName("X COMPONENT OF")]
        public static void XComponentOf() { }

        /// Y COMPONENT OF
        /// The Y Component of the specified Vector, usually representing a upward amount.
        /// Definitions:
        /// 
        /// Value - The vector from which to acquire the Y component. Can use most Vector based Value Syntax to provide this value.
        [WorkshopCodeName("Y COMPONENT OF")]
        public static void YComponentOf() { }

        /// Z COMPONENT OF
        /// The Z Component of the specified Vector, usually representing a forward amount.
        /// Definitions:
        /// 
        /// Value - The vector from which to acquire the Z component. Can use most Vector based Value Syntax to provide this value.
        [WorkshopCodeName("Z COMPONENT OF")]
        public static void ZComponentOf() { }

    }
}
