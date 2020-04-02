namespace ReColor
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;

    public class DiscordColorReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input,
            IServiceProvider services)
        {
            //From name.
            if (ReColor.ParseColorName(input, out Color namedColor))
                return Task.FromResult(TypeReaderResult.FromSuccess(namedColor));

            //From raw hexvalue.
            if (int.TryParse(input, NumberStyles.HexNumber, null, out var intHex))
                return Task.FromResult(TypeReaderResult.FromSuccess(intHex));

            //From string hexvalue
            if (ReColor.ParseHexValue(input, out Color stringHex))
                return Task.FromResult(TypeReaderResult.FromSuccess(namedColor));

            return Task.FromResult(TypeReaderResult.FromError(new ColorException(input)));
        }
    }
}