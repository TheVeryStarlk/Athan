using System.ComponentModel;

namespace Athan.Avalonia.Messages;

internal sealed record DialogTryAgainRequestMessage(INotifyPropertyChanged Requester);