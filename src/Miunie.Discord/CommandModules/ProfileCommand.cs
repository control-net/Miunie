using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Miunie.Core;
using Miunie.Discord.Convertors;

namespace Miunie.Discord.CommandModules
{
    public class ProfileCommand : ModuleBase<SocketCommandContext>
    {
        private readonly EntityConvertor _entityConvertor;
        private readonly MiunieUserConverter _userConverter;
        private readonly ProfileService _profileService;

        public ProfileCommand(EntityConvertor entityConvertor, ProfileService profileService, MiunieUserConverter userConverter)
        {
            _entityConvertor = entityConvertor;
            _profileService = profileService;
            _userConverter = userConverter;
        }

        [Command("profile")]
        public async Task ShowProfileAsync(SocketGuildUser user = null)
        {
            var m = _userConverter.DiscordMemberToMiunieUser(user ?? (Context.User as SocketGuildUser));
            if (m is null)
            {
                m = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            }
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _profileService.ShowProfileAsync(m, channel);
        }

        [Command("+rep")]
        public async Task AddReputationAsync(SocketGuildUser user)
        {
            var m = _userConverter.DiscordMemberToMiunieUser(user);
            var source = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _profileService.GiveReputationAsync(source, m, channel);
        }

        [Command("-rep")]
        public async Task RemoveReputationAsync(SocketGuildUser user)
        {
            var m = _userConverter.DiscordMemberToMiunieUser(user);
            var source = _entityConvertor.ConvertUser(Context.User as SocketGuildUser);
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _profileService.RemoveReputationAsync(source, m, channel);
        }

        [Command("guild")]
        public async Task ShowGuildInfoAsync()
        {
            var guild = _entityConvertor.ConvertGuild(Context.Guild);
            var channel = _entityConvertor.ConvertChannel(Context.Channel as SocketGuildChannel);
            await _profileService.ShowGuildProfileAsync(guild, channel);
        }
    }
}
