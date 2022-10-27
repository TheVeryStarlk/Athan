using Athan.Avalonia.Models;

namespace Athan.Avalonia.Messages;

internal sealed record NavigationRequestedMessage(string Navigable, Settings? Settings = null);