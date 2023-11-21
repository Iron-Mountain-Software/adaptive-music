using System;
using System.Collections.Generic;

namespace IronMountain.AdaptiveMusic
{
    public static class SongPlayersManager
    {
        public static event Action OnSongPlayersChanged;
        
        public static readonly List<SongPlayer> SongPlayers = new ();

        public static void Register(SongPlayer songPlayer)
        {
            if (SongPlayers.Contains(songPlayer)) return;
            SongPlayers.Add(songPlayer);
            OnSongPlayersChanged?.Invoke();
        }
        
        public static void Unregister(SongPlayer songPlayer)
        {
            if (!SongPlayers.Contains(songPlayer)) return;
            SongPlayers.Remove(songPlayer);
            OnSongPlayersChanged?.Invoke();
        }
    }
}