// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

using Miunie.Core;
using Miunie.Core.Entities.Views;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Miunie.ConsoleApp
{
    public class ServerMenu
    {
        private readonly MiunieBot _miunie;

        private GuildView _currentGuild;

        private TextChannelView _currentChannel;

        public ServerMenu(MiunieBot miunie)
        {
            _miunie = miunie;
        }

        public async Task ServerMenuAsync()
        {
            bool inServerMenu = true;
            FetchInfoAsync();
            while (inServerMenu)
            {
                Console.WriteLine($"Guild: {_currentGuild.Name} | Channel {_currentChannel.Name}");
                Console.WriteLine(ConsoleStrings.SERVER_MENU_OPTIONS);
                Console.Write(ConsoleStrings.PLEASE_ENTER_MENU_NUMBER);
                var success = int.TryParse(Console.ReadLine(), out var choice);

                if (!success)
                {
                    Console.WriteLine(ConsoleStrings.ERROR_CHOICE_NOT_A_NUMBER);
                }

                switch (choice)
                {
                    case 1:
                        {
                            await LoadMessagesAsync();
                            Console.WriteLine(ConsoleStrings.ANY_KEY_TO_CONTINUE);
                            _ = Console.ReadKey(true);
                            Console.Clear();
                            break;
                        }

                    case 2:
                        {
                            if (_currentChannel.CanSendMessages)
                            {
                                await SendMessageInChannelAsync();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ConsoleStrings.CANNOT_SEND_READONLY);
                                Console.ResetColor();
                                Console.WriteLine(ConsoleStrings.ANY_KEY_TO_CONTINUE);
                                _ = Console.ReadKey(true);
                                Console.Clear();
                            }

                            break;
                        }

                    case 3:
                        {
                            Console.Clear();
                            await SelectChannelAsync();
                            break;
                        }

                    case 4:
                        {
                            FetchInfoAsync();
                            break;
                        }

                    case 5:
                        {
                            inServerMenu = false;
                            break;
                        }

                    default:
                        {
                            Console.WriteLine(ConsoleStrings.UNKNOWN_OPTION_SELECTED);
                            Console.WriteLine(ConsoleStrings.ANY_KEY_TO_CONTINUE);
                            _ = Console.ReadKey(true);
                            break;
                        }
                }
            }
        }

        private async Task SendMessageInChannelAsync()
        {
            await LoadMessagesAsync();
            Console.Write(ConsoleStrings.SEND_MESSAGE_TO, _currentChannel.Name + ": ");
            var msg = Console.ReadLine();
            await _miunie.Impersonation.SendTextToChannelAsync(msg, _currentChannel.Id);
            Console.Clear();
        }

        private async Task LoadMessagesAsync()
        {
            var messages = await _miunie.Impersonation.GetMessagesFromTextChannelAsync(_currentGuild.Id, _currentChannel.Id);
            foreach (var m in messages.OrderBy(x => x.TimeStamp))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(m.AuthorName);
                Console.ResetColor();
                Console.WriteLine($"\n{m.Content}\n\n{m.TimeStamp}");
                Console.WriteLine("----------------------------");
            }
        }

        private async void FetchInfoAsync()
        {
            Console.Clear();
            SelectGuild();
            Console.Clear();
            await SelectChannelAsync();
            Console.Clear();
        }

        private void SelectGuild()
        {
            var guilds = _miunie.Impersonation.GetAvailableGuilds();
            var menu = new ConsoleMenu<GuildView>(guilds, g => g.Name);
            menu.SetTitle(ConsoleStrings.SELECT_GUILD);
            _currentGuild = menu.Present();
        }

        private async Task SelectChannelAsync()
        {
            var channels = await _miunie.Impersonation.GetAvailableTextChannelsAsync(_currentGuild.Id);
            var menu = new ConsoleMenu<TextChannelView>(channels, c => c.Name + (c.CanSendMessages ? string.Empty : " (read only)"));
            menu.SetTitle(ConsoleStrings.SELECT_CHANNEL);
            _currentChannel = menu.Present();
        }
    }
}
