using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Miunie.Core;
using Miunie.Core.Entities.Views;

namespace Miunie.ConsoleApp
{
    class ImpersonationChatPage
    {
        private readonly MiunieBot _miunie;
        
        private ulong _currentGuildId;

        public ImpersonationChatPage(MiunieBot miunie)
        {
            _miunie = miunie;
        }

        public void FetchInfo()
        {
            var selectedGuildId = SelectGuild();
            var SelectedChannel = (_miunie.Impersonation.GetAvailableTextChannelsAsync(selectedGuildId));
        }

        private ulong SelectGuild()
        {
            List<GuildView> Guilds = (List<GuildView>)_miunie.Impersonation.GetAvailableGuilds();
            ulong selectedGuildId = 0;
            int hoveringNumber = 0;
            bool hoveringNumberHasMoved = true;
            ConsoleKeyInfo keyPressed;
            do
            {
                if (hoveringNumberHasMoved)
                {
                    for (int i = 0; i < Guilds.Count; i++)
                    {
                        if (i == hoveringNumber)
                        {
                            Console.WriteLine("->" + Guilds[i].Name);
                            selectedGuildId = Guilds[i].Id;
                        }
                        else
                        {
                            Console.WriteLine(Guilds[i].Name);
                        }
                    }
                }
                keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    if (hoveringNumber != Guilds.Count - 1)
                    {
                        hoveringNumber++;
                        hoveringNumberHasMoved = true;
                        Console.Clear();
                    }
                    else hoveringNumberHasMoved = false;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    if (hoveringNumber != 0)
                    {
                        hoveringNumber--;
                        hoveringNumberHasMoved = true;
                        Console.Clear();
                    }
                    else hoveringNumberHasMoved = false;
                }
            } while (keyPressed.Key != ConsoleKey.Enter && selectedGuildId != 0);
            return selectedGuildId;
        }

        public async void SelectChannel(List<TextChannelView> Channels)
        {
            
        }

        public async void LoadMessages()
        {
            var currentMessages = await _miunie.Impersonation.GetMessagesFromTextChannelAsync(_currentGuildId, );
        }
    }
}
