using Butterfly.Database.Interfaces;
using Butterfly.HabboHotel.GameClients;

namespace Butterfly.HabboHotel.Rooms.Chat.Commands.Cmd

            if (Room.RpRoom)
                return;

            int Nombre = ButterflyEnvironment.GetRandomNumber(1, 3);
                using (IQueryAdapter queryreactor = ButterflyEnvironment.GetDatabaseManager().GetQueryReactor())
                    queryreactor.RunQuery("UPDATE users SET mazo = '" + Habbo.Mazo + "' WHERE id = " + Habbo.Id);