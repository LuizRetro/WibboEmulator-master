using Butterfly.Communication.Packets.Outgoing;
using Butterfly.HabboHotel.GameClients;

namespace Butterfly.Communication.Packets.Incoming.Structure
{
    class ModeratorActionEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (!Session.GetHabbo().HasFuse("fuse_alert"))
            string AlertMessage = Packet.PopString();
            bool IsCaution = AlertMode != 3;
        }
    }
}