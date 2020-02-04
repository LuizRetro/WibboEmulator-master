using Butterfly.Communication.Packets.Outgoing.Structure;
using Butterfly.Database.Interfaces;
using Butterfly.HabboHotel.GameClients;
using Butterfly.HabboHotel.Items;
using Butterfly.HabboHotel.Quests;
using Butterfly.HabboHotel.Rooms;
using Butterfly.HabboHotel.Rooms.Wired;
using System;
using System.Linq;

namespace Butterfly.Communication.Packets.Incoming.Structure
{
    class PlaceObjectEvent : IPacketEvent
    {
        public void Parse(GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null || !Session.GetHabbo().InRoom)
                return;

            Room Room = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            {
            }
            {
                Session.SendNotification(ButterflyEnvironment.GetLanguageManager().TryGetValue("roomsell.error.7", Session.Langue));
                return;
            }

            if (!int.TryParse(Data[0], out ItemId))
                return;
            {
                if (Session.GetHabbo().GetBadgeComponent().HasBadge(userItem.ExtraData))
                {
                    Session.SendNotification("Vous poss�der d�j� ce badge !");
                    return;
                }

                using (IQueryAdapter queryreactor = ButterflyEnvironment.GetDatabaseManager().GetQueryReactor())
                    queryreactor.RunQuery("DELETE FROM items WHERE id = " + ItemId);

                Session.GetHabbo().GetInventoryComponent().RemoveItem(ItemId);

                Session.GetHabbo().GetBadgeComponent().GiveBadge(userItem.ExtraData, true);
                Session.SendPacket(new ReceiveBadgeComposer(userItem.ExtraData));

                Session.SendNotification("Vous avez re�u le badge: " + userItem.ExtraData + " !");
                return;
            }
                    return;
                int Y = 0;
                int Rotation = 0;

                if (!int.TryParse(Data[1], out X)) { return; }
                if (!int.TryParse(Data[2], out Y)) { return; }
                if (!int.TryParse(Data[3], out Rotation)) { return; }
                {
                    Session.SendPacket(new RoomNotificationComposer("furni_placement_error", "message", "${room.error.cant_set_item}"));
                    return;
                }


            else if (userItem.IsWallItem)

                for (int i = 1; i < Data.Length; i++)
                {
                    CorrectedData[i - 1] = Data[i];
                }
                {
                    Item roomItem = new Item(userItem.Id, Room.Id, userItem.BaseItem, userItem.ExtraData, userItem.LimitedNo, userItem.LimitedTot, 0, 0, 0.0, 0, WallPos, Room);
                    if (Room.GetRoomItemHandler().SetWallItem(Session, roomItem))
                    {
                        using (IQueryAdapter queryreactor = ButterflyEnvironment.GetDatabaseManager().GetQueryReactor())
                            queryreactor.RunQuery("UPDATE items SET room_id = " + Room.Id + ", user_id = " + Room.RoomData.OwnerId + " WHERE id = " + ItemId);

                        Session.GetHabbo().GetInventoryComponent().RemoveItem(ItemId);
                    }
                }
                else
                {
                    Session.SendPacket(new RoomNotificationComposer("furni_placement_error", "message", "${room.error.cant_set_item}"));
                    return;
                }
        }

        private static bool TrySetWallItem(string[] data, out string position)
        {
            if (data.Length != 3 || !data[0].StartsWith(":w=") || !data[1].StartsWith("l=") || (data[2] != "r" && data[2] != "l"))
            {
                position = null;
                return false;
            }

            string wBit = data[0].Substring(3, data[0].Length - 3);
            string lBit = data[1].Substring(2, data[1].Length - 2);

            if (!wBit.Contains(",") || !lBit.Contains(","))
            {
                position = null;
                return false;
            }

            int w1 = 0;
            int w2 = 0;
            int l1 = 0;
            int l2 = 0;

            int.TryParse(wBit.Split(',')[0], out w1);
            int.TryParse(wBit.Split(',')[1], out w2);
            int.TryParse(lBit.Split(',')[0], out l1);
            int.TryParse(lBit.Split(',')[1], out l2);
            
            string WallPos = ":w=" + w1 + "," + w2 + " l=" + l1 + "," + l2 + " " + data[2];

            position = WallPositionCheck(WallPos);

            return (position != null);
        }

        private static string WallPositionCheck(string wallPosition)
        {
            //:w=3,2 l=9,63 l
            try
            {
                if (wallPosition.Contains(Convert.ToChar(13)))
                {
                    return null;
                }
                if (wallPosition.Contains(Convert.ToChar(9)))
                {
                    return null;
                }

                string[] posD = wallPosition.Split(' ');
                if (posD[2] != "l" && posD[2] != "r")
                    return null;

                string[] widD = posD[0].Substring(3).Split(',');
                int widthX = int.Parse(widD[0]);
                int widthY = int.Parse(widD[1]);
                //if (widthX < -1000 || widthY < -1 || widthX > 700 || widthY > 700)
                    //return null;

                string[] lenD = posD[1].Substring(2).Split(',');
                int lengthX = int.Parse(lenD[0]);
                int lengthY = int.Parse(lenD[1]);
                //if (lengthX < -1 || lengthY < -1000 || lengthX > 700 || lengthY > 700)
                    //return null;

                return ":w=" + widthX + "," + widthY + " " + "l=" + lengthX + "," + lengthY + " " + posD[2];
            }
            catch
            {
                return null;
            }
        }
    }
}