using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries.Maintenace;

public sealed class BackupsQuery : IRequest<IEnumerable<Backup>> { }
