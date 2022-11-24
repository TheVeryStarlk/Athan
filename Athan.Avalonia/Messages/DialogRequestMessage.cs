using System.ComponentModel;
using FluentResults;

namespace Athan.Avalonia.Messages;

internal sealed record DialogRequestMessage(INotifyPropertyChanged Requester, IError[] Errors);