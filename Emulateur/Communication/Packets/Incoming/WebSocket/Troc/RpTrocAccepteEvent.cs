﻿using Butterfly.HabboHotel.GameClients;
using Butterfly.HabboHotel.Roleplay.Player;
using Butterfly.HabboHotel.Rooms;
using Butterfly.HabboHotel.WebClients;

namespace Butterfly.Communication.Packets.Incoming.WebSocket
{
    class RpTrocAccepteEvent : IPacketWebEvent
    {
        public void Parse(WebClient Session, ClientPacket Packet)
        {
            GameClient Client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUserID(Session.UserId);
            if (Client == null || Client.GetHabbo() == null)
                return;

            Room Room = Client.GetHabbo().CurrentRoom;
            if (Room == null || !Room.RpRoom)
                return;

            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabboId(Client.GetHabbo().Id);
            if (User == null)
                return;

            RolePlayer Rp = User.Roleplayer;
            if (Rp == null || Rp.TradeId == 0)
                return;

            ButterflyEnvironment.GetGame().GetRoleplayManager().GetTrocManager().Accepte(Rp.TradeId, User.UserId);
        }
    }
}
