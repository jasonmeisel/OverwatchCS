using System;

namespace Workshop
{
    public enum Events
    {
        /// ONGOING - GLOBAL
        /// The ONGOING - GLOBAL event attribute will affect all entities in the game environment.
        /// This event attribute has no specific definitions.
        /// Tutorials:
        /// 
        /// Tutorial Video by WyomingMyst 308
        [WorkshopCodeName("ONGOING - GLOBAL")]
        OngoingGlobal,

        /// ONGOING - EACH PLAYER
        /// The ONGOING - EACH PLAYER event attribute will affect the specified players in the game environment.
        /// Definitions:
        /// 
        /// 
        /// Team
        /// 
        /// ALL - This event will affect both teams.
        /// TEAM 1 - This event will affect team 1 (blue/defense)
        /// TEAM 2 - This event will affect team 2 (red/attack)
        /// 
        /// 
        /// 
        /// Players
        /// 
        /// ALL - This event will affect all players regardless of team, slot position, or hero selected.
        /// SLOT # (0-11) -  This event affect the slot assignment of that specific player in the order of the Lobby (see diagram below)
        /// {HERO NAME} - This event affects any players using the specified hero.
        [WorkshopCodeName("ONGOING - EACH PLAYER")]
        OngoingEachPlayer,

        /// PLAYER EARNED ELIMINATION
        /// The PLAYER EARNED ELIMINATION event attribute will affect the specified players who successfully score an elimination in the game environment.
        /// Definitions:
        /// 
        /// 
        /// Team
        /// 
        /// ALL - This event will affect both teams.
        /// TEAM 1 - This event will affect team 1 (blue/defense)
        /// TEAM 2 - This event will affect team 2 (red/attack)
        /// 
        /// 
        /// 
        /// Players
        /// 
        /// ALL - This event will affect all players regardless of team, slot position, or hero selected.
        /// SLOT # (0-11) -  This event affect the slot assignment of that specific player in the order of the Lobby (see diagram below)
        /// {HERO NAME} - This event affects any players using the specified hero.
        [WorkshopCodeName("PLAYER EARNED ELIMINATION")]
        PlayerEarnedElimination,

        /// PLAYER DEALT FINAL BLOW
        /// The PLAYER DEALT FINAL BLOW event attribute will affect the specified players who successfully dealt the lethal damage against another player in the game environment.
        /// Definitions:
        /// 
        /// 
        /// Team
        /// 
        /// ALL - This event will affect both teams.
        /// TEAM 1 - This event will affect team 1 (blue/defense)
        /// TEAM 2 - This event will affect team 2 (red/attack)
        /// 
        /// 
        /// 
        /// Players
        /// 
        /// ALL - This event will affect all players regardless of team, slot position, or hero selected.
        /// SLOT # (0-11) -  This event affect the slot assignment of that specific player in the order of the Lobby (see diagram below)
        /// {HERO NAME} - This event affects any players using the specified hero.
        [WorkshopCodeName("PLAYER DEALT FINAL BLOW")]
        PlayerDealtFinalBlow,

        /// PLAYER DEALT DAMAGE
        /// The PLAYER DEALT DAMAGE event attribute will affect the specified players who successfully dealt damage against another player in the game environment.
        /// Definitions:
        /// 
        /// 
        /// Team
        /// 
        /// ALL - This event will affect both teams.
        /// TEAM 1 - This event will affect team 1 (blue/defense)
        /// TEAM 2 - This event will affect team 2 (red/attack)
        /// 
        /// 
        /// 
        /// Players
        /// 
        /// ALL - This event will affect all players regardless of team, slot position, or hero selected.
        /// SLOT # (0-11) -  This event affect the slot assignment of that specific player in the order of the Lobby (see diagram below)
        /// {HERO NAME} - This event affects any players using the specified hero.
        [WorkshopCodeName("PLAYER DEALT DAMAGE")]
        PlayerDealtDamage,

        /// PLAYER TOOK DAMAGE
        /// The PLAYER TOOK DAMAGE event attribute will affect the specified players who received damage in the game environment.
        /// Definitions:
        /// 
        /// 
        /// Team
        /// 
        /// ALL - This event will affect both teams.
        /// TEAM 1 - This event will affect team 1 (blue/defense)
        /// TEAM 2 - This event will affect team 2 (red/attack)
        /// 
        /// 
        /// 
        /// Players
        /// 
        /// ALL - This event will affect all players regardless of team, slot position, or hero selected.
        /// SLOT # (0-11) -  This event affect the slot assignment of that specific player in the order of the Lobby (see diagram below)
        /// {HERO NAME} - This event affects any players using the specified hero.
        [WorkshopCodeName("PLAYER TOOK DAMAGE")]
        PlayerTookDamage,

        /// PLAYER DIED
        /// The PLAYER DIED event attribute will affect the specified players who died in the game environment.
        /// Definitions:
        /// 
        /// 
        /// Team
        /// 
        /// ALL - This event will affect both teams.
        /// TEAM 1 - This event will affect team 1 (blue/defense)
        /// TEAM 2 - This event will affect team 2 (red/attack)
        /// 
        /// 
        /// 
        /// Players
        /// 
        /// ALL - This event will affect all players regardless of team, slot position, or hero selected.
        /// SLOT # (0-11) -  This event affect the slot assignment of that specific player in the order of the Lobby (see diagram below)
        /// {HERO NAME} - This event affects any players using the specified hero.
        [WorkshopCodeName("PLAYER DIED")]
        PlayerDied,
    }
}
